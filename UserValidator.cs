using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(user => user.CardNumber)
            .Length(5).WithMessage("Card number must be exactly 5 digits.")
            .Matches(@"^\d+$").WithMessage("Card number must be numeric.");
    }
}
