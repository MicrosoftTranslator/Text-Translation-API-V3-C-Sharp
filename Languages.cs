using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GetLanguages
{
    class Program
    {
        static void GetLanguages()
        {
            string host = "https://api.cognitive.microsofttranslator.com";
            string route = "/languages?api-version=3.0";
            string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Set the method to GET
                request.Method = HttpMethod.Get;
                // Construct the full URI
                request.RequestUri = new Uri(host + route);
                // Add the authorization header
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
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
