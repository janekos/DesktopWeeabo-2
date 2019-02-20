using DesktopWeeabo2.API.Shared;
using DesktopWeeabo2.Models;
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
                    Id = (int)items[i]["id"],
                    MeanScore = items[i]["meanScore"].Type == JTokenType.Null ? 0 : (int)items[i]["meanScore"],
                    Type = items[i]["type"].Type == JTokenType.Null ? "" : (string)items[i]["type"],
                    Format = items[i]["format"].Type == JTokenType.Null ? "" : (string)items[i]["format"],
                    Status = items[i]["status"].Type == JTokenType.Null ? "" : (string)items[i]["status"],
                    Description = items[i]["description"].Type == JTokenType.Null ? "" : (string)items[i]["description"],
                    Chapters = items[i]["chapters"].Type == JTokenType.Null ? 0 : (int)items[i]["chapters"],
                    Volumes = items[i]["volumes"].Type == JTokenType.Null ? 0 : (int)items[i]["volumes"],
                    Genres = items[i]["genres"].Type == JTokenType.Null ? "" : String.Join(", ", (items[1]["genres"] as JArray).ToObject<string[]>()),
                    Synonyms = items[i]["synonyms"].Type == JTokenType.Null ? "" : String.Join(", ", (items[1]["synonyms"] as JArray).ToObject<string[]>()),
                    TitleEnglish = items[i]["title"]["english"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["english"],
                    TitleRomaji = items[i]["title"]["romaji"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["romaji"],
                    TitleNative = items[i]["title"]["native"].Type == JTokenType.Null ? "" : (string)items[i]["title"]["native"],
                    CoverImage = items[i]["coverImage"]["large"].Type == JTokenType.Null ? "" : (string)items[i]["coverImage"]["large"],
					IsAdult = items[i]["isAdult"].Type == JTokenType.Null ? false : (bool)items[i]["isAdult"]
				};
            }

            return arr;
        }
    }
}
