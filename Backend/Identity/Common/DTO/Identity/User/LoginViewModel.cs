using System.Collections.Generic;
using System.Linq;

namespace Common.DTO.Identity.User
{
    public class LoginViewModel : AuthenticationDto
    {
        public bool AllowRememberLogin { get; set; } = true;
        public bool NewAccount { get; set; }
        public bool EnableLocalLogin { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders?.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
