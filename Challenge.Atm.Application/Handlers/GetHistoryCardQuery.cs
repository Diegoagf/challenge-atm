using Ardalis.Specification;
using AutoMapper;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using System.Threading;

namespace Challenge.Atm.Application.Handlers
{

    public class GetHistoryCardQuery : IRequest<PagedResponse<CardResponse?>>
    {
        public int PageNumber { get; set; }
        public int PagedSize { get; set; }

        public GetHistoryCardQuery(int pageNumber, int pagedSize)
        {
            PageNumber = pageNumber;
            PagedSize = pagedSize;
        }
    }

    public class GetHistoryCardQueryHandler : IRequestHandler<GetHistoryCardQuery, PagedResponse<CardResponse?>>
    {

        private readonly IReadRepositoryAsync<Card> _cardRepository;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        public GetHistoryCardQueryHandler(IReadRepositoryAsync<Card> cardRepository, IMapper mapper, ILoginService loginService)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<PagedResponse<CardResponse?>> Handle(GetHistoryCardQuery request, CancellationToken ct) 
        {
            (var cardNumber, _) = _loginService.ValidateCard();
            var user = await _cardRepository.FirstOrDefaultAsync(new CardWithTransactionsSpecification(request.PagedSize, request.PageNumber, cardNumber), ct);

            var userDto = _mapper.Map<CardResponse>(user);

            return new PagedResponse<CardResponse?>(userDto, request.PageNumber, request.PagedSize);
        }
    }
}
