using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.UpdateVoyage
{
    public class UpdateVoyageCommandValidator : AbstractValidator<UpdateVoyageCommand>
    {

        public UpdateVoyageCommandValidator()
        {
          RuleFor(x => x.VoyageDate)
                .NotEmpty().WithMessage("VoyageDate is required.")
                .NotNull().WithMessage("VoyageDate cannot be null.")
                .LessThan(DateTime.Now).WithMessage("VoyageDate must be in the past.");

            RuleFor(x => x.DeparturePortId)
                .NotEmpty().WithMessage("DeparturePortId is required.")
                .NotNull().WithMessage("DeparturePortId cannot be null.")
                .Must(BeAValidGuid).WithMessage("{PropertyName} is not a valid GUID.");

            RuleFor(x => x.ArrivalPortId)
                .NotEmpty().WithMessage("ArrivalPortId is required.")
                .NotNull().WithMessage("ArrivalPortId cannot be null.")
                .Must(BeAValidGuid).WithMessage("{PropertyName} is not a valid GUID.");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required.")
                .NotNull().WithMessage("Start date cannot be null.")
                .LessThan(DateTime.Now).WithMessage("Start date must be in the past.");

            RuleFor(x => x.End)
                .NotEmpty().WithMessage("End date is required.")
                .NotNull().WithMessage("End date cannot be null.")
                .GreaterThan(x => x.Start).WithMessage("End date must be greater than Start date.");
        }


        private bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }

    }
   
}
