using DesktopWeeabo2.data.db.entities.shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.db.entities {

    [Table("anime_entries")]
    public class AnimeEntity : BaseEntity {

        [Column("episodes")]
        public int episodes { get; set; }

        [Column("duration")]
        public int duration { get; set; }

        /*problem kids*/
        [Column("start_date")]
        public DateTime startDate { get; set; }

        [Column("end_date")]
        public DateTime endDate { get; set; }

        /*custom vars*/
        [Column("viewing_status")]
        public string viewingStatus { get; set; }

        [Column("watch_priority")]
        public int watchPriority { get; set; }

        [Column("rewatch_count")]
        public int rewatchCount { get; set; }

        [Column("current_episode")]
        public int currentEpisode { get; set; }
    }
}
