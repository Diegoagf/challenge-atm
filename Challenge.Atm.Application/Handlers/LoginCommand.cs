using Challenge.Atm.Application.Requests;
using Challenge.Atm.Domain.Interfaces;
using MediatR;

namespace Challenge.Atm.Application.Handlers
{

    public class LoginCommand: IRequest<string>
    {
        public LoginRequest Request { get; set; }

        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }

    public class LoginCommandHandler: IRequestHandler<LoginCommand, string>
    {
       private readonly ILoginService _loginService;

        public LoginCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken ct) 
        {
            return await _loginService.Login(command.Request.CardNumber, command.Request.Pin, ct);
        }
    }
}
