using System;

namespace DesktopWeeabo2.Helpers {
	public class ToastService {
		public static event Action<string, string> ToastMessageRecieved;

		public static void ShowToast(string message, string messageType) {
			ToastMessageRecieved(message, messageType);
		}
	}
}
