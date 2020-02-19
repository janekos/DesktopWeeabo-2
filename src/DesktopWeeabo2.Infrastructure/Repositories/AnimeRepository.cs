using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Repositories.Shared;
using LiteDB;

namespace DesktopWeeabo2.Infrastructure.Repositories {

	public class AnimeRepository : BaseRepository<AnimeModel>, IAnimeRepository {
		private readonly EntriesContext context;

		public AnimeRepository(EntriesContext context) {
			this.context = context;
		}

		protected override LiteCollection<AnimeModel> collection { get => context.AnimeItems; }
	}
}