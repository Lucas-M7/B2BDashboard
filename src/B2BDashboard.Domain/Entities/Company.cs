using B2BDashboard.Domain.Common;

namespace B2BDashboard.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Cnpj { get; private set; } = string.Empty;

    private readonly List<User> _users = [];
    public IReadOnlyCollection<User> users => _users.AsReadOnly();

    private readonly List<Client> _clients = [];
    public IReadOnlyCollection<Client> Clients => _clients.AsReadOnly();

    protected Company() { }

    public static Company Create(string name, string cnpj)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da empresa é obrigatório.", nameof(name));

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ é obrigatório.", nameof(cnpj));

        return new Company { Name = name, Cnpj = cnpj };
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(newName));

        Name = newName;
        MarkAsUpdated();
    }
}