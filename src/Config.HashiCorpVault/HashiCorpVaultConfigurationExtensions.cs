using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public static class HashiCorpVaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddHashiCorpVault(
            this IConfigurationBuilder configurationBuilder,
            IHashiCorpVaultClient client,
            string prefix,
            IEnumerable<string> secrets)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (secrets == null)
            {
                throw new ArgumentNullException(nameof(secrets));
            }

            configurationBuilder.Add(new HashiCorpVaultConfigurationSource()
            {
                Client = client,
                Prefix = prefix,
                Secrets = secrets
            });

            return configurationBuilder;
        }
    }
}