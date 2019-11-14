using DesktopWeeabo2.Core.API.Models.JsonTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DesktopWeeabo2.Core.API.Models.Shared {

	public abstract class BaseApiModel {

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("idMal")]
		public int? IdMal { get; set; }

		[JsonProperty("averageScore")]
		public int? AverageScore { get; set; }

		[JsonProperty("isAdult")]
		public bool? IsAdult { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("siteUrl")]
		public string siteUrl { get; set; }

		[JsonProperty("coverImage")]
		public CoverImage CoverImage { get; set; }

		[JsonProperty("title")]
		public Title Title { get; set; }

		[JsonProperty("externalLinks")]
		public List<ExternalLink> ExternalLinks { get; set; }

		[JsonProperty("genres")]
		public IEnumerable<string> Genres { get; set; }

		[JsonProperty("synonyms")]
		public IEnumerable<string> Synonyms { get; set; }
	}
}