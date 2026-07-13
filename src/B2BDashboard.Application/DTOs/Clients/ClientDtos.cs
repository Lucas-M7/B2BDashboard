namespace B2BDashboard.Application.DTOs.Clients;

public record CreateClientRequest(string Name, string Document, string Email);
public record ClientResponse(Guid Id, string Name, string Document, string Email);