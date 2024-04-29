using AutoMapper;
using Challenge.Atm.Application.Exceptions;
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
       private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        public LoginCommandHandler(IMapper mapper, ILoginService loginService)
        {
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken ct) 
        {
            var card = _mapper.Map<Card>(command.Request);

            return await _loginService.Login(card);
        }
    }
}
