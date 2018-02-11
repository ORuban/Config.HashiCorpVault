using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public static class HashiCorpVaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddHashiCorpVault(
            this IConfigurationBuilder configurationBuilder,
            string vaultUri,
            string token,
            string Prefix,
            IEnumerable<string> secrets)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (string.IsNullOrWhiteSpace(vaultUri))
            {
                throw new ArgumentException("Vault URI is required", nameof(vaultUri));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Auth token is required", nameof(token));
            }

            if (secrets == null)
            {
                throw new ArgumentNullException(nameof(secrets));
            }

            configurationBuilder.Add(new HashiCorpVaultConfigurationSource()
            {
                Client = new HashiCorpVaultClientWrapper(vaultUri, token),
                Prefix = Prefix,
                Secrets = secrets
            });

            return configurationBuilder;
        }
    }
}