using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using DesktopWeeabo2.Properties;

namespace DesktopWeeabo2.data {
    static class APIQueries {

        private static readonly HttpClient client = new HttpClient();

        private static string searchAnimeQuery = Resources.ResourceManager.GetString("SearchAnimeQuery");
        private static string searchMangaQuery = Resources.ResourceManager.GetString("SearchMangaQuery");

        static async public Task<string> search(string search, int page = 1, string sort = "", bool anime = true) => 
            await executeRequest(
                new Dictionary<string, string> {
                    { "query", anime ? searchAnimeQuery : searchMangaQuery },
                    { "variables", String.Format("{{\"search\": \"{0}\", \"page\": {1}, \"sort\": \"{2}\"}}", search, page, sort.Length > 0 ? sort : "TITLE_ENGLISH") }
                }
            );

        static async private Task<string> executeRequest(Dictionary<string, string> variables) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("https://graphql.anilist.co", new FormUrlEncodedContent(variables));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
