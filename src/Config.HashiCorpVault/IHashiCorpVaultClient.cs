using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public interface IHashiCorpVaultClient
    {
        Task<string> GetSecretAsync(string storagePath);
        Task<IDictionary<string, object>> GetSecretsAsync(string storagePath);
    }
}