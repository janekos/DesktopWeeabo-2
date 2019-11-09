using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopWeeabo2.CustomControls {

	public class ChangeConfirmer : Control {

		static ChangeConfirmer() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ChangeConfirmer), new FrameworkPropertyMetadata(typeof(ChangeConfirmer)));
		}

		public string SelectedItemPersonalReview {
			get { return (string) GetValue(SelectedItemPersonalReviewProperty); }
			set { SetValue(SelectedItemPersonalReviewProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemPersonalReviewProperty = DependencyProperty.Register("SelectedItemPersonalReview", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public int SelectedItemPersonalScore {
			get { return (int) GetValue(SelectedItemPersonalScoreProperty); }
			set { SetValue(SelectedItemPersonalScoreProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemPersonalScoreProperty = DependencyProperty.Register("SelectedItemPersonalScore", typeof(int), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand SaveItemCommand {
			get { return (ICommand) GetValue(SaveItemCommandProperty); }
			set { SetValue(SaveItemCommandProperty, value); }
		}

		public static readonly DependencyProperty SaveItemCommandProperty = DependencyProperty.Register("SaveItemCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand DeleteItemCommand {
			get { return (ICommand) GetValue(DeleteItemCommandProperty); }
			set { SetValue(DeleteItemCommandProperty, value); }
		}

		public static readonly DependencyProperty DeleteItemCommandProperty = DependencyProperty.Register("DeleteItemCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand TriggerViewStatusWasChangedCommand {
			get { return (ICommand) GetValue(TriggerViewStatusWasChangedCommandProperty); }
			set { SetValue(TriggerViewStatusWasChangedCommandProperty, value); }
		}

		public static readonly DependencyProperty TriggerViewStatusWasChangedCommandProperty = DependencyProperty.Register("TriggerViewStatusWasChangedCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public string SelectedItemTitle {
			get { return (string) GetValue(SelectedItemTitleProperty); }
			set { SetValue(SelectedItemTitleProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemTitleProperty = DependencyProperty.Register("SelectedItemTitle", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public string PressedTransferButton {
			get { return (string) GetValue(PressedTransferButtonProperty); }
			set { SetValue(PressedTransferButtonProperty, value); }
		}

		public static readonly DependencyProperty PressedTransferButtonProperty = DependencyProperty.Register("PressedTransferButton", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));
	}
}