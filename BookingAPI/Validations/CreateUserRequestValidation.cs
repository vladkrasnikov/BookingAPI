using BookingAPI.Models.User;
using FluentValidation;

namespace BookingAPI.Validations;

public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidation()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().Length(1, 255);
        RuleFor(x => x.LastName).NotEmpty().Length(1, 255);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}