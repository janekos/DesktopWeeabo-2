using DesktopWeeabo2.Data.Services;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Windows.Forms;

namespace DesktopWeeabo2.ViewModels {
	public class SettingsViewModel : BaseViewModel {

		public bool DoesAppBackUp { get; set;}
		public bool IsLightMode { get; set;}

		private string _PathToDW1Data { get; set; }
		public string PathToDW1Data {
			get { return _PathToDW1Data; }
			set {
				_PathToDW1Data = value;
				RaisePropertyChanged("PathToDW1Data");
			}
		}

		public DelegateCommand ShowFileSelectorDialog => new DelegateCommand(new Action(() => {
			OpenFileDialog fileDialog = new OpenFileDialog {
				InitialDirectory = string.IsNullOrEmpty(PathToDW1Data)
					? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
					: PathToDW1Data,

				Filter = "XML Files (*.xml)|*.xml",
				FilterIndex = 0,
				DefaultExt = "xml"
			};

			if (fileDialog.ShowDialog() == DialogResult.OK)
				if (!fileDialog.FileName.Contains("MainEntries.xml")) {
					MessageBox.Show(@"DesktopWeeabo 1 used a file called MainEntries.xml to save items. You can find it in Documents/DesktopWeeabo",
						"Invalid Import File",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				} else {
					PathToDW1Data = fileDialog.FileName;
				}
		}));

		public DelegateCommand ImportFromDW1 => new DelegateCommand(new Action(() => {
			if (!string.IsNullOrEmpty(PathToDW1Data)) IOService.ImportDW1Data(PathToDW1Data);
		}));
	}
}
