using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GetLanguages
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Languages API.
    /// </summary>
    public class LanguageResponse
    {
        [JsonProperty("translation")]
        public Dictionary<string, LanguageTranslation> Translations { get; set; }
        [JsonProperty("transliteration")]
        public Dictionary<string, LanguageTransliteration> Transliterations { get; set; }
        [JsonProperty("dictionary")]
        public Dictionary<string, LanguageDictionary> Dictionaries { get; set; }
    }

    public class LanguageTranslation
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
    }

    public class LanguageTransliteration
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public TransliterationScript[] Scripts { get; set; }
    }

    public class TransliterationScript
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
        public TransliterationToScript[] ToScripts { get; set; }
    }

    public class TransliterationToScript
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
    }

    public class LanguageDictionary
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
        public IEnumerable<DictionaryTranslation> Translations { get; set; }
    }

    public class DictionaryTranslation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
    }

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
                // Serialize json
                var languageResponse = JsonConvert.DeserializeObject<LanguageResponse>(jsonResponse);
                // Print the response
                foreach (var key in languageResponse.Translations.Keys)
                {
                    Console.WriteLine("Translation " + key + ":");
                    var item = languageResponse.Translations[key];
                    Console.WriteLine(item.Name + ":" + item.NativeName);
                }

                foreach (var key in languageResponse.Transliterations.Keys)
                {
                    Console.WriteLine("Transliteration " + key + ":");
                    var item = languageResponse.Transliterations[key];
                    Console.WriteLine(item.Name + ":" + item.NativeName);
                }

                foreach (var key in languageResponse.Dictionaries.Keys)
                {
                    Console.WriteLine("Dictionary " + key + ":");
                    var item = languageResponse.Dictionaries[key];
                    Console.WriteLine(item.Name + ":" + item.NativeName);
                }
                Console.WriteLine("Press any key to continue.");
            }
        }
        static void Main(string[] args)
        {
            GetLanguages();
            Console.ReadKey();
        }
    }
}
