using Domain.DTO.Identity.Token;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class SessionStorage : ISessionStorage
    {
        private readonly HttpContext _context;
        protected TokenClaimDto ClaimData { get; set; }
        public SessionStorage()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            _context = httpContextAccessor.HttpContext;
            SetClaims(_context);
            
        }

        public void SetClaims(HttpContext context)
        {
            
            var claims = context?.User;
            ClaimData = new TokenClaimDto()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,
                Email = claims?.FindFirst(t => t.Type == "Email")?.Value
            };
        }
        public long UserId => ClaimData.UserId != null ? long.Parse(ClaimData.UserId) : 0;

        public string Token => _context.Request.Headers["Authorization"];

    }
}
