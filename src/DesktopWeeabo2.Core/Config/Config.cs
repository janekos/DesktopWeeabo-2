using Newtonsoft.Json;
using System;
using System.IO;

namespace DesktopWeeabo2.Core.Config {

	public class Config {
		// immutable vars
		[JsonIgnore]
		public readonly string AppDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DesktopWeeabo2");
		[JsonIgnore]
		public string ConfigFilePath { get { return Path.Combine(AppDir, "config.json"); } }
		[JsonIgnore]
		public string BackupDirPath { get { return Path.Combine(AppDir, "backups"); } }
		[JsonIgnore]
		public string DatabaseFilePath { get { return Path.Combine(AppDir, "entries.db"); } }
		[JsonIgnore]
		public string BackupDatabaseFilePath { get { return Path.Combine(BackupDirPath, $"entries-{DateTime.Now.ToString("yyyy-MM-dd")}.db"); } }

		// mutable config vars
		public bool DoesAppBackUp { get; set; } = true;
		public int BackupFrequency { get; set; } = 0;
		public DateTime LastBackupDate { get; set; } = DateTime.Now;
		public bool IsDarkMode { get; set; } = false;
		public bool DoesUpdateOnStartup { get; set; } = true;
		public bool ShouldUpdateOnlyUnfinishedEntries { get; set; } = true;
		public int MangaReadingSpeed { get; set; } = 100;
	}
}