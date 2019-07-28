using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services.Shared {
	public interface IService<T> where T : BaseModel {
		Task<DBResponse> AddOrUpdate(T entity);
		DBResponse Delete(int id);
		T[] GetBySearchModelAndCurrentView(SearchModel search, string currentView);
		T[] GetCustom(Func<T, bool> condition);
	}
}
