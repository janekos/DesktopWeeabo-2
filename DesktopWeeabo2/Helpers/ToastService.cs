using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Helpers {
	public class ToastService {
		public static event Action<string, string> ToastMessageRecieved;

		public static void ShowToast(string message, string messageType) {
			ToastMessageRecieved(message, messageType);
		}
	}
}
