using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;


namespace Library.Tests;

public class BookCopyTests
{
    [Fact]
    public void Should_Create_Available_BookCopy_When_BookId_Is_Valid()
    {
        Guid bookId = Guid.NewGuid();

        BookCopy copy = new(bookId);

        Assert.NotEqual(Guid.Empty, copy.Id);
        Assert.Equal(bookId, copy.BookId);
        Assert.Equal(BookCopyStatus.Available, copy.Status);

    }

    [Fact]
    public void Should_Throw_DomainException_When_BookId_Is_Empty()
    {


        Assert.Throws<DomainException>(() =>
        {

            new BookCopy(Guid.Empty);

        });
    }


    [Fact]
    public void Should_Borrow_Available_BookCopy()
    {

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);
        copy.Borrow();

        Assert.Equal(BookCopyStatus.Borrowed, copy.Status);

    }


    [Fact]
    public void Should_Throw_DomainException_When_Borrowing_Borrowed_BookCopy()
    {

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);
        copy.Borrow();

        Assert.Throws<DomainException>(() =>
        {
            copy.Borrow();
        });

    }

    [Fact]
    public void Should_Return_Borrowed_BookCopy()
    {

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);
        copy.Borrow();

        copy.Return();

        Assert.Equal(BookCopyStatus.Available, copy.Status);

    }

    [Fact]
    public void Should_Throw_DomainException_When_Returning_Available_BookCopy()
    {

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        Assert.Throws<DomainException>(() =>
        {
            copy.Return();
        });

    }
}

