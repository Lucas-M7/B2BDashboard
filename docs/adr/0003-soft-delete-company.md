# ADR 0003 — Soft delete em Company, hard delete protegido em Client

## Status
Aceito

## Contexto
`Company` é o tenant raiz. Removê-la fisicamente cascatearia a exclusão de todos os
`Users` e `Clients` associados (inaceitável em um SaaS B2B real).
`Client`, por outro lado, pode ser removido de fato, mas não deveria ser possível
apagar um cliente que já possui vendas associadas (dado auditável).

## Decisão
- `Company`: soft delete via `IsActive`, nunca removida fisicamente.
- `Client`: hard delete permitido, mas protegido por `DeleteBehavior.Restrict` na
  foreign key com `Sale` (o banco recusa a exclusão se houver vendas vinculadas),
  e essa recusa é traduzida para HTTP 409 na camada de Infrastructure.

## Consequências
- Positivo: nenhum dado de tenant é perdido por engano.
- Positivo: histórico financeiro nunca fica órfão silenciosamente.
- Negativo: `DELETE /api/companies/me` é semanticamente uma desativação, não uma
  remoção real.