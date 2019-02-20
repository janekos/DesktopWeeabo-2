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

		public object DynamicItem {
			get { return (object)GetValue(DynamicItemProperty); }
			set { SetValue(DynamicItemProperty, value); }
		}

		public static readonly DependencyProperty DynamicItemProperty = DependencyProperty.Register("DynamicItem", typeof(object), typeof(InfoBoxTextBlock), new PropertyMetadata(null));

		public string StaticText {
			get { return (string)GetValue(StaticTextProperty); }
			set { SetValue(StaticTextProperty, value); }
		}

		public static readonly DependencyProperty StaticTextProperty = DependencyProperty.Register("StaticText", typeof(string), typeof(InfoBoxTextBlock), new PropertyMetadata(null));

		public Dock FirstItemLocation {
			get { return (Dock)GetValue(FirstItemLocationProperty); }
			set { SetValue(FirstItemLocationProperty, value); }
		}

		public static readonly DependencyProperty FirstItemLocationProperty = DependencyProperty.Register("FirstItemLocation", typeof(Dock), typeof(InfoBoxTextBlock), new PropertyMetadata(Dock.Left));
	}
}
