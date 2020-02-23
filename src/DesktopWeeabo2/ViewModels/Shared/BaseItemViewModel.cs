using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Helpers;
using System;
using System.ComponentModel;

namespace DesktopWeeabo2.ViewModels.Shared {

	public abstract class BaseItemViewModel : BaseViewModel {
		protected bool DontTriggerSearchChanged = false;
		protected readonly object _CollectionLock = new object();

		#region search model

		public SearchModel SearchModel { get; set; } = new SearchModel();

		public SortObject SelectedSort {
			get { return SearchModel.SelectedSort; }
			set {
				if (SearchModel.SelectedSort != value) {
					SearchModel.SelectedSort = value;
					RaisePropertyChanged("SelectedSort");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public bool IsDescending {
			get { return SearchModel.IsDescending; }
			set {
				if (SearchModel.IsDescending != value) {
					SearchModel.IsDescending = value;
					RaisePropertyChanged("IsDescending");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public bool IsAdult {
			get { return SearchModel.IsAdult; }
			set {
				if (SearchModel.IsAdult != value) {
					SearchModel.IsAdult = value;
					RaisePropertyChanged("IsAdult");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public string SearchText {
			get { return SearchModel.SearchText; }
			set {
				if (SearchModel.SearchText != value) {
					SearchModel.SearchText = value.ToLower();
					RaisePropertyChanged("SearchText");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public GenreObject[] SearchGenres {
			get { return SearchModel.GenresList; }
			set {
				if (SearchModel.GenresList != value) {
					SearchModel.GenresList = value;
					RaisePropertyChanged("SearchGenres");
				}
			}
		}

		#endregion search model

		#region api vars
		
		protected int _APICurrentPage { get; set; } = 1;

		public int APICurrentPage {
			get { return _APICurrentPage; }
			set { if (_APICurrentPage != value) { _APICurrentPage = value; RaisePropertyChanged("APICurrentPage"); } }
		}

		protected string _TotalAPIItems { get; set; } = "";

		public string TotalAPIItems {
			get { return _TotalAPIItems; }
			set { if (_TotalAPIItems != value) { _TotalAPIItems = value; RaisePropertyChanged("TotalAPIItems"); } }
		}

		protected int _TotalAPIPages { get; set; } = 1;

		public int TotalAPIPages {
			get { return _TotalAPIPages; }
			set { if (_TotalAPIPages != value) { _TotalAPIPages = value; RaisePropertyChanged("TotalAPIPages"); } }
		}

		protected bool _IsAdvancedVisible { get; set; } = false;

		#endregion api vars

		private string _SelectedGenres { get; set; }

		public string SelectedGenres {
			get { return _SelectedGenres; }
			set {
				if (_SelectedGenres != value) {
					_SelectedGenres = value;
					RaisePropertyChanged("SelectedGenres");
				}
			}
		}

		public bool IsAdvancedVisible {
			get { return _IsAdvancedVisible; }
			set { if (_IsAdvancedVisible != value) { _IsAdvancedVisible = value; RaisePropertyChanged("IsAdvancedVisible"); } }
		}

		protected int _TotalItems { get; set; } = 0;

		public int TotalItems {
			get { return _TotalItems; }
			set { if (_TotalItems != value) { _TotalItems = value; RaisePropertyChanged("TotalItems"); } }
		}

		protected bool _IsContentLoading { get; set; } = false;

		public bool IsContentLoading {
			get { return _IsContentLoading; }
			set { if (_IsContentLoading != value) { _IsContentLoading = value; RaisePropertyChanged("IsContentLoading"); } }
		}

		protected string _CurrentView { get; set; } = StatusView.ONLINE;

		public string CurrentView {
			get { return _CurrentView; }
			set { if (_CurrentView != value) { _CurrentView = value; RaisePropertyChanged("CurrentView"); } }
		}

		protected string _PressedTransferButton { get; set; } = "";

		public string PressedTransferButton {
			get { return _PressedTransferButton; }
			set { if (_PressedTransferButton != value) { _PressedTransferButton = value; RaisePropertyChanged("PressedTransferButton"); } }
		}

		protected BaseItemViewModel() {
			PropertyChanged += Property_Changed;
		}

		public DelegateCommand TriggerViewStatusWasChanged => new DelegateCommand(new Action<object>(e => PressedTransferButton = e as string));

		public DelegateCommand SetChecked => new DelegateCommand(new Action(() => {
			SelectedGenres = Utilities.GenreHelper(SearchModel.GenresList);
			RaisePropertyChanged("SearchChanged");
		}));

		protected abstract void Property_Changed(object sender, PropertyChangedEventArgs e);

		protected abstract void RenewView(bool isActionOnline = false);

		protected abstract void AddLocalItemsToView();
	}
}