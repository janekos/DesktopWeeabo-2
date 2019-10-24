namespace DesktopWeeabo2.Core.Models.Complex {
	public class Title {
		public string TitleEnglish { get; set; }

		public string TitleRomaji { get; set; }

		public string TitleNative { get; set; }

		public string GetFirstNonNullTitle() =>
			TitleEnglish
			?? TitleRomaji
			?? TitleNative
			?? "Title missing";

		public static implicit operator Title(API.Models.JsonTypes.Title jsonTitle) {
			return new Title {
				TitleEnglish = jsonTitle.English,
				TitleNative = jsonTitle.Native,
				TitleRomaji = jsonTitle.Romaji
			};
		}
	}
}
