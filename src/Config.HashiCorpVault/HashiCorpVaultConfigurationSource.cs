using System;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public class HashiCorpVaultConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new HashiCorpConfigurationProvider();
        }
    }
}
