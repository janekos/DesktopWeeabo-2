using DesktopWeeabo2.Data.Repositories.Shared;
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

        public async void Add(AnimeModel item) {
            _db.AnimeItems.Add(item);
            await _db.SaveChangesAsync();
        }

        public async void Delete(int id) {
            _db.AnimeItems.Remove(await Get(id));
			await _db.SaveChangesAsync();
		}

        public void Update(AnimeModel item) {
            Delete(item.Id);
            Add(item);
        }

        public async Task<AnimeModel> Get(int id) => await _db.AnimeItems.FindAsync((int)id);

		public Task<AnimeModel> Get(AnimeModel entity) {
			throw new NotImplementedException();
		}

		public IEnumerable<AnimeModel> FindEnumerable(Func<AnimeModel, bool> expression) => _db.AnimeItems.Where(expression);

		public void Dispose() => _db.Dispose();
	}
}
