using System;
using System.Linq;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.Token;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public class HashiCorpVaultClientWrapper : IHashiCorpVaultClient
    {
        private IVaultClient _vaultClientImplementation;

        public HashiCorpVaultClientWrapper(string uri, string token)
        {
            IAuthenticationInfo tokenAuthenticationInfo = new TokenAuthenticationInfo(token);
            _vaultClientImplementation = VaultClientFactory.CreateVaultClient(new Uri(uri), tokenAuthenticationInfo);
        }


        public async Task<string> GetSecretAsync(string path)
        {
            var rawData = await _vaultClientImplementation.ReadSecretAsync(path);
            return rawData.Data.Values.First().ToString();
        }
    }
}