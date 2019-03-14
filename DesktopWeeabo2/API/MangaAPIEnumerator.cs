using DesktopWeeabo2.API.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API {
    class MangaAPIEnumerator : APIEnumerator<MangaModel> {

        public MangaAPIEnumerator() {
			Type = false;
		}

        protected override MangaModel[] ManageItems(JArray items) {

            MangaModel[] arr = new MangaModel[items.Count];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = new MangaModel {
                    Id =       (int)items[i]["id"],
					IdMal =         items[i]["idMal"].Type ==               JTokenType.Null ? null : (int?)items[i]["idMal"],
					Volumes =       items[i]["volumes"].Type ==             JTokenType.Null ? null : (int?)items[i]["volumes"],
                    Chapters =      items[i]["chapters"].Type ==            JTokenType.Null ? null : (int?)items[i]["chapters"],
                    AverageScore =  items[i]["averageScore"].Type ==        JTokenType.Null ? null : (int?)items[i]["averageScore"],
                    Type =          items[i]["type"].Type ==                JTokenType.Null ? null : items[i]["type"].ToString(),
                    Format =        items[i]["format"].Type ==              JTokenType.Null ? null : items[i]["format"].ToString(),
                    Status =        items[i]["status"].Type ==              JTokenType.Null ? null : items[i]["status"].ToString(),
                    Description =   items[i]["description"].Type ==         JTokenType.Null ? null : items[i]["description"].ToString(),
					ExternalLinks = items[i]["externalLinks"].Type ==       JTokenType.Null ? null : items[i]["externalLinks"].ToString(),
                    TitleRomaji =   items[i]["title"]["romaji"].Type ==     JTokenType.Null ? null : items[i]["title"]["romaji"].ToString(),
                    TitleNative =   items[i]["title"]["native"].Type ==     JTokenType.Null ? null : items[i]["title"]["native"].ToString(),
                    TitleEnglish =  items[i]["title"]["english"].Type ==    JTokenType.Null ? null : items[i]["title"]["english"].ToString(),
                    CoverImage =    items[i]["coverImage"]["large"].Type == JTokenType.Null ? null : items[i]["coverImage"]["large"].ToString(),
                    Genres =        items[i]["genres"].Type ==              JTokenType.Null ? null : string.Join(", ", (items[1]["genres"] as JArray).ToObject<string[]>()),
                    Synonyms =      items[i]["synonyms"].Type ==            JTokenType.Null ? null : string.Join(", ", (items[1]["synonyms"] as JArray).ToObject<string[]>()),
					IsAdult =       items[i]["isAdult"].Type ==             JTokenType.Null ? false : (bool)items[i]["isAdult"]
				};

				arr[i].ExternalLinks = StringHelpers.PrependAdditionalUrls(
					new ExternalLink[] {
						new ExternalLink{ Url = $"https://myanimelist.net/{arr[i].Type.ToLower()}/{arr[i].IdMal}", Site="MAL" },
						new ExternalLink{ Url = items[i]["siteUrl"].ToString(), Site="Anilist" }
					}, 
					(items[i]["externalLinks"].Type == JTokenType.Null ? null : items[i]["externalLinks"] as JArray)
				);
			}

            return arr;
        }
    }
}
