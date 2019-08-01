using DesktopWeeabo2.ViewModels;
using System.Windows;

namespace DesktopWeeabo2 {
	public partial class MainWindow : Window {
        public MainWindow() {
            DataContext = new MainWindowViewModel();
			InitializeComponent();
        }
	}
}
