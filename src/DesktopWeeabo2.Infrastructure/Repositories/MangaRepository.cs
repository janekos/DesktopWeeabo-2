using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Repositories.Shared;
using LiteDB;

namespace DesktopWeeabo2.Infrastructure.Repositories {

	public class MangaRepository : BaseRepository<MangaModel>, IMangaRepository {
		private readonly EntriesContext context;

		public MangaRepository(EntriesContext context) {
			this.context = context;
		}

		protected override LiteCollection<MangaModel> collection { get => context.MangaItems; }
	}
}