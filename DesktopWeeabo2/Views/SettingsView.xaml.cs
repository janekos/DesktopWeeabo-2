using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;

namespace DesktopWeeabo2.Views {
	public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
			DataContext = new SettingsViewModel();
            InitializeComponent();
        }
    }
}
