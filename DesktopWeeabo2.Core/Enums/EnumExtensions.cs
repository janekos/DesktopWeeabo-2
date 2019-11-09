using System;

namespace DesktopWeeabo2.Core.Enums {

	public static class EnumExtensions {

		public static string ToString<T>(object value) =>
			Enum.GetName(typeof(T), value);

		public static T ToEnum<T>(this string value) =>
			(T) Enum.Parse(typeof(T), value, true);
	}
}