using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace JokeGenerator
{
    public class Name 
    {
        [JsonProperty("name")]
        public string firstName { get; set; }
        [JsonProperty("surname")]
        public string lastName { get; set; }
        public string gender { get; set; }
        public string region { get; set; }

        private static HttpClient _client = new HttpClient();
        private static string _baseUrl = "https://www.names.privserv.com/api/";

        /// <summary>
        /// Calls an external API that retrieves a random name
        /// </summary>
        /// <returns>A random name</returns>
        public static Name GetName()
        {
            try
            {
                return JsonConvert.DeserializeObject<Name>(_client.GetStringAsync(_baseUrl).Result);

            }catch(Exception ex)
            {
                Console.WriteLine("Error retrieving name");
                return null;
            }
        }
    }
}
