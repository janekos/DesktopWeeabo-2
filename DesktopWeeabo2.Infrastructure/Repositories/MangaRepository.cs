using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Repositories.Shared;

namespace DesktopWeeabo2.Infrastructure.Repositories {
	public class MangaRepository : BaseRepository<MangaEntity>, IMangaRepository {
		public MangaRepository(EntriesContext db)
			: base(db) { }
	}
}
