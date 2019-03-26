﻿using System;
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
            bool success = false;
            string movieJson;
            ResponseJSON resp;
            do
            {
                movieJson = GetJson().Result;
                resp = JsonConvert.DeserializeObject<ResponseJSON>(movieJson);
                if (resp.Response == "True")
                {
                    success = true;
                }
            } while (!success);

            return resp.Title;
        }
        private static async Task<string> GetJson()
        {
            Random rng = new Random();
            string baseUrl = "http://www.omdbapi.com/?apikey=351da02a&i=tt";
            string url;
            url = baseUrl;
            for (int i = 0; i < 7; i++)
            {
                url += rng.Next(0, 10);
            }
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
        public class Rating
        {
            public string Source { get; set; }
            public string Value { get; set; }
        }

        public class ResponseJSON
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string Rated { get; set; }
            public string Released { get; set; }
            public string Runtime { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
            public string Writer { get; set; }
            public string Actors { get; set; }
            public string Plot { get; set; }
            public string Language { get; set; }
            public string Country { get; set; }
            public string Awards { get; set; }
            public string Poster { get; set; }
            public List<Rating> Ratings { get; set; }
            public string Metascore { get; set; }
            public string imdbRating { get; set; }
            public string imdbVotes { get; set; }
            public string imdbID { get; set; }
            public string Type { get; set; }
            public string DVD { get; set; }
            public string BoxOffice { get; set; }
            public string Production { get; set; }
            public string Website { get; set; }
            public string Response { get; set; }
        }
    }
}
