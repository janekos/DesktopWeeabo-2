using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
	public class GenreObject {
		public string Name { get; }
		public bool IsSelected {
			get;
			set;
		}

		public GenreObject(string name) { Name = name; }
	}

	public class SortObject {

		public string SortName { get; }
		public string APIValue { get; }
		public string LocalValue { get; }
		public SortLocation VisibleIn { get; }

		public SortObject(string _SortName, string _SortValue, string _LocalValue, SortLocation _VisibleIn) {
			SortName = _SortName;
			APIValue = _SortValue;
			LocalValue = _LocalValue;
			VisibleIn = _VisibleIn;
		}
	}

	public class SearchModel {
		public SortObject[] InitSortsList () => new SortObject[] {
			new SortObject("Id",             "ID",            nameof(BaseModel.Id),             SortLocation.ONLINE),
			new SortObject("Score",          "SCORE",         nameof(BaseModel.AverageScore),   SortLocation.ONLINE),
			new SortObject("Status",         "STATUS",        nameof(BaseModel.Status),         SortLocation.ONLINE),
			new SortObject("Native title",   "TITLE_NATIVE",  nameof(BaseModel.TitleNative),    SortLocation.ONLINE),
			new SortObject("Romaji title",   "TITLE_ROMAJI",  nameof(BaseModel.TitleRomaji),    SortLocation.ONLINE),
			new SortObject("English title",  "TITLE_ENGLISH", nameof(BaseModel.TitleEnglish),   SortLocation.ONLINE),
			new SortObject("Date added",     "",              nameof(BaseModel.DateAdded),      SortLocation.LOCAL),
			new SortObject("Personal score", "",              nameof(BaseModel.PersonalScore),  SortLocation.LOCAL),
			new SortObject("End date",       "END_DATE",      nameof(AnimeModel.EndDate),       SortLocation.ANIME),
			new SortObject("Start date",     "START_DATE",    nameof(AnimeModel.StartDate),     SortLocation.ANIME),
			new SortObject("Watch priority", "",              nameof(AnimeModel.WatchPriority), SortLocation.ANIME),
			new SortObject("Read priority",  "",              nameof(MangaModel.ReadPriority),  SortLocation.MANGA),
		};

		public GenreObject[] InitGenresList () => new GenreObject[] {
			new GenreObject("Action"),
			new GenreObject("Adventure"),
			new GenreObject("Comedy"),
			new GenreObject("Drama"),
			new GenreObject("Ecchi"),
			new GenreObject("Fantasy"),
			new GenreObject("Hentai"),
			new GenreObject("Horror"),
			new GenreObject("Mahou Shoujo"),
			new GenreObject("Mecha"),
			new GenreObject("Music"),
			new GenreObject("Mystery"),
			new GenreObject("Psychological"),
			new GenreObject("Romance"),
			new GenreObject("Sci-Fi"),
			new GenreObject("Slice of Life"),
			new GenreObject("Sports"),
			new GenreObject("Supernatural"),
			new GenreObject("Thriller")
		};

		public SearchModel() {
			GenresList = InitGenresList();
			SortsList = InitSortsList();
			SelectedSort = SortsList[0];
		}

		public GenreObject[] GenresList { get; set; }
		public SortObject[] SortsList { get; }
		public string SearchText { get; set; } = "";
		public SortObject SelectedSort { get; set; }
		public bool IsAdult { get; set; } = true;
		public bool IsDescending { get; set; } = false;
	}
}
