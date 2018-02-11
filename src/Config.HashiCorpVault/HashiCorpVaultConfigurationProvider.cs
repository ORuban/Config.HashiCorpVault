using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ORuban.Extensions.Configuration.HashiCorpVault
{
    public class HashiCorpConfigurationProvider : ConfigurationProvider
    {
        private readonly string _basePath;
        private readonly IHashiCorpVaultClient _client;
        private readonly IEnumerable<string> _secrets;

        public HashiCorpConfigurationProvider(IHashiCorpVaultClient client, string keyPrefix, IEnumerable<string> secrets)
        {
            _client = client;
            _basePath = GetBasePath(keyPrefix);
            _secrets = secrets;
        }

        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);


            foreach (var secretItem in _secrets)
            {
                if (string.IsNullOrWhiteSpace(secretItem))
                {
                    continue;
                }

                var key = secretItem.Replace('/', ':');
                var value = await _client.GetSecretAsync($"{_basePath}{secretItem}").ConfigureAwait(false);

                data.Add(key, value);
            }

            Data = data;
        }

        private string GetBasePath(string keyPrefix)
        {
            if (string.IsNullOrWhiteSpace(keyPrefix))
            {
                return "";
            }

            if (keyPrefix.EndsWith(@"/")) return keyPrefix;

            return $"{keyPrefix}/";
        }
    }
}