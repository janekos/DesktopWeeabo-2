using DesktopWeeabo2.Properties;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API {
	static class APIQueries {
		private static readonly HttpClient client = new HttpClient();
		private static string AnilistSearchQuery = Resources.ResourceManager.GetString("AnilistSearchQuery");
		private static string GetAnimeByMALIds = Resources.ResourceManager.GetString("GetAnimeByMALIds");

		static async public Task<string> Search(string variableString, bool anime = true) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", anime ? AnilistSearchQuery.Replace("{mediaTypeToReplace}", "ANIME") : AnilistSearchQuery.Replace("{mediaTypeToReplace}", "MANGA")},
					{ "variables", variableString }
				}
			);

		static async public Task<string> GetByMALIds(string variableString) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", GetAnimeByMALIds},
					{ "variables", variableString }
				}
			);

		static async private Task<string> ExecuteRequest(Dictionary<string, string> variables) {
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var a = new FormUrlEncodedContent(variables);
			HttpResponseMessage response = await client.PostAsync("https://graphql.anilist.co", new FormUrlEncodedContent(variables));
			return await response.Content.ReadAsStringAsync();
		}
	}
}
