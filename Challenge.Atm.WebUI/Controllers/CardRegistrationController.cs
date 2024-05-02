using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Request;
using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Response;
using System.Net;

namespace Challenge.Atm.WebUI.Controllers
{

    /// <summary>
    /// Controlador para registar una tarjeta y obtenerlas.
    /// </summary>
    [Route("api/cards")]
    [ApiController]
    public class CardRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor del controlador de tarjeta
        /// </summary>
        /// <param name="mediator">Instancia de Mediator para manejar las solicitudes.</param>
        public CardRegistrationController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para crear una nueva tarjeta
        /// </summary>
        /// <param name="request">Datos de la nueva tarjeta.</param>
        /// <returns>Tarjeta creada.</returns>
        [HttpPost("new")]
        [ProducesResponseType(typeof(CustomResponse<CardResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Add([FromBody] CardRequest request)
        {
            var result = await _mediator.Send(new CreateCardCommand(request));

            return Created(HttpContext.Request.Path.Value,result);
        }

        /// <summary>
        /// Endpoint para obtener todas las tarjetas paginada
        /// </summary>
        /// <param name="filters">Filtros para paginacion.</param>
        /// <returns>Tarjetas registradas en el sistema.</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(CustomResponse<CardResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCardsParameters filters)
        {
            var result = await _mediator.Send(new GetAllCardsQuery(filters.PageNumber, filters.PageSize, filters.Name));

            return Ok(result);
        }
    }
}
