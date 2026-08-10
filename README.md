# B2B Dashboard — API

API backend multi-tenant para gestão de clientes e vendas B2B, com autenticação JWT
e arquitetura em camadas (Clean Architecture lite).

## Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core + PostgreSQL
- JWT (access + refresh token com rotação)
- xUnit + Moq + FluentAssertions
- Docker / Docker Compose

## Arquitetura

\`\`\`
Api → Infrastructure → Application → Domain
Api → Application
\`\`\`

Domain não possui nenhuma dependência externa (sem EF Core, sem ASP.NET Core).
Application depende apenas de interfaces, nunca de implementações concretas.
Detalhes das decisões de arquitetura estão documentados em [`/docs/adr`](./docs/adr).

## Como rodar localmente

Pré-requisitos: Docker e Docker Compose instalados.

\`\`\`bash
docker compose up -d
\`\`\`

Isso sobe a Api (porta 5000) e o PostgreSQL (porta 5432) já conectados entre si.

### Aplicando migrations (primeira vez ou após alteração de schema)

\`\`\`bash
dotnet ef database update \
  --project src/B2BDashboard.Infrastructure \
  --startup-project src/B2BDashboard.Api \
  --connection "Host=localhost;Port=5432;Database=b2bdashboard;Username=postgres;Password=postgres"
\`\`\`

### Documentação interativa da API

Com a Api rodando, acesse `http://localhost:5095/openapi/v1.json` (spec bruto),
importe a [coleção Postman](./docs/postman) para testar os endpoints ou
acesse `http://localhost:5095/swagger/index.html`.

## Rodando os testes

\`\`\`bash
dotnet test
\`\`\`

## Estrutura do projeto

| Camada | Responsabilidade |
|---|---|
| `Domain` | Entidades e regras de negócio puras |
| `Application` | DTOs, casos de uso (Services), contratos (interfaces) |
| `Infrastructure` | EF Core, PostgreSQL, JWT, BCrypt |
| `Api` | Controllers, autenticação, tratamento global de erro |

## Decisões de arquitetura

Ver [`/docs/adr`](./docs/adr) para o histórico de decisões técnicas com contexto e
justificativa (por que Repository Pattern específico, por que soft delete em Company,
por que refresh token com rotação, etc.).