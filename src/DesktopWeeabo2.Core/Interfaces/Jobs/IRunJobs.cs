﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Jobs {
	public interface IRunJobs<T> {
		void RunJob(params object[] args);
	}
}
