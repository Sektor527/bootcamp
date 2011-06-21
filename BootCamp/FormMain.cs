using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BootCamp
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			_ascending = true;
			_groupCategory = GroupCategory.Environment;
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
			None
		}

		private GroupCategory _groupCategory;
		private bool _ascending;

		private void OnGrouping(object sender, EventArgs e)
		{
			ToolStripMenuItem menuitem = (ToolStripMenuItem) sender;

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
					_ascending = true;
					break;
				case "GroupDescending":
					_ascending = false;
					break;
				default:
					throw new NotImplementedException();
			}

			UpdateGroups();
		}

		private void UpdateGroups()
		{
			switch (_groupCategory)
			{
				case GroupCategory.Name:
					throw new NotImplementedException();
				case GroupCategory.Genre:
					ShowGroups(Program.GamesManager.Genres, "Genre", _ascending);
					break;
				case GroupCategory.Environment:
					ShowGroups(new List<string>(Enum.GetNames(typeof(Environments))), "Environment", _ascending);
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

			PropertyInfo prop = typeof(Game).GetProperty(property);
			foreach (ListViewItem item in GamesList.Items)
			{
				Game game = (Game)item.Tag;
				GamesList.Groups[prop.GetValue(game, null).ToString()].Items.Add(item);
			}
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

			GroupAscending.Checked = _ascending;
			GroupDescending.Checked = !_ascending;
		}

		#endregion

		private void FillGamesList()
		{
			GamesList.Items.Clear();

			foreach (Game game in Program.GamesManager)
			{
				ListViewItem item = new ListViewItem(new[] { game.Name, game.Genre, game.Environment.ToString() }) { Tag = game };
				GamesList.Items.Add(item);
			}

			UpdateGroups();
		}

		private void OnGamesListDoubleClick(object sender, EventArgs e)
		{
			LaunchGame();
		}

		private void OnGamesListKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) LaunchGame();
		}

		private void LaunchGame()
		{
			if (SelectedGame == null) return;
			SelectedGame.Run();
		}

		private void OnAddGame(object sender, EventArgs e)
		{
			FormAddGame form = new FormAddGame();
			if (form.ShowDialog() == DialogResult.Cancel) return;

			Program.GamesManager.Add(form.Game);
			FillGamesList();
		}

		private void OnDeleteGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;

			if (MessageBox.Show(String.Format("Are you sure you want to remove '{0}'?", SelectedGame.Name), "Boot Camp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			Program.GamesManager.Remove(SelectedGame);
			FillGamesList();
		}

		private void OnPlayGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;
			SelectedGame.Run();
		}

		private void OnEditGame(object sender, EventArgs e)
		{
			if (SelectedGame == null) return;

			FormAddGame form = new FormAddGame(SelectedGame);
			if (form.ShowDialog() == DialogResult.Cancel) return;

			Program.GamesManager.Remove(SelectedGame);
			Program.GamesManager.Add(form.Game);
			FillGamesList();
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


			FormAddGame form = new FormAddGame(game);
			if (form.ShowDialog() == DialogResult.Cancel) return;

			Program.GamesManager.Add(form.Game);
			FillGamesList();
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
	}
}
