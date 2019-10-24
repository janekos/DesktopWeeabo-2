using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Services.Shared;
using System.Collections.Generic;
using System.Linq;

namespace DesktopWeeabo2.Infrastructure.Services {
	public class MangaService : BaseService<MangaModel, MangaEntity>, IMangaService {
		public MangaService(IMangaRepository repo)
			: base(repo) { }

		protected override bool IsAdultCondition(SearchModel search, MangaEntity item) =>
			!search.IsAdult
				? !item.IsAdult ?? true
				: true;

		protected override bool ContainsSearchTextCondition(SearchModel search, MangaEntity item) =>
			!string.IsNullOrWhiteSpace(search.SearchText)
				? (item.TitleEnglish.ToLower().Contains(search.SearchText.ToLower())
					|| item.TitleNative.ToLower().Contains(search.SearchText.ToLower())
					|| item.TitleRomaji.ToLower().Contains(search.SearchText.ToLower()))
				: true;

		protected override bool ContainsGenre(IEnumerable<string> selectedGenres, MangaEntity item) =>
			selectedGenres.Count() > 0
				? !selectedGenres.Except(item.Genres.Split('|')).Any()
				: true;

		protected override bool IsCorrectView(string currentView, MangaEntity item) =>
			item.ReadingStatus.Equals(currentView);

		protected override MangaEntity Cast(MangaModel model) => (MangaEntity)model;
	}
}
