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

		public string WPITitleEnglish {
			get { return (string)GetValue(WPITitleEnglishProperty); }
			set { SetValue(WPITitleEnglishProperty, value); }
		}

		public static readonly DependencyProperty WPITitleEnglishProperty = DependencyProperty.Register("WPITitleEnglish", typeof(string), typeof(WrapPanelItem), new PropertyMetadata(null));

		public string WPITitleRomaji {
			get { return (string)GetValue(WPITitleRomajiProperty); }
			set { SetValue(WPITitleRomajiProperty, value); }
		}

		public static readonly DependencyProperty WPITitleRomajiProperty = DependencyProperty.Register("WPITitleRomaji", typeof(string), typeof(WrapPanelItem), new PropertyMetadata(null));

		public string WPITitleNative {
			get { return (string)GetValue(WPITitleNativeProperty); }
			set { SetValue(WPITitleNativeProperty, value); }
		}

		public static readonly DependencyProperty WPITitleNativeProperty = DependencyProperty.Register("WPITitleNative", typeof(string), typeof(WrapPanelItem), new PropertyMetadata(null));
	}
}
