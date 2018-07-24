using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.objects {
    public class AnimeObject {

        public int id { get; set; }
        public int meanScore { get; set; }
        public int episodes { get; set; }
        public int duration { get; set; }
        public string type { get; set; }
        public string format { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public IList<string> genres { get; set; }
        public IList<string> synonyms { get; set; }
        public TitleObject title { get; set; }
        public DateObject startDate { get; set; }
        public DateObject endDate { get; set; }
        public CoverImageObject coverImage { get; set; }

        public string print() {
            return "Id: " + id +
                    ", \nScore: " + meanScore +
                    ", \nType: " + type +
                    ", \nFormat: " + format +
                    ", \nStatus: " + status +
                    ", \nCoverImage: " + coverImage.large +
                    ", \nDescription: " + description +
                    ", \nTitleNative: " + title.native +
                    ", \nIdTitleRomaji " + title.romaji +
                    ", \nTitleEnglish: " + title.english +
                    ", \nGenres: " + string.Join(",", genres.ToArray()) +
                    ", \nSynonyms: " + string.Join(",", synonyms.ToArray()) +
                    ", \nEpisodes: " + episodes + ", \nDuration: " + duration +
                    ", \nStartDate: " + startDate.toString() +
                    ", \nEndDate: " + endDate.toString();
        }
    }
}
