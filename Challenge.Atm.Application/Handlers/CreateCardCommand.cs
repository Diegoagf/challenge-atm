using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Challenge.Atm.Application.Handlers
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
        public CreateCardCommandHandler(IMapper mapper, IRepositoryAsync<Card> cardRepository )
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CustomResponse<CardResponse>> Handle(CreateCardCommand command, CancellationToken ct) 
        {
            
            var userEntity = _mapper.Map<Card>(command.Request);

            var result= await _cardRepository.AddAsync(userEntity);
                
            var cardDto = _mapper.Map<CardResponse>(result);

            return new CustomResponse<CardResponse>("Successfully created card", cardDto);
        }
    }
}
