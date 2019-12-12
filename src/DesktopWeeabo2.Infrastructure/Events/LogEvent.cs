using System;

namespace DesktopWeeabo2.Infrastructure.Events {

	public class LogEvent {
		public static string LogContent { get; private set; }

		public static event Action<string> LogLineReceived;

		public static void LogMessage(string message) {
			LogLineReceived?.Invoke(message);
		}

		public static void LogError(Exception ex, string additionalString = null) {
			var thisException = ex;
			var message = !string.IsNullOrEmpty(additionalString)
				? $"{additionalString}{Environment.NewLine}"
				: string.Empty;

			message += $"{thisException.Message}{Environment.NewLine}{thisException.StackTrace}{Environment.NewLine}";


			while (thisException.InnerException != null) {
				thisException = thisException.InnerException;
				message += "--------------- INNER EXCEPTION ---------------";
				message += $"{Environment.NewLine}{thisException.Message.Replace(Environment.NewLine, " ")}{Environment.NewLine}{thisException.StackTrace}{Environment.NewLine}";
			}

			LogLineReceived?.Invoke(message);
		}

		public static void SetLogContent(string value) =>
			LogContent = value;
	}
}