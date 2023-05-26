using Business.Constants;
using Business.Handlers.Production.Commands;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Validations
{
    public class ProductionValidator : AbstractValidator<CreateProductionCommand>
    {
        public ProductionValidator()
        {
            RuleFor(x => x.PersonelId).NotNull().NotEmpty();
            RuleFor(x => x.MakId).NotNull().NotEmpty();
            RuleFor(x => x.Ciid).NotNull().NotEmpty();
            RuleFor(x => x.LotNo).NotNull().NotEmpty();
            RuleFor(x => x.YapKod).NotNull().NotEmpty();
            RuleFor(x => x.StokKodu).NotNull().NotEmpty();
            RuleFor(x => x.IsemriNo).NotNull().NotEmpty();

            RuleFor(x => x.Dara)
                   .NotNull()
                   .NotEmpty()
                   .GreaterThan((decimal)0.1)
                   .When(x => x.UretTip != 1)
                   .WithMessage(Messages.TareIsGreaterThanZero);

            RuleFor(x => x.BAgirlik)
                .NotEmpty()
                .NotNull()
                .GreaterThan((decimal)0.01)
                .When(x => x.UretTip != 1)
                .WithMessage(Messages.UnitWeightIsGreaterThanZero);

            RuleFor(x => x.Brut)
                  .NotEmpty()
                  .WithMessage(Messages.GrossWeightIsEqualToNetPlusTare);

        }
    }
}
