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

}

