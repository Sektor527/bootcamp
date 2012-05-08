using System;
using System.IO;

namespace BootCamp
{
	internal class Game
	{
		public string Name { get; set; }
		public Environments Environment { get; set; }
		public string Genre { get; set; }
		public string Executable { get; set; }
		public string Arguments { get; set; }
		public string ISO { get; set; }

		public Game() : this("", "", "", Environments.Windows, "")
		{
		}

		public Game(string name, string executable, string arguments, Environments environment, string iso)
		{
			Name = name;
			Environment = environment;
			Genre = "";
			Executable = executable;
			Arguments = arguments;
			ISO = iso;
		}

		public void Run(EnvironmentManager envManager)
		{
			if (string.IsNullOrEmpty(Executable)) return;
			if (!File.Exists(Path.GetFullPath(Executable))) return;

			envManager.Run(this);

			RunCount++;
			RunTimestamp = DateTime.Now;
		}

		public int RunCount { get; internal set; }
		public DateTime RunTimestamp { get; internal set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return (obj != null && obj.GetType() == GetType() && obj.GetHashCode() == GetHashCode());
		}

		public override int GetHashCode()
		{
			return 3*Name.GetHashCode() +
			       5*Environment.GetHashCode() +
			       7*Path.GetFileName(Executable).GetHashCode();
		}
	}
}
