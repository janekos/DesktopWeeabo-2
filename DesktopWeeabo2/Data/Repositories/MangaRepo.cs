using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories
{
    class MangaRepo: IRepo<MangaModel> {

        private readonly EntriesContext _db;

        public MangaRepo(EntriesContext db) {
            _db = db;
        }

        public void Add(MangaModel item) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public void Update(MangaModel item) {
            throw new NotImplementedException();
        }

        public MangaModel[] GetAll() {
            throw new NotImplementedException();
        }

        public MangaModel GetById(int id) {
            throw new NotImplementedException();
        }
    }
}
