using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public class HashiCorpVaultConfigurationSource : IConfigurationSource
    {
        public IHashiCorpVaultClient Client { get; set; }
        public string Prefix { get; set; }

        public IEnumerable<string> Secrets { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new HashiCorpConfigurationProvider(Client, Prefix, Secrets);
        }
    }
}