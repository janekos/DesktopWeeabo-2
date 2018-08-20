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
    class AnimeViewModel : BaseItemViewModel {

        private AnimeAPIEnumerator aae = new AnimeAPIEnumerator("", true, "");

        public ObservableCollection<AnimeModel> AnimeItems { get; set; } = new ObservableCollection<AnimeModel>();
        
        public AnimeViewModel() : base() {            
            BindingOperations.EnableCollectionSynchronization(AnimeItems, _CollectionLock);
        }

        protected override void RenewView() {
            AnimeItems.Clear();
            TotalItems = 0;
        }        

        protected override void AddLocalItems() {

            lock (_CollectionLock) {
                Task.Run(() => {
                    bool search = false;
                    if (_SearchText.Length > 0) { search = true; }

                    using (var db = new EntriesContext()) {

                        IQueryable entries;

                        if (search) { entries = db.AnimeItems.Where(entry => entry.ViewingStatus.Equals(_CurrentView) && (entry.TitleEnglish.ToLower().Contains(_SearchText) || entry.TitleRomaji.ToLower().Contains(_SearchText) || entry.TitleNative.ToLower().Contains(_SearchText))); }
                        else { entries = db.AnimeItems.Where(entry => entry.ViewingStatus.Equals(_CurrentView)); }
                        
                        foreach (AnimeModel item in entries) { AnimeItems.Add(item); TotalItems += 1; }
                    }
                });
            };
        }

        protected override async void AddOnlineItems() {

            aae.SearchString = _SearchText;

            try {
                foreach (var item in await aae.GetCurrentSet()) {
                    AnimeItems.Add(item);
                    TotalItems += 1;
                }

                while (aae.TryMoveToNextSet()) {
                    foreach (var item in await aae.GetCurrentSet()) {
                        AnimeItems.Add(item);
                        TotalItems += 1;
                    }
                }

            }catch(ArgumentNullException ex) {
                //all is good
            }

        }
    }
}
