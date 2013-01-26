namespace FIFALoungeMode
{
    partial class Main
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tbpStandings = new System.Windows.Forms.TabPage();
            this.lstvStandings = new System.Windows.Forms.ListView();
            this.tbpScorers = new System.Windows.Forms.TabPage();
            this.lstvScorers = new System.Windows.Forms.ListView();
            this.tbpGames = new System.Windows.Forms.TabPage();
            this.lstvGames = new System.Windows.Forms.ListView();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemAddGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemEditProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemEditTeams = new System.Windows.Forms.ToolStripMenuItem();
            this.ckbVersion = new System.Windows.Forms.CheckBox();
            this.tabMain.SuspendLayout();
            this.tbpStandings.SuspendLayout();
            this.tbpScorers.SuspendLayout();
            this.tbpGames.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tbpStandings);
            this.tabMain.Controls.Add(this.tbpScorers);
            this.tabMain.Controls.Add(this.tbpGames);
            this.tabMain.Location = new System.Drawing.Point(0, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(584, 233);
            this.tabMain.TabIndex = 0;
            // 
            // tbpStandings
            // 
            this.tbpStandings.Controls.Add(this.lstvStandings);
            this.tbpStandings.Location = new System.Drawing.Point(4, 22);
            this.tbpStandings.Name = "tbpStandings";
            this.tbpStandings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpStandings.Size = new System.Drawing.Size(576, 207);
            this.tbpStandings.TabIndex = 0;
            this.tbpStandings.Text = "Standings";
            this.tbpStandings.UseVisualStyleBackColor = true;
            // 
            // lstvStandings
            // 
            this.lstvStandings.Location = new System.Drawing.Point(0, 6);
            this.lstvStandings.Name = "lstvStandings";
            this.lstvStandings.Size = new System.Drawing.Size(576, 201);
            this.lstvStandings.TabIndex = 0;
            this.lstvStandings.UseCompatibleStateImageBehavior = false;
            // 
            // tbpScorers
            // 
            this.tbpScorers.Controls.Add(this.lstvScorers);
            this.tbpScorers.Location = new System.Drawing.Point(4, 22);
            this.tbpScorers.Name = "tbpScorers";
            this.tbpScorers.Padding = new System.Windows.Forms.Padding(3);
            this.tbpScorers.Size = new System.Drawing.Size(576, 207);
            this.tbpScorers.TabIndex = 1;
            this.tbpScorers.Text = "Scorers";
            this.tbpScorers.UseVisualStyleBackColor = true;
            // 
            // lstvScorers
            // 
            this.lstvScorers.Location = new System.Drawing.Point(0, 4);
            this.lstvScorers.Name = "lstvScorers";
            this.lstvScorers.Size = new System.Drawing.Size(576, 203);
            this.lstvScorers.TabIndex = 1;
            this.lstvScorers.UseCompatibleStateImageBehavior = false;
            // 
            // tbpGames
            // 
            this.tbpGames.Controls.Add(this.lstvGames);
            this.tbpGames.Location = new System.Drawing.Point(4, 22);
            this.tbpGames.Name = "tbpGames";
            this.tbpGames.Size = new System.Drawing.Size(576, 207);
            this.tbpGames.TabIndex = 2;
            this.tbpGames.Text = "Games";
            this.tbpGames.UseVisualStyleBackColor = true;
            // 
            // lstvGames
            // 
            this.lstvGames.Location = new System.Drawing.Point(0, 4);
            this.lstvGames.Name = "lstvGames";
            this.lstvGames.Size = new System.Drawing.Size(576, 203);
            this.lstvGames.TabIndex = 1;
            this.lstvGames.UseCompatibleStateImageBehavior = false;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemFile,
            this.mnuItemEdit});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(584, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuItemFile
            // 
            this.mnuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemAddGame});
            this.mnuItemFile.Name = "mnuItemFile";
            this.mnuItemFile.Size = new System.Drawing.Size(37, 20);
            this.mnuItemFile.Text = "File";
            // 
            // mnuItemAddGame
            // 
            this.mnuItemAddGame.Name = "mnuItemAddGame";
            this.mnuItemAddGame.Size = new System.Drawing.Size(130, 22);
            this.mnuItemAddGame.Text = "Add Game";
            // 
            // mnuItemEdit
            // 
            this.mnuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemEditProfiles,
            this.mnuItemEditTeams});
            this.mnuItemEdit.Name = "mnuItemEdit";
            this.mnuItemEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuItemEdit.Text = "Edit";
            // 
            // mnuItemEditProfiles
            // 
            this.mnuItemEditProfiles.Name = "mnuItemEditProfiles";
            this.mnuItemEditProfiles.Size = new System.Drawing.Size(136, 22);
            this.mnuItemEditProfiles.Text = "Edit Profiles";
            // 
            // mnuItemEditTeams
            // 
            this.mnuItemEditTeams.Name = "mnuItemEditTeams";
            this.mnuItemEditTeams.Size = new System.Drawing.Size(136, 22);
            this.mnuItemEditTeams.Text = "Edit Teams";
            // 
            // ckbVersion
            // 
            this.ckbVersion.AutoSize = true;
            this.ckbVersion.Location = new System.Drawing.Point(478, 7);
            this.ckbVersion.Name = "ckbVersion";
            this.ckbVersion.Size = new System.Drawing.Size(84, 17);
            this.ckbVersion.TabIndex = 2;
            this.ckbVersion.Text = "Limit to FIFA";
            this.ckbVersion.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.ckbVersion);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Main";
            this.Text = "Form1";
            this.tabMain.ResumeLayout(false);
            this.tbpStandings.ResumeLayout(false);
            this.tbpScorers.ResumeLayout(false);
            this.tbpGames.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tbpStandings;
        private System.Windows.Forms.TabPage tbpScorers;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mnuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddGame;
        private System.Windows.Forms.ToolStripMenuItem mnuItemEditProfiles;
        private System.Windows.Forms.ToolStripMenuItem mnuItemEditTeams;
        private System.Windows.Forms.TabPage tbpGames;
        private System.Windows.Forms.ListView lstvStandings;
        private System.Windows.Forms.ListView lstvScorers;
        private System.Windows.Forms.ListView lstvGames;
        private System.Windows.Forms.CheckBox ckbVersion;

    }
}

