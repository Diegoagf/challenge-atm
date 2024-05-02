using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Request;
using Challenge.Atm.Application.Request.Parameters;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge.Atm.WebUI.Controllers
{
    /// <summary>
    /// Controlador para operaciones relacionadas con tarjetas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor del controlador de transacciones
        /// </summary>
        /// <param name="mediator">Instancia de Mediator para manejar las solicitudes.</param>
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Endpoint para realizar una transaccion de una tarjeta, sea una retiro o un deposito.
        /// </summary>
        /// <param name="request">Datos de la transaccion.</param>
        /// <returns>Transaccion creada.</returns>
        [HttpPost()]
        [Authorize]
        [ProducesResponseType(typeof(CustomResponse<TransactionResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Extract([FromBody] CreateTransactionRequest request)
        {
            var result = await _mediator.Send(new CreateTransactionCommand(request));

            return Created(HttpContext.Request.Path.Value,result);
        }

        /// <summary>
        /// Endpoint de operaciones para consultar todas las transacciones realizadas por tarjeta.
        /// </summary>
        /// <param name="filters">Filtros para la paginacion</param>
        /// <returns>Historial de transacciones</returns>
        [HttpGet("history")]
        [Authorize]
        [ProducesResponseType(typeof(CustomResponse<PagedResponse<List<TransactionResponse>>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistory([FromQuery] RequestParameters filters)
        {
            var result = await _mediator.Send(new GetHistoryTransactionsQuery(filters.PageNumber, filters.PageSize));

            return Ok(result);
        }

    }
}
