using AutoMapper;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Challenge.Atm.Application.Handlers.Commnads
{

    public class CreateCardCommand : IRequest<CustomResponse<CardResponse>>
    {
        public CardRequest Request { get; set; }

        public CreateCardCommand(CardRequest request)
        {
            Request = request;
        }
    }

    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, CustomResponse<CardResponse>>
    {

        private readonly IRepositoryAsync<Card> _cardRepository;
        private readonly IMapper _mapper;
        public CreateCardCommandHandler(IMapper mapper, IRepositoryAsync<Card> cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CustomResponse<CardResponse>> Handle(CreateCardCommand command, CancellationToken ct)
        {

            var cardEntity = _mapper.Map<Card>(command.Request);

            var exist = await _cardRepository.FirstOrDefaultAsync(new CardSpecification(cardEntity.CardNumber));
            if(exist != null)
            {
                throw new ApiCustomException("the cardNumber is already exist, provide another card number", HttpStatusCode.Conflict);
            }
            var result = await _cardRepository.AddAsync(cardEntity);

            var cardDto = _mapper.Map<CardResponse>(result);

            return new CustomResponse<CardResponse>("Successfully created card", cardDto);
        }
    }
}
