using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;

namespace Challenge.Atm.Application.Handlers
{

    public class CreateUserCommand : IRequest<User>
    {
        public CreateUserRequest Request { get; set; }

        public CreateUserCommand(CreateUserRequest request)
        {
            Request = request;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
       private readonly ILoginService _loginService;
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(ILoginService loginService, IMapper mapper, IRepositoryAsync<User> userRepository)
        {
            _loginService = loginService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken ct) 
        {
            var userEntity = _mapper.Map<User>(command.Request);

            return await _userRepository.AddAsync(userEntity);
        }
    }
}
