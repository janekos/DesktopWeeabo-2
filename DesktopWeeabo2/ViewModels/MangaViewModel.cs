using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {
    class MangaViewModel : BaseItemViewModel {

        //private EntriesContext db;
        public ObservableCollection<MangaModel> MangaItems { get; set; } = new ObservableCollection<MangaModel>();

        private MangaAPIEnumerator mae = new MangaAPIEnumerator("", false, "");

        public MangaViewModel() : base() {
            BindingOperations.EnableCollectionSynchronization(MangaItems, _CollectionLock);
        }

        protected override void RenewView() {
            MangaItems.Clear();
            TotalItems = 0;
        }

        protected override void AddLocalItems() {

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

        protected override async void AddOnlineItems() {

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
