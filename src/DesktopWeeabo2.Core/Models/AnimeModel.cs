using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesktopWeeabo2.Core.Models {

	public class AnimeModel : BaseModel {
		public int? Episodes { get; set; }

		public int? Duration { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public Complex.NextAiringEpisode NextAiringEpisode { get; set; }

		/*custom vars*/
		public string ViewingStatus { get; set; }

		public int? WatchPriority { get; set; }

		public int? RewatchCount { get; set; }

		public int? CurrentEpisode { get; set; }

		public static implicit operator AnimeModel(AnimeApiModel apiModel) {
			Complex.NextAiringEpisode nextAiringEpisode = null;

			if (apiModel.NextAiringEpisode != null) {
				nextAiringEpisode = new Complex.NextAiringEpisode {
					AiringAt = DateTimeOffset.FromUnixTimeSeconds((long) apiModel.NextAiringEpisode.AiringAt).UtcDateTime,
					Episode = apiModel.NextAiringEpisode.Episode
				};
			}

			return new AnimeModel {
				Id = apiModel.Id,
				IdMal = apiModel.IdMal,
				Title = apiModel.Title,
				Genres = apiModel.Genres,
				DateAdded = DateTime.Now,
				IsAdult = apiModel.IsAdult,
				Synonyms = apiModel.Synonyms,
				Episodes = apiModel.Episodes,
				Duration = apiModel.Duration,
				Description = apiModel.Description,
				EndDate = apiModel.EndDate.GetDate(),
				AverageScore = apiModel.AverageScore,
				NextAiringEpisode = nextAiringEpisode,
				CoverImage = apiModel.CoverImage.Large,
				ExternalLinks = apiModel.ExternalLinks,
				StartDate = apiModel.StartDate.GetDate(),
				Type = apiModel.Type.ToEnum<ContentType>(),
				Format = apiModel.Format.ToEnum<ContentFormat>(),
				Status = apiModel.Status.ToEnum<ContentStatus>()
			};
		}
	}
}