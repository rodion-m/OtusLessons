using FluentValidation;
using ValidationLesson.Models.Requests;

namespace ValidationLesson.Validators;

public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountRequestValidator() //можно использовать DI, т. к. lifetime Scoped
    {
        RuleFor(account => account.FirstName).NotEmpty();
        RuleFor(account => account.LastName).NotEmpty();
        RuleFor(account => account.Password).NotEmpty()
            .MinimumLength(8)
            .MaximumLength(20)
            .WithMessage("Пароль должен быть не более 20 символов.");
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
        RuleFor(email => email.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Не является действительным адресом электронной почты.");
        RuleFor(email => email.Type).IsInEnum(); //важно: https://www.oreilly.com/library/view/c-cookbook/0596003390/ch04s03.html
    }
}