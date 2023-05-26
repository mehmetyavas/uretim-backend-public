using Business.Constants;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Netsis.Sthar.Rules;
using Business.Handlers.Production.Queries;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Netsis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Sthar.Commands
{
    public class CreateStharCommand : IRequest<IResult>
    {
        public int UretId { get; set; }
        public string UretsonFisNo { get; set; }
    }
    class CreateStharCommandHandler : IRequestHandler<CreateStharCommand, IResult>
    {
        IStharRepository _stharRepository;
        IMediator _mediator;
        StharRules _rules;

        public CreateStharCommandHandler(IStharRepository stharRepository, IMediator mediator)
        {
            _stharRepository = stharRepository;
            _mediator = mediator;
            _rules = new StharRules(_mediator);
        }

        public async Task<IResult> Handle(CreateStharCommand request, CancellationToken cancellationToken)
        {

            var injectionRawMaterials = await _mediator.Send(new GetInjectionMaterialsQuery { UretId = request.UretId });

            var getInjectionProduct = await _mediator.Send(new GetProductionQuery { Id = request.UretId });

            var product = getInjectionProduct.Data;

            _stharRepository.Add(new Tblsthar
            {
                StokKodu = product.StokKodu,
                Fisno = request.UretsonFisNo,
                StharGcmik = product.Adet,
                StharGcmik2 = product.Net,
                Yapkod = product.YapKod,
                StharGckod = "G",
                StharTarih = DateTime.Now,
                DepoKodu = 599,
                StharAciklama = "Uretim",
                Duzeltmetarihi = DateTime.Now,
                StharHtur = "C",
                StharBgtip = "U",
                StharSipnum = product.IsemriNo,
                SubeKodu = 0,
                SYedek1 = product.Id.ToString()

            });


            foreach (var materials in injectionRawMaterials.Data)
            {
                
               var rulesResult = BusinessRules.Run(await _rules.CheckIfRawMaterialIsEnough(materials.VsStokKodu, materials.VsSeriNo, (int)materials.DepoKodu, materials.Harcanan));

                if (rulesResult != null)
                    return new ErrorResult(rulesResult.Message);

                _stharRepository.Add(new Tblsthar
                {
                    StokKodu = materials.VsStokKodu,
                    Fisno = request.UretsonFisNo,
                    StharGcmik = materials.Harcanan,
                    StharGckod = "C",
                    StharTarih = DateTime.Now,
                    DepoKodu = (short?)materials.DepoKodu,
                    StharAciklama = product.StokKodu,
                    StharHtur = "C",
                    StharBgtip = "V",
                    StharSipnum = product.IsemriNo,
                    SubeKodu = 0,
                    SYedek1 = materials.Id.ToString()

                });

            }
           


            await _stharRepository.SaveChangesAsync();

            return new SuccessResult(Messages.StharAdded);
        }
    }
}
