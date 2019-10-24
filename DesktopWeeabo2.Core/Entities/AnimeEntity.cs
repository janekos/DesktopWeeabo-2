using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopWeeabo2.Core.Entities {
	[Table("anime_entries")]
	public class AnimeEntity : BaseEntity {
		[Column("episodes")]
		public int? Episodes { get; set; }

		[Column("duration")]
		public int? Duration { get; set; }

		[Column("start_date")]
		public DateTime? StartDate { get; set; }

		[Column("end_date")]
		public DateTime? EndDate { get; set; }

		[Column("next_airing_episode_airing_at")]
		public DateTime? NextAiringEpisodeAiringAt { get; set; }

		[Column("next_airing_episode_episode")]
		public int? NextAiringEpisodeEpisode { get; set; }

		/*custom vars*/
		[Column("viewing_status")]
		public string ViewingStatus { get; set; }

		[Column("watch_priority")]
		public int? WatchPriority { get; set; }

		[Column("rewatch_count")]
		public int? RewatchCount { get; set; }

		[Column("current_episode")]
		public int? CurrentEpisode { get; set; }

		public static explicit operator AnimeEntity(AnimeModel model) {
			return new AnimeEntity {
				Id = model.Id,
				IdMal = model.IdMal,
				Type = (int)model.Type,
				IsAdult = model.IsAdult,
				EndDate = model.EndDate,
				Episodes = model.Episodes,
				Duration = model.Duration,
				Format = (int)model.Format,
				Status = (int)model.Status,
				DateAdded = model.DateAdded,
				StartDate = model.StartDate,
				CoverImage = model.CoverImage,
				Description = model.Description,
				AverageScore = model.AverageScore,
				RewatchCount = model.RewatchCount,
				PersonalScore = model.PersonalScore,
				ViewingStatus = model.ViewingStatus,
				WatchPriority = model.WatchPriority,
				PersonalReview = model.PersonalReview,
				CurrentEpisode = model.CurrentEpisode,
				TitleNative = model.Title?.TitleNative,
				TitleRomaji = model.Title?.TitleRomaji,
				TitleEnglish = model.Title?.TitleEnglish,
				Genres = string.Join("|", model.Genres),
				Synonyms = string.Join("|", model.Synonyms),
				NextAiringEpisodeEpisode = model.NextAiringEpisode?.Episode,
				NextAiringEpisodeAiringAt = model.NextAiringEpisode?.AiringAt,
				ExternalLinks = JsonConvert.SerializeObject(model.ExternalLinks),
			};
		}
	}
}
