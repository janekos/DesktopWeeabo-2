using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.Models;
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
    class MangaViewModel : BaseItemViewModel {

        public ObservableCollection<MangaModel> MangaItems { get; set; } = new ObservableCollection<MangaModel>();

        private MangaAPIEnumerator mae = new MangaAPIEnumerator("", false, "");
        private MangaModel _SelectedItem = null;

        public MangaModel SelectedItem {
            get { return _SelectedItem; }
            set {
                if (_SelectedItem != value) {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        public MangaViewModel() : base() {
            BindingOperations.EnableCollectionSynchronization(MangaItems, _CollectionLock);
        }

        protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "SearchText":

                    RenewView();

                    if (_CurrentView.Equals(StatusView.Online)) { AddOnlineItemsToView(); }
                    else { AddLocalItemsToView(); }
                    break;

                case "SelectedItem":
                    if (_SelectedItem != null) {
                        System.Diagnostics.Debug.WriteLine(_SelectedItem.Chapters);
                    }
                    break;

                default:
                    break;
            }
        }

        protected override void RenewView() {
            MangaItems.Clear();
            _SelectedItem = null;
            TotalItems = 0;
        }

        protected override void AddLocalItemsToView() {

            lock (_CollectionLock) {
                Task.Run(() => {
                    bool search = false;
                    if (_SearchText.Length > 0) { search = true; }

                    using (var db = new EntriesContext()) {

                        IQueryable entries;

                        if (search) { entries = db.MangaItems.Where(entry => entry.ReadingStatus.Equals(_CurrentView) && (entry.TitleEnglish.ToLower().Contains(_SearchText) || entry.TitleRomaji.ToLower().Contains(_SearchText) || entry.TitleNative.ToLower().Contains(_SearchText))); }
                        else { entries = db.MangaItems.Where(entry => entry.ReadingStatus.Equals(_CurrentView)); }

                        foreach (MangaModel item in entries) { MangaItems.Add(item); TotalItems += 1; }
                    }
                });
            };
        }

        protected override async void AddOnlineItemsToView() {

            mae.SearchString = _SearchText;

            try {
                foreach (var item in await mae.GetCurrentSet()) {
                    MangaItems.Add(item);
                    TotalItems += 1;
                }

                while (mae.TryMoveToNextSet()) {
                    foreach (var item in await mae.GetCurrentSet()) {
                        MangaItems.Add(item);
                        TotalItems += 1;
                    }
                }

            }
            catch (ArgumentNullException ex) {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

        }

    }
}
