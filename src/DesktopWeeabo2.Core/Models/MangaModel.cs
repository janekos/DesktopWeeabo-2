using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models.Shared;
using System;

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
				Type = apiModel.Type?.ToEnum<ContentType>() ?? ContentType.UNSPECIFIED,
				Format = apiModel.Format?.ToEnum<ContentFormat>() ?? ContentFormat.UNSPECIFIED,
				Status = apiModel.Status?.ToEnum<ContentStatus>() ?? ContentStatus.UNSPECIFIED
			};
		}
	}
}