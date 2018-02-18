using System;
using System.Linq;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.AppRole;
using VaultSharp.Backends.Authentication.Models.Token;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public class HashiCorpVaultClientWrapper : IHashiCorpVaultClient
    {
        private IVaultClient _vaultClientImplementation;

        public HashiCorpVaultClientWrapper(string vaultAddressWithPort, string token)
        {
            IAuthenticationInfo authenticationInfo = new TokenAuthenticationInfo(token);
            _vaultClientImplementation = VaultClientFactory.CreateVaultClient(new Uri(vaultAddressWithPort), authenticationInfo);
        }

        public HashiCorpVaultClientWrapper(string vaultAddressWithPort, string roleId, string secretId = null)
        {
            IAuthenticationInfo authenticationInfo = new AppRoleAuthenticationInfo(roleId, secretId);
            _vaultClientImplementation = VaultClientFactory.CreateVaultClient(new Uri(vaultAddressWithPort), authenticationInfo);
        }

        public async Task<string> GetSecretAsync(string path)
        {
            var rawData = await _vaultClientImplementation.ReadSecretAsync(path);
            return rawData.Data.Values.First().ToString();
        }
    }
}