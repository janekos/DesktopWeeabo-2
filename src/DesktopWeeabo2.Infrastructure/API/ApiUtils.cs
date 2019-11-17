using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesktopWeeabo2.Infrastructure.API {

	public static class ApiUtils {

		public static string APISearchVariables(string search = "", int page = 1, string sort = "TITLE_ROMAJI", string[] genres = null, bool isAdult = true) {
			var json = new JObject(
						new JProperty("page", page),
						new JProperty("sort", sort)
			);
			if (!isAdult)
				json.Add(new JProperty("isAdult", false));
			if (search.Length > 0)
				json.Add(new JProperty("search", search));
			if (genres.Length > 0)
				json.Add(new JProperty("genres", new JArray(genres)));
			return json.ToString(Formatting.None);
		}

		public static string APIGetByMalIdsVariables(int[] malIds) =>
			new JObject(
				new JProperty("page", 1),
				new JProperty("malIds", malIds)
			).ToString(Formatting.None);

		public static string APIGetByIdsVariables(int[] ids) =>
			new JObject(
				new JProperty("page", 1),
				new JProperty("ids", ids)
			).ToString(Formatting.None);
	}
}