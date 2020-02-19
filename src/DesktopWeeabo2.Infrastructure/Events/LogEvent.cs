using System;
using System.Text;

namespace DesktopWeeabo2.Infrastructure.Events {

	public static class LogEvent {
		public static string LogContent { get; private set; }

		public static event Action<object, LogLineReceivedEventArgs> LogLineReceived;

		public static void LogMessage(string message) {
			LogLineReceived?.Invoke(nameof(LogMessage), new LogLineReceivedEventArgs(message));
		}

		public static void LogError(Exception ex, string additionalString = null) {
			var thisException = ex;
			var message = new StringBuilder(
				!string.IsNullOrEmpty(additionalString)
					? $"{additionalString}{Environment.NewLine}"
					: string.Empty);

			message.Append($"{thisException.Message}{Environment.NewLine}{thisException.StackTrace}{Environment.NewLine}");

			while (thisException.InnerException != null) {
				thisException = thisException.InnerException;
				message.Append("--------------- INNER EXCEPTION ---------------");
				message.Append($"{Environment.NewLine}{thisException.Message.Replace(Environment.NewLine, " ")}{Environment.NewLine}{thisException.StackTrace}{Environment.NewLine}");
			}

			LogLineReceived?.Invoke(nameof(LogError), new LogLineReceivedEventArgs(message.ToString()));
		}

		public static void SetLogContent(string value) =>
			LogContent = value;
	}

	public class LogLineReceivedEventArgs : EventArgs {
		public string Message { get; private set; }

		public LogLineReceivedEventArgs(string message) {
			Message = message;
		}
	}
}