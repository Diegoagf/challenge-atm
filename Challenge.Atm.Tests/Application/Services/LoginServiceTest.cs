using AutoFixture;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Fixture _dataGenerator = new();
        public LoginServiceTest()
        {
            _sut = new LoginService(_jwtService.Object, _readCardRepository.Object,
                _cache.Object, _cardRepository.Object, _httpContextAccessor.Object);
        }

        [Fact]
        public async Task Test()
        {

            //Arrange
            var card= _dataGenerator.Build<Card>()
                .Create();

            //Act
            var result = _sut.Login(card);

            //Assert;
            result.Should().NotBeNull();
        }
    }
}
