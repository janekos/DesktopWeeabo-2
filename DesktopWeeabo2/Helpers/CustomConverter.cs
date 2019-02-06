using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DesktopWeeabo2.Helpers
{
    class CustomConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			string[] parameters = ((string)parameter).Split('|');
			switch (parameters[0]) {
				case "isValueTrue":
					return (value != null && (bool) value) ? Visibility.Visible : Visibility.Collapsed;
				case "isStringEmpty":
					return (value == null || value.ToString().Length == 0) ? Visibility.Collapsed : Visibility.Visible;
				case "isButtonSelected":
					return (value != null && parameters[1] != null && (string) value == parameters[1]);
				case "isValueNull":
					return (value == null) ? Visibility.Visible : Visibility.Hidden;
				case "isWhatViewingStatus":
					return !(value == null || ((AnimeModel)value).ViewingStatus == null || !((AnimeModel)value).ViewingStatus.Equals(parameters[1]));
				case "reverseBoolean":
					return !(value != null && (bool)value);
				case "formatInfoBoxTextBlock":
					if (value != null) {
						if (value.GetType() == typeof(DateTime)) return ((DateTime)value).ToString("dd/MM/yyyy");
					}
					return value;
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
