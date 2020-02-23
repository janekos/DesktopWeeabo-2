using DesktopWeeabo2.Core.Config;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Helpers;
using DesktopWeeabo2.Infrastructure.Jobs.Shared;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Jobs {
	public class BackupEntriesJob : BaseJob<BackupEntriesJob> {
		protected override bool PrepareAndCheckIfCanRun(object[] args) {
			isSilent = true;
			return AppHelpers.CheckRootDir() && ConfigurationManager.Config.DoesAppBackUp;
		}		

		protected override Task ExecuteJob() {
			if (!Directory.Exists(ConfigurationManager.Config.BackupDirPath)) {
				Directory.CreateDirectory(ConfigurationManager.Config.BackupDirPath);
			}

			var shouldBackupToday = false;
			var wasLastBackupThisYear = DateTime.Now.Year == ConfigurationManager.Config.LastBackupDate.Year;

			switch ((BackupFrequency) ConfigurationManager.Config.BackupFrequency) {
				case BackupFrequency.DAY:
					shouldBackupToday = true;
					break;
				case BackupFrequency.WEEK:
					shouldBackupToday = GetWeek(DateTime.Now) > GetWeek(ConfigurationManager.Config.LastBackupDate) || !wasLastBackupThisYear;
					break;
				case BackupFrequency.MONTH:
					shouldBackupToday = DateTime.Now.Month > ConfigurationManager.Config.LastBackupDate.Month || !wasLastBackupThisYear;
					break;
				case BackupFrequency.YEAR:
					shouldBackupToday = !wasLastBackupThisYear;
					break;
			}

			if (shouldBackupToday) {
				File.Copy(
					ConfigurationManager.Config.DatabaseFilePath,
					ConfigurationManager.Config.BackupDatabaseFilePath,
					overwrite: true);

				ConfigurationManager.Config.LastBackupDate = DateTime.Now;
			}

			return Task.CompletedTask;
		}

		private int GetWeek(DateTime date) =>
			CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
	}
}
