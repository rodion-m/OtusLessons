namespace ValidationLesson.Models.Requests;

public record RegisterAccountRequest(
    string FirstName, 
    string LastName,
    string Password,
    Address Address, 
    List<EmailAddress> EmailAddresses
);

public record Address(string Street, string State, string City, string ZipCode);

public record EmailAddress(string Email, EmailType Type);

public enum EmailType
{
    Private,
    Work
}