using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace AltTranslation
{
    class Program
    {
        static void AltTranslation()
        {
            string host = "https://api.cognitive.microsofttranslator.com";
            string route = "/dictionary/lookup?api-version=3.0&from=en&to=es";
            string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";

            System.Object[] body = new System.Object[] { new { Text = @"Elephants" } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Set the method to POST
                request.Method = HttpMethod.Post;
                // Construct the full URI
                request.RequestUri = new Uri(host + route);
                // Add the serialized JSON object to your request
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
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
            AltTranslation();
            Console.ReadLine();

        }
    }
}
