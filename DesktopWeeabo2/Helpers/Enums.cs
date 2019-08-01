﻿namespace DesktopWeeabo2.Helpers {
	public enum DBResponse {
		ADDED,
		UPDATED,
		DELETED,
		EXISTS,
		NOTEXISTS,
		NOCHANGES,
		ERROR
	}

	public enum SortLocation {
		ONLINE,
		LOCAL,
		ANIME,
		MANGA
	}

	public static class GlobalView {
		public const string ANIMEVIEW = "Animeview";
		public const string MANGAVIEW = "Mangaview";
		public const string CUSTOMVIEW = "Customview";
		public const string SETTINGSVIEW = "Settingsview";
	}

	public static class StatusView {
		public const string ONLINE = "Online";
		public const string TOWATCH = "Towatch";
		public const string TOREAD = "Toread";
		public const string RED = "Red";
		public const string READING = "Reading";
		public const string VIEWED = "Viewed";
		public const string WATCHING = "Watching";
		public const string DROPPEDMANGA = "Droppedmanga";
		public const string DROPPEDANIME = "Droppedanime";
		public const string DELETE = "Delete";
	}
}
