using DesktopWeeabo2.Core.Config;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Core.Interfaces.Misc;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.Infrastructure.Jobs;
using DesktopWeeabo2.ViewModels.Shared;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DesktopWeeabo2.ViewModels {

	public class SettingsViewModel : BaseViewModel {
		private readonly int PAGES_PER_CHAPTER = 19;

		private readonly IAnimeService animeService;
		private readonly IMangaService mangaService;
		private readonly IRunJobs<DWOneImportJob> dwOneImportJob;
		private readonly IRunJobs<UpdateDbEntries> updateDbEntriesJob;
		private readonly IDefineCanvasRoutines<Chart> canvasRoutines;

		public SettingsViewModel(IAnimeService animeService, IMangaService mangaService, IRunJobs<DWOneImportJob> dwOneImportJob, IRunJobs<UpdateDbEntries> updateDbEntriesJob, IDefineCanvasRoutines<Chart> canvasRoutines) {
			this.animeService = animeService;
			this.mangaService = mangaService;
			this.dwOneImportJob = dwOneImportJob;
			this.updateDbEntriesJob = updateDbEntriesJob;
			this.canvasRoutines = canvasRoutines;

			LogEvent.LogLineReceived += LogLineReceivedFunc;
		}

		~SettingsViewModel() {
			LogEvent.LogLineReceived -= LogLineReceivedFunc;
		}

		public bool DoesAppBackUp {
			get { return ConfigurationManager.Config.DoesAppBackUp; }
			set {
				ConfigurationManager.Config.DoesAppBackUp = value;
				RaisePropertyChanged(nameof(DoesAppBackUp));
			}
		}

		public bool IsDarkMode {
			get { return ConfigurationManager.Config.IsDarkMode; }
			set {
				ConfigurationManager.Config.IsDarkMode = value;
				RaisePropertyChanged(nameof(IsDarkMode));
			}
		}

		public bool DoesUpdateOnStartup {
			get { return ConfigurationManager.Config.DoesUpdateOnStartup; }
			set {
				ConfigurationManager.Config.DoesUpdateOnStartup = value;
				RaisePropertyChanged(nameof(DoesUpdateOnStartup));
			}
		}

		public bool ShouldUpdateOnlyUnfinishedEntries {
			get { return ConfigurationManager.Config.ShouldUpdateOnlyUnfinishedEntries; }
			set {
				ConfigurationManager.Config.ShouldUpdateOnlyUnfinishedEntries = value;
				RaisePropertyChanged(nameof(ShouldUpdateOnlyUnfinishedEntries));
			}
		}
		
		public int MangaReadingSpeed {
			get { return ConfigurationManager.Config.MangaReadingSpeed; }
			set {
				ConfigurationManager.Config.MangaReadingSpeed = value;
				RaisePropertyChanged(nameof(MangaReadingSpeed));
			}
		}

		private StatisticsViewModel statistics { get; set; } = new StatisticsViewModel();

		public StatisticsViewModel Statistics {
			get { return statistics; }
			set {
				statistics = value;
				RaisePropertyChanged(nameof(Statistics));
			}
		}

		private string pathToDW1Data { get; set; }

		public string PathToDW1Data {
			get { return pathToDW1Data; }
			set {
				pathToDW1Data = value;
				RaisePropertyChanged(nameof(PathToDW1Data));
			}
		}

		public string Log {
			get { return LogEvent.LogContent; }
			set {
				LogEvent.SetLogContent(value != null ? LogEvent.LogContent + $"{value}{Environment.NewLine}" : "");
				RaisePropertyChanged(nameof(Log));
			}
		}

		private void LogLineReceivedFunc(object sender, LogLineReceivedEventArgs args) => Log = args.Message;

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
			await updateDbEntriesJob.RunJob();
		});

		public DelegateCommand CalculateStatistics => new DelegateCommand(new Action(() => {
			Statistics = new StatisticsViewModel {
				Chart = canvasRoutines.GetRoutineData(CanvasRoutine.MEDIA_CONSUMED_PER_MONTH),
				HoursSpentWatchingAnimes = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.VIEWED)).Sum(a => (a.Episodes * a.Duration) / 60).ToString(),
				HoursNeededToWatchRemainingAnimes = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.TOWATCH)).Sum(a => (a.Episodes * a.Duration) / 60).ToString(),
				AnimesDropped = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.DROPPEDANIME)).Count().ToString(),
				AnimesToWatch = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.TOWATCH)).Count().ToString(),
				AnimesViewed = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.VIEWED)).Count().ToString(),
				HoursSpentReadingMangas = mangaService.GetCustom(a => a.ReadingStatus.Equals(StatusView.READ)).Sum(a => (PAGES_PER_CHAPTER * a.Chapters * a.Volumes) / ConfigurationManager.Config.MangaReadingSpeed).ToString(),
				HoursNeededToReadRemainingMangas = mangaService.GetCustom(a => a.ReadingStatus.Equals(StatusView.TOREAD)).Sum(m => (PAGES_PER_CHAPTER * m.Chapters * m.Volumes) / ConfigurationManager.Config.MangaReadingSpeed).ToString(),
				MangasDropped = mangaService.GetCustom(m => m.ReadingStatus.Equals(StatusView.DROPPEDMANGA)).Count().ToString(),
				MangasRed = mangaService.GetCustom(m => m.ReadingStatus.Equals(StatusView.READ)).Count().ToString(),
				MangasToRead = mangaService.GetCustom(m => m.ReadingStatus.Equals(StatusView.TOREAD)).Count().ToString(),			
			};
		}));
	}
}