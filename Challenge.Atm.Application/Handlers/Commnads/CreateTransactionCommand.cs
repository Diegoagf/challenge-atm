using AutoMapper;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Request;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Challenge.Atm.Application.Handlers.Commnads
{

    public class CreateTransactionCommand : IRequest<CustomResponse<TransactionResponse>>
    {
        public CreateTransactionRequest Request { get; set; }

        public CreateTransactionCommand(CreateTransactionRequest request)
        {
            Request = request;
        }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CustomResponse<TransactionResponse>>
    {

        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        public CreateTransactionCommandHandler(IMapper mapper, ILoginService loginService, ITransactionService transactionService)
        {
            _mapper = mapper;
            _loginService = loginService;
            _transactionService = transactionService;
        }

        public async Task<CustomResponse<TransactionResponse>> Handle(CreateTransactionCommand command, CancellationToken ct)
        {

            (var cardNumber, var userName) = _loginService.ValidateCard();

            var result = await _transactionService.CreateAsync(cardNumber, userName, command.Request.Amount, command.Request.Type, ct);

            var transactionDto = _mapper.Map<TransactionResponse>(result);

            return new CustomResponse<TransactionResponse>("Successfully created transaction", transactionDto);
        }
    }
}
