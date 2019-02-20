using DesktopWeeabo2.Properties;
using Newtonsoft.Json.Linq;
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
		private static string AnilistSearchQuery = Resources.ResourceManager.GetString("AnilistSearchQuery");

		static async public Task<string> Search(string variableString, bool anime = true) =>
			await ExecuteRequest(
				new Dictionary<string, string> {
					{ "query", anime ? AnilistSearchQuery.Replace("{mediaTypeToReplace}", "ANIME") : AnilistSearchQuery.Replace("{mediaTypeToReplace}", "MANGA")},
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
