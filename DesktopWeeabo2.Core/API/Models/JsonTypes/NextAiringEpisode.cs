using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class NextAiringEpisode {
		[JsonProperty("airingAt")]
		public long? AiringAt { get; set; }

		[JsonProperty("episode")]
		public int? Episode { get; set; }
	}
}
