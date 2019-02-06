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
    public class InfoBoxTextBlock : Control
    {
        static InfoBoxTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoBoxTextBlock), new FrameworkPropertyMetadata(typeof(InfoBoxTextBlock)));
        }

		public object IBTBDynamicItem {
			get { return (object)GetValue(IBTBDynamicItemProperty); }
			set { SetValue(IBTBDynamicItemProperty, value); }
		}

		public static readonly DependencyProperty IBTBDynamicItemProperty = DependencyProperty.Register("IBTBDynamicItem", typeof(object), typeof(InfoBoxTextBlock), new PropertyMetadata(null));

		public string IBTBStaticText {
			get { return (string)GetValue(IBTBStaticTextProperty); }
			set { SetValue(IBTBStaticTextProperty, value); }
		}

		public static readonly DependencyProperty IBTBStaticTextProperty = DependencyProperty.Register("IBTBStaticText", typeof(string), typeof(InfoBoxTextBlock), new PropertyMetadata(null));
	}
}
