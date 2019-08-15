using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GetLanguages
{
    class Program
    {
        private const string endpoint_var = "TRANSLATOR_TEXT_ENDPOINT";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);

        static Program()
        {
            if (null == endpoint)
            {
                throw new Exception("Please set/export the environment variable: " + endpoint_var);
            }
        }

        static void GetLanguages()
        {
            string route = "/languages?api-version=3.0";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Set the method to GET
                request.Method = HttpMethod.Get;
                // Construct the full URI
                request.RequestUri = new Uri(endpoint + route);
                // Send request, get response
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                // Print the response
                Console.WriteLine(jsonResponse);
                Console.WriteLine("Press any key to continue.");
            }
        }
        static void Main(string[] args)
        {
            GetLanguages();
            Console.ReadLine();

        }
    }
}
