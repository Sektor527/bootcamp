using System;
using System.Windows.Forms;

namespace BootCamp
{
	internal partial class FormAddGame : Form
	{
		private readonly Game _game = new Game();

		internal Game Game { get { return _game; } }

		internal FormAddGame()
		{
			InitializeComponent();

			UpdateEnvironmentList();
			UpdateFields();
		}

		internal FormAddGame(Game game)
		{
			InitializeComponent();

			_game.Name = game.Name;
			_game.Environment = game.Environment;
			_game.Executable = game.Executable;
			_game.Arguments = game.Arguments;
			_game.ISO = game.ISO;
			_game.Genre = game.Genre;

			UpdateEnvironmentList();
			UpdateFields();
		}

		private void UpdateEnvironmentList()
		{
			foreach (string envname in Enum.GetNames(typeof(Environments)))
				lstEnvironment.Items.Add(envname);
		}

		private void UpdateFields()
		{
			txtName.Text = _game.Name;
			txtExecutable.Text = _game.Executable;
			txtArguments.Text = _game.Arguments;
			txtISO.Text = _game.ISO;
			lstGenre.Text = _game.Genre;
			lstEnvironment.Text = _game.Environment.ToString();
		}

		private void OnClosed(object sender, FormClosedEventArgs e)
		{
			_game.Name = txtName.Text;
			_game.Executable = txtExecutable.Text;
			_game.Arguments = txtArguments.Text;
			_game.ISO = txtISO.Text;
			_game.Genre = lstGenre.Text;
			_game.Environment = (Environments)Enum.Parse(typeof(Environments), lstEnvironment.Text, true);

			Properties.Settings.Default.FormAdd_LastSelectedEnvironment = lstEnvironment.Text;
		}
	}
}
