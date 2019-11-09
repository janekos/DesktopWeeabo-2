using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.Enums;
using System;
using System.Collections.Generic;

namespace DesktopWeeabo2.Core.Models.Shared {

	public abstract class BaseModel {
		public int Id { get; set; }

		public int? IdMal { get; set; }

		public int? AverageScore { get; set; }

		public bool? IsAdult { get; set; }

		public ContentType Type { get; set; }

		public ContentFormat Format { get; set; }

		public ContentStatus Status { get; set; }

		public string Description { get; set; }

		public Complex.Title Title { get; set; }

		public string CoverImage { get; set; }

		public List<ExternalLink> ExternalLinks { get; set; }

		public IEnumerable<string> Genres { get; set; }

		public IEnumerable<string> Synonyms { get; set; }

		/*custom vars*/
		public int? PersonalScore { get; set; }

		public string PersonalReview { get; set; }

		public DateTime? DateAdded { get; set; }

		public string FirstWorkingTitle {
			get {
				string title = Title.GetFirstNonNullTitle();
				if (title.Length > 40) {
					title = title.Substring(0, 40);
					if (char.IsWhiteSpace(title[39]))
						title = title.Substring(0, 39);
					return $"{title}...";
				}
				return title;
			}
		}
	}
}