using AutoFixture;
using AutoMapper;
using Azure.Core;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.Atm.Tests.Application.Handlers.Queries
{
    public class GetHistoryTransactionsQueryHandlerTest
    {
        private readonly GetHistoryTransactionsQueryHandler _sut;
        private readonly Mock<ITransactionService> _transactionService = new();
        private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<ILoginService> _loginService = new();
        private Fixture _dataGenerator = new();
        public GetHistoryTransactionsQueryHandlerTest()
        {
            _sut = new GetHistoryTransactionsQueryHandler(_transactionService.Object,
                _mapper.Object,_loginService.Object);
        }

        [Fact]
        public async Task When_TransactionsIs_Null_Must_Return_A_Response_With_Empty_Transactions()
        {
            //Arrange
           var query =  _dataGenerator.Build< GetHistoryTransactionsQuery>().Create();
            var expected = new PagedResponse<List<TransactionResponse>?>(null, query.PageNumber, query.PageSize);
            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(expected);        
        }
    }
}