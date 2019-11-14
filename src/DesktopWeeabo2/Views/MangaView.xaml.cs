using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;
using Unity;

namespace DesktopWeeabo2.Views {

	public partial class MangaView : UserControl {

		[Dependency]
		public MangaViewModel ViewModel {
			set { DataContext = value; }
		}

		public MangaView() {
			InitializeComponent();
		}
	}
}