# Estágio 1: Build (Compilação)
# Usamos a imagem completa do .NET SDK para compilar a aplicação.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia os arquivos .csproj e restaura as dependências primeiro para otimizar o cache
COPY *.sln .
COPY MeuPetshop.Api/*.csproj MeuPetshop.Api/
COPY MeuPetshop.Application/*.csproj MeuPetshop.Application/
COPY MeuPetShop.Domain/*.csproj MeuPetShop.Domain/
COPY MeuPetshop.Infrastructure/*.csproj MeuPetshop.Infrastructure/
RUN dotnet restore

# Copia todo o resto do código fonte
COPY . .

# Publica a aplicação em modo Release
WORKDIR /source/MeuPetshop.Api
RUN dotnet publish -c Release -o /app/out

# Estágio 2: Final (Execução)
# Usamos a imagem leve do ASP.NET Runtime, que é menor e mais segura.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia apenas a aplicação publicada do estágio de build.
COPY --from=build /app/out .

# Define o comando que será executado quando o container iniciar.
ENTRYPOINT ["dotnet", "MeuPetshop.Api.dll"]