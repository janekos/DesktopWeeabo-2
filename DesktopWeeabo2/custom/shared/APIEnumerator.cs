using DesktopWeeabo2.data;
using DesktopWeeabo2.data.db.entities.shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.custom.shared
{
    abstract class APIEnumerator<T> where T : BaseEntity{

        protected int currentPage = 1;
        public bool hasNextPage { get; protected set; } = false;
        public int totalItems { get; protected set; } = 0;
        protected string _searchString;
        protected bool _type;
        protected string _sortBy;


        public string searchString {
            get { return _searchString;}
            set {
                _searchString = value;
                resetQueryVars();
            }
        }
        public bool type {
            get { return _type; }
            set {
                _type = value;
                resetQueryVars();
            }
        }
        public string sortBy {
            get { return _sortBy; }
            set {
                _sortBy = value;
                resetQueryVars();
            }
        }

        public APIEnumerator() { }

        public APIEnumerator(string _searchString, bool _type, string _sortBy) {
            searchString = _searchString;
            type = _type;
            sortBy = _sortBy;
        }

        public async Task<T[]> getCurrentSet() {

            if (searchString.Length == 0) { throw new ArgumentNullException("SearchString is empty."); }

            JObject result = JObject.Parse(await APIQueries.search(searchString, currentPage, sortBy, type));
            hasNextPage = (bool)result["data"]["Page"]["pageInfo"]["hasNextPage"];
            totalItems += (int)result["data"]["Page"]["pageInfo"]["total"];

            return manageItems((JArray)result["data"]["Page"]["media"]);
        }

        public bool tryMoveToNextSet() {
            if (hasNextPage) {
                currentPage += 1;
                return true;
            }else {
                return false;
            }
        }

        protected void resetQueryVars() {
            currentPage = 1;
            hasNextPage = false;
            totalItems = 0;
        }

        protected virtual T[] manageItems(JArray items) => new T[0];
    }
}
