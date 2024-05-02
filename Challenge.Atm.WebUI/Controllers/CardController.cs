using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Request;
using Microsoft.AspNetCore.Authorization;
using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Response;
using System.Net;
using Challenge.Atm.Application.Wrappers;

namespace Challenge.Atm.WebUI.Controllers
{
    /// <summary>
    /// Controlador para operar con una tarjeta.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor del controlador de tarjeta
        /// </summary>
        /// <param name="mediator">Instancia de Mediator para manejar las solicitudes.</param>
        public CardController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para iniciar sesión con un numero de tarjeta.
        /// </summary>
        /// <param name="loginRequest">Datos de inicio de sesión de la tarjeta.</param>
        /// <returns>Jwt asociado a la tarjeta.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(CustomResponse<AuthenticationResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Locked)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
             var result = await _mediator.Send(new LoginCommand(loginRequest));

             return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener el saldo de la tarjeta.
        /// </summary>
        /// <returns>Saldo de la tarjeta.</returns>
        [HttpGet("balance")]
        [Authorize]
        [ProducesResponseType(typeof(CustomResponse<PagedResponse<List<TransactionResponse>>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBalance()
        {
            var result = await _mediator.Send(new GetBalanceQuery());

            return Ok(result);
        }
    }
}
