# ADR 0004 — JWT de curta duração + Refresh Token com rotação

## Status
Aceito

## Contexto
Access tokens JWT são stateless e não podem ser revogados individualmente sem
infraestrutura extra (blocklist). É necessário um mecanismo de revogação real para
logout e mitigação de roubo de token.

## Decisão
- Access token: JWT, 15 minutos de expiração, claims incluindo `companyId` (fonte de
  verdade do tenant, substituindo qualquer dado vindo da URL/body).
- Refresh token: persistido no banco, 7 dias de expiração, revogado e reemitido
  (rotação) a cada uso — reuso de um refresh token antigo é bloqueado.

## Consequências
- Positivo: janela de exposição de um access token roubado é curta (15 min).
- Positivo: refresh token roubado e reutilizado é detectável (já estará revogado).
- Negativo: refresh token exige uma tabela e uma consulta a mais no banco a cada
  renovação, diferente de um esquema puramente stateless.