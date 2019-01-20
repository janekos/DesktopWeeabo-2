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
				if (_SearchString != value) {
					_SearchString = value;
					ResetQueryVars();
				}
            }
        }
        public bool Type {
            get { return _Type; }
            set {
				if (_Type != value) {
					_Type = value;
					ResetQueryVars();
				}
            }
        }
        public string SortBy {
            get { return _SortBy; }
            set {
				if (_SortBy != value) {
					_SortBy = value;
					ResetQueryVars();
				}
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

			if (HasNextPage) _CurrentPage = _CurrentPage + 1;

            return ManageItems((JArray)result["data"]["Page"]["media"]);
        }

        protected void ResetQueryVars() {
            _CurrentPage = 1;
            HasNextPage = false;
        }

        protected virtual T[] ManageItems(JArray items) => new T[0];
    }
}
