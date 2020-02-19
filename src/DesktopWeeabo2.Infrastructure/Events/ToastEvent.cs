using DesktopWeeabo2.Core.Enums;
using System;

namespace DesktopWeeabo2.Infrastructure.Events {

	public static class ToastEvent {

		public static event Action<object, ShowToastEventArgs> ToastMessageRecieved;

		public static void ShowToast(string message, ToastType messageType) {
			ToastMessageRecieved?.Invoke(nameof(ShowToast), new ShowToastEventArgs(message, messageType));
		}
	}

	public class ShowToastEventArgs {
		public string Message { get; private set; }
		public ToastType MessageType { get; private set; }

		public ShowToastEventArgs(string message, ToastType messageType) {
			Message = message;
			MessageType = messageType;
		}
	}
}