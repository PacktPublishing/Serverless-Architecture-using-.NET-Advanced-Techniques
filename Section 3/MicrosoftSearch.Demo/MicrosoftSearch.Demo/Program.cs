using System;
using System.IO;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using MicrosoftSearch.Demo.Models;
using Newtonsoft.Json;

namespace MicrosoftSearch.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = CreateSearchServiceClient(out var configuration);


//            var definition = new Index()
//            {
//                Name = "movies",
//                Fields = FieldBuilder.BuildForType<Movie>()
//            };
//
//           client.Indexes.Create(definition);


//            PopulateData(client);
//
            SearchData(client);
        }

        private static void SearchData(SearchServiceClient client)
        {
            var indexClient = client.Indexes.GetClient("movies");

            var results = indexClient.Documents.Search<Movie>("*", new SearchParameters()
            {
               // Filter = "year gt 2000"
            });

            foreach (var result in results.Results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result.Document));
            }
        }

        private static void PopulateData(SearchServiceClient client)
        {
            var indexClient = client.Indexes.GetClient("movies");

            var actions = new IndexAction<Movie>[]
            {
                IndexAction.Upload(new Movie()
                {
                    MovieId = "1",
                    Description = "Classic",
                    Name = "The GodFather",
                    Year = 1972
                }),

                IndexAction.Upload(new Movie()
                {
                    MovieId = "2",
                    Description = "Comedy",
                    Name = "Bruce Almighty",
                    Year = 2003
                }),

                IndexAction.Upload(new Movie()
                {
                    MovieId = "3",
                    Description = "Thriller",
                    Name = "Unstoppable",
                    Year = 2010
                })
            };

            var batch = IndexBatch.New(actions);

            try
            {
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static SearchServiceClient CreateSearchServiceClient(out IConfigurationRoot configuration)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var searchServiceName = configuration["SearchServiceName"];
            var apiKey = configuration["SearchServiceAPIKey"];


            var searchServiceClient = new SearchServiceClient(searchServiceName,
                new SearchCredentials(apiKey));

            return searchServiceClient;
        }
    }
}