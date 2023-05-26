using Business.Handlers.WorkOrders.Queries;
using Core.Utilities.Results;
using Entities.Concrete.Uretim;
using Entities.Dtos.Giris;
using Entities.Dtos.WorkOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.WorkOrder
{
    [Route("api/workOrder")]
    [ApiController]
    public class WorkOrdersController : BaseApiController
    {
        /// <summary>
        /// WorkOrder List
        /// </summary>
        /// <remarks>İşemri Listesi</remarks>
        /// <return> deneme  </return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkOrderDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet]
        public async Task<IActionResult> GetWork(int machineId)
        {
            return GetResponse(await Mediator.Send(new GetWorkOrderListQuery { MachineId = machineId }));
        }


        /// <summary>
        /// WorkOrder Min Max kontrol
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RpIsemriBilgi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("min-max")]
        public async Task<IActionResult> GetWorkOrderMinMax(string? workOrder)
        {

            return GetResponse(await Mediator.Send(new GetMinMaxQuery { IsemriNo = workOrder }));
        }


        /// <summary>
        /// WorkOrder Min Max kontrol
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetToBeProducedQuery))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("to-be-produced")]
        public async Task<IActionResult> GetToBeProduced(int ciid)
        {
            return GetResponse(await Mediator.Send(new GetToBeProducedQuery { Ciid = ciid }));
        }

        /// <summary>
        /// WorkOrder Min Max kontrol
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWorkOrderInfoQuery))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("info")]
        public async Task<IActionResult> GetWorkOrderInfo(workOrderInfoReqDto req)
        {
            return GetResponse(await Mediator.Send(new GetWorkOrderInfoQuery { IsemriNo = req.WorkOrder, MachineId = req.MachineId }));
        }
    }
}
