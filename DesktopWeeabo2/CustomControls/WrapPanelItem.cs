using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopWeeabo2.CustomControls
{
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
