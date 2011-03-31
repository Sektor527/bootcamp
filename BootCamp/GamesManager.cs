﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BootCamp
{
	class GamesManager : IList<Game>
	{
		private List<Game> _games = new List<Game>();

		public GamesManager()
		{
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
			if (!File.Exists(Properties.Settings.Default.BootListPath)) return;

			XmlReader reader = null;

			try
			{
				reader = XmlReader.Create(Properties.Settings.Default.BootListPath);

				reader.ReadStartElement("Games");
				while (reader.IsStartElement("Game"))
				{
					Game game = new Game();
					game.Name = reader.GetAttribute("Name");
					game.Executable = reader.GetAttribute("Executable");
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
				writer = XmlWriter.Create(Properties.Settings.Default.BootListPath, settings);
				writer.WriteStartElement("Games");
				foreach (Game game in _games)
				{
					writer.WriteStartElement("Game");
					writer.WriteAttributeString("Name", game.Name);
					writer.WriteAttributeString("Executable", game.Executable);
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
	}
}