using System;

namespace Library.Domain.Entities;

    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string AuthorName { get; private set; }
        public string Isbn { get; private set; }
        public int PublicationYear { get; private set; }

    public Book(
        string title,
        string authorName,
        string isbn,
        int publicationYear)
    {
        Id = Guid.NewGuid();

        Title = title;
        AuthorName = authorName;
        Isbn = isbn;
        PublicationYear = publicationYear;
    }

    }

