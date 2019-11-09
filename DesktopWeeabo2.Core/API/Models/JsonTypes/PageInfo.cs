using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {

	public class PageInfo {

		[JsonProperty("hasNextPage")]
		public bool? HasNextPage { get; set; }

		[JsonProperty("total")]
		public int? Total { get; set; }
	}
}