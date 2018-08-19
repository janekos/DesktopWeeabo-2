using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API.Shared {
    abstract class APIEnumerator<T> where T : BaseModel {

        protected int CurrentPage = 1;
        public bool HasNextPage { get; protected set; } = false;
        public int TotalItems { get; protected set; } = 0;
        protected string _SearchString;
        protected bool _Type;
        protected string _SortBy;


        public string SearchString {
            get { return _SearchString; }
            set {
                _SearchString = value;
                ResetQueryVars();
            }
        }
        public bool Type {
            get { return _Type; }
            set {
                _Type = value;
                ResetQueryVars();
            }
        }
        public string SortBy {
            get { return _SortBy; }
            set {
                _SortBy = value;
                ResetQueryVars();
            }
        }

        public APIEnumerator() { }

        public APIEnumerator(string _searchString, bool _type, string _sortBy) {
            SearchString = _searchString;
            Type = _type;
            SortBy = _sortBy;
        }

        public async Task<T[]> GetCurrentSet() {

            if (SearchString.Length == 0) { throw new ArgumentNullException("SearchString is empty."); }

            JObject result = JObject.Parse(await APIQueries.Search(SearchString, CurrentPage, SortBy, Type));
            HasNextPage = (bool)result["data"]["Page"]["pageInfo"]["hasNextPage"];
            TotalItems += (int)result["data"]["Page"]["pageInfo"]["total"];

            return ManageItems((JArray)result["data"]["Page"]["media"]);
        }

        public bool TryMoveToNextSet() {
            if (HasNextPage) {
                CurrentPage += 1;
                return true;
            }
            else {
                return false;
            }
        }

        protected void ResetQueryVars() {
            CurrentPage = 1;
            HasNextPage = false;
            TotalItems = 0;
        }

        protected virtual T[] ManageItems(JArray items) => new T[0];
    }
}
