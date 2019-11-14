using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesktopWeeabo2.Core.Models {

	public class MangaModel : BaseModel {
		public int? Volumes { get; set; }

		public int? Chapters { get; set; }

		public string ReadingStatus { get; set; }

		public int? ReadPriority { get; set; }

		public int? RereadCount { get; set; }

		public int? CurrentChapter { get; set; }

		public static implicit operator MangaModel(MangaApiModel apiModel) {
			return new MangaModel {
				Id = apiModel.Id,
				IdMal = apiModel.IdMal,
				Title = apiModel.Title,
				Genres = apiModel.Genres,
				DateAdded = DateTime.Now,
				IsAdult = apiModel.IsAdult,
				Volumes = apiModel.Volumes,
				Synonyms = apiModel.Synonyms,
				Chapters = apiModel.Chapters,
				Description = apiModel.Description,
				AverageScore = apiModel.AverageScore,
				CoverImage = apiModel.CoverImage.Large,
				ExternalLinks = apiModel.ExternalLinks,
				Type = apiModel.Type.ToEnum<ContentType>(),
				Format = apiModel.Format.ToEnum<ContentFormat>(),
				Status = apiModel.Status.ToEnum<ContentStatus>()
			};
		}

		public static implicit operator MangaModel(MangaEntity entity) {
			var title = new Complex.Title {
				TitleEnglish = entity.TitleEnglish,
				TitleNative = entity.TitleNative,
				TitleRomaji = entity.TitleRomaji
			};

			return new MangaModel() {
				Title = title,
				Id = entity.Id,
				IdMal = entity.IdMal,
				Genres = entity.Genres.Split('|'),
				IsAdult = entity.IsAdult,
				Volumes = entity.Volumes,
				Synonyms = entity.Synonyms.Split('|'),
				Chapters = entity.Chapters,
				DateAdded = entity.DateAdded,
				CoverImage = entity.CoverImage,
				Type = (ContentType) entity.Type,
				Description = entity.Description,
				RereadCount = entity.RereadCount,
				AverageScore = entity.AverageScore,
				ReadPriority = entity.ReadPriority,
				ExternalLinks = JsonConvert.DeserializeObject<List<ExternalLink>>(entity.ExternalLinks),
				PersonalScore = entity.PersonalScore,
				ReadingStatus = entity.ReadingStatus,
				Format = (ContentFormat) entity.Format,
				Status = (ContentStatus) entity.Status,
				PersonalReview = entity.PersonalReview,
				CurrentChapter = entity.CurrentChapter
			};
		}
	}
}