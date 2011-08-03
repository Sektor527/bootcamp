using System;
using System.Windows.Forms;

namespace BootCamp
{
	internal partial class FormAddGame : Form
	{
		internal Game Game { get; private set; }

		internal FormAddGame()
		{
			InitializeComponent();

			Game = new Game();

			UpdateEnvironmentList();
			UpdateGenreList();
			UpdateFields();
		}

		internal FormAddGame(ref Game game)
		{
			InitializeComponent();

			Game = game;

			UpdateEnvironmentList();
			UpdateGenreList();
			UpdateFields();
		}

		private void UpdateEnvironmentList()
		{
			foreach (string envname in Enum.GetNames(typeof(Environments)))
				lstEnvironment.Items.Add(envname);
		}

		private void UpdateGenreList()
		{
			foreach (string genre in Program.GamesManager.Genres)
				lstGenre.Items.Add(genre);
		}

		private void UpdateFields()
		{
			txtName.Text = Game.Name;
			txtExecutable.Text = Game.Executable;
			txtArguments.Text = Game.Arguments;
			txtISO.Text = Game.ISO;
			lstGenre.Text = Game.Genre;
			lstEnvironment.Text = Game.Environment.ToString();
		}

		private void OnClosed(object sender, FormClosedEventArgs e)
		{
			Game.Name = txtName.Text;
			Game.Executable = txtExecutable.Text;
			Game.Arguments = txtArguments.Text;
			Game.ISO = txtISO.Text;
			Game.Genre = lstGenre.Text;
			Game.Environment = (Environments)Enum.Parse(typeof(Environments), lstEnvironment.Text, true);

			Properties.Settings.Default.FormAdd_LastSelectedEnvironment = lstEnvironment.Text;
		}
	}
}
