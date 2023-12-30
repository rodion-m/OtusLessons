using FluentValidation.TestHelper;
using ValidationLesson.Models.Requests;
using ValidationLesson.Validators;

namespace FluentValidationExample.Test;

public class AccountValidationTests
{
    private readonly RegisterAccountRequestValidator _requestValidator = new();

    [Fact]
    public void Account_with_empty_first_name_is_invalid()
    {
        // Arrange
        var address = new Address("123 Main St", "State", "City", "12345");
        List<EmailAddress> emailAddresses = [new EmailAddress("john@example.com", EmailType.Private)];
        var account = new RegisterAccountRequest("", "Doe", "12345678", address, emailAddresses);

        // Act
        var result = _requestValidator.TestValidate(account);

        // Assert
        result.ShouldHaveValidationErrorFor(a => a.FirstName);
    }

    [Fact]
    public void Account_with_invalid_email_is_invalid()
    {
        // Arrange
        var address = new Address("123 Main St", "State", "City", "12345");
        List<EmailAddress> emailAddresses = [new EmailAddress("NOT-AN-EMAIL", EmailType.Private)];
        var account = new RegisterAccountRequest("John", "Doe", "12345678", address, emailAddresses);

        // Act
        var result = _requestValidator.TestValidate(account);

        // Assert
        result.ShouldHaveValidationErrorFor($"{nameof(account.EmailAddresses)}[0].{nameof(EmailAddress.Email)}");
        //result.ShouldHaveValidationErrorFor("EmailAddresses[0].Email"); //строка выше равносильна этой
    }
}
