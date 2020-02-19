using System;

namespace DesktopWeeabo2.Core.Config {

	public class Config {
		// immutable config vars
		public readonly string AppDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DesktopWeeabo2";

		// mutable config vars
		public bool DoesAppBackUp { get; set; } = true;
		public bool IsDarkMode { get; set; } = false;
		public bool DoesUpdateOnStartup { get; set; } = true;
		public bool ShouldUpdateOnlyUnfinishedEntries { get; set; } = true;
		public int MangaReadingSpeed { get; set; } = 100;
	}
}