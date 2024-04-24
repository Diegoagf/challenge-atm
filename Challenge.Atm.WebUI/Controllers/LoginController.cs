using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Handlers;
using FluentValidation;

namespace Challenge.Atm.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator) 
        {
            _mediator = mediator;
        }
 

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var result = _mediator.Send(new LoginCommand(loginRequest));

                return Ok(result);
            }
            catch (ValidationException ex)
            {

                return BadRequest(ex.Errors.ToList());
            }
 
        }
    }
}
