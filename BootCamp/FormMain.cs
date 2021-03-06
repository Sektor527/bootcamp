﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BootCamp
{
	public partial class FormMain : Form
	{
		public FormMain(EnvironmentManager envManager)
		{
			InitializeComponent();

			_environmentManager = envManager;
		}

		private void OnLoad(object sender, EventArgs e)
		{
			Size = Properties.Settings.Default.FormMain_WindowSize;

			_groupAscending = true;
			_groupCategory = GroupCategory.Environment;

			GamesList.ListViewItemSorter = new GamesListSorter();

			CreateGroups();
			FillGamesList();
		}

		private Game SelectedGame
		{
			get
			{
				if (GamesList.SelectedItems.Count != 1) return null;
				return (Game)GamesList.SelectedItems[0].Tag;
			}
		}

		#region Grouping

		private enum GroupCategory
		{
			Name,
			Genre,
			Environment,
			RunCount,
			RunTimestamp,
			None
		}

		private GroupCategory _groupCategory;
		private bool _groupAscending;

		private void OnGrouping(object sender, EventArgs e)
		{
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;

			switch (menuitem.Name)
			{
				case "GroupByName":
					_groupCategory = GroupCategory.Name;
					break;
				case "GroupByGenre":
					_groupCategory = GroupCategory.Genre;
					break;
				case "GroupByEnvironment":
					_groupCategory = GroupCategory.Environment;
					break;
				case "GroupByNone":
					_groupCategory = GroupCategory.None;
					break;
				case "GroupAscending":
					_groupAscending = true;
					break;
				case "GroupDescending":
					_groupAscending = false;
					break;
				default:
					throw new NotImplementedException();
			}

			CreateGroups();

			foreach (ListViewItem item in GamesList.Items)
			{
				AssignItemToGroup(item);
			}
		}

		private void CreateGroups()
		{
			switch (_groupCategory)
			{
				case GroupCategory.Name:
					AlphabeticGrouper grouper = new AlphabeticGrouper { MinimumItemsPerGroup = 15, Ascending = _groupAscending };
					ShowGroups(grouper.Group(Program.GamesManager.Names), "Name", _groupAscending);
					break;
				case GroupCategory.Genre:
					ShowGroups(Program.GamesManager.Genres, "Genre", _groupAscending);
					break;
				case GroupCategory.Environment:
					ShowGroups(new List<string>(Enum.GetNames(typeof(Environments))), "Environment", _groupAscending);
					break;
				case GroupCategory.None:
					HideGroups();
					break;
			}
		}

		internal void ShowGroups(List<string> list, string property, bool ascending)
		{
			GamesList.ShowGroups = true;
			GamesList.Groups.Clear();

			if (ascending)
				list.Sort((x, y) => x.CompareTo(y));
			else
				list.Sort((x, y) => y.CompareTo(x));

			foreach (string s in list)
				GamesList.Groups.Add(s, s);
		}

		private void AssignItemToGroup(ListViewItem item)
		{
			string property = "";
			switch (_groupCategory)
			{
				case GroupCategory.Name:
					property = "Name";
					break;
				case GroupCategory.Genre:
					property = "Genre";
					break;
				case GroupCategory.Environment:
					property = "Environment";
					break;
				default:
					return;
			}

			PropertyInfo prop = typeof(Game).GetProperty(property);

			Game game = (Game) item.Tag;
			if (_groupCategory == GroupCategory.Name)
			{
				foreach (ListViewGroup group in GamesList.Groups)
				{
					if (game.Name.Substring(0, 1).CompareTo(group.Name.Substring(0, 1)) >= 0 &&
					    game.Name.Substring(0, 1).CompareTo(group.Name.Substring(group.Name.Length - 1, 1)) <= 0)
					{
						group.Items.Add(item);
						continue;
					}
				}
			}
			else
				GamesList.Groups[prop.GetValue(game, null).ToString()].Items.Add(item);
		}

		internal void HideGroups()
		{
			GamesList.ShowGroups = false;
		}

		private void OnGroupMenuOpening(object sender, EventArgs e)
		{
			GroupByName.Checked = _groupCategory == GroupCategory.Name;
			GroupByGenre.Checked = _groupCategory == GroupCategory.Genre;
			GroupByEnvironment.Checked = _groupCategory == GroupCategory.Environment;
			GroupByNone.Checked = _groupCategory == GroupCategory.None;

			GroupAscending.Checked = _groupAscending;
			GroupDescending.Checked = !_groupAscending;
		}

		#endregion

		#region Sorting
		private GroupCategory _sortCategory;
		private bool _sortAscending;


		#endregion

		private void FillGamesList()
		{
			GamesList.Items.Clear();

			foreach (Game game in Program.GamesManager)
			{
				if (!string.IsNullOrEmpty(inputSearch.Text) && !game.Name.ToLower().Contains(inputSearch.Text.ToLower())) continue;

				string count = "";
				if (game.RunCount > 0)
					count = game.RunCount.ToString();

				string timestamp = "";
				if (game.RunTimestamp > DateTime.MinValue)
					timestamp = game.RunTimestamp.ToString("d MMMM yyyy, H:mm");

				ListViewItem item = new ListViewItem(new[] { game.Name, game.Genre, game.Environment.ToString(), count, timestamp }, Program.GamesManager.IsFavorite(game) ? 0 : -1) { Tag = game };
				if (!btnToggleFavorites.Checked || Program.GamesManager.IsFavorite(game))
				{
					GamesList.Items.Add(item);
					AssignItemToGroup(item);
				}
			}
		}

		private void OnGamesListDoubleClick(object sender, EventArgs e)
		{
			OnPlayGame(sender, EventArgs.Empty);
		}

		private void OnGamesListKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) OnPlayGame(sender, EventArgs.Empty);
		}

		private void OnAddGame(object sender, EventArgs e)
		{
			FormAddGame form = new FormAddGame();
			if (form.ShowDialog() == DialogResult.Cancel) return;

			Program.GamesManager.Add(form.Game);

			if (!btnToggleFavorites.Checked || Program.GamesManager.IsFavorite(form.Game))
			{
				ListViewItem item = new ListViewItem(new[] { form.Game.Name, form.Game.Genre, form.Game.Environment.ToString() }, Program.GamesManager.IsFavorite(form.Game) ? 0 : -1) { Tag = form.Game };
				GamesList.Items.Add(item);
				AssignItemToGroup(item);
				GamesList.Sort();
			}
		}

		private void OnDeleteGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;

			if (MessageBox.Show(String.Format("Are you sure you want to remove '{0}'?", SelectedGame.Name), "Boot Camp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			// Update game list
			Program.GamesManager.Remove(SelectedGame);

			// Update listview
			ListViewItem item = GamesList.SelectedItems[0];
			GamesList.Items.Remove(item);
		}

		private void OnPlayGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;
			
			Game.Result result = SelectedGame.Run(_environmentManager);
			switch (result)
			{
				case BootCamp.Game.Result.ExecutableEmptyError:
					MessageBox.Show("This game has no executable.\n\nEdit the game and fill in the 'Executable' text box.",
					                "No executable designated", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				case BootCamp.Game.Result.ExecutableDoesNotExistError:
					MessageBox.Show(
						"The executable does not exist.\n\nEdit the game and make sure the 'Executable' text box has a correct reference to an existing executable.",
						"Executable doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
			}

			Program.GamesManager.Save();
			GamesList.SelectedItems[0].SubItems[3].Text = SelectedGame.RunCount.ToString();
			GamesList.SelectedItems[0].SubItems[4].Text = SelectedGame.RunTimestamp.ToString("d MMMM yyyy, H:mm");
		}

		private void OnEditGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;

			Game selected = SelectedGame;

			// Fill information
			FormAddGame form = new FormAddGame(ref selected);
			if (form.ShowDialog() == DialogResult.Cancel) return;

			// Update game list
			Program.GamesManager.Remove(SelectedGame);
			Program.GamesManager.Add(form.Game);

			// Update listview
			ListViewItem item = GamesList.SelectedItems[0];
			item.SubItems[0].Text = form.Game.Name;
			item.SubItems[1].Text = form.Game.Genre;
			item.SubItems[2].Text = form.Game.Environment.ToString();
			item.ImageIndex = Program.GamesManager.IsFavorite(form.Game) ? 0 : 1;
			AssignItemToGroup(item);
			GamesList.Sort();
		}

		private void OnToggleFavorite(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;
			Program.GamesManager.SetFavorite(SelectedGame, !Program.GamesManager.IsFavorite(SelectedGame));

			ListViewItem item = GamesList.SelectedItems[0];
			item.ImageIndex = Program.GamesManager.IsFavorite((Game)item.Tag) ? 0 : 1;
		}

		private void OnDragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void OnDropFile(object sender, DragEventArgs e)
		{
			Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
			if (a == null) return;

			foreach (string filename in a)
			{
				BeginInvoke(new DelegateAddGame(AddGame), new object[] { filename });
			}
		}

		private delegate void DelegateAddGame(string filename);
		private void AddGame(string filename)
		{
			Game game = new Game();
			game.Name = Path.GetFileNameWithoutExtension(filename);
			game.Executable = GetRelativePath(filename);
			game.Environment = (Environments)Enum.Parse(typeof(Environments), Properties.Settings.Default.FormAdd_LastSelectedEnvironment);


			FormAddGame form = new FormAddGame(ref game);
			if (form.ShowDialog() == DialogResult.Cancel) return;

			Program.GamesManager.Add(form.Game);

			if (!btnToggleFavorites.Checked || Program.GamesManager.IsFavorite(form.Game))
			{
				ListViewItem item = new ListViewItem(new[] { form.Game.Name, form.Game.Genre, form.Game.Environment.ToString() }, Program.GamesManager.IsFavorite(form.Game) ? 0 : -1) { Tag = form.Game };
				GamesList.Items.Add(item);
				AssignItemToGroup(item);
				GamesList.Sort();
			}
		}

		private string GetRelativePath(string path)
		{

			string pathpath = path;
			string basepath = System.Environment.CurrentDirectory;
			if (!basepath.EndsWith("\\"))
				basepath += "\\";

			if (pathpath.StartsWith(basepath))
				return pathpath.Replace(basepath, "");
			else
				return path;
		}

		private void OnContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			menuPlayGame.Enabled = GamesList.SelectedItems.Count > 0;
			menuAddGame.Enabled = true;
			menuDeleteGame.Enabled = GamesList.SelectedItems.Count > 0;
			menuEditGame.Enabled = GamesList.SelectedItems.Count > 0;
			menuFavorite.Enabled = GamesList.SelectedItems.Count > 0;
		}

		private void OnResize(object sender, EventArgs e)
		{
			GamesList.Columns[GamesList.Columns.Count - 1].Width = -2;
		}

		private void OnStartDosbox(object sender, EventArgs e)
		{
			EnvironmentManager.Dosbox();
		}

		private void OnStartC64(object sender, EventArgs e)
		{
			EnvironmentManager.C64();
		}

		private void OnStartGameboy(object sender, EventArgs e)
		{
			EnvironmentManager.Gameboy();
		}

		private void OnStartNintendo64(object sender, EventArgs e)
		{
			EnvironmentManager.Nintendo64();
		}

		private void OnStartSuperNintendo(object sender, EventArgs e)
		{
			EnvironmentManager.SuperNintendo();
		}

		private void OnStartZMachine(object sender, EventArgs e)
		{
			EnvironmentManager.ZMachine();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.FormMain_WindowSize = Size;
		}

		private void OnFavoriteToggled(object sender, EventArgs e)
		{
			FillGamesList();
		}

		private void OnGamesListColumnClick(object sender, ColumnClickEventArgs e)
		{
			GamesListSorter sorter = GamesList.ListViewItemSorter as GamesListSorter;

			switch (e.Column)
			{
				case 0: // Name
					sorter.SortCategory = GroupCategory.Name;
					break;
				case 1: // Genre
					sorter.SortCategory = GroupCategory.Genre;
					break;
				case 2: // Environment
					sorter.SortCategory = GroupCategory.Environment;
					break;
				case 3: // Times Played
					sorter.SortCategory = GroupCategory.RunCount;
					break;
				case 4: // Last Played
					sorter.SortCategory = GroupCategory.RunTimestamp;
					break;
			}

			GamesList.Sort();
		}

		private class GamesListSorter : System.Collections.IComparer
		{
			private GroupCategory _sortCategory = GroupCategory.Name;
			private SortOrder _sortOrder = SortOrder.Ascending;

			public GroupCategory SortCategory
			{
				set
				{
					if (_sortCategory != value)
					{
						_sortCategory = value;
						_sortOrder = SortOrder.Ascending;
					}
					else
					{
						_sortOrder = (_sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
					}
				}
			}

			public int Compare(object x, object y)
			{
				if (x.GetType() != typeof(ListViewItem) || y.GetType() != typeof(ListViewItem)) throw new ArgumentException();

				ListViewItem _x = x as ListViewItem;
				ListViewItem _y = y as ListViewItem;

				switch (_sortCategory)
				{
					case GroupCategory.Name:
						return _x.SubItems[0].Text.CompareTo(_y.SubItems[0].Text) * (_sortOrder == SortOrder.Ascending ? 1 : -1);
					case GroupCategory.Genre:
						return _x.SubItems[1].Text.CompareTo(_y.SubItems[1].Text) * (_sortOrder == SortOrder.Ascending ? 1 : -1);
					case GroupCategory.Environment:
						return _x.SubItems[2].Text.CompareTo(_y.SubItems[2].Text) * (_sortOrder == SortOrder.Ascending ? 1 : -1);
					case GroupCategory.RunCount:
						return ((Game)(_x.Tag)).RunCount.CompareTo(((Game)_y.Tag).RunCount) * (_sortOrder == SortOrder.Ascending ? 1 : -1);
					case GroupCategory.RunTimestamp:
						return ((Game)(_x.Tag)).RunTimestamp.CompareTo(((Game)_y.Tag).RunTimestamp) * (_sortOrder == SortOrder.Ascending ? 1 : -1);
					default:
						throw new ArgumentException();
				}
			}
		}

		private EnvironmentManager _environmentManager;

		private void OnSearchChanged(object sender, EventArgs e)
		{
			FillGamesList();
		}

		private void OnSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.Handled = true;
				GamesList.Focus();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Control | Keys.F:
					inputSearch.Focus();
					inputSearch.Text = "";
					return true;

				case Keys.Escape:
					inputSearch.Text = "";
					GamesList.Focus();
					return true;
			}
			
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void theEmulatorZoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.emulator-zone.com");
		}

		private void c64comToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.c64.com");
		}

		private void romsCentralToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.romscentral.com/gb.htm");
		}

		private void romHustlerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://romhustler.net/");
		}

		private void coolROMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.coolrom.com/");
		}

		private void homeOfTheUnderdoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.hotud.org/");
		}

		private void magnusC64GamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://biphome.spray.se/magnus.welander/start.html");
		}

		private void theHouseOfGamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.thehouseofgames.net/");
		}
	}
}
