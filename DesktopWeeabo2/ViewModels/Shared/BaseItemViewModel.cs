using DesktopWeeabo2.Models;
using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.ViewModels.Shared {
    public abstract class BaseItemViewModel : BaseViewModel {

        protected readonly object _CollectionLock = new object();

		protected bool _APIEnumeratorHasNextPage { get; set; } = false;
		public bool APIEnumeratorHasNextPage {
			get { return _APIEnumeratorHasNextPage; }
			set { if (_APIEnumeratorHasNextPage != value) { _APIEnumeratorHasNextPage = value; RaisePropertyChanged("APIEnumeratorHasNextPage"); } }
		}

		protected int _TotalItems { get; set; } = 0;
        public int TotalItems {
            get { return _TotalItems; }
            set { if (_TotalItems != value) { _TotalItems = value; RaisePropertyChanged("TotalItems"); }}
        }

        protected bool _IsContentLoading { get; set; } = false;
		public bool IsContentLoading {
			get { return _IsContentLoading; }
			set { if (_IsContentLoading != value) { _IsContentLoading = value; RaisePropertyChanged("IsContentLoading"); }}
		}

        protected string _CurrentView { get; set; } = StatusView.Online;
		public string CurrentView{
            get { return _CurrentView; }
            set { if (_CurrentView != value) { _CurrentView = value; RaisePropertyChanged("CurrentView"); }}
        }

        protected string _SearchText { get; set; } = "";
        public string SearchText {
            get { return _SearchText; }
            set { if (_SearchText != value) { _SearchText = (value as string).ToLower(); RaisePropertyChanged("SearchText"); }}
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

        protected virtual void RenewView() {
            throw new NotImplementedException("RenewView has to be implemented in ViewModel.");
        }

        protected virtual void AddLocalItemsToView() {
            throw new NotImplementedException("AddLocalItems has to be implemented in ViewModel.");
        }
    }
}
