using Library.Domain.Enums;
using Library.Domain.Exceptions;
using System;


namespace Library.Domain.Entities;

public class BookCopy
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public BookCopyStatus Status { get; private set; }

    public BookCopy(Guid bookId)
    {
        Validate(bookId);

        Id = Guid.NewGuid();
        BookId = bookId;
        Status = BookCopyStatus.Available;
    }

    private static void Validate(Guid bookId)
    {
        if (bookId == Guid.Empty)
        {
            throw new DomainException("BookId is required.");
        }
    }

    public void Borrow()
    {
        if (Status != BookCopyStatus.Available)
        {
            throw new DomainException("Only available book copies can be borrowed.");
        }

        Status = BookCopyStatus.Borrowed;
    }

    public void Return()
    {
        if (Status != BookCopyStatus.Borrowed)
        {
            throw new DomainException("Only borrowed book copies can be returned.");
        }

        Status = BookCopyStatus.Available;
    }

}

