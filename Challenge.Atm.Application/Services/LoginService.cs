using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Services
{

    public class LoginService : ILoginService
    {
        private readonly IJwtService _jwtService;
        private readonly IReadRepositoryAsync<Card> _readCardRepository;
        private readonly IRepositoryAsync<Card> _cardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        private int failedAttempts;
        private const int FailedDuration = 2;

        public LoginService(IJwtService jwtService, IReadRepositoryAsync<Card> readCardRepository, IMemoryCache cache, IRepositoryAsync<Card> cardRepository, IHttpContextAccessor httpContextAccessor)
        {
            _jwtService = jwtService;
            _readCardRepository = readCardRepository;
            _cardRepository = cardRepository;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> Login(Card card)
        {

            var result = await _readCardRepository.FirstOrDefaultAsync(new CardSpecification(card.CardNumber!));
            if (result == null)
            {
                throw new ApiCustomException("The card with that number does not exist", HttpStatusCode.NotFound);
            }

            if (result.IsBlocked)
            {
                throw new ApiCustomException("The card is Blocked", HttpStatusCode.Locked);
            }
            failedAttempts = _cache.Get<int>(card.CardNumber);
            if (failedAttempts >= 4)
            {
                result.IsBlocked = true;
                await _cardRepository.UpdateAsync(result);
                throw new ApiCustomException("The card has been blocked, please contact your administrator", HttpStatusCode.Locked);
            }
            if (result.Pin != card.Pin)
            {
                failedAttempts++;
                _cache.Set(card.CardNumber, failedAttempts, DateTime.Now.AddMinutes(FailedDuration));
                throw new ApiCustomException("Incorrect Pin", HttpStatusCode.Forbidden);
            }

            return _jwtService.GenerateToken(result);
        }


        public (string cardNumber, string OwnerName) ValidateCard() =>
           _jwtService.ValidateToken(_httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last());
  
    }
}
