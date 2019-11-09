using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;
using Unity;

namespace DesktopWeeabo2.Views {

	public partial class SettingsView : UserControl {

		[Dependency]
		public SettingsViewModel ViewModel {
			set { DataContext = value; }
		}

		public SettingsView() {
			InitializeComponent();
		}
	}
}