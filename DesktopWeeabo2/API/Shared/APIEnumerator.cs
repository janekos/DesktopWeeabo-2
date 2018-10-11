using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API.Shared {
    abstract class APIEnumerator<T> where T : BaseModel {

        protected int _CurrentPage = 1;
        protected string _SearchString;
        protected bool _Type;
        protected string _SortBy;

        public bool HasNextPage { get; protected set; } = false;
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

            if (SearchString.Length == 0) { return new T[0]; }

            JObject result = JObject.Parse(await APIQueries.Search(_SearchString, _CurrentPage, _SortBy, _Type));

            // raiseproperty faulty query
            if (result["data"].Type == JTokenType.Null) { throw new ArgumentException("Something went wrong with the Query."); }

            HasNextPage = (bool)result["data"]["Page"]["pageInfo"]["hasNextPage"];

            return ManageItems((JArray)result["data"]["Page"]["media"]);
        }

        public bool TryMoveToNextSet() {
            if (HasNextPage) {
                _CurrentPage += 1;
                return true;
            } else { return false; }
        }

        protected void ResetQueryVars() {
            _CurrentPage = 1;
            HasNextPage = false;
        }

        protected virtual T[] ManageItems(JArray items) => new T[0];
    }
}
