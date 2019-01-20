using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Helpers {
	static class StringHelpers {
		public static string CleanDescription(string desc) => Regex.Replace(Regex.Replace(desc, @"<[^>]*>", ""), @"\t|\n|\r", "");

		public static string GetFirstNotNullItemTitle(BaseModel item) =>
			!string.IsNullOrWhiteSpace(item.TitleEnglish)
				? item.TitleEnglish
				: (!string.IsNullOrWhiteSpace(item.TitleRomaji)
					? item.TitleRomaji
					:(!string.IsNullOrWhiteSpace(item.TitleNative)
						? item.TitleNative
						: "No title present"));
	}
}
