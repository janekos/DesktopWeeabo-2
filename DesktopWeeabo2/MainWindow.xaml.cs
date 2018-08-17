using DesktopWeeabo2.data;
using DesktopWeeabo2.data.db;
using DesktopWeeabo2.data.db.entities;
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
using System.Collections.ObjectModel;
using DesktopWeeabo2.custom;

namespace DesktopWeeabo2 {
    public partial class MainWindow : Window {

        private bool hasSelected = false;
        private DateTime lastSearch;
        ObservableCollection<AnimeEntity> objects { get; set; }
        AnimeAPIEnumerator aae = new AnimeAPIEnumerator();

        public MainWindow() {
            InitializeComponent();
            //load db and wait for user input
            InitAppData.init();
            objects = new ObservableCollection<AnimeEntity>();
            itemsContainer.ItemsSource = objects;
            test();
        }

        private async void test() {
            /*AnimeAPIEnumerator aae = new AnimeAPIEnumerator("na", true, "");
            await Task.Run(() => {
                    System.Diagnostics.Debug.WriteLine("first task start");
                foreach (var item in aae.getCurrentSet().Result) {
                    Dispatcher.Invoke(() => objects.Add(item));
                }
                    System.Diagnostics.Debug.WriteLine("first task end");
            });

            if (aae.tryMoveToNextSet()) {
                await Task.Run(() => {
                    System.Diagnostics.Debug.WriteLine("second task start");
                    foreach (var item in aae.getCurrentSet().Result) {
                        Dispatcher.Invoke(() => objects.Add(item));
                    }
                    System.Diagnostics.Debug.WriteLine("second task end");
                });
            }

            /*Task.Run(() => {
                //foreach (AnimeEntity objekt in APIDataHandler.convertAnimeObjectToEntity(APIDataHandler.parseAndHandleAnimeObjects("na").Result)) {
                foreach (AnimeEntity objekt in ) {
                    try {
                        Dispatcher.Invoke(() => objects.Add(objekt));
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                }
            });*/
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
                for (int i = 0; i < itemsContainer.Items.Count; i++) {
                    (itemsContainer.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter).Width = itemsContainer.ActualWidth + 300;
                }
            }
            else {
                itemsDescription.Visibility = Visibility.Visible;
                itemsContainerGrid.Margin = new Thickness(0, 0, 300, 0);
                for (int i = 0; i < itemsContainer.Items.Count; i++) {
                    (itemsContainer.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter).Width = 250;
                }
            }
        }

        private void Item_MouseEnter(object sender, MouseEventArgs e) {
            if (!hasSelected) {

            }
        }

        private void Item_Selected(object sender, MouseButtonEventArgs e) {
            if (hasSelected) {
                hasSelected = false;
            }
            else {
                hasSelected = true;
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e) {
            DateTime now = DateTime.Now;

            if (now > lastSearch.AddSeconds(2)) {
                System.Diagnostics.Debug.WriteLine("object count: "+objects.Count);
                for (var i = 0; i < objects.Count; i++) { objects.RemoveAt(i); System.Diagnostics.Debug.WriteLine(i); }
                System.Diagnostics.Debug.WriteLine("object count after: "+objects.Count);

                if (searchBox.Text.Length == 0) { return; }
                else { aae.searchString = searchBox.Text; }
                aae.type = true;
                aae.sortBy = "";

                Task.Run(() => {
                    foreach (var item in aae.getCurrentSet().Result) {
                        Dispatcher.Invoke(() => objects.Add(item));
                    }
                    System.Diagnostics.Debug.WriteLine(aae.searchString);
                    System.Diagnostics.Debug.WriteLine("items: "+aae.totalItems);
                });

                lastSearch = now;
            }
        }
    }
}
