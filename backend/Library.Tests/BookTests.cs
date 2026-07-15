using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;

namespace Library.Tests
{
    public class BookTests
    {
        [Fact]
        public void Should_Throw_DomainException_When_Title_Is_Null_Or_WhiteSpace()
        {
            Isbn isbn = new("094728890001");

            Assert.Throws<DomainException>(() =>
            {
                new Book(
                "",
                "Franz Kafka",
                isbn,
                1915
                );
            });
        }

        [Fact]
        public void Should_Throw_DomainException_When_Publication_Year_Is_Invalid()
        {
            Isbn isbn = new("094728890001");

            Assert.Throws<DomainException>(() =>
            {
                new Book(
                "A Metamorfose",
                "Franz Kafka",
                isbn,
                -1500
                );
            });
        }
    }
}