using Ardalis.Specification;
using AutoFixture;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Domain.Enums;
using Challenge.Atm.Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace Challenge.Atm.Tests.Application.Services
{
    public class TransactionServiceTest
    {
        private readonly TransactionService _sut;
        private readonly Mock<IRepositoryAsync<Card>> _cardRepository = new();
        private Fixture _dataGenerator = new();
        public TransactionServiceTest()
        {
            _sut = new TransactionService(_cardRepository.Object);
        }

        [Fact]
        public async Task Given_CardNumber_Should_Return_Transactions_Paged()
        {
            //Arrange
            var pageSize = 1;
            var pageNumber = 1;
            var cardNumber = "123412341241234";
            var transactions = _dataGenerator.Build<Transaction>().CreateMany().ToList();

            var cardWithTransactions = _dataGenerator.Build<Card>()
                .With(x => x.Transactions, transactions)
                .Create();

            _cardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                .ReturnsAsync(cardWithTransactions);
            //Act
            var result = await _sut.GetTransactionsAsync(pageSize, pageNumber, cardNumber, CancellationToken.None);

            //Assert
            result.Count.Should().Be(pageSize);
        }

        [Fact]
        public async Task GivenCard_Without_Transactos_Should_Return_Null()
        {
            //Arrange
            var pageSize = 1;
            var pageNumber = 1;
            var cardNumber = "123412341241234";

            _cardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                .ReturnsAsync((Card?)null);
            //Act
            var result = await _sut.GetTransactionsAsync(pageSize, pageNumber, cardNumber, CancellationToken.None);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
       public async Task GivenCard_With_Insufficient_Funds_Should_Return_Exception()
        {
            //Arrange
            var userName = "Test";
            var amount = 100M;
            var type = TransactionType.Extraction.ToString();
            var cardNumber = "123412341241234";

            var card = _dataGenerator.Build<Card>()
                 .With(x => x.Balance, amount-1)
                 .Create();

            _cardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                .ReturnsAsync(card);
            //Act
            var funcResult = ()=> _sut.CreateAsync(cardNumber, userName, amount, type, CancellationToken.None);

            //Assert
            await funcResult.Should().ThrowAsync<ApiCustomException>()
                .WithMessage("Insufficient funds");
        }


        [Fact]
        public async Task GivenCardBalance_Sufficient_And_Is_Extraction_Should_Return_New_Transaction()
        {
            //Arrange
            var userName = "Test";
            var amount = 100M;
            var type = TransactionType.Extraction;
            var cardNumber = "123412341241234";

            var card = _dataGenerator.Build<Card>()
                 .With(x => x.Balance, amount*2)
                 .Create();

            _cardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                .ReturnsAsync(card);
            //Act
            var result = await  _sut.CreateAsync(cardNumber, userName, amount, type.ToString(), CancellationToken.None);

            //Assert
            result.Amount.Should().Be(amount);
            result.CreatedBy.Should().Be(userName);
            result.LastModifiedBy.Should().Be(userName);
            result.Type.Should().Be(type);
            card.Balance.Should().Be(amount);
        }
    }
}
