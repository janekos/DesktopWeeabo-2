using DesktopWeeabo2.Core.Config;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.Infrastructure.Jobs;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {

	public class MainWindowViewModel : BaseViewModel {

		public string IntroMessage {
			get {
				string[] veryFunnyMessages = new string[]{
					"loading...",
					"hajimeing...",
					"it's you again...",
					"almost there!",
					"starting!",
					"what?",
					"thinking..."
				};

				return veryFunnyMessages[new Random().Next(veryFunnyMessages.Length)];
			}
		}

		private bool _IsLoading = true;

		public bool IsLoading {
			get { return _IsLoading; }
			set {
				if (value != _IsLoading) {
					_IsLoading = value;
					RaisePropertyChanged(nameof(IsLoading));
				}
			}
		}

		private GlobalView _CurrentGlobalView = GlobalView.ANIMEVIEW;

		public GlobalView CurrentGlobalView {
			get { return _CurrentGlobalView; }
			set {
				if (_CurrentGlobalView != value) {
					_CurrentGlobalView = value;
					RaisePropertyChanged(nameof(CurrentGlobalView));
				}
			}
		}

		private ICollectionView _ViewModelsView;

		public ICollectionView ViewModelsView {
			get { return _ViewModelsView; }
			set {
				_ViewModelsView = value;
				RaisePropertyChanged(nameof(ViewModelsView));
			}
		}

		private ObservableCollection<BaseViewModel> _ViewModels;

		public ObservableCollection<BaseViewModel> ViewModels {
			get { return _ViewModels; }
			set {
				_ViewModels = value;
				RaisePropertyChanged(nameof(ViewModels));
			}
		}

		#region job dialog

		private bool _IsJobRunning = false;

		public bool IsJobRunning {
			get { return _IsJobRunning; }
			set {
				if (value != _IsJobRunning) {
					_IsJobRunning = value;
					RaisePropertyChanged(nameof(IsJobRunning));
				}
			}
		}

		private string _JobDescription = "";

		public string JobDescription {
			get { return _JobDescription; }
			set {
				_JobDescription = value;
				RaisePropertyChanged(nameof(JobDescription));
			}
		}

		private int _JobProgressMaximum = 100;

		public int JobProgressMaximum {
			get { return _JobProgressMaximum; }
			set {
				_JobProgressMaximum = value;
				RaisePropertyChanged(nameof(JobProgressMaximum));
			}
		}

		private int _JobProgressCurrent = 0;

		public int JobProgressCurrent {
			get { return _JobProgressCurrent; }
			set {
				_JobProgressCurrent = value;
				RaisePropertyChanged(nameof(JobProgressCurrent));
			}
		}

		private string _JobStage = "";

		public string JobStage {
			get { return _JobStage; }
			set {
				_JobStage = value;
				RaisePropertyChanged(nameof(JobStage));
			}
		}

		#endregion job dialog

		#region consent page

		private Visibility _ConsentBoxVisibility = Visibility.Collapsed;

		public Visibility ConsentBoxVisibility {
			get { return _ConsentBoxVisibility; }
			set {
				_ConsentBoxVisibility = value;
				RaisePropertyChanged(nameof(ConsentBoxVisibility));
			}
		}

		#endregion consent page

		#region toast

		private string _ToastMessage = "";

		public string ToastMessage {
			get { return _ToastMessage; }
			set {
				_ToastMessage = value;
				RaisePropertyChanged(nameof(ToastMessage));
			}
		}

		private ToastType _ToastMessageType;

		public ToastType ToastMessageType {
			get { return _ToastMessageType; }
			set {
				_ToastMessageType = value;
				RaisePropertyChanged(nameof(ToastMessageType));
			}
		}

		#endregion toast

		public readonly AnimeViewModel _animeViewModel;
		public readonly MangaViewModel _mangaViewModel;
		public readonly SettingsViewModel _settingsViewModel;

		private readonly IRunJobs<UpdateDbEntries> _updateDbEntries;

		public MainWindowViewModel(AnimeViewModel animeViewModel, MangaViewModel mangaViewModel, SettingsViewModel settingsViewModel, IRunJobs<UpdateDbEntries> updateDbEntries) {
			_animeViewModel = animeViewModel;
			_mangaViewModel = mangaViewModel;
			_settingsViewModel = settingsViewModel;
			_updateDbEntries = updateDbEntries;

			ViewModels = new ObservableCollection<BaseViewModel>(){
				_animeViewModel,
				_mangaViewModel,
				_settingsViewModel
			};
			ViewModelsView = CollectionViewSource.GetDefaultView(ViewModels);

			if (AppInitHelpers.CheckRootDir())
				InitApp();
			else
				ConsentBoxVisibility = Visibility.Visible;

			ToastEvent.ToastMessageRecieved += (sender, args) => {
				ToastMessage = args.Message;
				ToastMessageType = args.MessageType;
			};

			JobEvent.JobStarted += (sender, args) => {
				IsJobRunning = true;
				JobDescription = args.JobDescription;
				JobProgressMaximum = args.JobLength;
			};

			JobEvent.JobProgressChanged += (sender, args) => {
				JobProgressCurrent = args.IsIncremental
					? (JobProgressCurrent + args.Progress)
					: args.Progress;

				if (args.StageDescriptor != null)
					JobStage = args.StageDescriptor;
			};

			JobEvent.JobEnded += (sender) => {
				JobProgressCurrent = 0;
				IsJobRunning = false;
			};
		}

		private void InitApp() {
			AppInitHelpers.Init();
			IsLoading = false;

			if (ConfigurationManager.Config.DoesUpdateOnStartup) {
				_updateDbEntries.RunJob();
			}
		}

		public DelegateCommand GiveConsent => new DelegateCommand(
			new Action<object>((e) => {
				if (e.ToString().Equals("ACCEPT")) {
					ConsentBoxVisibility = Visibility.Collapsed;
					InitApp();
				} else {
					Environment.Exit(0);
				}
			})
		);

		public DelegateCommand ChangeView => new DelegateCommand(
		new Action<object>(
		(e) => {
			if (ViewModelsView != null) {
				CurrentGlobalView = (GlobalView) e;
				switch (e) {
					case GlobalView.ANIMEVIEW:
						ViewModelsView.MoveCurrentToPosition(0);
						break;

					case GlobalView.MANGAVIEW:
						ViewModelsView.MoveCurrentToPosition(1);
						break;

					case GlobalView.SETTINGSVIEW:
						ViewModelsView.MoveCurrentToPosition(2);
						break;

					default:
						ViewModelsView.MoveCurrentToPosition(0);
						break;
				}
			}
		}));
	}
}