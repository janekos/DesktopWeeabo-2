using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class PageInfo {
		[JsonProperty("hasNextPage")]
		public bool? HasNextPage { get; set; }

		[JsonProperty("total")]
		public int? Total { get; set; }
	}
}
