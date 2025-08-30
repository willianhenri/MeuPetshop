using MeuPetshop.Application.Services;
using MeuPetShop.Domain.Interfaces;
using MeuPetShop.Domain.Interfaces.IClients;
using MeuPetShop.Domain.Interfaces.IPets;
using MeuPetShop.Domain.Interfaces.IProducts;
using MeuPetshop.Infrastructure.Data;
using MeuPetshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IClientService, ClientServices>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPetService, PetServices>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
