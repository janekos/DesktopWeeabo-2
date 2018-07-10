using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace DesktopWeeabo2.data
{
    static class Queries
    {

        private static readonly HttpClient client = new HttpClient();

        static async public Task<string> QueryAPI()
        {
            string query = @"query ($id: Int, $page: Int, $search: String) {
                              Page(page: $page, perPage: 10000) {
                                pageInfo { hasNextPage}
                                media(id: $id, type: ANIME, search: $search, sort: START_DATE) {
                                    id 
                                    title { 
                                        english 
                                        romaji
                                        native
                                    }
                                    synonyms 
                                    genres 
                                    meanScore
                                    format 
                                    status 
                                    description(asHtml: false) 
                                    startDate{
                                        year
                                        month
                                        day
                                    }
                                    endDate{
                                        year
                                        month
                                        day
                                    }
                                    episodes 
                                    duration 
                                    coverImage{ large }
                                }
                              }
                           }";

            string variables = "{\"search\": \"oreimo\",\"page\": 1}";

            var values = new Dictionary<string, string>
            {
               { "query", query },
               { "variables", variables }
            };

            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync("https://graphql.anilist.co", content);
            
            return await response.Content.ReadAsStringAsync();

        }

        static async public void QueryLocal()
        {

        }
    }
}
