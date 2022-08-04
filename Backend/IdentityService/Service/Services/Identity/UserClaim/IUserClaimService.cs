using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Identity.UserClaim
{
    public interface IUserClaimService
    {
        Task<IResult> GeyUserClaimsAsync(long userId, string authenticationMethod , string appCode);
    }
}
