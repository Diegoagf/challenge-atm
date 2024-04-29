using Ardalis.Specification;
using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;

namespace Challenge.Atm.Application.Handlers
{

    public class GetBalanceQuery : IRequest<CardResponse>
    {

        public GetBalanceQuery()
        {
        }
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, CardResponse>
    {

        private readonly IReadRepositoryAsync<Card> _cardRepository;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        public GetBalanceQueryHandler(IReadRepositoryAsync<Card> cardRepository, IMapper mapper, ILoginService loginService)
        {
            _cardRepository = cardRepository;
            _mapper =mapper;
            _loginService = loginService;
        }

        public async Task<CardResponse> Handle(GetBalanceQuery command, CancellationToken ct) 
        {
            (var cardNumber,_) =  _loginService.ValidateCard();

            var card = await _cardRepository.FirstOrDefaultAsync(new CardSpecification(cardNumber), ct);

            return _mapper.Map<CardResponse>(card);

        }
    }
}
