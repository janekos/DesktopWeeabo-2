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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
