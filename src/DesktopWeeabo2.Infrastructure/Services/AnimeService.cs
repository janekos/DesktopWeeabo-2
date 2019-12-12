using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Services.Shared;

namespace DesktopWeeabo2.Infrastructure.Services {

	public class AnimeService : BaseService<AnimeModel>, IAnimeService {

		public AnimeService(IAnimeRepository repo)
			: base(repo) { }

		protected override bool IsCorrectView(string currentView, AnimeModel item) =>
			item.ViewingStatus.Equals(currentView);
	}
}