using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace DesktopWeeabo2.API.Shared {
	public abstract class APIEnumerator<T> where T : BaseModel {

		protected int _CurrentPage = 1;
		protected string _SearchString;
        protected bool _Type;
        protected bool _IsAdult;
        protected string _SortBy;
        protected string[] _Genres = new string[0];

		public bool HasNextPage { get; protected set; } = false;
		public int TotalItems { get; protected set; } = 0;

		public string SearchString {
            get { return _SearchString; }
            set {
				if (_SearchString != value) {
					_SearchString = value;
					ResetQueryVars();
				}
            }
        }
		public int CurrentPage {
			get { return _CurrentPage; }
			set {
				if (_CurrentPage != value) {
					_CurrentPage = value;
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
		public bool IsAdult {
			get { return _IsAdult; }
			set {
				if (_IsAdult != value) {
					_IsAdult = value;
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
		public string[] Genres {
			get { return _Genres; }
			set {
				if (string.Join(",",_Genres) != string.Join(",", value)) {
					_Genres = value;
					ResetQueryVars();
				}
			}
		}

		public APIEnumerator() { }

		public async Task<T[]> GetByMalIdSet(int[] malIds) =>
			GetSet(JObject.Parse(await APIQueries.GetByMALIds(StringHelpers.APIGetByMalIdsVariables(malIds))));

		public async Task<T[]> GetCurrentSearchSet() =>
			GetSet(JObject.Parse(await APIQueries.Search(StringHelpers.APISearchVariables(_SearchString, CurrentPage, _SortBy, _Genres, _IsAdult), _Type)));

        protected T[] GetSet(JObject result) {

            if (result["data"].Type == JTokenType.Null) { throw new ArgumentException("Server returned nothing."); }

            HasNextPage = (bool)result["data"]["Page"]["pageInfo"]["hasNextPage"];
            TotalItems = (int)result["data"]["Page"]["pageInfo"]["total"];

			if (HasNextPage) CurrentPage = CurrentPage + 1;

            return ManageItems((JArray)result["data"]["Page"]["media"]);
        }

        protected void ResetQueryVars() {
			CurrentPage = 1;
            HasNextPage = false;
            TotalItems = 0;
        }

        protected virtual T[] ManageItems(JArray items) => new T[0];
    }
}
