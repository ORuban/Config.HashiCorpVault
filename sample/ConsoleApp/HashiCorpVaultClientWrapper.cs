using System;
using System.Linq;
using System.Threading.Tasks;
using ORuban.Extensions.Configuration.HashiCorpVault;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.Token;

namespace ConsoleApp
{
    public class HashiCorpVaultClientWrapper : IHashiCorpVaultClient
    {
        private IVaultClient _vaultClientImplementation;

        public HashiCorpVaultClientWrapper(string vaultAddressWithPort, string token)
        {
            IAuthenticationInfo authenticationInfo = new TokenAuthenticationInfo(token);
            _vaultClientImplementation = VaultClientFactory.CreateVaultClient(new Uri(vaultAddressWithPort), authenticationInfo);
        }

        public async Task<string> GetSecretAsync(string path)
        {
            var rawData = await _vaultClientImplementation.ReadSecretAsync(path);
            return rawData.Data.Values.First().ToString();
        }
    }
}