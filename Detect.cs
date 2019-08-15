// This sample requires C# 7.1 or later for async/await.

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// Install Newtonsoft.Json with NuGet
using Newtonsoft.Json;

namespace DetectSample
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class DetectResult
    {
        public string Language { get; set; }
        public float Score { get; set; }
        public bool IsTranslationSupported { get; set; }
        public bool IsTransliterationSupported { get; set; }
        public AltTranslations[] Alternatives { get; set; }
    }
    public class AltTranslations
    {
        public string Language { get; set; }
        public float Score { get; set; }
        public bool IsTranslationSupported { get; set; }
        public bool IsTransliterationSupported { get; set; }
    }

    class Program
    {
        private const string key_var = "TRANSLATOR_TEXT_SUBSCRIPTION_KEY";
        private static readonly string subscription_key = Environment.GetEnvironmentVariable(key_var);

        private const string endpoint_var = "TRANSLATOR_TEXT_ENDPOINT";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);

        static Program()
        {
            if (null == subscription_key)
            {
                throw new Exception("Please set/export the environment variable: " + key_var);
            }
            if (null == endpoint)
            {
                throw new Exception("Please set/export the environment variable: " + endpoint_var);
            }
        }

        // Async call to the Translator Text API
        static public async Task DetectTextRequest(string subscriptionKey, string host, string route, string inputText)
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                DetectResult[] deserializedOutput = JsonConvert.DeserializeObject<DetectResult[]>(result);
                //Iterate through the response.
                foreach (DetectResult o in deserializedOutput)
                {
                    Console.WriteLine("The detected language is '{0}'. Confidence is: {1}.\nTranslation supported: {2}.\nTransliteration supported: {3}.\n",
                        o.Language, o.Score, o.IsTranslationSupported, o.IsTransliterationSupported);
                    int counter = 0;
                    // Iterate through alternatives. Use counter for alternative number.
                    foreach (AltTranslations a in o.Alternatives)
                    {
                        counter++;
                        Console.WriteLine("Alternative {0}", counter);
                        Console.WriteLine("The detected language is '{0}'. Confidence is: {1}.\nTranslation supported: {2}.\nTransliteration supported: {3}.\n",
                            a.Language, a.Score, a.IsTranslationSupported, a.IsTransliterationSupported);
                    }
                }
            }
        }

        static async Task Main(string[] args)
        {
            // This is our main function.
            // Output languages are defined in the route.
            // For a complete list of options, see API reference.
            // https://docs.microsoft.com/azure/cognitive-services/translator/reference/v3-0-detect
            string route = "/detect?api-version=3.0";
            string breakSentenceText = @"How are you doing today? The weather is pretty pleasant. Have you been to the movies lately?";
            await DetectTextRequest(subscription_key, endpoint, route, breakSentenceText);
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
