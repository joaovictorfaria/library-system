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
    private const int MaximumActiveLoans = 5;

    public User( 
        string name, 
        Email email)
    {
        Validate(name, email);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public Loan BorrowBook(BookCopy bookCopy)
    {
        if (bookCopy is null)
        {
            throw new DomainException("Book copy is required.");
        }

        if (_activeLoans.Count >= MaximumActiveLoans)
        {
            throw new DomainException($"Active loan limit reached. Maximum: {MaximumActiveLoans} loans.");
        }

        Loan loan = new(this, bookCopy);

        _activeLoans.Add(loan);

        return loan;
    }

    public void ReturnLoan(Guid loanId)
    {
        if (loanId == Guid.Empty)
        {
            throw new DomainException("Loan ID is required.");
        }

        Loan? loan = _activeLoans.FirstOrDefault(
            loan => loan.Id == loanId);

        if (loan is null)
        {
            throw new DomainException("Active loan was not found.");
        }

        loan.Return();

        _activeLoans.Remove(loan);

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

