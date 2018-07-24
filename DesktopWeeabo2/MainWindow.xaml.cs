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

namespace DesktopWeeabo2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load db and wait for user input
            InitAppData.init();
            test();            
        }

        private async void test()
        {
            /*string a = await APIQueries.QueryAPI("oreimo", 1, true);
            Console.WriteLine(a);
            DataHandler.ParseAnimeObjects(a);*/

            using (var db = new EntityContext())
            {
                var ae = db.Set<AnimeEntity>();
                ae.Add(new AnimeEntity {
                    id = 1,
                    meanScore = 1,
                    episodes = 1,
                    duration = 1,
                    personalScore = 1,
                    watchPriority = 1,
                    rewatchCount = 1,
                    currentEpisode = 1,
                    type = "foo",
                    format = "foo",
                    status = "foo",
                    description = "foo",
                    genres = "foo",
                    synonyms = "foo",
                    titleEnglish = "foo",
                    titleRomaji = "foo",
                    titleNative = "foo",
                    startDate = "foo",
                    endDate = "foo",
                    coverImage = "foo",
                    viewingStatus = "foo",
                    personalReview = "foo",
                });

                db.SaveChanges();
            };

    }

        private void ChangeView(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((sender as Button).Content);
        }

        private void GridView(object sender, RoutedEventArgs e)
        {
            if ((bool) gridViewChanger.IsChecked)
            {
                itemsDescription.Visibility = Visibility.Hidden;
                itemsContainerGrid.Margin = new Thickness(0);
                itemsContainer.Children.OfType<Rectangle>().ToList().ForEach(r => {
                    r.Width = itemsContainer.ActualWidth + 300;
                });
            }
            else
            {
                itemsDescription.Visibility = Visibility.Visible;
                itemsContainerGrid.Margin = new Thickness(0,0,300,0);
                itemsContainer.Children.OfType<Rectangle>().ToList().ForEach(r => {
                    r.Width = 100;
                });
            }
        }
    }
}
