using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopWeeabo2.Core.Entities.Shared {

	public abstract class BaseEntity {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("id")]
		public int Id { get; set; }

		[Column("id_mal")]
		public int? IdMal { get; set; }

		[Column("average_score")]
		public int? AverageScore { get; set; }

		[Column("type")]
		public int? Type { get; set; }

		[Column("format")]
		public int? Format { get; set; }

		[Column("status")]
		public int? Status { get; set; }

		[Column("description")]
		public string Description { get; set; }

		[Column("is_adult")]
		public bool? IsAdult { get; set; }

		/*problem kids*/

		[Column("genres")]
		public string Genres { get; set; }

		[Column("synonyms")]
		public string Synonyms { get; set; }

		[Column("title_English")]
		public string TitleEnglish { get; set; }

		[Column("title_romaji")]
		public string TitleRomaji { get; set; }

		[Column("title_native")]
		public string TitleNative { get; set; }

		[Column("cover_image")]
		public string CoverImage { get; set; }

		[Column("external_links")]
		public string ExternalLinks { get; set; }

		/*custom vars*/

		[Column("personal_score")]
		public int? PersonalScore { get; set; }

		[Column("personal_review")]
		public string PersonalReview { get; set; }

		[Column("date_added")]
		public DateTime? DateAdded { get; set; }
	}
}