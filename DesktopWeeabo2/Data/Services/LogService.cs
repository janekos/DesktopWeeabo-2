using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services {
	public class LogService {
		public static string LogContent { get; set; }

		public static event Action<string> LogLineReceived;

		public static void LogMessage(string message) {
			LogLineReceived(message);
		}
	}
}
