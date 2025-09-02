📖 Sobre o Projeto
Meu Petshop API é um sistema de backend robusto para o gerenciamento completo de um pet shop. O projeto foi desenvolvido com foco em uma arquitetura limpa e escalável, seguindo as melhores práticas do desenvolvimento moderno de software. A API gerencia clientes, seus pets, os serviços oferecidos e o agendamento de atendimentos, com um sistema de autenticação e autorização baseado em perfis.

Este projeto representa a fundação (o "cérebro") da aplicação, que será consumida por uma interface de frontend no futuro.


✨ Funcionalidades Principais
Gerenciamento de Clientes, Pets, Produtos e Serviços: Operações CRUD completas para todas as entidades principais do negócio.

Sistema de Agendamentos: Lógica para criar, atualizar, cancelar e consultar agendamentos, com validações de regras de negócio.

Autenticação e Autorização: Sistema de segurança completo usando ASP.NET Core Identity e Tokens JWT.

Gerenciamento de Perfis (Roles): Distinção entre usuários Admin e Funcionario, com endpoints protegidos por nível de acesso.

Consultas Avançadas: Endpoints com suporte a paginação e busca por substrings para uma experiência de usuário mais rica.

Deploy Contínuo: Configurado para deploy automático na nuvem a partir do GitHub.


🚀 API em Produção (Live Demo)
A documentação completa da API, gerada pelo Swagger, está disponível e pode ser testada ao vivo no seguinte endereço:

https://manager-petshop.onrender.com/swagger


🛠️ Tecnologias Utilizadas
Este projeto foi construído com um stack de tecnologias moderno e robusto:

Backend: C#, .NET 8, ASP.NET Core Web API

Banco de Dados: PostgreSQL

ORM: Entity Framework Core 8

Arquitetura:

N-Tier com princípios de Arquitetura Limpa (Domain, Application, Infrastructure, Api)

Padrão de Repositório (Repository Pattern)

Injeção de Dependência (Dependency Injection)

Autenticação: ASP.NET Core Identity, JSON Web Tokens (JWT)

Deploy: Docker & Render



🗺️ Roadmap e Melhorias Futuras
Este projeto está em constante evolução. Os próximos passos planejados são:

[ ] Desenvolvimento do Frontend: Criar a interface de usuário completa em React, Blazor ou Vue.js para consumir esta API e oferecer uma experiência de gerenciamento visual para os funcionários do petshop.

[ ] Melhoria de Endpoints:

Implementar um sistema de validação mais robusto com FluentValidation.

Refatorar a lógica de negócio para usar um padrão de Result em vez de exceções.

Criar endpoints para um Dashboard com estatísticas (ex: agendamentos do dia, faturamento, etc.).

[ ] Sistema de Notificações: Enviar e-mails de confirmação e lembrete para os clientes sobre seus agendamentos.

[ ] Cobertura de Testes: Escrever testes unitários e de integração para garantir a qualidade e a estabilidade do código.
