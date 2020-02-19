using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Jobs {

	public interface IRunJobs<T> {

		Task RunJob(params object[] args);
	}
}