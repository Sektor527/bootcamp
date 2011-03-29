using System.IO;

namespace BootCamp
{
	internal class Game
	{
		public string Name { get; set; }
		public Environments Environment { get; set; }
		public string Genre { get; set; }
		public string Executable { get; set; }

		public Game()
		{
			Environment = Environments.Windows;
		}
		public Game(string name, string executable, Environments environment)
		{
			Name = name;
			Executable = executable;
			Environment = environment;
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
