using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers;
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

		public async void Update(AnimeModel item) {
			var entity = await Get(item.Id);
			if (entity == null) return;

			_db.Entry(entity).CurrentValues.SetValues(item);
			await _db.SaveChangesAsync();
		}

		public async void Delete(int id) {
			var item = await Get(id);
			if (item == null) return;

			_db.AnimeItems.Remove(item);
			await _db.SaveChangesAsync();
		}

		public async Task<AnimeModel> Get(int id) => await _db.AnimeItems.FindAsync((int)id);
		public IEnumerable<AnimeModel> FindSet(Func<AnimeModel, bool> expression, Func<AnimeModel, object> orderBy, bool isDescending = false) => isDescending ? _db.AnimeItems.Where(expression).OrderByDescending(orderBy) : _db.AnimeItems.Where(expression).OrderBy(orderBy);

		public void Dispose() => _db.Dispose();
	}
}
