using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories {
    class AnimeRepo : IRepo<AnimeModel> {

        private readonly EntriesContext _db;

        public AnimeRepo(EntriesContext db) {
            _db = db;
        }

        public void Add(AnimeModel item) {
            _db.AnimeItems.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id) {
            _db.AnimeItems.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Update(AnimeModel item) {
            Delete(item.Id);
            Add(item);
        }

        public AnimeModel[] GetAll() {
            throw new NotImplementedException();
        }

        public AnimeModel GetById(int id) {
            return _db.AnimeItems.Find((int)id);
        }
    }
}
