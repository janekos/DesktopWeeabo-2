using System;

namespace DesktopWeeabo2.Infrastructure.Events {

	public class LogEvent {
		public static string LogContent { get; set; }

		public static event Action<string> LogLineReceived;

		public static void LogMessage(string message) {
			LogLineReceived?.Invoke(message);
		}
	}
}