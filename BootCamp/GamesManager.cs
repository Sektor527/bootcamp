using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BootCamp
{
	class GamesManager : IList<Game>
	{
		private List<Game> _games = new List<Game>();
		private string _bootListPath;

		public List<string> Names 
		{ 
			get 
			{ 
				List<string> result = new List<string>(); 
				foreach (Game game in _games) 
					result.Add(game.Name);
				return result;
			}
		} 

		public GamesManager(string bootListPath)
		{
			_bootListPath = bootListPath;
			Load();
		}

		public IEnumerator<Game> GetEnumerator()
		{
			return _games.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(Game item)
		{
			_games.Add(item);
			Save();
		}

		public void Clear()
		{
			_games.Clear();
			Save();
		}

		public bool Contains(Game item)
		{
			return _games.Contains(item);
		}

		public void CopyTo(Game[] array, int arrayIndex)
		{
			_games.CopyTo(array, arrayIndex);
			Save();
		}

		public bool Remove(Game item)
		{
			bool result = _games.Remove(item);
			Save();
			return result;
		}

		public int Count
		{
			get { return _games.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public List<string> Genres
		{
			get
			{
				List<string> result = new List<string>();
				foreach (Game game in _games)
				{
					if (!result.Contains(game.Genre))
						result.Add(game.Genre);
				}

				result.Sort();
				return result;
			}
		}

		public int IndexOf(Game item)
		{
			return _games.IndexOf(item);
		}

		public void Insert(int index, Game item)
		{
			_games.Insert(index, item);
			Save();
		}

		public void RemoveAt(int index)
		{
			_games.RemoveAt(index);
			Save();
		}

		public Game this[int index]
		{
			get { return _games[index]; }
			set { _games[index] = value; }
		}

		private void Load()
		{
			if (!File.Exists(_bootListPath)) return;

			XmlReader reader = null;

			try
			{
				reader = XmlReader.Create(_bootListPath);

				reader.ReadStartElement("Games");
				while (reader.IsStartElement("Game"))
				{
					Game game = new Game();
					game.Name = reader.GetAttribute("Name");
					game.Executable = reader.GetAttribute("Executable");
					game.ISO = reader.GetAttribute("ISO");
					game.Environment = (Environments)Enum.Parse(typeof(Environments), reader.GetAttribute("Environment"), true);
					game.Genre = reader.GetAttribute("Genre");
					reader.ReadStartElement();

					_games.Add(game);
				}
				reader.ReadEndElement();
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		private void Save()
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			XmlWriter writer = null;

			try
			{
				writer = XmlWriter.Create(_bootListPath, settings);
				writer.WriteStartElement("Games");
				foreach (Game game in _games)
				{
					writer.WriteStartElement("Game");
					writer.WriteAttributeString("Name", game.Name);
					writer.WriteAttributeString("Executable", game.Executable);
					writer.WriteAttributeString("ISO", game.ISO);
					writer.WriteAttributeString("Environment", game.Environment.ToString());
					writer.WriteAttributeString("Genre", game.Genre);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		#region Favorites
		private List<Game> _favorites = new List<Game>();
		public int CountFavorites
		{
			get { return _favorites.Count; }
		}

		public void SetFavorite(Game game, bool favorite)
		{
			if (!_games.Contains(game)) return;

			if (favorite && !_favorites.Contains(game))
				_favorites.Add(game);

			if (!favorite && _favorites.Contains(game))
				_favorites.Remove(game);
		}

		public bool IsFavorite(Game game)
		{
			return _favorites.Contains(game);
		}

		public void SaveFavorites()
		{
			foreach (Game game in _favorites)
				Properties.Settings.Default.FavoritesList.Add(game.Name);
		}

		public void LoadFavorites()
		{
			foreach (String name in Properties.Settings.Default.FavoritesList)
				_favorites.Add(_games.Find(g => g.Name == name));
		}

		#endregion
	}
}
