using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.CreateVoyage
{
    public class CreateVoyageCommandValidator : AbstractValidator<CreateVoyageCommand>
    {
        public CreateVoyageCommandValidator()
        {
            RuleFor(v => v.VoyageDate)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");

            RuleFor(v => v.DeparturePortId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");

            RuleFor(v => v.ArrivalPortId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");

            RuleFor(v => v.Start)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");

            RuleFor(v => v.End)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");

            RuleFor(v => v.ShipId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} is required.");
        }
    }
    
}
