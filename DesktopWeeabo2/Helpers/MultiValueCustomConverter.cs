using DesktopWeeabo2.ViewModels.Shared;
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
	class MultiValueCustomConverter : IMultiValueConverter {
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			switch (parameter) {
				case "mainBoxText":
					if (values[0] != null && values[1] != null && values[2] != null && values[3] != null) {
						if ((bool)values[3]) return "Loading";
						if ((string)values[2] == StatusView.Online && (int) values[0] == 0) {
							if (((string)values[1]).Length == 0) return "Try searching for something";
							return "No result for " + values[1];
						}
					    string currView = (((string)values[2]).Equals(StatusView.Towatch) ? "To watch" : ((string)values[2]).Equals(StatusView.Toread) ? "To read" : (string)values[2]);
						if ((int) values[0] == 0 && ((string)values[1]).Length != 0) return "No result for " + values[1] + " in \""+ currView + "\" view"; ;
						if((int) values[0] == 0) return "No items in \"" + currView + "\" view";					
					}
					return null;
				default:
					return null;
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
