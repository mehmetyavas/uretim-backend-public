using Business.Constants;
using Business.Handlers.Netsis.Seritra.Queries;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Netsis.Sthar.Rules
{
    public class StharRules
    {
        IMediator _mediator;

        public StharRules(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IResult> CheckIfRawMaterialIsEnough(string stokKod, string seriNo, int depoKod, decimal harcanan)
        {
            if (stokKod.Contains("150-02-"))
            {
                depoKod = 599;
            }
            var stoks = await _mediator.Send(new GetStockRemainingAmountQuery { StokKodu = stokKod, DepoKod = depoKod, SeriNo = seriNo });

            if (!stoks.Success)
                return new ErrorResult(stoks.Message);

            foreach (var stok in stoks.Data)
            {
                //burası küçük olmalı küçük eşit olursa eşit olmasına rağmen harcama işlemi yapmıyor
                if (stok.Bakiye < harcanan)
                    return new ErrorResult(Messages.NotEnoughRawMaterial);
            }


            return new SuccessResult();

        }
    }
}
