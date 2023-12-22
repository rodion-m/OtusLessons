using FluentValidation.TestHelper;
using ValidationLesson.Models;
using ValidationLesson.Validators;

namespace FluentValidationLesson.Test;

public class AccountValidationTests
{
    private readonly AccountValidator _validator = new AccountValidator();

    [Fact]
    public void Account_with_empty_first_name_is_invalid()
    {
        // Arrange
        var account = new Account("", "Doe", new Address("123 Main St", "State", "City", "12345"), 
            new List<EmailAddress> { new EmailAddress("john@example.com", EmailType.Private) });

        // Act
        var result = _validator.TestValidate(account);

        // Assert
        result.ShouldHaveValidationErrorFor(account => account.FirstName);
    }

    [Fact]
    public void Account_with_invalid_email_is_invalid()
    {
        // Arrange
        var account = new Account("John", "Doe", new Address("123 Main St", "State", "City", "12345"), 
            new List<EmailAddress> { new EmailAddress("not-an-email", EmailType.Private) });

        // Act
        var result = _validator.TestValidate(account);

        // Assert
        result.ShouldHaveValidationErrorFor("EmailAddresses[0].Email");
    }

    // Additional tests can be written in a similar fashion for other fields and scenarios.
}
