using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopWeeabo2.CustomControls {
	public class SelectableButton : Control {
		static SelectableButton() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableButton), new FrameworkPropertyMetadata(typeof(SelectableButton)));
		}

		public string SBContent {
			get { return (string)GetValue(SBContentProperty); }
			set { SetValue(SBContentProperty, value); }
		}

		public static readonly DependencyProperty SBContentProperty = DependencyProperty.Register("SBContent", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string SBConverterParameter {
			get { return (string)GetValue(SBConverterParameterProperty); }
			set { SetValue(SBConverterParameterProperty, value); }
		}

		public static readonly DependencyProperty SBConverterParameterProperty = DependencyProperty.Register("SBConverterParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string SBPressedTransferButton{
			get { return (string)GetValue(SBPressedTransferButtonProperty); }
			set { SetValue(SBPressedTransferButtonProperty, value); }
		}

		public static readonly DependencyProperty SBPressedTransferButtonProperty = DependencyProperty.Register("SBPressedTransferButton", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public object SBSelectedItem {
			get { return (object)GetValue(SBSelectedItemProperty); }
			set { SetValue(SBSelectedItemProperty, value); }
		}

		public static readonly DependencyProperty SBSelectedItemProperty = DependencyProperty.Register("SBSelectedItem", typeof(object), typeof(SelectableButton), new PropertyMetadata(null));

		public ICommand SBCommand {
			get { return (ICommand)GetValue(SBCommandProperty); }
			set { SetValue(SBCommandProperty, value); }
		}

		public static readonly DependencyProperty SBCommandProperty = DependencyProperty.Register("SBCommand", typeof(ICommand), typeof(SelectableButton), new PropertyMetadata(null));

		public object SBCommandParameter {
			get { return GetValue(SBCommandParameterProperty); }
			set { SetValue(SBCommandParameterProperty, value); }
		}

		public static readonly DependencyProperty SBCommandParameterProperty = DependencyProperty.Register("SBCommandParameter", typeof(object), typeof(SelectableButton), new PropertyMetadata(null));

		public string SBIsSelectedParameter{
			get { return (string) GetValue(SBIsSelectedProperty); }
			set { SetValue(SBIsSelectedProperty, value); }
		}

		public static readonly DependencyProperty SBIsSelectedProperty = DependencyProperty.Register("SBIsSelectedParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string SBIsEnabledParameter {
			get { return (string)GetValue(SBIsEnabledParameterProperty); }
			set { SetValue(SBIsEnabledParameterProperty, value); }
		}

		public static readonly DependencyProperty SBIsEnabledParameterProperty = DependencyProperty.Register("SBIsEnabledParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string SBCurrentView {
			get { return (string)GetValue(SBCurrentViewProperty); }
			set { SetValue(SBCurrentViewProperty, value); }
		}

		public static readonly DependencyProperty SBCurrentViewProperty = DependencyProperty.Register("SBCurrentView", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));
	}
}
