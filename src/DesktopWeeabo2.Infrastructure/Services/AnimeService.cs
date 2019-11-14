using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Services.Shared;
using System.Collections.Generic;
using System.Linq;

namespace DesktopWeeabo2.Infrastructure.Services {

	public class AnimeService : BaseService<AnimeModel, AnimeEntity>, IAnimeService {

		public AnimeService(IAnimeRepository repo)
			: base(repo) { }

		protected override bool IsAdultCondition(SearchModel search, AnimeEntity item) =>
			!search.IsAdult
				? !item.IsAdult ?? true
				: true;

		protected override bool ContainsSearchTextCondition(SearchModel search, AnimeEntity item) =>
			!string.IsNullOrWhiteSpace(search.SearchText)
				? (item.TitleEnglish.ToLower().Contains(search.SearchText.ToLower())
					|| item.TitleNative.ToLower().Contains(search.SearchText.ToLower())
					|| item.TitleRomaji.ToLower().Contains(search.SearchText.ToLower()))
				: true;

		protected override bool ContainsGenre(IEnumerable<string> selectedGenres, AnimeEntity item) =>
			selectedGenres.Count() > 0
				? !selectedGenres.Except(item.Genres.Split('|')).Any()
				: true;

		protected override bool IsCorrectView(string currentView, AnimeEntity item) =>
			item.ViewingStatus.Equals(currentView);

		protected override AnimeEntity Cast(AnimeModel model) => (AnimeEntity) model;

		protected override AnimeModel Cast(AnimeEntity model) => (AnimeModel) model;
	}
}