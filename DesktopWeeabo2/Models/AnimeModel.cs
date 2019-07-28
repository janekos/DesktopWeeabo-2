using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
    [Table("anime_entries")]
    public class AnimeModel : BaseModel {

        [Column("episodes")]
        public int? Episodes { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        /*problem kids*/
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

		[Column("next_airing_episode")]
		public string NextAiringEpisode { get; set; }

		/*custom vars*/
		[Column("viewing_status")]
        public string ViewingStatus { get; set; }

        [Column("watch_priority")]
        public int? WatchPriority { get; set; }

        [Column("rewatch_count")]
        public int? RewatchCount { get; set; }

        [Column("current_episode")]
        public int? CurrentEpisode { get; set; }

		[UnReflectable]
		public int? GetNextAiringEpisodeNumber {
			get {
				if (int.TryParse(NextAiringEpisode?.Split('|')[1], out int result)) return result;
				return null;
			}
		}

		[UnReflectable]
		public DateTime? GetNextAiringEpisodeDate {
			get {
				if (long.TryParse(NextAiringEpisode?.Split('|')[0], out long result)) return DateTimeOffset.FromUnixTimeSeconds(result).UtcDateTime;
				return null;
			}
		}
	}
}
