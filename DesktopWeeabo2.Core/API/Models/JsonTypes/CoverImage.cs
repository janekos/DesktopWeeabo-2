using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class CoverImage {
		[JsonProperty("large")]
		public string Large { get; set; }
	}
}
