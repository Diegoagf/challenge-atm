using AutoMapper;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Request;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Challenge.Atm.Application.Handlers
{

    public class CreateMovementCommand : IRequest<CustomResponse<CardResponse>>
    {
        public CreateMovementRequest Request { get; set; }

        public CreateMovementCommand(CreateMovementRequest request)
        {
            Request = request;
        }
    }

    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, CustomResponse<CardResponse>>
    {

        private readonly IRepositoryAsync<Card> _cardRepository;
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public CreateMovementCommandHandler(IMapper mapper, IRepositoryAsync<Card> cardRepository, ILoginService loginService )
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _loginService = loginService;
        }

        public async Task<CustomResponse<CardResponse>> Handle(CreateMovementCommand command, CancellationToken ct) 
        {

            (var cardNumber, _) = _loginService.ValidateCard();

            var card = await _cardRepository.FirstOrDefaultAsync(new CardSpecification(cardNumber));

            var transaction = _mapper.Map<Transaction>(command.Request);

            if(card!.Transactions == null )
            {
                card.Transactions = new List<Transaction>();
            }
            card!.Transactions.Add(transaction);

            await _cardRepository.UpdateAsync(card, ct);
                
            return new CustomResponse<CardResponse>("Successfully created movement");
        }
    }
}
