using DesktopWeeabo2.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API {
    static class APIQueries {

        private static readonly HttpClient client = new HttpClient();

        private static string SearchAnimeQuery = Resources.ResourceManager.GetString("SearchAnimeQuery");
        private static string SearchMangaQuery = Resources.ResourceManager.GetString("SearchMangaQuery");

        static async public Task<string> Search(string search, int page = 1, string sort = "", bool anime = true) =>
            await ExecuteRequest(
                new Dictionary<string, string> {
                    { "query", anime ? SearchAnimeQuery : SearchMangaQuery },
                    { "variables", String.Format("{{\"search\": \"{0}\", \"page\": {1}, \"sort\": \"{2}\"}}", search, page, sort.Length > 0 ? sort : "TITLE_ENGLISH") }
                }
            );

        static async private Task<string> ExecuteRequest(Dictionary<string, string> variables) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("https://graphql.anilist.co", new FormUrlEncodedContent(variables));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
