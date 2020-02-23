using DesktopWeeabo2.Core.Config;
using DesktopWeeabo2.Core.Helpers;
using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Infrastructure.Jobs;
using DesktopWeeabo2.ViewModels;
using System.ComponentModel;
using System.Windows;
using Unity;

namespace DesktopWeeabo2 {

	public partial class MainWindow : Window {

		[Dependency]
		public MainWindowViewModel ViewModel {
			set { DataContext = value; }
		}

		[Dependency]
		public IRunJobs<BackupEntriesJob> BackupEntriesJob {
			set { backupEntriesJob = value; }
		}

		private IRunJobs<BackupEntriesJob> backupEntriesJob;

		public MainWindow() {
			InitializeComponent();
		}

		private async void MainWIndow_Closing(object sender, CancelEventArgs e) {
			if (AppHelpers.CheckRootDir()) {
				if (ConfigurationManager.Config.DoesAppBackUp) {
					await backupEntriesJob?.RunJob();
				}

				ConfigurationManager.SaveConfig();
			}
		}
	}
}