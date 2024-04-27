using Ardalis.Specification;
using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using System.Threading;

namespace Challenge.Atm.Application.Handlers
{

    public class GetUsersQuery : IRequest<List<User>?>
    {

    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>?>
    {

        private readonly IReadRepositoryAsync<User> _userRepository;
        public GetUsersQueryHandler(IReadRepositoryAsync<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>?> Handle(GetUsersQuery command, CancellationToken ct) 
        {
            var specification = new UsersWithCardsSpecification();
            return await _userRepository.ListAsync(specification, ct);

        }
    }
}
