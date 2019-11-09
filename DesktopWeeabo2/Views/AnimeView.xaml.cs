using DesktopWeeabo2.ViewModels;
using System.Windows.Controls;
using Unity;

namespace DesktopWeeabo2.Views {

	public partial class AnimeView : UserControl {

		[Dependency]
		public AnimeViewModel ViewModel {
			set { DataContext = value; }
		}

		public AnimeView() {
			InitializeComponent();
		}
	}
}