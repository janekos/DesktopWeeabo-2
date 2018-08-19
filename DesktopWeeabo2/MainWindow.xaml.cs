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
using DesktopWeeabo2.Models;
using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.ViewModels;

namespace DesktopWeeabo2 {
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            //load db and wait for user input
            InitAppData.Init();
            DataContext = new MainWindowViewModel();
            //test();
        }


        private void test() {
            for (var i = 0; i< 100; i++) {
                using (var db = new EntriesContext()) {
                    var ae = db.Set<AnimeModel>();
                    var me = db.Set<MangaModel>();
                    ae.Add(new AnimeModel {
                        Id = i,
                        MeanScore = 4,
                        Episodes = 5,
                        Duration = 6,
                        PersonalScore = 7,
                        WatchPriority = 8,
                        RewatchCount = 9,
                        CurrentEpisode = 10,
                        Type = "Anime",
                        Format = "format",
                        Status = "status",
                        Description = "description",
                        Genres = "genres",
                        Synonyms = "synonyms",
                        TitleEnglish = "titleEnglish",
                        TitleRomaji = "titleRomaji",
                        TitleNative = "titleNative",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        CoverImage = "coverImage",
                        ViewingStatus = "viewingStatus",
                        PersonalReview = "personalReview",
                        DateAdded = DateTime.Now
                    });
                    me.Add(new MangaModel {
                        Id = i,
                        MeanScore = 4,
                        Volumes = 5,
                        Chapters = 6,
                        PersonalScore = 7,
                        ReadPriority = 8,
                        RereadCount = 9,
                        CurrentChapter = 10,
                        Type = "manga",
                        Format = "format",
                        Status = "status",
                        Description = "description",
                        Genres = "genres",
                        Synonyms = "synonyms",
                        TitleEnglish = "titleEnglish",
                        TitleRomaji = "titleRomaji",
                        TitleNative = "titleNative",
                        CoverImage = "coverImage",
                        ReadingStatus = "readingStatus",
                        PersonalReview = "personalReview",
                        DateAdded = DateTime.Now
                    });

                    db.SaveChanges();
                }

            }

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

            Task.Run(() => {
                //foreach (AnimeEntity objekt in APIDataHandler.convertAnimeObjectToEntity(APIDataHandler.parseAndHandleAnimeObjects("na").Result)) {
                foreach (AnimeEntity objekt in ) {
                    try {
                        Dispatcher.Invoke(() => objects.Add(objekt));
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                }
            });
            using (var db = new EntityContext()) {

                db.Set<AnimeEntity>().Add(new AnimeEntity { id = 1 });
                db.SaveChanges();
            };*/

        }

        /*
        private void saveEntry_click(o e, re ea) {
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

        private void updateViewingStatus_click(o e, re ea) {
            //notify and update view
            //update entry in db -- just change view, depending on status add other vars (personal review/score etc.)
        }


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
            searchString = searchBox.Text;
            System.Diagnostics.Debug.WriteLine(searchString);
            DateTime now = DateTime.Now;

            if (now > lastSearch.AddSeconds(2)) {
                System.Diagnostics.Debug.WriteLine("object count: " + objects.Count);
                for (var i = 0; i < objects.Count; i++) { objects.RemoveAt(i); System.Diagnostics.Debug.WriteLine(i); }
                System.Diagnostics.Debug.WriteLine("object count after: " + objects.Count);

                if (searchBox.Text.Length == 0) { return; }
                else { aae.searchString = searchBox.Text; }
                aae.type = true;
                aae.sortBy = "";

                Task.Run(() => {
                    foreach (var item in aae.getCurrentSet().Result) {
                        Dispatcher.Invoke(() => objects.Add(item));
                    }
                    System.Diagnostics.Debug.WriteLine(aae.searchString);
                    System.Diagnostics.Debug.WriteLine("items: " + aae.totalItems);
                });

                lastSearch = now;
            }
        }*/
    }
}
