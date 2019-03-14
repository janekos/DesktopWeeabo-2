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

namespace DesktopWeeabo2.CustomControls {
	public class SelectableInfoBlock : InfoBlock {
		static SelectableInfoBlock() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableInfoBlock), new FrameworkPropertyMetadata(typeof(SelectableInfoBlock)));
		}

		public bool IsVertical {
			get { return (bool)GetValue(IsVerticalProperty); }
			set { SetValue(IsVerticalProperty, value); }
		}

		public static readonly DependencyProperty IsVerticalProperty = DependencyProperty.Register("IsVertical", typeof(bool), typeof(SelectableInfoBlock), new PropertyMetadata(false));

		public bool HasBorder {
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		public static readonly DependencyProperty HasBorderProperty = DependencyProperty.Register("HasBorder", typeof(bool), typeof(SelectableInfoBlock), new PropertyMetadata(false));

		public bool IsEditable {
			get { return (bool)GetValue(IsEditableProperty); }
			set { SetValue(IsEditableProperty, value); }
		}

		public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(SelectableInfoBlock), new PropertyMetadata(false));

		public ICommand EditCommand {
			get { return (ICommand)GetValue(EditCommandProperty); }
			set { SetValue(EditCommandProperty, value); }
		}

		public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(SelectableInfoBlock), new PropertyMetadata(null));
	}
}
