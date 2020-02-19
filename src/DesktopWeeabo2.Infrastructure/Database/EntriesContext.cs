using DesktopWeeabo2.Core.Config;
using DesktopWeeabo2.Core.Models;
using LiteDB;
using System;
using System.IO;

namespace DesktopWeeabo2.Infrastructure.Database {

	public class EntriesContext : IDisposable {
		private readonly LiteDatabase db;

		public EntriesContext() {
			db = new LiteDatabase($"{ConfigurationManager.Config.AppDir}\\entries.db");
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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					db.Dispose();
				}

				disposedValue = true;
			}
		}
		public void Dispose() {
			Dispose(true);
		}
		#endregion
	}
}