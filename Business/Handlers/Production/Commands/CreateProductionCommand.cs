using AutoMapper;
using Business.Constants;
using Business.Constants.Netsis;
using Business.Handlers.InjectionRawMaterial.Commands;
using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Netsis.Stokurs.Commands;
using Business.Handlers.Production.Queries;
using Business.Handlers.Production.Rules;
using Business.Handlers.Production.Validations;
using Business.Handlers.WorkOrders.Queries;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Production.Commands
{
    public class CreateProductionCommand : IRequest<IDataResult<RpEtiketTbl>>
    {
        public int PersonelId { get; set; }
        public string IsemriNo { get; set; }
        public int MakId { get; set; }
        public decimal BAgirlik { get; set; }
        public decimal Dara { get; set; }
        public decimal Net { get; set; }
        public decimal Adet { get; set; }
        public string StokKodu { get; set; }
        public string YapKod { get; set; }
        public byte UretTip { get; set; }
        public string SipNo { get; set; }
        public string Ciid { get; set; }
        public decimal? Brut { get; set; }
        public string LotNo { get; set; }
        public string Vardiya { get; set; }
        public string Terazi { get; set; }
    }


    public class CreateProductionCommandHandler : IRequestHandler<CreateProductionCommand, IDataResult<RpEtiketTbl>>
    {
        IProductionRepository _productionRepository;
        IEsnekYapilandirmaRepository _esnekYapilandirmaRepository;
        IMachineRepository _machineRepository;
        IIsemriRecRepository _isemriRecRepository;
        ISeritraRepository _seritraRepository;
        ProductionRules _rules;
        IMediator _mediator;
        IMapper _mapper;



        public CreateProductionCommandHandler(
            IProductionRepository productionRepository,
            IMapper mapper,
            IEsnekYapilandirmaRepository esnekYapilandirmaRepository,
            IMachineRepository machineRepository,
            IIsemriRecRepository isemriRecRepository,
            ISeritraRepository seritraRepository,
            IMediator mediator)
        {
            _seritraRepository = seritraRepository;

            _productionRepository = productionRepository;
            _esnekYapilandirmaRepository = esnekYapilandirmaRepository;
            _machineRepository = machineRepository;
            _isemriRecRepository = isemriRecRepository;

            _mapper = mapper;
            _mediator = mediator;
            _rules = new(
                _productionRepository,
                _isemriRecRepository,
                _seritraRepository,
                _mediator
                );

        }


        [TransactionScopeAspectAsync]
        [LogAspect(typeof(MsSqlLogger))]
        [ValidationAspect(typeof(ProductionValidator))]

        public async Task<IDataResult<RpEtiketTbl>> Handle(CreateProductionCommand request, CancellationToken cancellationToken)
        {
            var mappedReq = _mapper.Map<RpUretimSeri>(request);

            var controlClass = new ProductionControlClass(
                _productionRepository,
                _machineRepository,
                _esnekYapilandirmaRepository
                );

            var getMachineType = await _machineRepository.GetAsync(x => x.MachineCode == mappedReq.MakId.ToString());


            //yeni seri numarası
            mappedReq.SeriNo = await controlClass.GenerateNewSerialNo(mappedReq, getMachineType.Code);


            bool isColorTransparent = await controlClass.CheckColorIsTransParent(mappedReq.YapKod);


            ///<summary>
            ///Business Kuralları
            ///üret tipi 1 ise koli ve poşet kontrolü gerekiyor mu ? 
            /// min maxın üret tip kontrolü yapıldı
            ///</summary>

            var RulesResult = BusinessRules.Run(
                await _rules.CheckIfSerialNoExists(mappedReq.SeriNo),

               request.UretTip == 0 || request.UretTip == 3
                   ? await _rules.CheckIfWorkOrderAmountIsEnough(mappedReq.IsemriNo, mappedReq.Adet)
                   : new SuccessResult(),

                await _rules.CheckValueIsBetweenMaxMin(mappedReq.IsemriNo, mappedReq.Net, mappedReq.Adet, mappedReq.UretTip),

                await _rules.CheckIfPackageOrBagIsEnough(mappedReq.IsemriNo, NetsisConsts.PackageCode, mappedReq.UretTip),

                await _rules.CheckIfPackageOrBagIsEnough(mappedReq.IsemriNo, NetsisConsts.BagCode, mappedReq.UretTip));

            //kuralları gözden geçir kayıt kısmına ilerle

            if (RulesResult != null)
                throw new Exception(RulesResult.Message);


            if (getMachineType.Description2 == "E")
            {
                var injRulesResults = BusinessRules.Run(
                await _rules.CheckIfRawMaterialIsEnough(mappedReq.IsemriNo, mappedReq.Net, getMachineType.Description2),

                isColorTransparent
                     ? new SuccessResult()
                     : await _rules.CheckIfColorIsEnough(mappedReq.IsemriNo, mappedReq.Net));

                if (injRulesResults != null)
                    throw new Exception(injRulesResults.Message);
            }
            //Montaj ve Serigrafi
            else
            {
                throw new Exception("Montaj Üretimi şuanda yapılamıyor");
            }


            mappedReq.Tarih = DateTime.Now;
            mappedReq.Created = DateTime.Now;

            var deneme = await _productionRepository.AddAsync(mappedReq);


            //Enjeksiyon
            if (getMachineType.Description2 == "E")
            {

                var injectionMaterialResponse = await _mediator.Send(new CreateMaterialsComand { SeriNo = mappedReq.SeriNo, IsColorTransparent = isColorTransparent, MacCode = getMachineType.Code });

                if (!injectionMaterialResponse.Success)
                    throw new Exception(injectionMaterialResponse.Message);
            }
            //Montaj ve Serigrafi
            else
            {
                throw new Exception("Montaj Üretimi şuanda yapılamıyor");
            }




            //Fiş
            var labelInfoResult = await _mediator.Send(new GetProductionLabelInfoQuery { SerialNo = mappedReq.SeriNo, ProductType = mappedReq.UretTip });

            if (labelInfoResult.Data == null)
                throw new Exception("Fiş Hatası");

            return labelInfoResult;
        }
    }
}

