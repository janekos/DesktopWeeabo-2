using DesktopWeeabo2.data.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data
{
    static class DataHandler
    {
        public static void ParseAnimeObjects(string rawJSON)
        {
            var jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
            bool hasNextPage = false;
            int lastKnownIndex = 0;

            rawJSON = rawJSON.Remove(0, 43);
            rawJSON = rawJSON.Remove(rawJSON.Length - 4);
            if (rawJSON[0] == 't') { hasNextPage = true; }
            rawJSON = rawJSON.Remove(0, 15);

            foreach (Match m in Regex.Matches(rawJSON, "(},{)"))
            {
                Console.WriteLine(rawJSON.Substring(lastKnownIndex, m.Index - lastKnownIndex + 1));
                lastKnownIndex = m.Index + 2;
            }

            AnimeObject ae = JsonConvert.DeserializeObject<AnimeObject>(rawJSON.Substring(lastKnownIndex, rawJSON.Length - lastKnownIndex), jsonSettings);
            Console.WriteLine(ae.Print());
        }

        public static void ParseMangaObjects(string rawJSON)
        {
            bool hasNextPage = false;
            int lastKnownIndex = 0;

            rawJSON = rawJSON.Remove(0, 43);
            rawJSON = rawJSON.Remove(rawJSON.Length - 4);
            if (rawJSON[0] == 't') { hasNextPage = true; }
            rawJSON = rawJSON.Remove(0, 15);
            Console.WriteLine(rawJSON);
            foreach (Match m in Regex.Matches(rawJSON, "(},{)"))
            {
                Console.WriteLine(rawJSON.Substring(lastKnownIndex, m.Index - lastKnownIndex + 1));
                lastKnownIndex = m.Index + 2;
            }
            string s = rawJSON.Substring(lastKnownIndex, rawJSON.Length - lastKnownIndex);
            Console.WriteLine(s);
            MangaObject ae = JsonConvert.DeserializeObject<MangaObject>(s);
            Console.WriteLine(ae.Print());
        }
    }
}
