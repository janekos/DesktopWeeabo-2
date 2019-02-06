namespace DesktopWeeabo2.Models {
	public class GenreModel {

		public int Id { get; }
		public string Name { get; }
		public bool IsChecked { get; set; }

		public GenreModel(int id, string name) {
			Id = id;
			Name = name;
			IsChecked = false;
		}
	}
}
