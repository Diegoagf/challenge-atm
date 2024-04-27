using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Handlers;
using FluentValidation;

namespace Challenge.Atm.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
             var result = await _mediator.Send(new LoginUserCommand(loginRequest));

             return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateUserRequest request)
        {
            var result = await _mediator.Send(new CreateUserCommand(request));

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());

            return Ok(result);
        }
    }
}
