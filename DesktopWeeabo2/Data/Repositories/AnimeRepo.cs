using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers.Enums;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories {
    class AnimeRepo : IRepo<AnimeModel>, IDisposable {

        private readonly EntriesContext _db = new EntriesContext();

        public async Task<RepoResponse> AddOrUpdate(AnimeModel item) {
			if(await Get(item.Id) == null) {
				_db.AnimeItems.Add(item);
	            await _db.SaveChangesAsync();
				return RepoResponse.ADDED;
			} else if (await Delete(item.Id) == RepoResponse.DELETED) {
				_db.AnimeItems.Add(item);
				await _db.SaveChangesAsync();
				return RepoResponse.UPDATED;
			}
			return RepoResponse.ERROR;
		}

        public async Task<RepoResponse> Delete(int id) {
			var item = await Get(id);

			if (item != null) {
				_db.AnimeItems.Remove(item);
				await _db.SaveChangesAsync();
				return RepoResponse.DELETED;
			}
			return RepoResponse.NOTEXISTS;
		}

        public async Task<AnimeModel> Get(int id) => await _db.AnimeItems.FindAsync((int)id);

		public Task<AnimeModel> Get(AnimeModel entity) {
			throw new NotImplementedException();
		}

		public IEnumerable<AnimeModel> FindEnumerable(Func<AnimeModel, bool> expression) => _db.AnimeItems.Where(expression);

		public void Dispose() => _db.Dispose();
	}
}
