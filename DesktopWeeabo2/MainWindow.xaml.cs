using DesktopWeeabo2.ViewModels;
using System.Windows;
using Unity;

namespace DesktopWeeabo2 {

	public partial class MainWindow : Window {

		[Dependency]
		public MainWindowViewModel ViewModel {
			set { DataContext = value; }
		}

		public MainWindow() {
			InitializeComponent();
		}
	}
}