// This sample requires C# 7.1 or later for async/await.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// Install Newtonsoft.Json with NuGet
using Newtonsoft.Json;

namespace TranslateTextSample
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public TextResult SourceText { get; set; }
        public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }
        public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
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
        static public async Task TranslateTextRequest(string subscriptionKey, string host, string route, string inputText)
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
                TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                // Iterate over the deserialized results.
                foreach (TranslationResult o in deserializedOutput)
                {
                    // Print the detected input languge and confidence score.
                    Console.WriteLine("Detected input language: {0}\nConfidence score: {1}\n", o.DetectedLanguage.Language, o.DetectedLanguage.Score);
                    // Iterate over the results and print each translation.
                    foreach (Translation t in o.Translations)
                    {
                        Console.WriteLine("Translated to {0}: {1}", t.To, t.Text);
                    }
                }
            }
        }

        static async Task Main(string[] args)
        {
            // This is our main function.
            // Output languages are defined in the route.
            // For a complete list of options, see API reference.
            // https://docs.microsoft.com/azure/cognitive-services/translator/reference/v3-0-translate
            string route = "/translate?api-version=3.0&to=de&to=it&to=ja&to=th";
            // Prompts you for text to translate. If you'd prefer, you can
            // provide a string as textToTranslate.
            Console.Write("Type the phrase you'd like to translate? ");
            string textToTranslate = Console.ReadLine();
            await TranslateTextRequest(subscription_key, endpoint, route, textToTranslate);
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
