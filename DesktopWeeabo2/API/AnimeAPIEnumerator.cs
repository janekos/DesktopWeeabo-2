using DesktopWeeabo2.API.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json.Linq;
using System;

namespace DesktopWeeabo2.API {
	public class AnimeAPIEnumerator : APIEnumerator<AnimeModel> {

		public AnimeAPIEnumerator() {
			Type = true;
		}

		protected override AnimeModel[] ManageItems(JArray items) {
			AnimeModel[] arr = new AnimeModel[items.Count];

			for (int i = 0; i < items.Count; i++) {
				arr[i] = new AnimeModel {
					Id =            (int)items[i]["id"],
					IdMal =              items[i]["idMal"].Type ==               JTokenType.Null ? null : (int?)items[i]["idMal"],
					Episodes =           items[i]["episodes"].Type ==            JTokenType.Null ? null : (int?)items[i]["episodes"],
					Duration =           items[i]["duration"].Type ==            JTokenType.Null ? null : (int?)items[i]["duration"],
					AverageScore =       items[i]["averageScore"].Type ==        JTokenType.Null ? null : (int?)items[i]["averageScore"],
					Type =               items[i]["type"].Type ==                JTokenType.Null ? null : items[i]["type"].ToString(),
					Format =             items[i]["format"].Type ==              JTokenType.Null ? null : items[i]["format"].ToString(),
					Status =             items[i]["status"].Type ==              JTokenType.Null ? null : items[i]["status"].ToString(),
					TitleRomaji =        items[i]["title"]["romaji"].Type ==     JTokenType.Null ? null : items[i]["title"]["romaji"].ToString(),
					TitleNative =        items[i]["title"]["native"].Type ==     JTokenType.Null ? null : items[i]["title"]["native"].ToString(),
					TitleEnglish =       items[i]["title"]["english"].Type ==    JTokenType.Null ? null : items[i]["title"]["english"].ToString(),
					CoverImage =         items[i]["coverImage"]["large"].Type == JTokenType.Null ? null : items[i]["coverImage"]["large"].ToString(),
					Description =        items[i]["description"].Type ==         JTokenType.Null ? null : StringHelpers.CleanDescription(items[i]["description"].ToString()),
					Genres =             items[i]["genres"].Type ==              JTokenType.Null ? null : string.Join(", ", (items[i]["genres"] as JArray).ToObject<string[]>()),
					Synonyms =           items[i]["synonyms"].Type ==            JTokenType.Null ? null : string.Join(", ", (items[i]["synonyms"] as JArray).ToObject<string[]>()),
					IsAdult =            items[i]["isAdult"].Type ==             JTokenType.Null ? false : (bool)items[i]["isAdult"],
					NextAiringEpisode = (items[i]["nextAiringEpisode"].Type ==   JTokenType.Null || items[i]["nextAiringEpisode"]["airingAt"].Type == JTokenType.Null || items[i]["nextAiringEpisode"]["episode"].Type == JTokenType.Null) ? null : $"{items[i]["nextAiringEpisode"]["airingAt"].ToString()}|{items[i]["nextAiringEpisode"]["episode"].ToString()}",
					StartDate = DateTime.Parse(
						(items[i]["startDate"]["year"].Type ==  JTokenType.Null ? "0001" : items[i]["startDate"]["year"].ToString()) + "-" +
						(items[i]["startDate"]["month"].Type == JTokenType.Null ? "01" : items[i]["startDate"]["month"].ToString()) + "-" +
						(items[i]["startDate"]["day"].Type ==   JTokenType.Null ? "01" : items[i]["startDate"]["day"].ToString())),
					EndDate = DateTime.Parse(
						(items[i]["endDate"]["year"].Type ==  JTokenType.Null ? "0001" : items[i]["endDate"]["year"].ToString()) + "-" +
						(items[i]["endDate"]["month"].Type == JTokenType.Null ? "01" : items[i]["endDate"]["month"].ToString()) + "-" +
						(items[i]["endDate"]["day"].Type ==   JTokenType.Null ? "01" : items[i]["endDate"]["day"].ToString()))
				};

				arr[i].ExternalLinks = StringHelpers.PrependAdditionalUrls(new ExternalLink[] {
					new ExternalLink{ Url = $"https://myanimelist.net/{arr[i].Type.ToLower()}/{arr[i].IdMal}", Site="MAL" },
					new ExternalLink{ Url = items[i]["siteUrl"].ToString(), Site="Anilist" }
				}, (items[i]["externalLinks"].Type == JTokenType.Null ? null : items[i]["externalLinks"] as JArray));
			}
			return arr;
		}
	}
}
