﻿using System.ComponentModel.DataAnnotations;

namespace ValidationLesson.Models;

public record Account([Required] string FirstName, string LastName, Address Address, List<EmailAddress> EmailAddresses);

public record Address(string Street, string State, string City, string ZipCode);

public record EmailAddress(string Email, EmailType Type);

public enum EmailType : byte
{
    Private,
    Work
}