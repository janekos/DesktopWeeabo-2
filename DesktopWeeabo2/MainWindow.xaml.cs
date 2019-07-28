using System;
using System.Windows;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.ViewModels;

namespace DesktopWeeabo2 {
    public partial class MainWindow : Window {
        public MainWindow() {
			InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
	}
}
