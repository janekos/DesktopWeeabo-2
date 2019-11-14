using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopWeeabo2.CustomControls {

	public class SelectableButton : Control {

		static SelectableButton() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableButton), new FrameworkPropertyMetadata(typeof(SelectableButton)));
		}

		public string Content {
			get { return (string) GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string ConverterParameter {
			get { return (string) GetValue(ConverterParameterProperty); }
			set { SetValue(ConverterParameterProperty, value); }
		}

		public static readonly DependencyProperty ConverterParameterProperty = DependencyProperty.Register("ConverterParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string PressedTransferButton {
			get { return (string) GetValue(PressedTransferButtonProperty); }
			set { SetValue(PressedTransferButtonProperty, value); }
		}

		public static readonly DependencyProperty PressedTransferButtonProperty = DependencyProperty.Register("PressedTransferButton", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public object SelectedItem {
			get { return (object) GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(SelectableButton), new PropertyMetadata(null));

		public ICommand Command {
			get { return (ICommand) GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SelectableButton), new PropertyMetadata(null));

		public object CommandParameter {
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(SelectableButton), new PropertyMetadata(null));

		public string IsSelectedParameter {
			get { return (string) GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelectedParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string IsEnabledParameter {
			get { return (string) GetValue(IsEnabledParameterProperty); }
			set { SetValue(IsEnabledParameterProperty, value); }
		}

		public static readonly DependencyProperty IsEnabledParameterProperty = DependencyProperty.Register("IsEnabledParameter", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));

		public string CurrentView {
			get { return (string) GetValue(CurrentViewProperty); }
			set { SetValue(CurrentViewProperty, value); }
		}

		public static readonly DependencyProperty CurrentViewProperty = DependencyProperty.Register("CurrentView", typeof(string), typeof(SelectableButton), new PropertyMetadata(null));
	}
}