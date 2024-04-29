using Ardalis.Specification;
using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using System.Threading;

namespace Challenge.Atm.Application.Handlers
{

    public class GetAllCardsQuery : IRequest<PagedResponse<List<CardResponse>?>>
    {
        public int PageNumber { get; set; }
        public int PagedSize { get; set; }

        public string? Name { get; set; }

        public GetAllCardsQuery(int pageNumber, int pagedSize, string? name)
        {
            PageNumber = pageNumber;
            PagedSize = pagedSize;
            Name = name;
        }
    }

    public class GetAllCardsQueryHandler : IRequestHandler<GetAllCardsQuery, PagedResponse<List<CardResponse>?>>
    {

        private readonly IReadRepositoryAsync<Card> _cardRepository;
        private readonly IMapper _mapper;
        public GetAllCardsQueryHandler(IReadRepositoryAsync<Card> cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper =mapper;
        }

        public async Task<PagedResponse<List<CardResponse>?>> Handle(GetAllCardsQuery request, CancellationToken ct) 
        {
            var users = await _cardRepository.ListAsync(new CardsPagedSpecification(request.PagedSize, request.PageNumber, request.Name), ct);

            var userDto = _mapper.Map<List<CardResponse>>(users);

            return new PagedResponse<List<CardResponse>?>(userDto, request.PageNumber, request.PagedSize);
        }
    }
}
