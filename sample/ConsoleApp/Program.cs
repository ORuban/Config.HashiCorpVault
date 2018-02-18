using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.Token;
using ORuban.Extensions.Configuration.HashiCorpVault;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //start Vault Server in dev mode
            // ./vault server -dev

            string vaultAddressWithPort = "http://127.0.0.1:8200";
            string token = "75a244db-9164-4130-db60-50c7081f50ba";

            System.Console.WriteLine("Creating Vault client");
            System.Console.WriteLine($"Vault Address: {vaultAddressWithPort}");
            System.Console.WriteLine($"Auth Token: {token}");

            IAuthenticationInfo tokenAuthenticationInfo = new TokenAuthenticationInfo(token);
            var vaultClient = VaultClientFactory.CreateVaultClient(new Uri(vaultAddressWithPort), tokenAuthenticationInfo);

            System.Console.WriteLine("Writing data to Vault...");
            var resulA = vaultClient.WriteSecretAsync("secret/keyA", new Dictionary<string, object>(){
                {"prop1", "valueA"}
            }).Result;

            var resultB = vaultClient.WriteSecretAsync("secret/keyB", new Dictionary<string, object>(){
                {"prop1", "valueB"}
            }).Result;

            var resultC = vaultClient.WriteSecretAsync("secret/group1/keyC", new Dictionary<string, object>(){
                {"prop1", "valueC"}
            }).Result;

            var resultD = vaultClient.WriteSecretAsync("secret/group2/keyD", new Dictionary<string, object>(){
                {"prop1", "valueD"}
            }).Result;

             var client = new HashiCorpVaultClientWrapper(vaultAddressWithPort, token);

            IConfigurationBuilder configBuilder =
                new ConfigurationBuilder()
                .AddHashiCorpVault(client, "secret", new[] { "keyA", "keyB", "group1/keyC" })
                .AddHashiCorpVault(client, "secret/group2", new[] { "keyD" });

            System.Console.WriteLine("Building configuration...");

            IConfiguration config = configBuilder.Build();

            System.Console.WriteLine("The following settings populated:");

            foreach (var item in config.AsEnumerable())
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
