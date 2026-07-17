using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;
using System;

namespace Library.Domain.Entities;

public class Book
{
    private readonly List<BookCopy> _copies = new();
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string AuthorName { get; private set; }
    public Isbn Isbn { get; private set; }
    public int PublicationYear { get; private set; }
    public IReadOnlyCollection<BookCopy> Copies => _copies;

    public Book(
      string title,
      string authorName,
      Isbn isbn,
      int publicationYear)
    {
        Validate(title, authorName, isbn, publicationYear);

        Id = Guid.NewGuid();

        Title = title;
        AuthorName = authorName;
        Isbn = isbn;
        PublicationYear = publicationYear;
    }

    public void AddCopy()
    {
        BookCopy copy = new(Id);

        _copies.Add(copy);
    }

    private static void Validate(
        string title,
        string authorName,
        Isbn isbn,
        int publicationYear)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new DomainException("Author name is required.");
        }

        if (isbn is null)
        {
            throw new DomainException("ISBN is required.");
        }

        if (publicationYear <= 0 || publicationYear > DateTime.Now.Year)
        {
            throw new DomainException("Publication year is invalid.");
        }
    }

  



}

