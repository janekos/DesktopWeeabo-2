using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Enums {
	public static class EnumExtensions {
		public static string ToString<T>(object value) =>
			Enum.GetName(typeof(T), value);

		public static T ToEnum<T>(this string value) =>
			(T)Enum.Parse(typeof(T), value, true);
	}
}
