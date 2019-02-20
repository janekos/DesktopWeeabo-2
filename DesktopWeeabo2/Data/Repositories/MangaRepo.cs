using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories
{
	class MangaRepo : IRepo<MangaModel>, IDisposable {
		public void Add(MangaModel item) {
			throw new NotImplementedException();
		}

		public void Delete(int id) {
			throw new NotImplementedException();
		}

		public void Dispose() {
			throw new NotImplementedException();
		}

		public IEnumerable<MangaModel> FindSet(Func<MangaModel, bool> expression, Func<MangaModel, object> orderBy, bool isDescending) {
			throw new NotImplementedException();
		}

		public Task<MangaModel> Get(int id) {
			throw new NotImplementedException();
		}

		public void Update(MangaModel item) {
			throw new NotImplementedException();
		}
	}
}
