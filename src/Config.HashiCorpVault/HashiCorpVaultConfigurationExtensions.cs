using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public static class HashiCorpVaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddHashiCorpVault(
            this IConfigurationBuilder configurationBuilder,
            string vaultAddressWithPort,
            string token,
            string prefix,
            IEnumerable<string> secrets)
        {
            if (string.IsNullOrWhiteSpace(vaultAddressWithPort))
            {
                throw new ArgumentException("Vault Address is required", nameof(vaultAddressWithPort));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Auth token is required", nameof(token));
            }

            var client = new HashiCorpVaultClientWrapper(vaultAddressWithPort, token);

            return AddHashiCorpVault(configurationBuilder, client, prefix, secrets);
        }

        public static IConfigurationBuilder AddHashiCorpVault(
            this IConfigurationBuilder configurationBuilder,
            string vaultAddressWithPort,
            string roleId, 
            string secretId,
            string prefix,
            IEnumerable<string> secrets)
        {
            if (string.IsNullOrWhiteSpace(vaultAddressWithPort))
            {
                throw new ArgumentException("Vault Address is required", nameof(vaultAddressWithPort));
            }

            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentException("Role Id is required", nameof(roleId));
            }

            var client = new HashiCorpVaultClientWrapper(vaultAddressWithPort, roleId, secretId);

            return AddHashiCorpVault(configurationBuilder, client, prefix, secrets);
        }

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