using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.Entities;
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

		public static implicit operator AnimeModel(AnimeEntity entity) {
			var title = new Complex.Title {
				TitleEnglish = entity.TitleEnglish,
				TitleNative = entity.TitleNative,
				TitleRomaji = entity.TitleRomaji
			};

			Complex.NextAiringEpisode nextAiringEpisode = null;

			if (entity.NextAiringEpisodeAiringAt != null && entity.NextAiringEpisodeEpisode != null) {
				nextAiringEpisode = new Complex.NextAiringEpisode {
					AiringAt = entity.NextAiringEpisodeAiringAt,
					Episode = entity.NextAiringEpisodeEpisode
				};
			}

			return new AnimeModel() {
				Title = title,
				Id = entity.Id,
				IdMal = entity.IdMal,
				Genres = entity.Genres.Split('|'),
				IsAdult = entity.IsAdult,
				EndDate = entity.EndDate,
				Synonyms = entity.Synonyms.Split('|'),
				Episodes = entity.Episodes,
				Duration = entity.Duration,
				DateAdded = entity.DateAdded,
				StartDate = entity.StartDate,
				CoverImage = entity.CoverImage,
				Type = (ContentType) entity.Type,
				Description = entity.Description,
				AverageScore = entity.AverageScore,
				RewatchCount = entity.RewatchCount,
				ExternalLinks = JsonConvert.DeserializeObject<List<ExternalLink>>(entity.ExternalLinks),
				PersonalScore = entity.PersonalScore,
				ViewingStatus = entity.ViewingStatus,
				WatchPriority = entity.WatchPriority,
				Format = (ContentFormat) entity.Format,
				Status = (ContentStatus) entity.Status,
				NextAiringEpisode = nextAiringEpisode,
				PersonalReview = entity.PersonalReview,
				CurrentEpisode = entity.CurrentEpisode
			};
		}
	}
}