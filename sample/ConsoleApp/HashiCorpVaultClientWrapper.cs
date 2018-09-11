using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ORuban.Extensions.Configuration.HashiCorpVault;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace ConsoleApp
{
    public class HashiCorpVaultClientWrapper : IHashiCorpVaultClient
    {
        private readonly IVaultClient _vaultClientImplementation;

        public HashiCorpVaultClientWrapper(string vaultAddressWithPort, string token)
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(token);
            var vaultClientSettings = new VaultClientSettings(vaultAddressWithPort, authMethod);
            _vaultClientImplementation = new VaultClient(vaultClientSettings);
        }

        public async Task<string> GetSecretAsync(string path)
        {
            var rawData = await _vaultClientImplementation.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);
            return rawData.Data.Data.First().Value.ToString();
        }

        public async Task<IDictionary<string, object>> GetSecretsAsync(string path)
        {
            var rawData = await _vaultClientImplementation.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);
            return rawData.Data.Data;
        }
    }
}