namespace DesktopWeeabo2.Core.Models.Complex {

	public class Title {
		public string English { get; set; }

		public string Romaji { get; set; }

		public string Native { get; set; }

		public string GetFirstNonNullTitle() =>
			English
			?? Romaji
			?? Native
			?? "Title missing";

		public static implicit operator Title(API.Models.JsonTypes.Title jsonTitle) {
			return new Title {
				English = jsonTitle.English,
				Native = jsonTitle.Native,
				Romaji = jsonTitle.Romaji
			};
		}
	}
}