using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Infrastructure.Events;
using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Jobs.Shared {

	public abstract class BaseJob<T> : IRunJobs<T>, IDisposable {
		private bool disposed = false;
		protected int jobMaxProgress = 0;
		protected string jobTitle = string.Empty;
		protected string jobDescription = string.Empty;
		private Stopwatch watch = new Stopwatch();
		private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		/// <summary>
		/// Args are to be defined by each implementor.
		/// </summary>
		/// <param name="args"></param>
		public async Task RunJob(params object[] args) {
			try {
				await Task.Run(async () => {
					PrepareJob(args);
					StartJob();
					await ExecuteJob();
					EndJob();				
				});
			} catch (Exception ex) {
				EndJob(ex);
			}

			Dispose();
		}

		protected virtual void PrepareJob(object[] args) =>
			throw new InvalidOperationException("Job not prepared.");

		protected virtual void StartJob() {
			watch.Start();

			JobEvent.StartJob(jobDescription, jobMaxProgress);
			LogEvent.LogMessage(JobStartMessage);
			if (!string.IsNullOrEmpty(jobTitle))
				ToastEvent.ShowToast(JobStartMessage, ToastType.INFO);
		}

		#pragma warning disable CS1998
		protected abstract Task ExecuteJob();
		#pragma warning restore CS1998

		protected virtual void EndJob(Exception ex = null) {
			watch.Stop();
			JobEvent.EndJob();

			if (ex == null) {
				LogEvent.LogMessage($"{JobSuccessMessage} Elapsed time: {watch.Elapsed}.");
				if (!string.IsNullOrEmpty(jobTitle))
					ToastEvent.ShowToast(JobSuccessMessage, ToastType.SUCCESS);
			} else {
				LogEvent.LogError(ex, $"{JobFailMessage} Elapsed time: {watch.Elapsed}.");

				if (!string.IsNullOrEmpty(jobTitle))
					ToastEvent.ShowToast($"{JobFailMessage} Reason: {ex.Message}.", ToastType.DANGER);
			}
		}

		private string JobStartMessage { get { return $"Started job: '{jobTitle}'."; } }
		private string JobSuccessMessage { get { return $"Job '{jobTitle}' finished successfully!"; } }
		private string JobFailMessage { get { return $"Job '{jobTitle}' failed."; } }

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (disposed)
				return;

			if (disposing) {
				handle.Dispose();
			}

			disposed = true;
		}
	}
}