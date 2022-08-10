using System;
using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Identity.UserClaim
{
    public interface IUserClaimService
    {
        Task<IFinalResult> GeyUserClaimsAsync(Guid userId, string authenticationMethod , string appCode);
    }
}
