# ADR 0001 — Clean Architecture em 4 camadas

## Status
Aceito

## Contexto
O projeto precisa ser testável isoladamente (regra de negócio sem depender de banco
real) e permitir trocar tecnologia de persistência sem reescrever regra de negócio.

## Decisão
Dividir o backend em 4 projetos com regra de dependência unidirecional:
Api → Infrastructure → Application → Domain, e Api → Application.
Domain não possui nenhuma dependência de pacote externo (nem EF Core, nem ASP.NET Core).

## Consequências
- Positivo: testes unitários da Application não exigem banco real (mock de interfaces).
- Positivo: troca de ORM/banco não exige alteração no Domain.
- Negativo: mais boilerplate inicial (interfaces + implementações) comparado a um
  projeto único com tudo junto (aceitável dado o ganho de testabilidade).