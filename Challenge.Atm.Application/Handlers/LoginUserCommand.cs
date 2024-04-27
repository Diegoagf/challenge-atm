using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Domain.Interfaces;
using MediatR;

namespace Challenge.Atm.Application.Handlers
{

    public class LoginUserCommand: IRequest<string>
    {
        public LoginRequest Request { get; set; }

        public LoginUserCommand(LoginRequest request)
        {
            Request = request;
        }
    }

    public class LoginCommandHandler: IRequestHandler<LoginUserCommand, string>
    {
       private readonly ILoginService _loginService;
       private readonly IMapper _mapper;
        public LoginCommandHandler(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        public async Task<string> Handle(LoginUserCommand command, CancellationToken ct) 
        {
            return await _loginService.Login(command.Request.CardNumber, command.Request.Pin, ct);
        }
    }
}
