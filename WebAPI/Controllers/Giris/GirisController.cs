using Business.Handlers.Giris.Commands;
using Business.Handlers.Giris.Queries;
using Business.Handlers.Machines.Queries;
using Business.Handlers.WorkOrders.Queries;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.Giris;
using Entities.Dtos.Giris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Giris
{
    [Route("api/giris")]
    [ApiController]
    public class GirisController : BaseApiController
    {
        /// <summary>
        /// Giris List
        /// </summary>
        /// <remarks>Makine Ve Çalışanların listesi</remarks>
        /// <return> deneme  </return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<GirisDTO>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await Mediator.Send(new GetGirisQuery()));
        }

        /// <summary>
        /// Hygiene Questions List
        /// </summary>
        /// <remarks>Temizlik soru listesi</remarks>
        /// <return> deneme  </return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<List<EryMakineTemizlikSoru>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpGet("questions")]
        public async Task<IActionResult> GetListOfHygieneQuestions(int workingArea)
        {
            return GetResponse(await Mediator.Send(new GetHygieneQuestionsQuery { WorkingArea = workingArea }));
        }


        /// <summary>
        /// Hygiene Questions List
        /// </summary>
        /// <remarks>Temizlik soru listesi</remarks>
        /// <return> deneme  </return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<CreateHygieneAnswerCommand>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        [HttpPost("hygiene-add")]
        public async Task<IActionResult> AddHygieneAnswer(CreateHygieneAnswerCommand req)
        {
            return GetResponseOnlyResult(await Mediator.Send(req));
        }

    }
}
