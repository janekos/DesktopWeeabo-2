using System;

namespace DesktopWeeabo2.Infrastructure.DomainServices {
	public class ToastService {
		public static event Action<string, string> ToastMessageRecieved;

		public static void ShowToast(string message, string messageType) {
			ToastMessageRecieved?.Invoke(message, messageType);
		}
	}
}
