namespace DesktopWeeabo2.Models {
	public class SortModel {

		public string Name { get; }
		public string Value { get; }
		public bool IsLocal { get; }

		public SortModel(string _Value, string _Name, bool _IsLocal) {
			Value = _Value;
			Name = _Name;
			IsLocal = _IsLocal;
		}
	}
}
