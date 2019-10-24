using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class Title {
		[JsonProperty("romaji")]
		public string Romaji { get; set; }

		[JsonProperty("native")]
		public string Native { get; set; }

		[JsonProperty("english")]
		public string English { get; set; }
	}
}
