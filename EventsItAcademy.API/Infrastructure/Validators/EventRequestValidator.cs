using EventsItAcademy.API.Infrastructure.Localization;
using EventsItAcademy.Application.Events.Requests;
using FluentValidation;

namespace EventsItAcademy.API.Infrastructure.Validators
{
    public class EventRequestValidator:AbstractValidator<EventRequestModel>
    {
        public EventRequestValidator()
        {
            RuleFor(e=>e.Title)
                .NotEmpty().WithMessage(ErrorMessages.Empty)
                .MinimumLength(2).WithMessage(ErrorMessages.MaxLength)
                .MaximumLength(20).WithMessage(ErrorMessages.MinLength);

            RuleFor(e => e.TicketQuantity)
                .NotEmpty().WithMessage(ErrorMessages.Empty)
                .GreaterThan(0);

            RuleFor(e => e.Destription)
               .MaximumLength(300).WithMessage(ErrorMessages.MaxLength);
        }
    }
}
