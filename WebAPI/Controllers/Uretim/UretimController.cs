using Business.Handlers.InjectionRawMaterial.Queries;
using Business.Handlers.Production.Commands;
using Business.Handlers.Production.Queries;
using Business.Handlers.Products.Queries;
using Business.Handlers.WorkOrders.Queries;
using Core.Utilities.Results;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production;
using Entities.Dtos.Production.enjeksiyon;
using Entities.Dtos.WorkOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Replication.PgOutput.Messages;
using Org.BouncyCastle.Ocsp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Uretim
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimController : BaseApiController
    {

        /// <summary>
        /// Uretim Add 
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RpEtiketTbl>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost]
        public async Task<IActionResult> Uret(CreateProductionCommand req)
        {
            return GetResponse(await Mediator.Send(req));
        }


        /// <summary>
        /// Uretim List
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomPaginationResult<List<GetProducedDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("produced-item")]
        public async Task<IActionResult> GetProducedItem(string workOrder, int? page, int? limit)
        {
            return GetResponse(await Mediator.Send(new GetProducedItemsByPagingQuery
            {
                WorkOrder = workOrder,
                Page = page,
                Limit = limit
            }));
        }



        /// <summary>
        /// Product Materials List
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnjeksiyonHammaddeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("produced-materials")]
        public async Task<IActionResult> GetProducedItem(int uretId)
        {
            return GetResponse(await Mediator.Send(new GetMaterialsQuery
            {
                UretId = uretId
            }));
        }





        /// <summary>
        /// Etiket
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RpEtiketTbl>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("produced-label")]
        public async Task<IActionResult> GetProducedLabel(string serialNo, int uretTip)
        {
            return GetResponse(await Mediator.Send(new GetProductLabelQuery { SerialNo = serialNo, UretTip = uretTip }));
        }





    }
}
