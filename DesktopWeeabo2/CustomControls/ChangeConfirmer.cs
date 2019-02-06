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
    public class ChangeConfirmer : Control
    {
        static ChangeConfirmer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChangeConfirmer), new FrameworkPropertyMetadata(typeof(ChangeConfirmer)));
        }

		public string CCSelectedItemPersonalReview {
			get { return (string)GetValue(CCSelectedItemPersonalReviewProperty); }
			set { SetValue(CCSelectedItemPersonalReviewProperty, value); }
		}

		public static readonly DependencyProperty CCSelectedItemPersonalReviewProperty = DependencyProperty.Register("CCSelectedItemPersonalReview", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public int CCSelectedItemPersonalScore {
			get { return (int)GetValue(CCSelectedItemPersonalScoreProperty); }
			set { SetValue(CCSelectedItemPersonalScoreProperty, value); }
		}

		public static readonly DependencyProperty CCSelectedItemPersonalScoreProperty = DependencyProperty.Register("CCSelectedItemPersonalScore", typeof(int), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand CCSaveItemCommand {
			get { return (ICommand)GetValue(CCSaveItemCommandProperty); }
			set { SetValue(CCSaveItemCommandProperty, value); }
		}

		public static readonly DependencyProperty CCSaveItemCommandProperty = DependencyProperty.Register("CCSaveItemCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand CCDeleteItemCommand {
			get { return (ICommand)GetValue(CCDeleteItemCommandProperty); }
			set { SetValue(CCDeleteItemCommandProperty, value); }
		}

		public static readonly DependencyProperty CCDeleteItemCommandProperty = DependencyProperty.Register("CCDeleteItemCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public ICommand CCTriggerViewStatusWasChangedCommand {
			get { return (ICommand)GetValue(CCTriggerViewStatusWasChangedCommandProperty); }
			set { SetValue(CCTriggerViewStatusWasChangedCommandProperty, value); }
		}

		public static readonly DependencyProperty CCTriggerViewStatusWasChangedCommandProperty = DependencyProperty.Register("CCTriggerViewStatusWasChangedCommand", typeof(ICommand), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public string CCSelectedItemTitle {
			get { return (string)GetValue(CCSelectedItemTitleProperty); }
			set { SetValue(CCSelectedItemTitleProperty, value); }
		}

		public static readonly DependencyProperty CCSelectedItemTitleProperty = DependencyProperty.Register("CCSelectedItemTitle", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));

		public string CCPressedTransferButton {
			get { return (string)GetValue(CCPressedTransferButtonProperty); }
			set { SetValue(CCPressedTransferButtonProperty, value); }
		}

		public static readonly DependencyProperty CCPressedTransferButtonProperty = DependencyProperty.Register("CCPressedTransferButton", typeof(string), typeof(ChangeConfirmer), new PropertyMetadata(null));
	}
}
