using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
    [Table("anime_entries")]
    public class AnimeModel : BaseModel {

        [Column("episodes")]
        public int Episodes { get; set; }

        [Column("duration")]
        public int Duration { get; set; }

        /*problem kids*/
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        /*custom vars*/
        [Column("viewing_status")]
        public string ViewingStatus { get; set; }

        [Column("watch_priority")]
        public int WatchPriority { get; set; }

        [Column("rewatch_count")]
        public int RewatchCount { get; set; }

        [Column("current_episode")]
        public int CurrentEpisode { get; set; }
    }
}
