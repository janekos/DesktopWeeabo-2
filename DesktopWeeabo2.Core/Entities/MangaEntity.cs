using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Entities {
	[Table("manga_entries")]
	public class MangaEntity : BaseEntity {
		[Column("volumes")]
		public int? Volumes { get; set; }

		[Column("chapters")]
		public int? Chapters { get; set; }

		[Column("reading_status")]
		public string ReadingStatus { get; set; }

		[Column("read_priority")]
		public int? ReadPriority { get; set; }

		[Column("reread_count")]
		public int? RereadCount { get; set; }

		[Column("current_chapter")]
		public int? CurrentChapter { get; set; }

		public static implicit operator MangaEntity(MangaModel model) {
			return new MangaEntity() {
				Id = model.Id,
				IdMal = model.IdMal,
				Type = (int)model.Type,
				IsAdult = model.IsAdult,
				Volumes = model.Volumes,
				Chapters = model.Chapters,
				Format = (int)model.Format,
				Status = (int)model.Status,
				DateAdded = model.DateAdded,
				CoverImage = model.CoverImage,
				Description = model.Description,
				RereadCount = model.RereadCount,
				AverageScore = model.AverageScore,
				ReadPriority = model.ReadPriority,
				PersonalScore = model.PersonalScore,
				ReadingStatus = model.ReadingStatus,
				PersonalReview = model.PersonalReview,
				CurrentChapter = model.CurrentChapter,
				TitleNative = model.Title?.TitleNative,
				TitleRomaji = model.Title?.TitleRomaji,
				TitleEnglish = model.Title?.TitleEnglish,
				Genres = string.Join("|", model.Genres),
				Synonyms = string.Join("|", model.Synonyms),
				ExternalLinks = JsonConvert.SerializeObject(model.ExternalLinks),
			};
		}
	}
}
