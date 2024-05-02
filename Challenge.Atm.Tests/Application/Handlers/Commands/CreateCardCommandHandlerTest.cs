using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Request;
using Challenge.Atm.Application.Requests;
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

namespace Challenge.Atm.Tests.Application.Handlers.Commands
{
    public class CreateCardCommandHandlerTest
    {
        private readonly CreateCardCommandHandler _sut;
        private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<IRepositoryAsync<Card>> _cardRepository = new();
        private Fixture _dataGenerator = new();
        public CreateCardCommandHandlerTest()
        {
            _sut = new CreateCardCommandHandler(_mapper.Object, _cardRepository.Object);
        }

        [Fact]
        public async Task When_Data_IsValid_Should_Return_CardResponse()
        {
            //Arrange
            var request = _dataGenerator.Build<CardRequest>().Create();
            var command = _dataGenerator.Build<CreateCardCommand>()
                .With(x=> x.Request,request)
                .Create();

            var responseCard = _dataGenerator.Build<CardResponse>()
                .Create();

            _mapper.Setup(x => x.Map<Card>(It.IsAny<CardRequest>()))
          .Returns(new Card() { CardNumber = "1231" });

           _mapper.Setup(x => x.Map<CardResponse>(It.IsAny<Card>()))
                .Returns(responseCard);

            var expected = new CustomResponse<CardResponse>("Successfully created card", responseCard);
            //Act
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task When_Card_With_Card_Number_Exist_Should_Return_Excption()
        {
            //Arrange
            var request = _dataGenerator.Build<CardRequest>().Create();
            var command = _dataGenerator.Build<CreateCardCommand>()
                .With(x => x.Request, request)
                .Create();
            var cardExist = new Card() { CardNumber = request.CardNumber };
           _mapper.Setup(x => x.Map<Card>(It.IsAny<CardRequest>()))
                .Returns(cardExist);

            _cardRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Card>>(), CancellationToken.None))
                   .ReturnsAsync(cardExist);
            //Act
            var result = ()=> _sut.Handle(command, CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<ApiCustomException>()
                    .WithMessage("the cardNumber is already exist, provide another card number");
        }
    }
}
