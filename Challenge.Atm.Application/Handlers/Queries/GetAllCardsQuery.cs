using Ardalis.Specification;
using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using System.Threading;

namespace Challenge.Atm.Application.Handlers.Queries
{

    public class GetAllCardsQuery : IRequest<PagedResponse<List<CardWithPinResponse>?>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Name { get; set; }

        public GetAllCardsQuery(int pageNumber, int pagedSize, string? name)
        {
            PageNumber = pageNumber;
            PageSize = pagedSize;
            Name = name;
        }
    }

    public class GetAllCardsQueryHandler : IRequestHandler<GetAllCardsQuery, PagedResponse<List<CardWithPinResponse>?>>
    {

        private readonly IReadRepositoryAsync<Card> _cardRepository;
        private readonly IMapper _mapper;
        public GetAllCardsQueryHandler(IReadRepositoryAsync<Card> cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<CardWithPinResponse>?>> Handle(GetAllCardsQuery request, CancellationToken ct)
        {
            var cards = await _cardRepository.ListAsync(new CardsPagedSpecification(request.PageSize, request.PageNumber, request.Name), ct);

            var cardsDto = _mapper.Map<List<CardWithPinResponse>>(cards);

            return new PagedResponse<List<CardWithPinResponse>?>(cardsDto, request.PageNumber, request.PageSize);
        }
    }
}
