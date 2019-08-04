using DesktopWeeabo2.Data;
using DesktopWeeabo2.Data.Services;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Services;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

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

		private Visibility _ConsentBoxVisibility = Visibility.Collapsed;
		public Visibility ConsentBoxVisibility {
			get { return _ConsentBoxVisibility; }
			set {
				if (value != _ConsentBoxVisibility) {
					_ConsentBoxVisibility = value;
					RaisePropertyChanged("ConsentBoxVisibility");
				}
			}
		}

		private bool _IsLoading = true;
		public bool IsLoading {
			get { return _IsLoading; }
			set {
				if (value != _IsLoading) {
					_IsLoading = value;
					RaisePropertyChanged("IsLoading");
				}
			}
		}

		private string _ToastMessage = "";
		public string ToastMessage {
			get { return _ToastMessage; }
			set {
				if (_ToastMessage != value) {
					_ToastMessage = value;
				}
				RaisePropertyChanged("ToastMessage");
			}
		}

		private string _CurrentGlobalView = GlobalView.ANIMEVIEW;
		public string CurrentGlobalView {
			get { return _CurrentGlobalView; }
			set {
				if (_CurrentGlobalView != value) {
					_CurrentGlobalView = value;
				}
				RaisePropertyChanged("CurrentGlobalView");
			}
		}

		private string _ToastBorderColor = Brushes.Transparent.ToString();
		public string ToastBorderColor {
			get { return _ToastBorderColor; }
			set {
				if (_ToastBorderColor != value) {
					_ToastBorderColor = value;
					RaisePropertyChanged("ToastBorderColor");
				}
			}
		}

		private string _ToastBackgroundColor = Brushes.Transparent.ToString();
		public string ToastBackgroundColor {
			get { return _ToastBackgroundColor; }
			set {
				if (_ToastBackgroundColor != value) {
					_ToastBackgroundColor = value;
					RaisePropertyChanged("ToastBackgroundColor");
				}
			}
		}

		private string _ToastTextColor = Brushes.Transparent.ToString();
		public string ToastTextColor {
			get { return _ToastTextColor; }
			set {
				if (_ToastTextColor != value) {
					_ToastTextColor = value;
					RaisePropertyChanged("ToastTextColor");
				}
			}
		}

		private ICollectionView _ViewModelsView;
		public ICollectionView ViewModelsView {
			get { return _ViewModelsView; }
			set {
				_ViewModelsView = value;
				RaisePropertyChanged("ViewModelsView");
			}
		}

		private ObservableCollection<BaseViewModel> _ViewModels;
		public ObservableCollection<BaseViewModel> ViewModels {
			get { return _ViewModels; }
			set {
				_ViewModels = value;
				RaisePropertyChanged("ViewModels");
			}
		}

		public readonly AnimeViewModel _animeViewModel;
		public readonly MangaViewModel _mangaViewModel;
		public readonly SettingsViewModel _settingsViewModel;

		public MainWindowViewModel(AnimeViewModel animeViewModel, MangaViewModel mangaViewModel, SettingsViewModel settingsViewModel) {
			_animeViewModel = animeViewModel;
			_mangaViewModel = mangaViewModel;
			_settingsViewModel = settingsViewModel;

			ViewModels = new ObservableCollection<BaseViewModel>(){
				_animeViewModel,
				_mangaViewModel,
				_settingsViewModel
			};
			ViewModelsView = CollectionViewSource.GetDefaultView(ViewModels);

			if (InitAppData.CheckRootDir()) InitApp();
			else ConsentBoxVisibility = Visibility.Visible;

			ToastService.ToastMessageRecieved += (message, messageType) => {
				switch (messageType) {
					case "warning":
						ToastBackgroundColor = "#fcf8e3";
						ToastBorderColor = "#faf2cc";
						ToastTextColor = "#8a6d3b";
						break;
					case "danger":
						ToastBackgroundColor = "#f2dede";
						ToastBorderColor = "#ebcccc";
						ToastTextColor = "#a94442";
						break;
					case "success":
						ToastBackgroundColor = "#dff0d8";
						ToastBorderColor = "#d0e9c6";
						ToastTextColor = "#3c763d";
						break;
					case "info":
						ToastBackgroundColor = "#d9edf7";
						ToastBorderColor = "#bcdff1";
						ToastTextColor = "#31708f";
						break;
				}

				ToastMessage = message;
			};
		}

		private async void InitApp() {
			InitAppData.Init();
			await InitAppData.WakeDB();
			IsLoading = false;
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
			LogService.LogMessage($"Changed view to: {e}");
			if (ViewModelsView != null) {
				CurrentGlobalView = e as string;
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
		}),
		(e) => { return true; });
	}
}
