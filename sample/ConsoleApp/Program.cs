using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ORuban.Extensions.Configuration.HashiCorpVault;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //start Vault Server in dev mode
            // ./vault server -dev

            string vaultAddressWithPort = "http://127.0.0.1:8200";
            string token = "75a244db-9164-4130-db60-50c7081f50ba";

            Console.WriteLine("Creating Vault client");
            Console.WriteLine($"Vault Address: {vaultAddressWithPort}");
            Console.WriteLine($"Auth Token: {token}");

            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(token);
            var vaultClientSettings = new VaultClientSettings(vaultAddressWithPort, authMethod);
            var vaultClient = new VaultClient(vaultClientSettings);

            Console.WriteLine("Writing data to Vault...");
            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("secret/keyA", new Dictionary<string, object>
            {
                {"prop1", "valueA"}
            });

            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("secret/keyB", new Dictionary<string, object>
            {
                {"prop1", "valueB"}
            });

            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("secret/group1/keyC", new Dictionary<string, object>
            {
                {"prop1", "valueC"}
            });

            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("secret/group2/keyD", new Dictionary<string, object>
            {
                {"prop1", "valueD"}
            });

            var client = new HashiCorpVaultClientWrapper(vaultAddressWithPort, token);

            var configBuilder =
                new ConfigurationBuilder()
                    .AddHashiCorpVault(client, "secret", new[] {"keyA", "keyB", "group1/keyC"})
                    .AddHashiCorpVault(client, "secret/group2", new[] {"keyD"});

            Console.WriteLine("Building configuration...");

            IConfiguration config = configBuilder.Build();

            Console.WriteLine("The following settings populated:");

            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine(item);
            }
        }
    }
}
