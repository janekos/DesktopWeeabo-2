using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services {
	public class MangaService : IService<MangaModel> {
		public Task<DBResponse> AddOrUpdate(MangaModel entity) {
			throw new NotImplementedException();
		}

		public DBResponse Delete(int id) {
			throw new NotImplementedException();
		}

		public MangaModel[] GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			throw new NotImplementedException();
		}

		public MangaModel[] GetCustom(Func<AnimeModel, bool> condition) {
			throw new NotImplementedException();
		}
	}
}
