using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Repositories.Shared;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Repositories {

	public class AnimeRepository : BaseRepository<AnimeEntity>, IAnimeRepository {

		public AnimeRepository(EntriesContext context)
			: base(context) { }
	}
}