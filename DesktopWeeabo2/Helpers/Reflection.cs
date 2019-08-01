using System;
using System.Linq;
using System.Reflection;

namespace DesktopWeeabo2.Helpers {
	public class UnReflectable : Attribute {}

	public static class TypeExtensions {
		public static PropertyInfo[] GetFilteredProperties(this Type type) => type.GetProperties().Where(pi => !Attribute.IsDefined(pi, typeof(UnReflectable))).ToArray();
	}

	public static class ReflectionHelpers {
		public static bool CompareObjectValues<T>(T oldValue, T newValue) {
			foreach (PropertyInfo prop in newValue.GetType().GetFilteredProperties()) {
				var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
				var newItemVal = prop.GetValue(newValue, null) ?? 0;
				var oldItemVal = prop.GetValue(oldValue, null) ?? 0;

				if (!newItemVal.Equals(oldItemVal)) return true;
			}
			return false;
		}
	}
}
