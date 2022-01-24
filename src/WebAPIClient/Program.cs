using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            ConfigureClient();
            // await ProcessAnswerAsStringAndSaveInFile();
            var repositories = await DeserializeJsonResult();

            foreach (var repository in repositories)
            {
                Console.WriteLine(repository.Name);
                Console.WriteLine(repository.Description);
                Console.WriteLine(repository.GitHubHomeUrl);
                Console.WriteLine(repository.Homepage);
                Console.WriteLine(repository.Watchers);
                Console.WriteLine(repository.LastPush);
                Console.WriteLine();
            }
                
        }

        private static void ConfigureClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        private static async Task ProcessAnswerAsStringAndSaveInFile()
        {
            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            Console.WriteLine(Directory.GetCurrentDirectory());

            var msg = await stringTask;
            File.WriteAllText($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}githubAnswer.json", msg);
            Console.ReadLine();
            Console.Write(msg);
        }

        private static async Task<List<Repository>> DeserializeJsonResult()
        {
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(
                await streamTask, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return repositories;
        }
    }
}