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
            Task.Run(() => {
                string[] animeStatus = { "Towatch", "Viewed", "Watching", "Dropped"};
                string[] mangaStatus = { "Toread", "Red", "Reading", "Dropped"};
                string[] names = {"Joyce", "Harriette", "Marylee", "Dagny", "Shawana", "Dorris", "Samira", "Brigette", "Lael", "Ignacia", "Cristobal", "Leah", "Antonio", "Brendon", "Kati", "Hilaria", "Harry", "Silvia", "Elaina", "Johnathan"};

                using (var db = new EntriesContext()) {
                    var ae = db.Set<AnimeModel>();
                    var me = db.Set<MangaModel>();

                    for (var i = 0; i < 5000; i++) {
                        System.Diagnostics.Debug.WriteLine("added " + i);

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
                            TitleEnglish = names[i%20],
                            TitleRomaji = names[i % 20],
                            TitleNative = names[i % 20],
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            CoverImage = "coverImage",
                            ViewingStatus = animeStatus[i%4],
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
                            TitleEnglish = names[i % 20],
                            TitleRomaji = names[i % 20],
                            TitleNative = names[i % 20],
                            CoverImage = "coverImage",
                            ReadingStatus = mangaStatus[i%4],
                            PersonalReview = "personalReview",
                            DateAdded = DateTime.Now
                        });

                    }
                    db.SaveChanges();

                }
            });
        }
    }
}
