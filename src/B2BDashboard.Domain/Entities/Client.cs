using B2BDashboard.Domain.Common;

namespace B2BDashboard.Domain.Entities;

public class Client : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty; // CPF ou CNPJ
    public string Email { get; private set; } = string.Empty;

    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;

    private readonly List<Sale> _sales = [];
    public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();

    protected Client() { }

    public static Client Create(string name, string document, string email, Guid companyId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório.", nameof(name));

        return new Client
        {
            Name = name,
            Document = document,
            Email = email,
            CompanyId = companyId
        };
    }

    public void Update(string name, string document, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório.", nameof(name));

        Name = name;
        Document = document;
        Email = email;
        MarkAsUpdated();
    }
}