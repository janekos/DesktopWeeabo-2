using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.API.Models.Shared;
using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models {
	public class AnimeApiModel : BaseApiModel {
		[JsonProperty("episodes")]
		public int? Episodes { get; set; }

		[JsonProperty("duration")]
		public int? Duration { get; set; }

		[JsonProperty("startDate")]
		public Date StartDate { get; set; }

		[JsonProperty("endDate")]
		public Date EndDate { get; set; }

		[JsonProperty("nextAiringEpisode")]
		public NextAiringEpisode NextAiringEpisode { get; set; }
	}
}
