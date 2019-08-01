using DesktopWeeabo2.Models;
using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesktopWeeabo2.Helpers {
	public static class StringHelpers {
		public static string CleanDescription(string desc) => Regex.Replace(Regex.Replace(desc, @"<[^>]*>", ""), @"\t|\n|\r", "");

		public static string GetFirstNotNullItemTitle(BaseModel item) =>
			!string.IsNullOrWhiteSpace(item.TitleEnglish)
				? item.TitleEnglish
				: (!string.IsNullOrWhiteSpace(item.TitleRomaji)
					? item.TitleRomaji
					: (!string.IsNullOrWhiteSpace(item.TitleNative)
						? item.TitleNative
						: "No title present"));

		public static string GenreHelper(GenreObject[] genreList) {
			string genreString = string.Join(", ", genreList.Where(g => g.IsSelected).Select(g => g.Name).ToArray());
			return genreString.Length > 0 ? genreString : "All";
		}

		public static string APISearchVariables(string search = "", int page = 1, string sort = "TITLE_ROMAJI", string[] genres = null, bool isAdult = true) {
			var json = new JObject(
						new JProperty("page", page),
						new JProperty("sort", sort)
			);
			if (!isAdult) json.Add(new JProperty("isAdult", false));
			if (search.Length > 0) json.Add(new JProperty("search", search));
			if (genres.Length > 0) json.Add(new JProperty("genres", new JArray(genres)));
			return json.ToString(Formatting.None);
		}

		public static string APIGetByMalIdsVariables(int[] malIds) =>
			new JObject(
				new JProperty("page", 1),
				new JProperty("malIds", malIds)
			).ToString(Formatting.None);


		public static string PrependAdditionalUrls(ExternalLink[] additionalUrls, JArray urls) {
			for (int i = 0; i < additionalUrls.Length; i++) {
				urls.Insert(0, new JObject(
					new JProperty("site", additionalUrls[i].Site),
					new JProperty("url", additionalUrls[i].Url)
				));
			}
			return urls.ToString(Formatting.None);
		}
	}
}
