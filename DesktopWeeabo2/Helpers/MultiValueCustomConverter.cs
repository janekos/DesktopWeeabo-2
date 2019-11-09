using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopWeeabo2.Helpers {

	internal class MultiValueCustomConverter : IMultiValueConverter {

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			if (values[0] == DependencyProperty.UnsetValue)
				return null;

			string[] parameters = (
				parameter == null
					? (values[values.Length - 1] != null
						? (string) values[values.Length - 1]
						: "")
					: (string) parameter
			).Split('|');

			switch (parameters[0]) {
				case "mainBoxText":
					if (values[0] != null && values[1] != null && values[2] != null && values[3] != null) {
						if ((bool) values[3])
							return "Loading";
						if ((string) values[2] == StatusView.ONLINE && (int) values[0] == 0)
							return (((string) values[1]).Length == 0) ? "Try searching for something or press the 'Search' button" : $"No result for {values[1]}";

						string currView;
						switch ((string) values[2]) {
							case StatusView.TOWATCH:
								currView = "To watch";
								break;

							case StatusView.TOREAD:
								currView = "To read";
								break;

							case StatusView.DROPPEDANIME:
							case StatusView.DROPPEDMANGA:
								currView = "Dropped";
								break;

							default:
								currView = (string) values[2];
								break;
						}

						if ((int) values[0] == 0 && ((string) values[1]).Length != 0)
							return $"No result for {values[1]} in '{currView}' view";
						;
						if ((int) values[0] == 0)
							return $"No items in '{currView}' view";
					}
					return null;

				case "isSwitcherButtonEnabled":
					if (values[0] != null && values[1] != null && parameters[1] != null) {
						if (((string) values[1]).Length > 0)
							return false;
						if (values[0].GetType() == typeof(AnimeModel)) {
							return ((AnimeModel) values[0]).ViewingStatus == null
								? !(parameters[1] == StatusView.DELETE)
								: !((AnimeModel) values[0]).ViewingStatus.Equals(parameters[1]);
						} else if (values[0].GetType() == typeof(MangaModel)) {
							return ((MangaModel) values[0]).ReadingStatus == null
								? !(parameters[1] == StatusView.DELETE)
								: !((MangaModel) values[0]).ReadingStatus.Equals(parameters[1]);
						}
					}
					return null;

				case "isButtonSelected":
					if (values[0] != null && parameters[1] != null) {
						return (values[0].GetType() == typeof(string) && ((string) values[0]).ToLower().Equals(parameters[1].ToLower()))
							|| (values[0].GetType() == typeof(AnimeModel) && ((AnimeModel) values[0]).ViewingStatus != null && ((AnimeModel) values[0]).ViewingStatus.Equals(parameters[1]))
							|| (values[0].GetType() == typeof(MangaModel) && ((MangaModel) values[0]).ReadingStatus != null && ((MangaModel) values[0]).ReadingStatus.Equals(parameters[1]));
					}
					return false;

				case "AreLocalSortsVisible":
					if (values[0] != null && values[1] != null) {
						switch (values[1]) {
							case StatusView.TOWATCH:
							case StatusView.VIEWED:
							case StatusView.WATCHING:
							case StatusView.DROPPEDANIME:
								return (SortLocation) values[0] != SortLocation.MANGA;

							case StatusView.TOREAD:
							case StatusView.RED:
							case StatusView.READING:
							case StatusView.DROPPEDMANGA:
								return (SortLocation) values[0] != SortLocation.ANIME;

							case StatusView.ONLINE:
								return (SortLocation) values[0] == SortLocation.ONLINE;

							default:
								return false;
						}
					}
					return false;

				case "PlaceHolderVisibilty":
					// 0 - ischecked
					// 1 - text.isempty
					if (values[0] != null && values[1] != null)
						return !((bool) values[0]) && (bool) values[1] ? Visibility.Visible : Visibility.Collapsed;
					return Visibility.Hidden;

				default:
					return null;
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}