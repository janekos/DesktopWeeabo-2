using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class ExternalLink {
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("site")]
		public string Site { get; set; }
	}
}
