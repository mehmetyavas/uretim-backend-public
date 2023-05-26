using Business.Constants.Netsis;
using Business.Constants;
using Business.Handlers.Netsis.Stokurs.Commands;
using Business.Handlers.Production.Commands;
using Business.Handlers.Production.Queries;
using Core.Extensions;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Dtos.Production.enjeksiyon;

namespace Business.Handlers.InjectionRawMaterial.Commands
{
    public class CreateMaterialsComand : IRequest<IDataResult<CreateMaterialsComandDto>>
    {
        public string SeriNo { get; set; }
        public bool IsColorTransparent { get; set; }
        public string MacCode { get; set; }
    }

    public class CreateMaterialsComandHandler : IRequestHandler<CreateMaterialsComand, IDataResult<CreateMaterialsComandDto>>
    {
        readonly IMediator _mediator;

        public CreateMaterialsComandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IDataResult<CreateMaterialsComandDto>> Handle(CreateMaterialsComand request, CancellationToken cancellationToken)
        {

            var addedRecord = await _mediator.Send(new GetProductionQuery { SeriNo = request.SeriNo });

            var record = addedRecord.Data;

            if (record == null)
                return new ErrorDataResult<CreateMaterialsComandDto>("Hata Oluştu! ( CREATE_MATERIALS_COMMAND )");


            //add raw material{workOrder = deneme.serino}
            var rawMaterial = await _mediator.Send(new CreateRawMaterialCommand
            { SeriNo = record.SeriNo, IsColorTransparent = request.IsColorTransparent });

            if (!rawMaterial.Success)
                return new ErrorDataResult<CreateMaterialsComandDto>(Messages.RawMaterialError);


            // add raw material color
            if (!request.IsColorTransparent)
            {
                var colorMaterial = await _mediator.Send(new CreateColorMaterialCommand { SeriNo = record.SeriNo });
                if (!colorMaterial.Success)
                {
                    return new ErrorDataResult<CreateMaterialsComandDto>(Messages.ColorMaterialError);
                    //burada Hammaddenin de silinmesi gerek!
                }
            }


            // numune hariç diğer üretimler
            if (record.UretTip != 1)
            {
                //add bag 
                var package = await _mediator.Send(new CreatePackageCommand { Code1 = NetsisConsts.PackageCode, SeriNo = record.SeriNo, IsFire = false });
                if (!package.Success)
                {
                    return new ErrorDataResult<CreateMaterialsComandDto>(Messages.ProductDeleted);
                }

                //kalan etiket buraya gelecek
                if (record.UretTip == 0 || record.UretTip == 5)
                {
                    //add package
                    var bag = await _mediator.Send(new CreatePackageCommand { Code1 = NetsisConsts.BagCode, SeriNo = record.SeriNo, IsFire = false });
                }
                else
                {
                    //add package
                    // buradaki Code1 Değişecek fire ve renk geçişi poşet kodu olacak
                    var bag = await _mediator.Send(new CreatePackageCommand { Code1 = NetsisConsts.BagCode, SeriNo = request.SeriNo, IsFire = true });
                }


                if (record.UretTip == 0)
                {
                    var MaterialLastControl = await _mediator.Send(new GetInjectionMaterialsControlQuery { WorkOrder = record.IsemriNo, ProductId = record.Id });

                    // kütle denkliği yapılacak

                    if (!MaterialLastControl.Success)
                        return new ErrorDataResult<CreateMaterialsComandDto>(MaterialLastControl.Message);
                }


                var NetsisRecord = await _mediator.Send(new CreateStokursCommand
                {
                    MachineCode = request.MacCode,
                    Miktar = record.Adet,
                    StockCode = record.StokKodu,
                    UretId = record.Id,
                    WorkOrder = record.IsemriNo,
                    Yapkod = record.YapKod
                });

                if (!NetsisRecord.Success)
                    return new ErrorDataResult<CreateMaterialsComandDto>(NetsisRecord.Message);

                record.Uretildi = true;
                record.SuskNo = NetsisRecord.Data;

                await _mediator.Send(new UpdateProductionCommand { RpUretimSeri = record });
            }

            return new SuccessDataResult<CreateMaterialsComandDto>
            {
                Data = new CreateMaterialsComandDto
                {
                    SeriNo = record.SeriNo,
                    UretTip = record.UretTip
                }
            };


        }
    }
}
