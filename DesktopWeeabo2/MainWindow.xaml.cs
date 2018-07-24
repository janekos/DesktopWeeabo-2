using DesktopWeeabo2.data;
using DesktopWeeabo2.data.db;
using DesktopWeeabo2.data.db.entities;
using DesktopWeeabo2.data.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopWeeabo2.Properties;

namespace DesktopWeeabo2 {
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            //load db and wait for user input
            InitAppData.init();
            test();
        }

        private async void test() {
            string a = await APIQueries.search("na");
            //List<AnimeObject> aes = new List<AnimeObject>();

            //System.Diagnostics.Debug.WriteLine(a);

            APIDataHandler.parseAnimeObjects(a);

            /*using (var db = new EntityContext()) {

                db.Set<AnimeEntity>().Add(new AnimeEntity { id = 1 });
                db.SaveChanges();
            };*/

        }

        /*
        private void saveEntry_click(o e, re ea){
            //notify and update view
            //add entry to db -- entry will be of type Anime(Manga)Object and needs to be converted to db style

            var ae = db.Set<AnimeEntity>();
                var date = new DateObject {
                    day = 23,
                    month = 10,
                    year = 2000
                };
                ae.Add(new AnimeEntity {
                    id = 4,
                    meanScore = 4,
                    episodes = 5,
                    duration = 6,
                    personalScore = 7,
                    watchPriority = 8,
                    rewatchCount = 9,
                    currentEpisode = 10,
                    type = "type",
                    format = "format",
                    status = "status",
                    description = "description",
                    genres = "genres",
                    synonyms = "synonyms",
                    titleEnglish = "titleEnglish",
                    titleRomaji = "titleRomaji",
                    titleNative = "titleNative",
                    startDate = date.getDateTime(),
                    endDate = date.getDateTime(),
                    coverImage = "coverImage",
                    viewingStatus = "viewingStatus",
                    personalReview = "personalReview",
                    date_added = date.getDateTime()
                });

        db.SaveChanges();

        }

        private void updateViewingStatus_click(o e, re ea){
            //notify and update view
            //update entry in db -- just change view, depending on status add other vars (personal review/score etc.)
        }
             */

        private void ChangeView(object sender, RoutedEventArgs e) {
            Console.WriteLine((sender as Button).Content);
        }

        private void GridView(object sender, RoutedEventArgs e) {
            if ((bool)gridViewChanger.IsChecked) {
                itemsDescription.Visibility = Visibility.Hidden;
                itemsContainerGrid.Margin = new Thickness(0);
                itemsContainer.Children.OfType<Rectangle>().ToList().ForEach(r => {
                    r.Width = itemsContainer.ActualWidth + 300;
                });
            }
            else {
                itemsDescription.Visibility = Visibility.Visible;
                itemsContainerGrid.Margin = new Thickness(0, 0, 300, 0);
                itemsContainer.Children.OfType<Rectangle>().ToList().ForEach(r => {
                    r.Width = 100;
                });
            }
        }
    }
}
