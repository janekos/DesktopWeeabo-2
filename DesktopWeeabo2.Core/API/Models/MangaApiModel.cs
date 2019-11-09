using DesktopWeeabo2.Core.API.Models.Shared;
using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models {

	public class MangaApiModel : BaseApiModel {

		[JsonProperty("volumes")]
		public int? Volumes { get; set; }

		[JsonProperty("chapters")]
		public int? Chapters { get; set; }
	}
}