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
using System.Data.Entity.Core.EntityClient;
using DesktopWeeabo2.Data.Repositories;

namespace DesktopWeeabo2 {
    public partial class MainWindow : Window {

        public MainWindow() {
			InitializeComponent();
            InitAppData.Init();
            DataContext = new MainWindowViewModel();
        }

		private async void Window_ContentRendered(object sender, EventArgs e) {
			await InitAppData.WakeDB();
			StartingLoader.IsHitTestVisible = false;
		}
	}
}
