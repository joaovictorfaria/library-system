using Library.Domain.Exceptions;

namespace Library.Domain.ValueObjects;

public class Isbn
{
    public string Value { get; }

    public Isbn(string value)
    {
        Validate(value);

        Value = value;
    }
    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ISBN is required.");
        }
    }
}

