using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Models {
	class SettingsModel {
		public bool DoesAppBackUp { get; set; }
		public bool IsLightMode { get; set; }
		public string AppDataPath { get; set; }
	}
}
