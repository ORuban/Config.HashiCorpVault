using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.Token;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public interface IHashiCorpVaultClient
    {
        Task<string> GetSecretAsync(string storagePath);
    }
}