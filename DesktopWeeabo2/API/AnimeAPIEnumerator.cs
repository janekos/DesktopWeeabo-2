using DesktopWeeabo2.API.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API {
	class AnimeAPIEnumerator : APIEnumerator<AnimeModel> {

		public AnimeAPIEnumerator() {
			Type = true;
		}

		protected override AnimeModel[] ManageItems(JArray items) {
			AnimeModel[] arr = new AnimeModel[items.Count];

			for (int i = 0; i < items.Count; i++) {
				arr[i] = new AnimeModel {
					Id = (int)items[i]["id"],
					MeanScore = items[i]["meanScore"].Type == JTokenType.Null ? 0 : (int)items[i]["meanScore"],
					Type = items[i]["type"].Type == JTokenType.Null ? "" : (string)items[i]["type"],
					Format = items[i]["format"].Type == JTokenType.Null ? "" : (string)items[i]["format"],
					Status = items[i]["status"].Type == JTokenType.Null ? "" : (string)items[i]["status"],
					Description = items[i]["description"].Type == JTokenType.Null ? "" : StringHelpers.CleanDescription((string)items[i]["description"]),
					Episodes = items[i]["episodes"].Type == JTokenType.Null ? 0 : (int)items[i]["episodes"],
					Duration = items[i]["duration"].Type == JTokenType.Null ? 0 : (int)items[i]["duration"],
					Genres = items[i]["genres"].Type == JTokenType.Null ? "" : string.Join(", ", (items[i]["genres"] as JArray).ToObject<string[]>()),
					Synonyms = items[i]["synonyms"].Type == JTokenType.Null ? "" : string.Join(", ", (items[i]["synonyms"] as JArray).ToObject<string[]>()),
					TitleEnglish = items[i]["title"]["english"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["english"],
					TitleRomaji = items[i]["title"]["romaji"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["romaji"],
					TitleNative = items[i]["title"]["native"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["native"],
					CoverImage = items[i]["coverImage"]["large"].Type == JTokenType.Null ? "" : (string)items[i]["coverImage"]["large"],
					StartDate = DateTime.Parse(
						(items[i]["startDate"]["year"].Type == JTokenType.Null ? "0001" : (string)items[i]["startDate"]["year"]) + "-" +
						(items[i]["startDate"]["month"].Type == JTokenType.Null ? "01" : (string)items[i]["startDate"]["month"]) + "-" +
						(items[i]["startDate"]["day"].Type == JTokenType.Null ? "01" : (string)items[i]["startDate"]["day"])),
					EndDate = DateTime.Parse(
						(items[i]["endDate"]["year"].Type == JTokenType.Null ? "0001" : (string)items[i]["endDate"]["year"]) + "-" +
						(items[i]["endDate"]["month"].Type == JTokenType.Null ? "01" : (string)items[i]["endDate"]["month"]) + "-" +
						(items[i]["endDate"]["day"].Type == JTokenType.Null ? "01" : (string)items[i]["endDate"]["day"]))
				};
			}
			return arr;
		}
	}
}
