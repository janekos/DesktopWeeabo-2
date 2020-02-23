using DesktopWeeabo2.Core.Properties;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.API {

	public static class ApiQueries {
		private static readonly HttpClient client = new HttpClient();
		private static readonly MediaTypeWithQualityHeaderValue jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
		private static string AnilistSearchQuery = Resources.ResourceManager.GetString("AnilistSearchQuery");
		private static string GetAnimeByMALIds = Resources.ResourceManager.GetString("GetAnimeByMALIds");
		private static string GetEntriesByIds = Resources.ResourceManager.GetString("GetEntriesByIds");

		static ApiQueries() {
			if (!client.DefaultRequestHeaders.Accept.Contains(jsonHeader))
				client.DefaultRequestHeaders.Accept.Add(jsonHeader);
		}

		public static async Task<string> Search(string variableString, bool isAnime = true) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", AnilistSearchQuery.Replace("{mediaTypeToReplace}", isAnime ? "ANIME" : "MANGA") },
					{ "variables", variableString }
				}
			);

		public static async Task<string> GetByMALIds(string variableString) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", GetAnimeByMALIds},
					{ "variables", variableString }
				}
			);

		public static async Task<string> GetByIds(string variableString, bool anime = true) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", GetEntriesByIds.Replace("{typeToQuery}", anime ? "ANIME" : "MANGA") },
					{ "variables", variableString }
				}
			);

		public static async Task<string> ExecuteRequest(Dictionary<string, string> variables) {
			HttpResponseMessage response = await client.PostAsync("https://graphql.anilist.co", new FormUrlEncodedContent(variables));
			return await response.Content.ReadAsStringAsync();
		}
	}
}