using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BootCamp
{
	static class Program
	{
		public static GamesManager GamesManager { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Properties.Settings.Default.Reload();

			GamesManager = new GamesManager(Properties.Settings.Default.BootListPath);

			Application.Run(new FormMain(new EnvironmentManager()));

			Properties.Settings.Default.Save();
		}
	}
}
