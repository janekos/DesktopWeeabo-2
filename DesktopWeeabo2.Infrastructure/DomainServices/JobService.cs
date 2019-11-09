using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.DomainServices {
	public class JobService {
		private static bool IsJobRunning = false;

		public static event Action<string, int> JobStarted;
		public static event Action JobEnded;
		public static event Action<int> JobProgressChanged;

		public static void StartJob(string jobDescription, int jobLength) {
			if (!IsJobRunning) {
				IsJobRunning = true;
				JobStarted?.Invoke(jobDescription, jobLength);
			}
		}

		public static void NotifyJobProgressChange(int progress) {
			if(IsJobRunning) JobProgressChanged?.Invoke(progress);
		}

		public static void EndJob() {
			IsJobRunning = false;
			JobEnded?.Invoke();
		}
	}
}
