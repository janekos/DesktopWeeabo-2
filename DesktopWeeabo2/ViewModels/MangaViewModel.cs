using DesktopWeeabo2.Data;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.ViewModels {
    class MangaViewModel : BaseViewModel {

        //private EntriesContext db;
        public ObservableCollection<MangaModel> MangaItems { get; set; }

        public MangaViewModel() {
            MangaItems = new ObservableCollection<MangaModel>();

            using (var db = new EntriesContext()) {
                foreach (MangaModel item in db.MangaItems) {
                    MangaItems.Add(item);
                }
            }
        }
    }
}
