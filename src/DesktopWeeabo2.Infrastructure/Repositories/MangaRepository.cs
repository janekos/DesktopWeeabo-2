using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Repositories.Shared;
using LiteDB;

namespace DesktopWeeabo2.Infrastructure.Repositories {

	public class MangaRepository : BaseRepository<MangaModel>, IMangaRepository {
		public MangaRepository(EntriesContext context)
			: base(context.MangaItems) { }
	}
}