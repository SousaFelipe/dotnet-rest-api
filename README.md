# REST API .NET 8

API REST desenvolvida em C#, com .NET 8, para exercitar habilidades em desenvolvimento backend e implementação de padrões de projeto.


## 🎯 Objetivo

Este projeto foi criado para praticar e demonstrar conhecimentos em:
- Desenvolvimento de APIs REST com .NET 8
- Implementação de padrões de arquitetura
- Autenticação e autorização
- Persistência de dados com ORM
- Documentação de APIs


## ✨ Funcionalidades

- **Autenticação de Usuários**: Sistema de login com geração de tokens JWT
- **CRUD de Usuários**: Operações completas de criação, leitura, atualização e exclusão
- **Proteção de Rotas**: Endpoints protegidos por autenticação JWT
- **Tratamento Global de Exceções**: Gerenciamento centralizado de erros
- **Documentação Interativa**: Interface Swagger para testes e exploração da API


## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **SQL Server**: Banco de dados relacional
- **NHibernate**: ORM com mapeamento XML
- **Migrations**: Controle de versão do banco de dados
- **JWT (JSON Web Tokens)**: Autenticação e autorização
- **Swagger**: Documentação e interface de testes da API


## 📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Git](https://git-scm.com/)


## 🚀 Instalação

1. Clone o repositório:
```bash
git clone https://github.com/SousaFelipe/dotnet-rest-api.git
```

2. Navegue até o diretório do projeto:
```bash
cd dotnet-rest-api
```

3. Restaure as dependências:
```bash
dotnet restore
```

4. Compile o projeto:
```bash
dotnet build
```

5. Execute a aplicação:\
Obs: será necessário incluir o parâmetro `-- up` apenas na primeira inicialização do projeto.
```bash
dotnet run --project Api.Domain -- up
```


## 💻 Como Usar

1. Após iniciar a aplicação, acesse a documentação Swagger:
```
http://localhost:5035/swagger/index.html
```

2. Utilize a interface Swagger para:
   - Visualizar todos os endpoints disponíveis
   - Testar as rotas de autenticação
   - Realizar operações CRUD de usuários
   - Explorar os modelos de dados


## 📐 Arquitetura

O projeto segue uma **Arquitetura em Camadas (N-Layer Architecture)** com separação clara de responsabilidades, organizado em três projetos principais:


### Camadas da Aplicação

#### 1. **Api.Domain** (Presentation Layer)
Camada de apresentação responsável por:
- Inicialização da aplicação
- Controllers (endpoints da API)
- Configurações iniciais da aplicação
- Comunicação HTTP com os clientes

#### 2. **Api.Service** (Business Logic Layer)
Camada de lógica de negócio que:
- Implementa as regras de negócio da aplicação
- Orquestra a comunicação entre Controllers e Repository
- Processa e valida dados
- Toma decisões sobre os processos de negócio

#### 3. **Api.Repository** (Data Access Layer)
Camada de acesso a dados responsável por:
- Configurações do NHibernate
- Manipulação do banco de dados
- Operações de persistência (CRUD)
- Comunicação direta com o SQL Server


### Fluxo de Dados

```
                         ┌──────────────────────────┐
          Cliente HTTP → │   (Domain) Controllers   │
                         └──────────────────────────┘
                                     ↓ ↑
                         ┌──────────────────────────┐
                         │    (Service) Business    │
                         └──────────────────────────┘
                                     ↓ ↑
                         ┌──────────────────────────┐     ┌──────────────────┐
                         │ (Repository) Persistence │  →  │     DataBase     │
                         └──────────────────────────┘     └──────────────────┘
```


### Benefícios desta Arquitetura

- ✅ **Separação de Responsabilidades**: Cada camada possui funções bem definidas
- ✅ **Manutenibilidade**: Alterações em uma camada não afetam diretamente as outras
- ✅ **Testabilidade**: Cada camada pode ser testada de forma isolada
- ✅ **Escalabilidade**: Facilita a evolução e expansão do projeto
- ✅ **Dependência Unidirecional**: Fluxo claro de dependências entre camadas

Este padrão garante:
- Respostas de erro consistentes
- Logging centralizado
- Melhor manutenibilidade do código
- Separação de responsabilidades


## 📝 Endpoints

- `POST /auth/login` - Autenticação de usuários
- `POST /users` - Criar novo usuário
- `GET /users/{id}` - Obter usuário específico (protegido)
- `GET /users/{page}/{size}` - Listar usuários de forma paginada (protegido)
- `PUT /users/{id}` - Atualizar usuário (protegido)
- `DELETE /users/{id}` - Remover usuário (protegido)


## 🔐 Autenticação

A API utiliza **JWT (JSON Web Tokens)** para autenticação:

1. Faça login através do endpoint `/auth/login`
2. Utilize o token retornado no header `Authorization: Bearer {token}`
3. Acesse os endpoints protegidos com o token válido


## 🤝 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir novas funcionalidades
- Enviar pull requests


## 👨‍💻 Autor

**Felipe S. Carmo**

- [Email](mailto:flpssdocarmo@gmail.com)
- [Linkedin](www.linkedin.com/in/fscarmo)
- [Instagram](https://www.instagram.com/flpss.carmo/)


## 📄 Licença

Este projeto está sob a licença MIT. Consulte o arquivo <a href="https://github.com/SousaFelipe/dotnet-rest-api/blob/master/LICENSE">LICENSE</a> para mais detalhes.
