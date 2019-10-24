using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Services {
	public interface IHandleIO {
		void ImportDW1Data(string path);
		void ImportDW2Data(string path);
		void UpdateDbEntries();
	}
}
