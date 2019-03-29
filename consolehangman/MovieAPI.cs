using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace consolehangman
{
    class MovieAPI
    {
        public static string GetWord()
        {
            Random rng = new Random();
            bool success = false;
            string movieJson;
            RootObject resp;
            movieJson = GetJson().Result;
            resp = JsonConvert.DeserializeObject<RootObject>(movieJson);
            string Title = resp.results[rng.Next(0, 20)].title;
            return Title;
        }
        private static async Task<string> GetJson()
        {
            Random rng = new Random();
            int page = rng.Next(0, 1000);
            string url = String.Concat("https://api.themoviedb.org/3/discover/movie?api_key=03448c1c3b3d463ef0d0d2f32bc8ebe0&language=en-US&page=", page);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    return strResult;
                }
                else
                {
                    return null;
                }
            }

        }
        public class Result
        {
            public int vote_count { get; set; }
            public int id { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public string title { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public List<int> genre_ids { get; set; }
            public string backdrop_path { get; set; }
            public bool adult { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; }
        }

        public class RootObject
        {
            public int page { get; set; }
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public List<Result> results { get; set; }
        }
    }
}
