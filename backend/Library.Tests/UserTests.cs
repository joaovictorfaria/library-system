

using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;

namespace Library.Tests;

    public class UserTests
    {

    [Fact]
    public void Should_Create_User_When_Data_Is_Valid()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("João", user.Name);
        Assert.Equal(email, user.Email);
        Assert.Empty(user.ActiveLoans);
    }

    [Fact]
    public void Should_Throw_DomainException_When_Name_Is_Null_Or_WhiteSpace()
    {
        Email email = new("joao@joao.com");

        Assert.Throws<DomainException>(() =>
        {
            new User(null!, email);
        });
    }

    [Fact]
    public void Should_Throw_DomainException_When_Email_Is_Null()
    {
        Assert.Throws<DomainException>(() =>
        {
           new User("João", null!);
        });
    }

    [Fact]
    public void Should_Add_Active_Loan_When_Borrowing_Available_BookCopy()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);
        BookCopy bookCopy = new(Guid.NewGuid());

        Loan loan = user.BorrowBook(bookCopy);

        Assert.Contains(loan, user.ActiveLoans);
        Assert.Equal(user, loan.User);
        Assert.Equal(bookCopy, loan.BookCopy);
        Assert.Equal(LoanStatus.Active, loan.Status);
        Assert.Single(user.ActiveLoans);
        Assert.Equal(BookCopyStatus.Borrowed, bookCopy.Status);
    }

    [Fact]
    public void Should_Throw_DomainException_When_BookCopy_Is_Null()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);

        Assert.Throws<DomainException>(() =>
        {
            user.BorrowBook(null!);
        });
    }

    [Fact]
    public void Should_Throw_DomainException_When_Active_Loan_Limit_Is_Reached()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);

        for (int i = 0; i < 5; i++)
        {
            user.BorrowBook(new BookCopy(Guid.NewGuid()));
        }

        BookCopy bookCopy6 = new(Guid.NewGuid());

        Assert.Throws<DomainException>(() =>
        {
            user.BorrowBook(bookCopy6);
        });

        Assert.Equal(5, user.ActiveLoans.Count);
    }

    [Fact]
    public void Should_Return_Active_Loan_And_Remove_It_From_ActiveLoans()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);
        BookCopy bookCopy = new(Guid.NewGuid());

        Loan loan = user.BorrowBook(bookCopy);

        user.ReturnLoan(loan.Id);

        Assert.Empty(user.ActiveLoans);
        Assert.NotNull(loan.ReturnDate);
        Assert.Equal(BookCopyStatus.Available, bookCopy.Status);
        Assert.Equal(LoanStatus.Returned, loan.Status);
    }

    [Fact]
    public void Should_Throw_DomainException_When_LoanId_Is_Empty()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);

        Assert.Throws<DomainException>(() =>
        {
            user.ReturnLoan(Guid.Empty);
        });
    }

    [Fact]
    public void Should_Throw_DomainException_When_Active_Loan_Is_Not_Found()
    {
        Email email = new("joao@joao.com");
        User user = new("João", email);

        Assert.Throws<DomainException>(() =>
        {
            user.ReturnLoan(Guid.NewGuid());
        });
    }
}

