using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

					if (reader.GetAttribute("RunCount") != null)
						game.RunCount = int.Parse(reader.GetAttribute("RunCount"));

					if (reader.GetAttribute("RunTimestamp") != null)
						game.RunTimestamp = DateTime.ParseExact(reader.GetAttribute("RunTimestamp"), "u", DateTimeFormatInfo.InvariantInfo);

					if (reader.GetAttribute("Favorite") != null)
						game.Favorite = reader.GetAttribute("Favorite") == "True";

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

		internal void Save()
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
					writer.WriteAttributeString("Favorite", game.Favorite.ToString());
					writer.WriteAttributeString("RunCount", game.RunCount.ToString());
					writer.WriteAttributeString("RunTimestamp", game.RunTimestamp.ToString("u", DateTimeFormatInfo.InvariantInfo));
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
		public void SetFavorite(Game game, bool favorite)
		{
			game.Favorite = favorite;
			Save();
		}

		public bool IsFavorite(Game game)
		{
			return game.Favorite;
		}
		#endregion
	}
}
