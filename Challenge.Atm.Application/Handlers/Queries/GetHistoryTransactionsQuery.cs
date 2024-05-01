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

namespace Challenge.Atm.Application.Handlers.Queries
{

    public class GetHistoryTransactionsQuery : IRequest<PagedResponse<List<TransactionResponse>?>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetHistoryTransactionsQuery(int pageNumber, int pagedSize)
        {
            PageNumber = pageNumber;
            PageSize = pagedSize;
        }
    }

    public class GetHistoryTransactionsQueryHandler : IRequestHandler<GetHistoryTransactionsQuery, PagedResponse<List<TransactionResponse>?>>
    {

        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        public GetHistoryTransactionsQueryHandler(ITransactionService transactionService, IMapper mapper, ILoginService loginService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<PagedResponse<List<TransactionResponse>?>> Handle(GetHistoryTransactionsQuery request, CancellationToken ct)
        {
            (var cardNumber, _) = _loginService.ValidateCard();

           var transactions = await _transactionService.GetTransactionsAsync(request.PageSize, request.PageNumber, cardNumber, ct);
            
           var transactionsDto = _mapper.Map<List<TransactionResponse>>(transactions);

            return new PagedResponse<List<TransactionResponse>?>(transactionsDto, request.PageNumber, request.PageSize);
        }
    }
}
