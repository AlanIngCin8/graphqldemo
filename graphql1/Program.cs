using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var graphQLClient = new GraphQLHttpClient("https://swapi-graphql.netlify.app/.netlify/functions/index", new NewtonsoftJsonSerializer());

        var query = new GraphQLRequest
        {
            Query = @"
            {
                allPeople {
                    people {
                        name
                    }
                }
            }"
        };

        var response = await graphQLClient.SendQueryAsync<GraphQLRequest>(query);
        Console.WriteLine(response.Data["allPeople"]);

        JObject allPeople = (JObject)(response.Data["allPeople"]);
        JToken people = allPeople["people"];
        foreach (var person in people)
        {
            Console.WriteLine(person["name"]);
        }
    }
}
