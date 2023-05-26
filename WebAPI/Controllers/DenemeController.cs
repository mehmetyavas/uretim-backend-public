using Business.Handlers.Deneme.Commands;
using Business.Handlers.Deneme.Queries;
using Business.Handlers.Netsis.Seritra.Queries;
using Business.Handlers.Netsis.stsabit.Queries;
using Business.Handlers.Production.Commands;
using Business.Handlers.Production.Queries;
using Business.Handlers.Products.Queries;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos.deneme;
using Entities.Dtos.Netsis;
using Entities.Dtos.Production;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : BaseApiController
    {


        Peksan23Context _context;
        IAssemblyRepository _assemblyRepository;
        IEnjeksiyonRepository _enjeksiyonRepository;
        ISeritraRepository _seritraRepository;
        IMediator _mediator;
        IIsemriRecRepository _ısemriRecRepository;

        public DenemeController(Peksan23Context context,
            IEnjeksiyonRepository enjeksiyonRepository,
            IAssemblyRepository assemblyRepository,
            ISeritraRepository seritraRepository,
            IMediator mediator,
            IIsemriRecRepository ısemriRecRepository)
        {
            _context = context;
            _enjeksiyonRepository = enjeksiyonRepository;
            _assemblyRepository = assemblyRepository;
            _seritraRepository = seritraRepository;
            _mediator = mediator;
            _ısemriRecRepository = ısemriRecRepository;
        }

        [HttpGet("deneme")]
        public async Task<IActionResult> deneme(string stokKodu)
        {
            var packageStockCode = await _mediator.Send(new GetBagOrPackageQuery { Code1 = "11" });
            var bagStockCode = await _mediator.Send(new GetBagOrPackageQuery { Code1 = "16" });

            var isemriRepoPackage = await _ısemriRecRepository
                .GetAsync(x =>
                    x.Isemrino == "00000001677-000" &&
                     packageStockCode.Data.Contains(x.HamKodu));

            var isemriRepoBag = await _ısemriRecRepository
                .GetAsync(x =>
                    x.Isemrino == "00000001677-000" &&
                     bagStockCode.Data.Contains(x.HamKodu));

            var result = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = isemriRepoPackage.HamKodu });

            return Ok(result);

        }


        [HttpGet("uretim-sonu")]
        public async Task<IActionResult> UretimSonu(string stokKodu,string serino)
        {
            //var result = await _mediator.Send(new GetPackageRemainingQuery { StokKodu = stokKodu });

            //var result =await _context.EryStokListes.ToListAsync();

            var list = new List<int> {500,501,599 };
            var result = await _seritraRepository.GetStockRemainingAmount(stokKodu, list, serino);

            return Ok(result);

        }


        [HttpGet("page")]
        public async Task<IActionResult> UretimSonu(string workOrder, int page, int limit)
        {
            var result = await _mediator.Send(new GetProducedItemsByPagingQuery { WorkOrder = workOrder, Page = page, Limit = limit });


            return Ok(result);

        }


        [HttpGet("etiket")]
        public async Task<IActionResult> Etiket(string seri, int? uretTip)
        {

            var deneme = await _context.Tblseritras
                    .Where(x => x.Belgeno.Contains($"R{seri}0")).MaxAsync(x => x.Belgeno);

            return Ok(deneme);
        }


        [HttpGet("hata")]
        public IActionResult hata()
        {

            return Ok(_mediator.Send(new GetProductionsByPagingQuery { }));
        }
        [HttpGet("stockk")]
        public async Task<IActionResult> hata2(string stokKod, int depoKod, string seriNo)
        {


            var stoks = await _mediator.Send(new GetStockRemainingAmountQuery { StokKodu = stokKod, DepoKod = depoKod, SeriNo = seriNo });

            return Ok(stoks);
        }
        [HttpPost("uretim")]
        public async Task<IActionResult> Uretim(CreateProductionCommand req)
        {
            var result = await _mediator.Send(req);
            return Ok(result.Message);
        }
        
        [HttpPost("uretim-seri")]
        public async Task<IActionResult> ExtraUretim(CreateExtraProductCommand req)
        {
            var result = await _mediator.Send(req);
            return Ok(result.Message);
        }





    }
}
