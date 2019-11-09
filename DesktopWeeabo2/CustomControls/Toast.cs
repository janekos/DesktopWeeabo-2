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
	public class Toast : Control {
		static Toast() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Toast), new FrameworkPropertyMetadata(typeof(Toast)));
		}

		public string ToastMessageType {
			get { return (string) GetValue(ToastMessageTypeProperty); }
			set { SetValue(ToastMessageTypeProperty, value); }
		}

		public static readonly DependencyProperty ToastMessageTypeProperty = DependencyProperty.Register("ToastMessageType", typeof(string), typeof(Toast), new PropertyMetadata(OnMessageTypeChanged));

		private static void OnMessageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			SetToastAppearanceByMessageType(d as Toast, (string)e.NewValue);
		}

		public string ToastMessage {
			get { return (string) GetValue(ToastMessageProperty); }
			set { SetValue(ToastMessageProperty, value); }
		}

		public static readonly DependencyProperty ToastMessageProperty = DependencyProperty.Register("ToastMessage", typeof(string), typeof(Toast), new PropertyMetadata(null));

		public string ToastBorderColor {
			get { return (string) GetValue(ToastBorderColorProperty); }
			set { SetValue(ToastBorderColorProperty, value); }
		}

		public static readonly DependencyProperty ToastBorderColorProperty = DependencyProperty.Register("ToastBorderColor", typeof(string), typeof(Toast), new PropertyMetadata(Brushes.Transparent.ToString()));

		public string ToastBackgroundColor {
			get { return (string) GetValue(ToastBackgroundColorProperty); }
			set { SetValue(ToastBackgroundColorProperty, value); }
		}

		public static readonly DependencyProperty ToastBackgroundColorProperty = DependencyProperty.Register("ToastBackgroundColor", typeof(string), typeof(Toast), new PropertyMetadata(Brushes.Transparent.ToString()));

		public string ToastTextColor {
			get { return (string) GetValue(ToastTextColorProperty); }
			set { SetValue(ToastTextColorProperty, value); }
		}

		public static readonly DependencyProperty ToastTextColorProperty = DependencyProperty.Register("ToastTextColor", typeof(string), typeof(Toast), new PropertyMetadata(Brushes.Transparent.ToString()));

		private static void SetToastAppearanceByMessageType(Toast thisToast, string messageType) {
			switch (messageType) {
				case "warning":
					thisToast.ToastBackgroundColor = "#fcf8e3";
					thisToast.ToastBorderColor = "#faf2cc";
					thisToast.ToastTextColor = "#8a6d3b";
					break;

				case "danger":
					thisToast.ToastBackgroundColor = "#f2dede";
					thisToast.ToastBorderColor = "#ebcccc";
					thisToast.ToastTextColor = "#a94442";
					break;

				case "success":
					thisToast.ToastBackgroundColor = "#dff0d8";
					thisToast.ToastBorderColor = "#d0e9c6";
					thisToast.ToastTextColor = "#3c763d";
					break;

				case "info":
					thisToast.ToastBackgroundColor = "#d9edf7";
					thisToast.ToastBorderColor = "#bcdff1";
					thisToast.ToastTextColor = "#31708f";
					break;
			}
		}
	}
}
