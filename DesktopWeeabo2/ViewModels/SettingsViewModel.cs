﻿using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.ViewModels {
    class SettingsViewModel : BaseViewModel {
		public bool DoesAppBackUp {
			get;
			set;
		}

		public bool IsLightMode {
			get;
			set;
		}

		public DelegateCommand ImportFromDW1 => new DelegateCommand(new Action<object>((e) => {

		}));
	}
}
