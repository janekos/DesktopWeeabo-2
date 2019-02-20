﻿using DesktopWeeabo2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models.Shared {
    public abstract class BaseModel {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

		[Column("id_mal")]
		public int IdMal { get; set; }

		[Column("mean_score")]
        public int MeanScore { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("format")]
        public string Format { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("description")]
        public string Description { get; set; }

		[Column("is_adult")]
		public bool IsAdult { get; set; }

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

        /*custom vars*/
        [Column("personal_score")]
        public int PersonalScore { get; set; }

        [Column("personal_review")]
        public string PersonalReview { get; set; }

        [Column("date_added")]
        public DateTime DateAdded { get; set; }

		public string FirstWorkingTitle {
			get {
				string title = StringHelpers.GetFirstNotNullItemTitle(this);
				if (title.Length > 40) {
					title = title.Substring(0, 40);
					if (char.IsWhiteSpace(title[39])) title = title.Substring(0, 39);
					return $"{title}...";
				}
				return title;
			}
		}
	}
}
