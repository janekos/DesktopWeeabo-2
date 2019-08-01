using System.Windows;
using System.Windows.Controls;

namespace DesktopWeeabo2.CustomControls {
	public class WrapPanelItem : Control
    {
        static WrapPanelItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WrapPanelItem), new FrameworkPropertyMetadata(typeof(WrapPanelItem)));
        }

		public string WPIImage {
			get { return (string)GetValue(WPIImageProperty); }
			set { SetValue(WPIImageProperty, value); }
		}

		public static readonly DependencyProperty WPIImageProperty = DependencyProperty.Register("WPIImage", typeof(string), typeof(WrapPanelItem), new PropertyMetadata(null));

		public string WPITitle{
			get { return (string)GetValue(WPITitleProperty); }
			set { SetValue(WPITitleProperty, value); }
		}

		public static readonly DependencyProperty WPITitleProperty = DependencyProperty.Register("WPITitle", typeof(string), typeof(WrapPanelItem), new PropertyMetadata(null));
	}
}
