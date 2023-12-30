using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsExample.Models.Requests;

public class RegisterAccountRequest
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Пароль должен быть не более 20 символов.")]
    public string Password { get; set; }

    [Required] public Address Address { get; set; }

    [Required] public List<EmailAddress> EmailAddresses { get; set; }
}

public class Address
{
    [Required] public string Street { get; set; }

    [Required] public string State { get; set; }

    [Required] public string City { get; set; }

    [Required] public string ZipCode { get; set; }
}

public class EmailAddress
{
    [Required]
    [EmailAddress(ErrorMessage = "Не является действительным адресом электронной почты.")]
    public string Email { get; set; }

    [EnumDataType(typeof(EmailType))] // Assuming EmailType is an enum
    public EmailType Type { get; set; }
}

public enum EmailType
{
    Private,
    Work
}