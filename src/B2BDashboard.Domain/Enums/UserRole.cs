namespace B2BDashboard.Domain.Enums;

public enum UserRole
{
    Admin = 1, // gerencia usuários e configurações da Company
    Manager = 2, // vê a gerencia de dados de venda/clientes
    Viewer = 3 // só visualiza o dashboard, sem editar
}