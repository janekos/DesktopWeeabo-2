using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.Services.Shared;

namespace DesktopWeeabo2.Infrastructure.Services {

	public class MangaService : BaseService<MangaModel>, IMangaService {

		public MangaService(IMangaRepository repo)
			: base(repo) { }

		protected override bool IsCorrectView(string currentView, MangaModel item) =>
			item.ReadingStatus.Equals(currentView);
	}
}