using DesktopWeeabo2.custom.shared;
using DesktopWeeabo2.data.db.entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.custom {
    class AnimeAPIEnumerator : APIEnumerator<AnimeEntity> {

        public AnimeAPIEnumerator() : base() {}
        public AnimeAPIEnumerator(string _searchString, bool _type, string _sortBy) : base(_searchString, _type, _sortBy) {}

        protected override AnimeEntity[] manageItems(JArray items) {
            AnimeEntity[] arr = new AnimeEntity[items.Count];
            
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = new AnimeEntity {
                    id = (int) items[i]["id"],
                    meanScore = items[i]["meanScore"].Type == JTokenType.Null ? 0 : (int) items[i]["meanScore"],
                    type = items[i]["type"].Type == JTokenType.Null ? "" : (string) items[i]["type"],
                    format = items[i]["format"].Type == JTokenType.Null ? "" : (string) items[i]["format"],
                    status = items[i]["status"].Type == JTokenType.Null ? "" : (string) items[i]["status"],
                    description = items[i]["description"].Type == JTokenType.Null ? "" : (string) items[i]["description"],
                    episodes = items[i]["episodes"].Type == JTokenType.Null ? 0 : (int) items[i]["episodes"],
                    duration = items[i]["duration"].Type == JTokenType.Null ? 0 : (int) items[i]["duration"],
                    genres = items[i]["genres"].Type == JTokenType.Null ? "" : String.Join(", ", (items[1]["genres"] as JArray).ToObject<string[]>()),
                    synonyms = items[i]["synonyms"].Type == JTokenType.Null ? "" : String.Join(", ", (items[1]["synonyms"] as JArray).ToObject<string[]>()),
                    titleEnglish = items[i]["title"]["english"].Type == JTokenType.Null ? "" : (string) items[i]["title"]["english"],
                    titleRomaji = items[i]["title"]["romaji"].Type == JTokenType.Null ? "" : (string) items[i]["title"]["romaji"],
                    titleNative = items[i]["title"]["native"].Type == JTokenType.Null ? "" : (string) items[i]["title"]["native"],
                    coverImage = items[i]["coverImage"]["large"].Type == JTokenType.Null ? "" : (string) items[i]["coverImage"]["large"],
                    startDate = DateTime.Parse(
                        (items[i]["startDate"]["year"].Type == JTokenType.Null ? "0001" : (string)items[i]["startDate"]["year"]) + "-" +
                        (items[i]["startDate"]["month"].Type == JTokenType.Null ? "01" : (string)items[i]["startDate"]["month"]) + "-" +
                        (items[i]["startDate"]["day"].Type == JTokenType.Null ? "01" : (string)items[i]["startDate"]["day"])),
                    endDate = DateTime.Parse(
                        (items[i]["endDate"]["year"].Type == JTokenType.Null ? "0001" : (string)items[i]["endDate"]["year"]) + "-" +
                        (items[i]["endDate"]["month"].Type == JTokenType.Null ? "01" : (string)items[i]["endDate"]["month"]) + "-" +
                        (items[i]["endDate"]["day"].Type == JTokenType.Null ? "01" : (string)items[i]["endDate"]["day"]))
                };
            }

            return arr;
        }
    }
}
