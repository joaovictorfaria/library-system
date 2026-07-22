

using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;

namespace Library.Tests;

    public class LoanTests
    {

    [Fact]
    public void Should_Create_Active_Loan_When_Data_Is_Valid()
    {
        Email email = new("joao@email.com");
        User user = new("João", email);

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        Loan loan = new(user, copy);

        Assert.NotEqual(Guid.Empty, loan.Id);
        Assert.Equal(user, loan.User);
        Assert.Equal(copy, loan.BookCopy);
        Assert.Equal(LoanStatus.Active, loan.Status);
        Assert.Equal(BookCopyStatus.Borrowed, copy.Status);
        Assert.Null(loan.ReturnDate);
        Assert.Equal(loan.LoanDate.AddDays(14), loan.DueDate);
    }

    [Fact]
    public void Should_Throw_DomainException_When_User_Is_Null()
    {

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        Assert.Throws<DomainException>(() =>
        {
            Loan loan = new(null!, copy);
        });

        
    }

    [Fact]
    public void Should_Throw_DomainException_When_BookCopy_Is_Null()
    {
        Email email = new("joao@email.com");
        User user = new("João", email);

        Assert.Throws<DomainException>(() =>
        {
            Loan loan = new(user, null!);
        });
    }

    [Fact]
    public void Should_Throw_DomainException_When_BookCopy_Is_Already_Borrowed()
    {
        Email email = new("joao@email.com");
        User user = new("João", email);

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        new Loan(user, copy);

        Assert.Throws<DomainException>(() =>
        {
            new Loan(user, copy);
        });
    }

    [Fact]
    public void Should_Return_Active_Loan()
    {
        Email email = new("joao@email.com");
        User user = new("João", email);

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        Loan loan = new(user, copy);

        loan.Return();

        Assert.Equal(LoanStatus.Returned, loan.Status);
        Assert.Equal(BookCopyStatus.Available, copy.Status);
        Assert.NotNull(loan.ReturnDate);
    }

    [Fact]
    public void Should_Throw_DomainException_When_Returning_Returned_Loan()
    {
        Email email = new("joao@email.com");
        User user = new("João", email);

        Guid bookId = Guid.NewGuid();
        BookCopy copy = new(bookId);

        Loan loan = new(user, copy);

        loan.Return();

        Assert.Throws<DomainException>(() =>
        {
            loan.Return();
        });
    }
}

