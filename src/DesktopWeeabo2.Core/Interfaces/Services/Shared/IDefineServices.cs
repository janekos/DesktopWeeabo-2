using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DesktopWeeabo2.Core.Interfaces.Services.Shared {

	public interface IDefineServices<T> where T : BaseModel {

		DBResponse AddOrUpdate(T entity);

		DBResponse AddOrUpdateRange(IEnumerable<T> entities, Action<object> onActionCallback = null);

		DBResponse Delete(int id);

		IEnumerable<T> GetBySearchModelAndCurrentView(SearchModel search, string currentView);

		IEnumerable<T> GetCustom(Expression<Func<T, bool>> condition);

		IEnumerable<T> GetAll();
	}
}