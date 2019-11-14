namespace DesktopWeeabo2.Core.Interfaces.Services {

	public interface IHandleIO {

		void ImportDW1Data(string path);

		void ImportDW2Data(string path);

		void UpdateDbEntries();
	}
}