using AutoFixture;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.Atm.Tests.Infrastructure.Service
{
    
    public class JwtServiceTest
    {

        private readonly JwtService _sut;
        private Fixture _dataGenerator = new();

        public JwtServiceTest()
        {
            var memorySettings = new Dictionary<string, string>
            {
                { "Jwt:DurationInMinutes", "15" },
                {"Jwt:Key","Lh2JlgDp6tD6Eg5dcuc5dSvLwAX0Q2KGuxuAfvzG" }
            };
            var config = new ConfigurationBuilder().AddInMemoryCollection(memorySettings).Build();
            _sut = new JwtService(config);
        }


        [Fact]
        public void Given_A_Card_Should_GenerateToken_And_Validate_Is_Valid()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().Create();
            //Act
            var jwt = _sut.GenerateToken(card);
            var result = _sut.ValidateToken(jwt);
            //Assert
            result.OwnerName.Should().Be(card.OwnerName);
            result.cardNumber.Should().Be(card.CardNumber);
        }

        [Fact]
        public void When_Jwt_Is_Modifiied_Shoult_Return_Exception()
        {
            //Arrange
            var card = _dataGenerator.Build<Card>().Create();
            //Act
            var jwtModified = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjYXJkX251bWJlciI6IkNhcmROdW1iZXIxZWE0YjZiZC1jZjdmLTQ2YTYtYTBmYy1iM2NlMzE1ZDQ3MDEiLCJuYW1lIjoiT3duZXJOYW1lNzFhYThkMGItZGU5NS00MjQ1LWIxOWMtMWNiMTFkODNjYzY1IiwiZXhwIjoxNzE0NjI3Njg5fQ.UAMhC29VSjDW2wBV9AsqFL4Ov-WI-ID54nKu0I7I5Rg";
            var funcResult = ()=>_sut.ValidateToken(jwtModified);
            //Assert
            funcResult.Should().Throw<ApiCustomException>();
        }

    }
}
