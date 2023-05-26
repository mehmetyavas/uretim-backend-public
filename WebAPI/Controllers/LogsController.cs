using Business.Handlers.Logs.Queries;
using Business.Handlers.Prints.Commands;
using Business.Handlers.Prints.Queries;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.Uretim;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// List Logs
        /// </summary>
        /// <remarks>bla bla bla Logs</remarks>
        /// <return>Logs List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetLogDtoQuery()));
        }


        /// <summary>
        /// List PrintLogs
        /// </summary>
        /// <remarks>bla bla bla Logs</remarks>
        /// <return>Logs List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PrintLog>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("product-print")]
        public async Task<IActionResult> GetPrintList(string? serialNo = null)
        {
            return GetResponse(await Mediator.Send(new GetProductPrintQuery { SerialNo = serialNo }));
        }



        /// <summary>
        /// Post PrintLogs
        /// </summary>
        /// <remarks>bla bla bla Logs</remarks>
        /// <return>Logs List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Core.Utilities.Results.IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("product-print")]
        public async Task<IActionResult> PostPrint(CreatePrintLogsCommand req)
        {
            var result = await Mediator.Send(req);

            return Ok(result);
        }
    }
}