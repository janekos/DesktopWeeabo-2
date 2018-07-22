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
    static class APIQueries
    {

        private static readonly HttpClient client = new HttpClient();

        private static string searchAnimeQuery = @"query ($id: Int, $page: Int, $search: String) {
                                                      Page(page: $page, perPage: 10000) {
                                                        pageInfo { hasNextPage}
                                                        media(id: $id, type: ANIME, search: $search) {
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

        private static string searchMangaQuery = @"query ($id: Int, $page: Int, $search: String) {
                                                      Page(page: $page, perPage: 10000) {
                                                        pageInfo { hasNextPage}
                                                        media(id: $id, type: MANGA, search: $search) {
                                                            id 
                                                            title { 
                                                                english 
                                                                romaji
                                                                native
                                                            }
                                                            synonyms 
                                                            genres 
                                                            meanScore
                                                            type
                                                            format 
                                                            status 
                                                            description(asHtml: false) 
                                                            volumes
                                                            chapters
                                                            coverImage{ large }
                                                        }
                                                      }
                                                    }";

        static async public Task<string> QueryAPI(string search, int page, bool anime)
        {

            string variables = "{\"search\": \""+search+"\",\"page\": "+page+"}";
            string query = "";

            if (anime) { query = searchAnimeQuery; }
            else { query = searchMangaQuery; }

            Dictionary<string, string> values = new Dictionary<string, string>
            {
               { "query", query },
               { "variables", variables }
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("https://graphql.anilist.co", content);
            
            return await response.Content.ReadAsStringAsync();
        }

        static async public void QueryLocal()
        {

        }
    }
}
