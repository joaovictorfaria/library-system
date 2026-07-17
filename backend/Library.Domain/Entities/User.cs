using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;


namespace Library.Domain.Entities;

public class User
{
    private readonly List<Loan> _activeLoans = new();
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyCollection<Loan> ActiveLoans => _activeLoans;

    public User( 
        string name, 
        Email email)
    {
        Validate(name, email);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    private static void Validate(string name, 
        Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Name is required.");
        }

        if (email is null)
        {
            throw new DomainException("Email is required.");
        }
    }
}

