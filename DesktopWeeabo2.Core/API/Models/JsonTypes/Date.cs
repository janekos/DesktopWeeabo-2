using Newtonsoft.Json;
using System;

namespace DesktopWeeabo2.Core.API.Models.JsonTypes {
	public class Date {
		[JsonProperty("year")]
		public string Year { get; set; }

		[JsonProperty("month")]
		public string Month { get; set; }

		[JsonProperty("day")]
		public string Day { get; set; }

		public DateTime? GetDate() =>
			!string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(Month) && !string.IsNullOrEmpty(Day)
				? DateTime.Parse($"{Year}-{Month}-{Day}")
				: (DateTime?)null;
	}
}
