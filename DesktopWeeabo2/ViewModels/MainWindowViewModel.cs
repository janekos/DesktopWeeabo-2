using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {
	class MainWindowViewModel : BaseViewModel {

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

		private string _ToastBorderColor = "";
		public string ToastBorderColor {
			get { return _ToastBorderColor; }
			set {
				if (_ToastBorderColor != value) {
					_ToastBorderColor = value;
					RaisePropertyChanged("ToastBorderColor");
				}
			}
		}

		private string _ToastBackgroundColor = "";
		public string ToastBackgroundColor {
			get { return _ToastBackgroundColor; }
			set {
				if (_ToastBackgroundColor != value) {
					_ToastBackgroundColor = value;
					RaisePropertyChanged("ToastBackgroundColor");
				}
			}
		}

		private string _ToastTextColor = "";
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

		public MainWindowViewModel() {
			ViewModels = new ObservableCollection<BaseViewModel>(){
				new AnimeViewModel(),
				new MangaViewModel(),
				new SettingsViewModel()
			};
			ViewModelsView = CollectionViewSource.GetDefaultView(ViewModels);

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


		public DelegateCommand ChangeView => new DelegateCommand(
		new Action<object>(
		(e) => {
			if (ViewModelsView != null) {
				switch (e) {
					case "AnimeView":
						ViewModelsView.MoveCurrentToPosition(0);
						break;
					case "MangaView":
						ViewModelsView.MoveCurrentToPosition(1);
						break;
					case "SettingsView":
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
