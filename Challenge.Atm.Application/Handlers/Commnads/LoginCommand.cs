using AutoMapper;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Challenge.Atm.Application.Handlers.Commnads
{

    public class LoginCommand : IRequest<CustomResponse<AuthenticationResponse>>
    {
        public LoginRequest Request { get; set; }

        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, CustomResponse<AuthenticationResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;
        public LoginCommandHandler(IMapper mapper, ILoginService loginService, IConfiguration config)
        {
            _mapper = mapper;
            _loginService = loginService;
            _config = config;
        }

        public async Task<CustomResponse<AuthenticationResponse>> Handle(LoginCommand command, CancellationToken ct)
        {
            var card = _mapper.Map<Card>(command.Request);

            var result=  await _loginService.Login(card,ct);
            var jwt = new AuthenticationResponse(result, _config.GetValue<int>("Jwt:DurationInMinutes"));
            return new CustomResponse<AuthenticationResponse>("Login succeded", jwt);
        }
    }
}
