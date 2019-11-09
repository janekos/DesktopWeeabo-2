using DesktopWeeabo2.Core.API;
using DesktopWeeabo2.Core.Models.Shared;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.API.Shared {

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
				if (string.Join(",", _Genres) != string.Join(",", value)) {
					_Genres = value;
					ResetQueryVars();
				}
			}
		}

		public APIEnumerator() {
		}

		public async Task<T[]> GetByMalIdSet(int[] malIds) =>
			GetItems(await APIQueries.GetByMALIds(ApiUtils.APIGetByMalIdsVariables(malIds)));

		public async Task<T[]> GetCurrentSearchSet() =>
			GetItems(await APIQueries.Search(ApiUtils.APISearchVariables(_SearchString, CurrentPage, _SortBy, _Genres, _IsAdult), _Type));

		protected void ResetQueryVars() {
			CurrentPage = 1;
			HasNextPage = false;
			TotalItems = 0;
		}

		protected virtual T[] GetItems(string result) => new T[0];
	}
}