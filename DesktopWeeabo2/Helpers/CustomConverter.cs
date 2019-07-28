using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopWeeabo2.Helpers
{
    class CustomConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			string[] parameters = ((string)parameter).Split('|');
			switch (parameters[0]) {
				case "isValueTrue":
					return (value != null && (bool) value) ? Visibility.Visible : Visibility.Collapsed;
				case "isValueEmpty":
					if (value != null) {
						if (value.GetType() == typeof(DateTime)) return ((DateTime)value).Equals(DateTime.MinValue) ? Visibility.Collapsed : Visibility.Visible;
						return value.ToString().Length == 0 ? Visibility.Collapsed : Visibility.Visible;
					}
					return Visibility.Collapsed;
				case "isButtonSelected":
					return (value != null && parameters[1] != null && (string) value == parameters[1]);
				case "isValueNull":
					return (value == null) ? Visibility.Visible : Visibility.Collapsed;
				case "isValueNotNull":
					return (value == null) ? Visibility.Collapsed : Visibility.Visible;
				case "isWhatViewingStatus":
					return !(value == null || ((AnimeModel)value).ViewingStatus == null || !((AnimeModel)value).ViewingStatus.Equals(parameters[1]));
				case "reverseBoolean":
					return !(value != null && (bool)value);
				case "formatInfoBlockDynamicItem":
					if (value != null) {
						if (value.GetType() == typeof(DateTime)) return ((DateTime)value).ToString("dd MMM yyyy");
						return value;
					}
					return null;
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return value;
		}
	}
}
