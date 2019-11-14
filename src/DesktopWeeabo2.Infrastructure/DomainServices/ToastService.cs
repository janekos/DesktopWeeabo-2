using DesktopWeeabo2.Core.Enums;
using System;

namespace DesktopWeeabo2.Infrastructure.DomainServices {

	public class ToastService {

		public static event Action<string, ToastType> ToastMessageRecieved;

		public static void ShowToast(string message, ToastType messageType) {
			ToastMessageRecieved?.Invoke(message, messageType);
		}
	}
}