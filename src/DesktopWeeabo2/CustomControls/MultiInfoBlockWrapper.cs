using System.Windows;
using System.Windows.Controls;

namespace DesktopWeeabo2.CustomControls {

	public class MultiInfoBlockWrapper : ItemsControl {

		static MultiInfoBlockWrapper() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiInfoBlockWrapper), new FrameworkPropertyMetadata(typeof(MultiInfoBlockWrapper)));
		}
	}
}