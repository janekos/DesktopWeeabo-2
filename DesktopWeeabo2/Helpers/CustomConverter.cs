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
					if (value != null && (bool) value == true) return Visibility.Visible;
					return Visibility.Collapsed;
				case "isStringEmpty":
					if (value == null || value.ToString().Length == 0) return Visibility.Collapsed;
					return Visibility.Visible;
				case "isButtonSelected":
					if (value != null && parameters[1] != null && (string) value == parameters[1]) return true;
					return false;
				case "isValueNull":
					if (value == null) return Visibility.Visible;
					return Visibility.Hidden;
				case "isWhatViewingStatus":
					if (value == null || ((AnimeModel)value).ViewingStatus == null || !((AnimeModel)value).ViewingStatus.Equals(parameters[1])) return false;
					return true;
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
