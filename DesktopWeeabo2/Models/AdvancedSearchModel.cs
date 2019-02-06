using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
	class AdvancedSearchModel {

		public SortModel[] Sort { get; } = {
			new SortModel("Id", "ID", false),
			new SortModel("Score", "SCORE", false),
			new SortModel("Status", "STATUS", false),
			new SortModel("End date", "END_DATE", false),
			new SortModel("Start date", "START_DATE", false),
			new SortModel("Native title", "TITLE_NATIVE", false),
			new SortModel("Romaji title", "TITLE_ROMAJI", false),
			new SortModel("English title", "TITLE_ENGLISH", false),
			new SortModel("Priority", "PRIORITY", true),
			new SortModel("Date added", "DATE_ADDED", true),
			new SortModel("Personal score", "PERSONAL_SCORE", true),
		};

		public GenreModel[] Genre { get; } = {
			new GenreModel(1, "Action"),
			new GenreModel(2, "Adventure"),
			new GenreModel(3, "Comedy"),
			new GenreModel(4, "Drama"),
			new GenreModel(5, "Ecchi"),
			new GenreModel(6, "Fantasy"),
			new GenreModel(7, "Hentai"),
			new GenreModel(8, "Horror"),
			new GenreModel(9, "Mahou Shoujo"),
			new GenreModel(10, "Mecha"),
			new GenreModel(11, "Music"),
			new GenreModel(12, "Mystery"),
			new GenreModel(13, "Psychological"),
			new GenreModel(14, "Romance"),
			new GenreModel(15, "Sci-Fi"),
			new GenreModel(16, "Slice of Life"),
			new GenreModel(17, "Sports"),
			new GenreModel(18, "Supernatural"),
			new GenreModel(19, "Thriller")
		};

		public string SelectedSort { get; set; }
		public string SelectedGenreString { get; set; }
		public bool IsAdult { get; set; } = true;
		public bool IsDescending { get; set; } = false;
	}
}
