using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.ViewModels.Shared {
    public abstract class BaseItemViewModel : BaseViewModel {

        protected int _TotalItems { get; set; } = 0;
        protected string _SearchText { get; set; } = "";
        protected string _CurrentView { get; set; } = "Online";
        protected readonly object _CollectionLock = new object();

        public int TotalItems {
            get { return _TotalItems; }
            set {
                if (_TotalItems != value) {
                    _TotalItems = value;
                    RaisePropertyChanged("TotalItems");
                }
            }
        }

        public string SearchText {
            get { return _SearchText; }
            set {
                if (_SearchText != value) {
                    _SearchText = (value as string).ToLower();
                    RaisePropertyChanged("SearchText");
                }
            }
        }

        public BaseItemViewModel() {
            PropertyChanged += Property_Changed;
        }

        protected virtual void Property_Changed(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "SearchText":

                    RenewView();

                    if (_CurrentView.Equals(StatusView.Online)) { AddOnlineItems(); }
                    else { AddLocalItems(); }
                    break;

                default:
                    break;
            }
        }

        public DelegateCommand ChangeItemSource => new DelegateCommand(
            new Action<object>((e) => {

                _CurrentView = e as string;
                RenewView();

                if (e.Equals(StatusView.Online)) { AddOnlineItems(); }
                else { AddLocalItems(); }
            }),
            (e) => { return true; }
        );

        protected virtual void RenewView() {
            throw new NotImplementedException("RenewView has to be implemented in ViewModel.");
        }

        protected virtual void AddLocalItems() {
            throw new NotImplementedException("AddLocalItems has to be implemented in ViewModel.");
        }

        protected virtual void AddOnlineItems() {
            throw new NotImplementedException("AddOnlineItems has to be implemented in ViewModel.");
        }
    }
}
