using System;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.DomainServices {

	public class JobService {
		private static bool IsJobRunning = false;

		public static event Action<string, int> JobStarted;

		public static event Action JobEnded;

		public static event Action<int, string, bool> JobProgressChanged;

		public async static void StartJob(string jobDescription, int jobLength) {
			if (!IsJobRunning) {
				IsJobRunning = true;
				JobStarted?.Invoke(jobDescription, jobLength);
				// task delay so that user would atleast see something happening
				await Task.Delay(1000);
			}
		}

		public static void NotifyJobProgressChange(int progress, string stage = null, bool isIncremental = false) {
			if (IsJobRunning)
				JobProgressChanged?.Invoke(progress, stage, isIncremental);
		}

		public async static void EndJob() {
			await Task.Delay(1000);
			IsJobRunning = false;
			JobEnded?.Invoke();
		}
	}
}