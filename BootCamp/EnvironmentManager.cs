using System;
using System.Diagnostics;
using System.IO;

namespace BootCamp
{
	enum Environments
	{
		Windows,
		Dosbox,
		C64,
		ScummVM,
		Gameboy,
		Nintendo64,
		SuperNintendo,
		GameAndWatch
	}

	internal static class EnvironmentManager
	{
		internal static void Run(Game game)
		{
			switch (game.Environment)
			{
				case Environments.Windows:
				case Environments.GameAndWatch:
					{
						ProcessStartInfo info = new ProcessStartInfo(Path.GetFileName(game.Executable));
						info.WorkingDirectory = Path.GetFullPath(Path.GetDirectoryName(game.Executable));
						Process.Start(info);
						break;
					}
				case Environments.Dosbox:
					{
						ProcessStartInfo info = new ProcessStartInfo("dosbox.exe");
						info.WorkingDirectory = Path.GetFullPath(Path.Combine(Properties.Settings.Default.EmulatorsPath, "Dosbox"));
						info.Arguments = String.Format("\"{0}\" -exit -noconsole -fullscreen", Path.GetFullPath(game.Executable));
						if (!String.IsNullOrEmpty(game.ISO))
						{
							info.Arguments += String.Format(" -c \"imgmount d '{0}' -t iso\"", Path.GetFullPath(game.ISO));
						}
						Process.Start(info);
						break;
					}
				case Environments.C64:
					{
						ProcessStartInfo info = new ProcessStartInfo("ccs64.exe");
						info.WorkingDirectory = Path.GetFullPath(Path.Combine(Properties.Settings.Default.EmulatorsPath, "CCS64"));
						info.Arguments = String.Format("\"{0}\"", Path.GetFullPath(game.Executable));
						Process.Start(info);
						break;
					}
				case Environments.ScummVM:
					{

						break;
					}
				case Environments.Gameboy:
					{
						ProcessStartInfo info = new ProcessStartInfo("VisualBoyAdvance.exe");
						info.WorkingDirectory = Path.GetFullPath(Path.Combine(Properties.Settings.Default.EmulatorsPath, "Gameboy"));
						info.Arguments = String.Format("\"{0}\"", Path.GetFullPath(game.Executable));
						Process.Start(info);
						break;
					}
				case Environments.Nintendo64:
					{
						ProcessStartInfo info = new ProcessStartInfo("project64.exe");
						info.WorkingDirectory = Path.GetFullPath(Path.Combine(Properties.Settings.Default.EmulatorsPath, "nintendo 64"));
						info.Arguments = String.Format("\"{0}\"", Path.GetFullPath(game.Executable));
						Process.Start(info);
						break;
					}
				case Environments.SuperNintendo:
					{
						ProcessStartInfo info = new ProcessStartInfo("snes9x.exe");
						info.WorkingDirectory = Path.GetFullPath(Path.Combine(Properties.Settings.Default.EmulatorsPath, "super nintendo"));
						info.Arguments = String.Format("\"{0}\"", Path.GetFullPath(game.Executable));
						Process.Start(info);
						break;
					}
			}
		}
	}
}
