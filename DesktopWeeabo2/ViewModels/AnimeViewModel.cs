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
    class AnimeViewModel : BaseViewModel{

        //private EntriesContext db;
        public ObservableCollection<AnimeModel> AnimeItems { get; set; }

        public AnimeViewModel() {

            AnimeItems = new ObservableCollection<AnimeModel>();

            using (var db = new EntriesContext()) {
                foreach (AnimeModel item in db.AnimeItems) {
                    AnimeItems.Add(item);
                }
            }
                

        }
    }
}
