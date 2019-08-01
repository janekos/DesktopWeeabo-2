using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.ComponentModel;

namespace DesktopWeeabo2.ViewModels.Shared {
	public abstract class BaseItemViewModel : BaseViewModel {

		protected bool DontTriggerSearchChanged = false;
        protected readonly object _CollectionLock = new object();

		public SearchModel SearchModel { get; set; } = new SearchModel();

		public SortObject SelectedSort {
			get { return SearchModel.SelectedSort; }
			set { if (SearchModel.SelectedSort != value) {
					SearchModel.SelectedSort = value;
					RaisePropertyChanged("SelectedSort");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public bool IsDescending {
			get { return SearchModel.IsDescending; }
			set { if (SearchModel.IsDescending != value) {
					SearchModel.IsDescending = value;
					RaisePropertyChanged("IsDescending");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public bool IsAdult {
			get { return SearchModel.IsAdult; }
			set { if (SearchModel.IsAdult != value) {
					SearchModel.IsAdult = value;
					RaisePropertyChanged("IsAdult");
					RaisePropertyChanged("SearchChanged");
				}
			}
		}

		public string SearchText {
			get { return SearchModel.SearchText; }
			set { if (SearchModel.SearchText != value) {
					SearchModel.SearchText = (value as string).ToLower();
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

		protected bool _APIHasNextPage { get; set; } = false;
		public bool APIHasNextPage {
			get { return _APIHasNextPage; }
			set { if (_APIHasNextPage != value) { _APIHasNextPage = value; RaisePropertyChanged("APIHasNextPage"); } }
		}

		protected int _APICurrentPage { get; set; } = 1;
		public int APICurrentPage {
			get { return _APICurrentPage; }
			set { if (_APICurrentPage != value) { _APICurrentPage = value; RaisePropertyChanged("APICurrentPage"); } }
		}

		protected bool _IsAdvancedVisible { get; set; } = false;
		public bool IsAdvancedVisible {
			get { return _IsAdvancedVisible; }
			set { if (_IsAdvancedVisible != value) { _IsAdvancedVisible = value; RaisePropertyChanged("IsAdvancedVisible"); } }
		}

		protected int _TotalItems { get; set; } = 0;
        public int TotalItems {
            get { return _TotalItems; }
            set { if (_TotalItems != value) { _TotalItems = value; RaisePropertyChanged("TotalItems"); }}
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

		protected bool _IsContentLoading { get; set; } = false;
		public bool IsContentLoading {
			get { return _IsContentLoading; }
			set { if (_IsContentLoading != value) { _IsContentLoading = value; RaisePropertyChanged("IsContentLoading"); }}
		}

		protected string _CurrentView { get; set; } = StatusView.ONLINE;
		public string CurrentView{
            get { return _CurrentView; }
            set { if (_CurrentView != value) { _CurrentView = value; RaisePropertyChanged("CurrentView"); }}
        }

        protected string _PressedTransferButton { get; set; } = "";
		public string PressedTransferButton {
            get { return _PressedTransferButton; }
            set { if (_PressedTransferButton != value) { _PressedTransferButton = value as string; RaisePropertyChanged("PressedTransferButton"); }}
        }

		public BaseItemViewModel() { PropertyChanged += Property_Changed; }

        protected virtual void Property_Changed(object sender, PropertyChangedEventArgs e) {
            throw new NotImplementedException("Property_Changed has to be implemented in ViewModel.");
        }

        protected virtual void RenewView(bool isOnline = false) {
            throw new NotImplementedException("RenewView has to be implemented in ViewModel.");
        }

        protected virtual void AddLocalItemsToView() {
            throw new NotImplementedException("AddLocalItems has to be implemented in ViewModel.");
        }

		public DelegateCommand TriggerViewStatusWasChanged => new DelegateCommand(new Action<object>(e => PressedTransferButton = e as string));

		public DelegateCommand SetChecked => new DelegateCommand(new Action(() => {
			SelectedGenres = StringHelpers.GenreHelper(SearchModel.GenresList);
			RaisePropertyChanged("SearchChanged");
		}));
	}
}
