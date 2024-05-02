using Ardalis.Specification;
using AutoFixture;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Challenge.Atm.Tests.Application.Services
{

    public class LoginServiceTest
    {
        private LoginService _sut;
        private readonly Mock<IJwtService> _jwtService = new();
        private readonly Mock<IReadRepositoryAsync<Card>> _readCardRepository = new();
        private readonly Mock<IRepositoryAsync<Card>> _cardRepository = new();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<IMemoryCache> _cache = new();
        private readonly Mock<ICacheEntry> _cacheEntry = new();
        private delegate void OutDelegate<Tin, Tout>(Tin input, out Tout output);
        private Fixture _dataGenerator = new();
        public LoginServiceTest()
        {
            _sut = new LoginService(_jwtService.Object, _readCardRepository.Object,
                _cache.Object, _cardRepository.Object, _httpContextAccessor.Object);
        }


        [Fact]
        public async Task WhenNotExist_The_Card_Mut_Return_Exception()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>()
             .Create();

            _readCardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                   .ReturnsAsync((Card?)null);

            //Act
            var funcResult = ()=> _sut.Login(card, CancellationToken.None);

            //Assert
            await funcResult.Should().ThrowAsync<ApiCustomException>()
                                .WithMessage("The card with that number does not exist"); ;
        }

        [Fact]
        public async Task When_Card_Is_Blocked_Must_Return_Exception()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>()
                .With(x => x.IsBlocked, true)
                .Create();

            _readCardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                   .ReturnsAsync(card);

            //Act
            var funcResult = () => _sut.Login(card, CancellationToken.None);

            //Assert
            await funcResult.Should().ThrowAsync<ApiCustomException>()
                .WithMessage("The card is Blocked");
        }


        [Fact]
        public async Task When_FailedAttempts_Is_Greathet_Than_4_Must_Return_Exception()
        {

            //Arrange
            var card = _dataGenerator.Build<Card>()
                .With(x=> x.IsBlocked, false)
                .Create();

            _readCardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                               .ReturnsAsync(card);

            object? t;
            _cache
            .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out t))
            .Callback(new OutDelegate<object, object>((object k, out object v) =>
                v = 5)) 
            .Returns(true);

            //Act
            var funcResult = () => _sut.Login(card, CancellationToken.None);
            //Assert;
            await funcResult.Should().ThrowAsync<ApiCustomException>()
                    .WithMessage("The card has been blocked, please contact your administrator");
            _cardRepository.Verify(x => x.UpdateAsync(card,CancellationToken.None), Times.Once());

        }
      
        [Fact]
        public async Task WhenAllDataIsValid_Should_Return_A_Jwt()
        {

            //Arrange
            var card = _dataGenerator.Build<Card>()
                 .With(x => x.IsBlocked, false)
                .Create();
            
            var token = "token";

            _readCardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                               .ReturnsAsync(card);
            MockGetCache(1);                
            _cardRepository.Setup(x => x.UpdateAsync(It.IsAny<Card>(), CancellationToken.None));
            _jwtService.Setup(x => x.GenerateToken(It.IsAny<Card>()))
                .Returns(token);
            //Act
            var result = await _sut.Login(card, CancellationToken.None);

            //Assert;
            result.Should().Be(token);
        }
        private void MockGetCache(object? value= default)
        {
            object? t;
            _cache
            .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out t))
            .Callback(new OutDelegate<object, object>((object k, out object v) =>
                v = value))
            .Returns(true);

        }
    }
}
