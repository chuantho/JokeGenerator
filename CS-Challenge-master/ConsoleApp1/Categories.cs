using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace JokeGenerator
{
    public class Categories
    {
        public Dictionary<int, string> categories { get; set; }

        private static HttpClient _client = new HttpClient();
        private static string _baseUrl = "https://api.chucknorris.io/";

        public Categories(Dictionary<int, string> categories)
        {
            this.categories = categories;
        }

        /// <summary>
        /// Creates a dictionary of categories
        /// </summary>
        /// <returns> Returns a category object</returns>
        public static Categories GetCategories()
        {
            try
            {
                List<string> categoryList = JsonConvert.DeserializeObject<List<string>>(_client.GetStringAsync($"{_baseUrl}jokes/categories").Result);
                Dictionary<int, string> categoryDict = categoryList.Select((category, index) => new { index, category }).ToDictionary(x => x.index, x => x.category);
                return new Categories(categoryDict);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error retrieving categories");
                return null;
            }
        }
    }
}
