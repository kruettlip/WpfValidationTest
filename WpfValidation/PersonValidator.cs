using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfValidation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(2)
                .Matches(@"^[a-zA-ZÄÖÜäöü'\s]+$").WithMessage("Enter a valid name");
            RuleFor(p => p.Birthday)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^((0[0-9])|([1-2][0-9])|(3[0-1]))\.((0[1-9])|(1[1-2]))\.([1-2][0-9]{3})$").WithMessage("Enter a valid date")
                .Must(bd => DateTime.ParseExact(bd, "dd.MM.yyyy", null) >= new DateTime(1940, 1, 1)).When(p => !string.IsNullOrEmpty(p.Birthday)).WithMessage("Date cannot be earlier than 1.1.1940");
            RuleFor(p => p.Phonenumber)
                .Cascade(CascadeMode.Stop)
                .Matches(@"((\+?[0-9]{1,3}\s?[0-9]{2})|(0[0-9]{3}))\s?[0-9]{3}\s?[0-9]{2}\s?[0-9]{2}").WithMessage("Enter a valid phone number");
        }
    }
}
