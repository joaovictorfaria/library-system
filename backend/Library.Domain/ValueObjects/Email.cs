using Library.Domain.Exceptions;

using System.Net.Mail;


namespace Library.Domain.ValueObjects;

    public class Email
    {
        public string Value { get; }

        public Email(string value)
    {
        Validate(value);

        Value = value;
    }

        private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email is required.");
        }

        try {

            MailAddress address = new(value);
        
        } catch(FormatException)
        {
            throw new DomainException("Email is invalid.");
        }

    }
    }

