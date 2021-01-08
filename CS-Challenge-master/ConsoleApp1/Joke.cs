using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace JokeGenerator
{
    public class Joke
    {

        [JsonProperty("value")]
        public string joke { get; set; }

        private static HttpClient _client = new HttpClient();
        private static string _baseUrl = "https://api.chucknorris.io/";
        /// <summary>
        /// Calls an external API to get a random joke or one specifically from a category if supplied
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <returns>Returns a joke</returns>
        public static Joke GetRandom(string category = null, Name name = null)
        {
            try
            {
                //append category to the end of url if category was supplied
                string categoryUrl = string.Empty;
                if (!string.IsNullOrEmpty(category))
                {
                    categoryUrl = $"?category={category}";
                }
                //Calls the baseurl API and returns the result in a Joke object
                Joke chuckNorrisJoke = JsonConvert.DeserializeObject<Joke>(_client.GetStringAsync($"{_baseUrl}jokes/random{categoryUrl}").Result);

                if (name != null && !string.IsNullOrEmpty(name.firstName) && !string.IsNullOrEmpty(name.lastName))
                {
                    string chuckNorris = "Chuck Norris";
                    int index = chuckNorrisJoke.joke.IndexOf(chuckNorris);
                    //Finds the first instance of Chuck Norris and replaces it with a random name
                    chuckNorrisJoke.joke = $"{ chuckNorrisJoke.joke.Substring(0, index)}{name.firstName} {name.lastName}{chuckNorrisJoke.joke.Substring(index + chuckNorris.Length)}";
                }
                return chuckNorrisJoke;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving joke");
                return null;
            }

        }
    }
}
