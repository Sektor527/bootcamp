using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using BootCamp;

namespace BootCampTests

{
	[TestFixture]
	public class Favorites
	{
		GamesManager _manager;
		Game _game;

		[SetUp]
		public void Init()
		{
			_manager = new GamesManager("bootlist.cfg");
			_game = new Game();

			_manager.Add(_game);
		}

		[TearDown]
		public void Dispose()
		{
			File.Delete("bootlist.cfg");
		}

		[Test]
		public void SetFavorite()
		{
			_manager.SetFavorite(_game, true);

			Assert.AreEqual(1, _manager.CountFavorites);
			Assert.IsTrue(_manager.IsFavorite(_game));
		}

		[Test]
		public void SetFavoriteTwice()
		{
			_manager.SetFavorite(_game, true);
			_manager.SetFavorite(_game, true);
			Assert.AreEqual(1, _manager.CountFavorites);
			Assert.IsTrue(_manager.IsFavorite(_game));
		}

		[Test]
		public void SetNonExistingFavorite()
		{
			Game _nogame = new Game();
			_manager.SetFavorite(_nogame, true);

			Assert.AreEqual(0, _manager.CountFavorites);
			Assert.IsFalse(_manager.IsFavorite(_nogame));
		}

		[Test]
		public void UnsetFavorite()
		{
			_manager.SetFavorite(_game, true);
			_manager.SetFavorite(_game, false);
			Assert.AreEqual(0, _manager.CountFavorites);
			Assert.IsFalse(_manager.IsFavorite(_game));
		}

		[Test]
		public void UnsetNonExistingFavorite()
		{
			_manager.SetFavorite(_game, false);
			Assert.AreEqual(0, _manager.CountFavorites);
			Assert.IsFalse(_manager.IsFavorite(_game));
		}
	}
}
