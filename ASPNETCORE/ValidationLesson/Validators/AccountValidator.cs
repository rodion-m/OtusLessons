using FluentValidation;
using ValidationLesson.Models;

namespace ValidationLesson.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(account => account.FirstName).NotEmpty();
        RuleFor(account => account.LastName).NotEmpty();
        RuleFor(account => account.Address).SetValidator(new AddressValidator());
        RuleForEach(account => account.EmailAddresses).SetValidator(new EmailAddressValidator());
    }
}

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.Street).NotEmpty();
        RuleFor(address => address.State).NotEmpty();
        RuleFor(address => address.City).NotEmpty();
        RuleFor(address => address.ZipCode).NotEmpty();
    }
}

public class EmailAddressValidator : AbstractValidator<EmailAddress>
{
    public EmailAddressValidator()
    {
        RuleFor(email => email.Email).NotEmpty().EmailAddress();
        RuleFor(email => email.Type).IsInEnum();
    }
}