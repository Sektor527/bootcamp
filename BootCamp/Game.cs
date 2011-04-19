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

		public Game()
		{
			Environment = Environments.Windows;
		}
		public Game(string name, string executable, string arguments, Environments environment, string iso)
		{
			Name = name;
			Executable = executable;
			Arguments = arguments;
			Environment = environment;
			ISO = iso;
		}

		public void Run()
		{
			if (string.IsNullOrEmpty(Executable)) return;

			EnvironmentManager.Run(this);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
