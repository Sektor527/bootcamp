namespace BootCamp
{
	partial class FormAddGame
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
			System.Windows.Forms.Button btnCancel;
			System.Windows.Forms.Button btnOK;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			this.txtExecutable = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lstGenre = new System.Windows.Forms.ComboBox();
			this.lstEnvironment = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtISO = new System.Windows.Forms.TextBox();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(203, 174);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 23);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOK.Location = new System.Drawing.Point(122, 174);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 23);
			btnOK.TabIndex = 4;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(35, 13);
			label1.TabIndex = 6;
			label1.Text = "Name";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 43);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(60, 13);
			label2.TabIndex = 7;
			label2.Text = "Executable";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 110);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(36, 13);
			label3.TabIndex = 8;
			label3.Text = "Genre";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(12, 144);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(66, 13);
			label4.TabIndex = 9;
			label4.Text = "Environment";
			// 
			// txtExecutable
			// 
			this.txtExecutable.Location = new System.Drawing.Point(84, 40);
			this.txtExecutable.Name = "txtExecutable";
			this.txtExecutable.Size = new System.Drawing.Size(195, 20);
			this.txtExecutable.TabIndex = 1;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(84, 6);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(195, 20);
			this.txtName.TabIndex = 0;
			// 
			// lstGenre
			// 
			this.lstGenre.FormattingEnabled = true;
			this.lstGenre.Location = new System.Drawing.Point(84, 107);
			this.lstGenre.Name = "lstGenre";
			this.lstGenre.Size = new System.Drawing.Size(194, 21);
			this.lstGenre.TabIndex = 2;
			// 
			// lstEnvironment
			// 
			this.lstEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstEnvironment.FormattingEnabled = true;
			this.lstEnvironment.Location = new System.Drawing.Point(84, 141);
			this.lstEnvironment.Name = "lstEnvironment";
			this.lstEnvironment.Size = new System.Drawing.Size(194, 21);
			this.lstEnvironment.Sorted = true;
			this.lstEnvironment.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 76);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "ISO";
			// 
			// txtISO
			// 
			this.txtISO.Location = new System.Drawing.Point(84, 73);
			this.txtISO.Name = "txtISO";
			this.txtISO.Size = new System.Drawing.Size(195, 20);
			this.txtISO.TabIndex = 11;
			// 
			// FormAddGame
			// 
			this.AcceptButton = btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = btnCancel;
			this.ClientSize = new System.Drawing.Size(291, 209);
			this.Controls.Add(this.txtISO);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lstEnvironment);
			this.Controls.Add(this.lstGenre);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtExecutable);
			this.Controls.Add(label4);
			this.Controls.Add(label3);
			this.Controls.Add(label2);
			this.Controls.Add(label1);
			this.Controls.Add(btnOK);
			this.Controls.Add(btnCancel);
			this.Name = "FormAddGame";
			this.Text = "Game Properties";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtExecutable;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ComboBox lstGenre;
		private System.Windows.Forms.ComboBox lstEnvironment;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtISO;


	}
}