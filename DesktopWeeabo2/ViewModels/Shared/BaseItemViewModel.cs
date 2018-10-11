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

        protected int _TotalItems { get; set; } = 0;
        protected string _SearchText { get; set; } = "";
        protected string _CurrentView { get; set; } = StatusView.Online;
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

        public string CurrentView{
            get { return _CurrentView; }
            set {
                if (_CurrentView != value) {
                    _CurrentView = value;
                    RaisePropertyChanged("CurrentView");
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

        public DelegateCommand ChangeItemSource => new DelegateCommand(
            new Action<object>((e) => {

                _CurrentView = e as string;
                RenewView();

                if (e.Equals(StatusView.Online)) { AddOnlineItemsToView(); }
                else { AddLocalItemsToView(); }
            }),
            (e) => { return true; }
        );

        protected virtual void Property_Changed(object sender, PropertyChangedEventArgs e) {
            throw new NotImplementedException("Property_Changed has to be implemented in ViewModel.");
        }

        protected virtual void RenewView() {
            throw new NotImplementedException("RenewView has to be implemented in ViewModel.");
        }

        protected virtual void AddLocalItemsToView() {
            throw new NotImplementedException("AddLocalItems has to be implemented in ViewModel.");
        }

        protected virtual void AddOnlineItemsToView() {
            throw new NotImplementedException("AddOnlineItems has to be implemented in ViewModel.");
        }

        protected virtual void DeleteItemFromDb() {
            throw new NotImplementedException("DeleteItemFromDb has to be implemented in ViewModel.");
        }
    }
}
