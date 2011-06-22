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

			GamesManager = new GamesManager();

			Application.Run(new FormMain());

			Properties.Settings.Default.Save();
		}
	}
}
