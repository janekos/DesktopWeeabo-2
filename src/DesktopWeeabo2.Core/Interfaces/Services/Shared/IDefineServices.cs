using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Services.Shared {

	public interface IDefineServices<T, U> where T : BaseModel where U : BaseEntity {

		Task<DBResponse> AddOrUpdate(T entity);

		Task<DBResponse> AddOrUpdateRange(IEnumerable<T> entities, UpdateMethod updateMethod = UpdateMethod.ALL, Action<object> onActionCallback = null);

		Task<DBResponse> Delete(int id);

		IEnumerable<T> GetBySearchModelAndCurrentView(SearchModel search, string currentView);

		IEnumerable<T> GetCustom(Func<U, bool> condition);
	}
}