using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;

namespace DesktopWeeabo2.Views {
	public partial class AnimeView : UserControl {
        public AnimeView() {
            DataContext = new AnimeViewModel();
            InitializeComponent();
        }
	}
}
