# ADR 0002 — Repository Pattern específico por entidade, não genérico

## Status
Aceito

## Contexto
Um `IRepository<T>` genérico (Add/Get/Delete para qualquer entidade) é comum em
tutoriais, mas tende a reimplementar o `DbSet<T>` do EF Core sem ganho real, e não
expõe intenção de consulta específica de cada agregado.

## Decisão
Cada entidade principal tem sua própria interface de repositório
(`ICompanyRepository`, `IClientRepository`, `ISaleRepository`), expondo apenas os
métodos que aquele agregado realmente precisa (ex: `GetByCnpjAsync`).

## Consequências
- Positivo: a assinatura da interface documenta quais consultas o domínio suporta.
- Positivo: evita over-engineering de um repositório genérico não utilizado por inteiro.
- Negativo: mais interfaces para manter conforme o domínio cresce.