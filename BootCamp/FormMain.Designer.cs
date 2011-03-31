﻿namespace BootCamp
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.GroupByName = new System.Windows.Forms.ToolStripMenuItem();
			this.GroupByGenre = new System.Windows.Forms.ToolStripMenuItem();
			this.GroupByEnvironment = new System.Windows.Forms.ToolStripMenuItem();
			this.GroupByNone = new System.Windows.Forms.ToolStripMenuItem();
			this.GroupAscending = new System.Windows.Forms.ToolStripMenuItem();
			this.GroupDescending = new System.Windows.Forms.ToolStripMenuItem();
			this.GamesList = new System.Windows.Forms.ListView();
			this.Game = new System.Windows.Forms.ColumnHeader();
			this.Genre = new System.Windows.Forms.ColumnHeader();
			this.Environment = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuPlayGame = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuAddGame = new System.Windows.Forms.ToolStripMenuItem();
			this.menuDeleteGame = new System.Windows.Forms.ToolStripMenuItem();
			this.menuEditGame = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnPlayGame = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnAddGame = new System.Windows.Forms.ToolStripButton();
			this.btnDeleteGame = new System.Windows.Forms.ToolStripButton();
			this.btnEditGame = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupByName
			// 
			this.GroupByName.Name = "GroupByName";
			this.GroupByName.Size = new System.Drawing.Size(152, 22);
			this.GroupByName.Text = "Name";
			this.GroupByName.Click += new System.EventHandler(this.OnGrouping);
			// 
			// GroupByGenre
			// 
			this.GroupByGenre.Name = "GroupByGenre";
			this.GroupByGenre.Size = new System.Drawing.Size(152, 22);
			this.GroupByGenre.Text = "Genre";
			this.GroupByGenre.Click += new System.EventHandler(this.OnGrouping);
			// 
			// GroupByEnvironment
			// 
			this.GroupByEnvironment.Name = "GroupByEnvironment";
			this.GroupByEnvironment.Size = new System.Drawing.Size(152, 22);
			this.GroupByEnvironment.Text = "Environment";
			this.GroupByEnvironment.Click += new System.EventHandler(this.OnGrouping);
			// 
			// GroupByNone
			// 
			this.GroupByNone.Name = "GroupByNone";
			this.GroupByNone.Size = new System.Drawing.Size(152, 22);
			this.GroupByNone.Text = "(None)";
			this.GroupByNone.Click += new System.EventHandler(this.OnGrouping);
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
			// 
			// GroupAscending
			// 
			this.GroupAscending.Name = "GroupAscending";
			this.GroupAscending.Size = new System.Drawing.Size(152, 22);
			this.GroupAscending.Text = "Ascending";
			this.GroupAscending.Click += new System.EventHandler(this.OnGrouping);
			// 
			// GroupDescending
			// 
			this.GroupDescending.Name = "GroupDescending";
			this.GroupDescending.Size = new System.Drawing.Size(152, 22);
			this.GroupDescending.Text = "Descending";
			this.GroupDescending.Click += new System.EventHandler(this.OnGrouping);
			// 
			// GamesList
			// 
			this.GamesList.AllowDrop = true;
			this.GamesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.GamesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Game,
            this.Genre,
            this.Environment});
			this.GamesList.ContextMenuStrip = this.contextMenuStrip1;
			this.GamesList.FullRowSelect = true;
			this.GamesList.Location = new System.Drawing.Point(0, 28);
			this.GamesList.MultiSelect = false;
			this.GamesList.Name = "GamesList";
			this.GamesList.Size = new System.Drawing.Size(652, 296);
			this.GamesList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.GamesList.TabIndex = 2;
			this.GamesList.UseCompatibleStateImageBehavior = false;
			this.GamesList.View = System.Windows.Forms.View.Details;
			this.GamesList.DoubleClick += new System.EventHandler(this.OnGamesListDoubleClick);
			this.GamesList.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDropFile);
			this.GamesList.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
			this.GamesList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGamesListKey);
			// 
			// Game
			// 
			this.Game.Text = "Game";
			this.Game.Width = 203;
			// 
			// Genre
			// 
			this.Genre.Text = "Genre";
			this.Genre.Width = 322;
			// 
			// Environment
			// 
			this.Environment.Text = "Environment";
			this.Environment.Width = 123;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPlayGame,
            this.toolStripSeparator4,
            this.menuAddGame,
            this.menuDeleteGame,
            this.menuEditGame});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(142, 98);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
			// 
			// menuPlayGame
			// 
			this.menuPlayGame.Name = "menuPlayGame";
			this.menuPlayGame.Size = new System.Drawing.Size(141, 22);
			this.menuPlayGame.Text = "Play Game";
			this.menuPlayGame.Click += new System.EventHandler(this.OnPlayGame);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(138, 6);
			// 
			// menuAddGame
			// 
			this.menuAddGame.Name = "menuAddGame";
			this.menuAddGame.Size = new System.Drawing.Size(141, 22);
			this.menuAddGame.Text = "Add Game";
			this.menuAddGame.Click += new System.EventHandler(this.OnAddGame);
			// 
			// menuDeleteGame
			// 
			this.menuDeleteGame.Name = "menuDeleteGame";
			this.menuDeleteGame.Size = new System.Drawing.Size(141, 22);
			this.menuDeleteGame.Text = "Delete Game";
			this.menuDeleteGame.Click += new System.EventHandler(this.OnDeleteGame);
			// 
			// menuEditGame
			// 
			this.menuEditGame.Name = "menuEditGame";
			this.menuEditGame.Size = new System.Drawing.Size(141, 22);
			this.menuEditGame.Text = "Edit Game";
			this.menuEditGame.Click += new System.EventHandler(this.OnEditGame);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayGame,
            this.toolStripSeparator1,
            this.btnAddGame,
            this.btnDeleteGame,
            this.btnEditGame,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(652, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnPlayGame
			// 
			this.btnPlayGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPlayGame.Image = global::BootCamp.Properties.Resources.media_playback_start_7;
			this.btnPlayGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPlayGame.Name = "btnPlayGame";
			this.btnPlayGame.Size = new System.Drawing.Size(23, 22);
			this.btnPlayGame.Text = "Play game";
			this.btnPlayGame.Click += new System.EventHandler(this.OnPlayGame);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnAddGame
			// 
			this.btnAddGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAddGame.Image = global::BootCamp.Properties.Resources.edit_add_2;
			this.btnAddGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAddGame.Name = "btnAddGame";
			this.btnAddGame.Size = new System.Drawing.Size(23, 22);
			this.btnAddGame.Text = "Add Game";
			this.btnAddGame.Click += new System.EventHandler(this.OnAddGame);
			// 
			// btnDeleteGame
			// 
			this.btnDeleteGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDeleteGame.Image = global::BootCamp.Properties.Resources.edit_remove_3;
			this.btnDeleteGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDeleteGame.Name = "btnDeleteGame";
			this.btnDeleteGame.Size = new System.Drawing.Size(23, 22);
			this.btnDeleteGame.Text = "Delete Game";
			this.btnDeleteGame.Click += new System.EventHandler(this.OnDeleteGame);
			// 
			// btnEditGame
			// 
			this.btnEditGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnEditGame.Image = global::BootCamp.Properties.Resources.edit_3;
			this.btnEditGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEditGame.Name = "btnEditGame";
			this.btnEditGame.Size = new System.Drawing.Size(23, 22);
			this.btnEditGame.Text = "Edit Game";
			this.btnEditGame.Click += new System.EventHandler(this.OnEditGame);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroupByName,
            this.GroupByGenre,
            this.GroupByEnvironment,
            this.GroupByNone,
            toolStripSeparator3,
            this.GroupDescending,
            this.GroupAscending});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(69, 22);
			this.toolStripDropDownButton1.Text = "Group By";
			this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(this.OnGroupMenuOpening);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 324);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.GamesList);
			this.Name = "FormMain";
			this.Text = "Boot Camp";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView GamesList;
		private System.Windows.Forms.ColumnHeader Game;
		private System.Windows.Forms.ColumnHeader Environment;
		private System.Windows.Forms.ColumnHeader Genre;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAddGame;
		private System.Windows.Forms.ToolStripButton btnDeleteGame;
		private System.Windows.Forms.ToolStripButton btnPlayGame;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnEditGame;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuPlayGame;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem menuAddGame;
		private System.Windows.Forms.ToolStripMenuItem menuDeleteGame;
		private System.Windows.Forms.ToolStripMenuItem menuEditGame;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem GroupByName;
		private System.Windows.Forms.ToolStripMenuItem GroupByGenre;
		private System.Windows.Forms.ToolStripMenuItem GroupByEnvironment;
		private System.Windows.Forms.ToolStripMenuItem GroupByNone;
		private System.Windows.Forms.ToolStripMenuItem GroupAscending;
		private System.Windows.Forms.ToolStripMenuItem GroupDescending;
	}
}
