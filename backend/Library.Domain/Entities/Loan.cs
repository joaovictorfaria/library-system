

using Library.Domain.Enums;
using Library.Domain.Exceptions;

namespace Library.Domain.Entities;

public class Loan
{
    public Guid Id { get; private set; }
    public User User { get; private set; }
    public BookCopy BookCopy { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public LoanStatus Status { get; private set; }

    public Loan(User user, BookCopy bookCopy)
    {
        Validate(user, bookCopy);

        bookCopy.Borrow();

        Id = Guid.NewGuid();
        User = user;
        BookCopy = bookCopy;


        LoanDate = DateTime.Now;
        DueDate = LoanDate.AddDays(14);

        Status = LoanStatus.Active;

        ReturnDate = null;
    }

    public void Return()
    {
        if (Status != LoanStatus.Active)
        {
            throw new DomainException("Only active loans can be returned.");
        }

        BookCopy.Return();

        ReturnDate = DateTime.Now;

        Status = LoanStatus.Returned;
    }

    private static void Validate(User user, BookCopy bookCopy)
    {
        if (user is null)
        {
            throw new DomainException("User is required.");
        }

        if (bookCopy is null)
        {
            throw new DomainException("BookCopy is required.");
        }
    }
}

