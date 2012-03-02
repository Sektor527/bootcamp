using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BootCamp;

namespace BootCampTests
{
	[TestFixture]
	public class GamesTests
	{
		MockEnvironmentManager _voidEnvManager = new MockEnvironmentManager(MockAction.DoNothing);
		MockEnvironmentManager _excEnvManager = new MockEnvironmentManager(MockAction.ThrowException);


		[Test]
		public void Hashing_Self()
		{
			// Compare with self
			Game game1 = new Game("name_1", "path\\exec_1.exe", "arguments_1", Environments.Dosbox, "ISO_1");
			Assert.AreEqual(game1.GetHashCode(), game1.GetHashCode());
		}


		[Test]
		public void Hashing_Other_SingleMatches()
		{
			Game game1 = new Game("name_1", "path\\exec_1.exe", "arguments_1", Environments.Dosbox, "ISO_1");
			Game game2;

			game2 = new Game("name_1", "path\\exec_2.exe", "arguments_2", Environments.Windows, "ISO_2");
			Assert.AreNotEqual(game1.GetHashCode(), game2.GetHashCode());

			game2 = new Game("name_2", "path\\exec_1.exe", "arguments_2", Environments.Windows, "ISO_2");
			Assert.AreNotEqual(game1.GetHashCode(), game2.GetHashCode());

			game2 = new Game("name_2", "path\\exec_2.exe", "arguments_1", Environments.Windows, "ISO_2");
			Assert.AreNotEqual(game1.GetHashCode(), game2.GetHashCode());

			game2 = new Game("name_2", "path\\exec_2.exe", "arguments_2", Environments.Dosbox, "ISO_2");
			Assert.AreNotEqual(game1.GetHashCode(), game2.GetHashCode());

			game2 = new Game("name_2", "path\\exec_2.exe", "arguments_2", Environments.Windows, "ISO_1");
			Assert.AreNotEqual(game1.GetHashCode(), game2.GetHashCode());
		}

		[Test]
		public void Hashing_Other_AllMatches()
		{
			Game game1 = new Game("name_1", "path\\exec_1.exe", "arguments_1", Environments.Dosbox, "ISO_1");
			Game game2;

			game2 = new Game("name_1", "path\\exec_1.exe", "arguments_1", Environments.Dosbox, "ISO_1");
			Assert.AreEqual(game1.GetHashCode(), game2.GetHashCode());
		}

		[Test]
		public void Hashing_Other_NecessaryMatches()
		{
			Game game1 = new Game("name_1", "path\\exec_1.exe", "arguments_1", Environments.Dosbox, "ISO_1");
			Game game2;

			// Exact same executable path
			game2 = new Game("name_1", "path\\exec_1.exe", "arguments_2", Environments.Dosbox, "ISO_2");
			Assert.AreEqual(game1.GetHashCode(), game2.GetHashCode());

			// Only same executable filename
			game2 = new Game("name_1", "other_path\\exec_1.exe", "arguments_2", Environments.Dosbox, "ISO_2");
			Assert.AreEqual(game1.GetHashCode(), game2.GetHashCode());
		}

		[Test]
		public void RunCount_Increment()
		{
			Game game1 = new Game("name", "path\\exec.exe", "arguments", Environments.Dosbox, "ISO");
			Assert.AreEqual(0, game1.RunCount);

			game1.Run(_voidEnvManager);
			Assert.AreEqual(1, game1.RunCount);

			game1.Run(_voidEnvManager);
			Assert.AreEqual(2, game1.RunCount);
		}

		[Test]
		public void RunCount_RunFailed()
		{
			Game game1 = new Game("name", "path\\exec.exe", "arguments", Environments.Dosbox, "ISO");

			try { game1.Run(_excEnvManager); }
			catch (Win32Exception) { }
			Assert.AreEqual(0, game1.RunCount);

			try { game1.Run(_excEnvManager); }
			catch (Win32Exception) { }
			Assert.AreEqual(0, game1.RunCount);
		}
	}
}
