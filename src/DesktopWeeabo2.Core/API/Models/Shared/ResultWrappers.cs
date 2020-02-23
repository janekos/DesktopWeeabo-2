using Newtonsoft.Json;

namespace DesktopWeeabo2.Core.API.Models.Shared {

	public class ApiReturnValue<T> where T : BaseApiModel {

		[JsonProperty("data")]
		public Data<T> Data { get; set; }
	}

	public class Data<T> where T : BaseApiModel {

		[JsonProperty("Page")]
		public Page<T> Page { get; set; }
	}

	public class Page<T> where T : BaseApiModel {

		[JsonProperty("pageInfo")]
		public PageInfo PageInfo { get; set; }

		[JsonProperty("media")]
		public T[] Media { get; set; }
	}

	public class PageInfo {

		[JsonProperty("hasNextPage")]
		public bool? HasNextPage { get; set; }

		[JsonProperty("total")]
		public int? Total { get; set; }

		[JsonProperty("lastPage")]
		public int? LastPage { get; set; }
	}
}