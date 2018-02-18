using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public interface IHashiCorpVaultClient
    {
        Task<string> GetSecretAsync(string storagePath);
    }
}