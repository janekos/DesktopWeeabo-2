using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;

namespace DesktopWeeabo2.Views {
	public partial class MangaView : UserControl {
        public MangaView() {
            DataContext = new MangaViewModel();
            InitializeComponent();
        }
    }
}
