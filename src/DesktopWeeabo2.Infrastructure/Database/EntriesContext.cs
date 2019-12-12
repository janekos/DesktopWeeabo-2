using DesktopWeeabo2.Core;
using DesktopWeeabo2.Core.Models;
using LiteDB;
using System.IO;

namespace DesktopWeeabo2.Infrastructure.Database {

	public class EntriesContext {

		private readonly LiteDatabase db;

		public EntriesContext() {
			db = new LiteDatabase($"{GlobalConfig.AppDir}\\entries.db");
		}

		public LiteCollection<AnimeModel> AnimeItems {
			get {
				try {
					var returnable = db.GetCollection<AnimeModel>("Animes");
					returnable.EnsureIndex(ae => ae.Id);

					return returnable;
				} catch (DirectoryNotFoundException) {
					return null;
				}
			}
		}

		public LiteCollection<MangaModel> MangaItems {
			get {
				try {
					var returnable = db.GetCollection<MangaModel>("Mangas");
					returnable.EnsureIndex(me => me.Id);

					return returnable;
				} catch (DirectoryNotFoundException) {
					return null;
				}
			}
		}
	}
}