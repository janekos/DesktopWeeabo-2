using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.Infrastructure.Jobs;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Windows.Forms;

namespace DesktopWeeabo2.ViewModels {

	public class SettingsViewModel : BaseViewModel {

		private readonly IRunJobs<DWOneImportJob> dwOneImportJob;
		private readonly IRunJobs<UpdateDbEntries> updateDbEntriesJob;

		public SettingsViewModel(IRunJobs<DWOneImportJob> dwOneImportJob, IRunJobs<UpdateDbEntries> updateDbEntriesJob) {
			this.dwOneImportJob = dwOneImportJob;
			this.updateDbEntriesJob = updateDbEntriesJob;

			LogEvent.LogLineReceived += LogLineReceivedFunc;
		}

		~SettingsViewModel() {
			LogEvent.LogLineReceived -= LogLineReceivedFunc;
		}

		private bool _DoesAppBackUp { get; set; } = true;

		public bool DoesAppBackUp {
			get { return _DoesAppBackUp; }
			set {
				_DoesAppBackUp = value;
				RaisePropertyChanged("DoesAppBackUp");
			}
		}

		private bool _IsLightMode { get; set; } = false;

		public bool IsLightMode {
			get { return _IsLightMode; }
			set {
				_IsLightMode = value;
				RaisePropertyChanged("IsLightMode");
			}
		}

		private bool _DoesUpdateOnStartup { get; set; } = true;

		public bool DoesUpdateOnStartup {
			get { return _DoesUpdateOnStartup; }
			set {
				_DoesUpdateOnStartup = value;
				RaisePropertyChanged("DoesUpdateOnStartup");
			}
		}

		private bool _ShouldUpdateOnlyUnfinishedEntries { get; set; } = true;

		public bool ShouldUpdateOnlyUnfinishedEntries {
			get { return _ShouldUpdateOnlyUnfinishedEntries; }
			set {
				_ShouldUpdateOnlyUnfinishedEntries = value;
				RaisePropertyChanged("ShouldUpdateOnlyUnfinishedEntries");
			}
		}

		private string _PathToDW1Data { get; set; }

		public string PathToDW1Data {
			get { return _PathToDW1Data; }
			set {
				_PathToDW1Data = value;
				RaisePropertyChanged("PathToDW1Data");
			}
		}

		public string Log {
			get { return LogEvent.LogContent; }
			set {
				LogEvent.SetLogContent(value != null ? LogEvent.LogContent + $"{value}{Environment.NewLine}" : "");
				RaisePropertyChanged("Log");
			}
		}

		private void LogLineReceivedFunc(string message) => Log = message;

		public DelegateCommand ClearLog => new DelegateCommand(new Action(() => {
			LogEvent.SetLogContent(string.Empty);
		}));

		public DelegateCommand ShowFileSelectorDialog => new DelegateCommand(new Action(() => {
			OpenFileDialog fileDialog = new OpenFileDialog {
				InitialDirectory = string.IsNullOrEmpty(PathToDW1Data)
					? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
					: PathToDW1Data,

				Filter = "XML Files (*.xml)|*.xml",
				FilterIndex = 0,
				DefaultExt = "xml"
			};

			if (fileDialog.ShowDialog() == DialogResult.OK)
				if (!fileDialog.FileName.Contains("MainEntries.xml")) {
					MessageBox.Show(@"DesktopWeeabo 1 used a file called MainEntries.xml to save items. You can find it in Documents/DesktopWeeabo",
						"Invalid Import File",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				} else {
					PathToDW1Data = fileDialog.FileName;
				}
		}));

		public DelegateCommand ImportFromDW1 => new DelegateCommand(async () => {
			if (!string.IsNullOrEmpty(PathToDW1Data)) {
				await dwOneImportJob.RunJob(PathToDW1Data);
			}
		});

		public DelegateCommand UpdateEntries => new DelegateCommand(async () => {
			await updateDbEntriesJob.RunJob(ShouldUpdateOnlyUnfinishedEntries);
		});
	}
}