using System;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Events {

	public static class JobEvent {
		private static bool IsJobRunning = false;

		public static event Action<object, JobStartedEventArgs> JobStarted;
		public static event Action<object> JobEnded;
		public static event Action<object, JobProgressChangedEventArgs> JobProgressChanged;

		public async static Task StartJob(string jobDescription, int jobLength) {
			if (!IsJobRunning) {
				IsJobRunning = true;
				JobStarted?.Invoke(nameof(StartJob), new JobStartedEventArgs(jobDescription, jobLength));
				// task delay so that user would atleast see something happening
				await Task.Delay(1000);
			}
		}

		public static void NotifyJobProgressChange(int progress, string stage = null, bool isIncremental = false) {
			if (IsJobRunning)
				JobProgressChanged?.Invoke(nameof(NotifyJobProgressChange), new JobProgressChangedEventArgs(progress, stage, isIncremental));
		}

		public async static Task EndJob() {
			await Task.Delay(1000);
			IsJobRunning = false;
			JobEnded?.Invoke(nameof(EndJob));
		}
	}

	public class JobStartedEventArgs : EventArgs {
		public string JobDescription { get; private set; }
		public int JobLength { get; private set; }
		
		public JobStartedEventArgs(string jobDescription, int jobLength) {
			JobDescription = jobDescription;
			JobLength = jobLength;
		}
	}

	public class JobProgressChangedEventArgs : EventArgs {
		public int Progress { get; private set; }
		public string StageDescriptor { get; private set; }
		public bool IsIncremental { get; private set; }

		public JobProgressChangedEventArgs(int progress, string stageDescriptor, bool isIncremental) {
			Progress = progress;
			StageDescriptor = stageDescriptor;
			IsIncremental = isIncremental;
		}
	}
}