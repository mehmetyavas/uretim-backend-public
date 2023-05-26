using Business.Handlers.AssemblyRawMaterial.Queries;
using Core.Utilities.Results;
using Entities.Concrete.Uretim;
using Entities.Dtos.Production.Assembly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Uretim.AssemblyQueries
{
    [Route("api/assembly")]
    [ApiController]
    public class AssemblyController : BaseApiController
    {

        /// <summary>
        /// Assembly Materials
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDataResult<GetAssemblyRawMaterialQueryDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("material")]
        public async Task<IActionResult> GetAssemblyRawMaterial(string workOrder)
        {
            var result = await Mediator.Send(new GetAssemblyRawMaterialQuery { WorkOrder = workOrder });
            return GetResponse(result);
        }







    }
}
