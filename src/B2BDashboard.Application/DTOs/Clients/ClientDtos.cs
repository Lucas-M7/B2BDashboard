using System.ComponentModel.DataAnnotations;

namespace B2BDashboard.Application.DTOs.Clients;

public record CreateClientRequest(
    [Required] string Name, 
    string Document, 
    [EmailAddress] string Email);
public record ClientResponse(Guid Id, string Name, string Document, string Email);