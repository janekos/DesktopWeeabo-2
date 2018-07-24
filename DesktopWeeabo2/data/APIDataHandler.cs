using DesktopWeeabo2.data.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data {
    static class APIDataHandler {

        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static List<AnimeObject> parseAnimeObjects(string rawJSON) {            
            bool hasNextPage = false;
            
            rawJSON = rawJSON.Remove(rawJSON.Length - 3);
            if (rawJSON.Contains("\"hasNextPage\":true")) {
                hasNextPage = true;
                rawJSON = rawJSON.Remove(0, 57);
            } else {
                rawJSON = rawJSON.Remove(0, 58);
            }

            Console.WriteLine(hasNextPage);

            List<AnimeObject> aes = JsonConvert.DeserializeObject<List<AnimeObject>>(rawJSON, jsonSettings);

            return JsonConvert.DeserializeObject<List<AnimeObject>>(rawJSON, jsonSettings);
            


            /*foreach (var item in aes) {
                Console.WriteLine(item.print());
                Console.WriteLine("--------------------------------------------------------------------------");
            }*/
        }

        private static IEnumerable<AnimeObject> iterateThroughObjects(string rawJSON) {
            foreach(var aObject in JsonConvert.DeserializeObject<List<AnimeObject>>(rawJSON, jsonSettings)) {
                yield return aObject;
            }
        }
    }
}
