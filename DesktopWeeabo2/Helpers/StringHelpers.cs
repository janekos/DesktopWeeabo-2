using DesktopWeeabo2.Core.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesktopWeeabo2.Helpers {

	public static class StringHelpers {

		public static string CleanDescription(string desc) =>
			Regex.Replace(Regex.Replace(desc, @"<[^>]*>", string.Empty), @"\t", string.Empty);

		public static string GenreHelper(GenreObject[] genreList) {
			string genreString = string.Join(", ", genreList.Where(g => g.IsSelected).Select(g => g.Name));
			return genreString.Length > 0
				? genreString
				: "All";
		}
	}
}