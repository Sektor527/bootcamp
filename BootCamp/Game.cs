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
		public bool Favorite { get; set; }

		public Game() : this("", "", "", Environments.Windows, "", false)
		{
		}

		public Game(string name, string executable, string arguments, Environments environment, string iso, bool favorite)
		{
			Name = name;
			Environment = environment;
			Genre = "";
			Executable = executable;
			Arguments = arguments;
			ISO = iso;
			Favorite = favorite;
		}

		public Result Run(EnvironmentManager envManager)
		{
			if (string.IsNullOrEmpty(Executable)) return Result.ExecutableEmptyError;
			if (!File.Exists(Path.GetFullPath(Executable))) return Result.ExecutableDoesNotExistError;

			envManager.Run(this);

			RunCount++;
			RunTimestamp = DateTime.Now;

			return Result.OK;
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

		public enum Result
		{
			OK,

			ExecutableEmptyError,
			ExecutableDoesNotExistError
		}
	}
}
