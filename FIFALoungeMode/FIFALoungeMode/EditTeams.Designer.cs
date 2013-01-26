namespace FIFALoungeMode
{
    partial class EditTeams
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
            this.btnFinish = new System.Windows.Forms.Button();
            this.lstbPlayers = new System.Windows.Forms.ListBox();
            this.lblTeamName = new System.Windows.Forms.Label();
            this.txbTeamName = new System.Windows.Forms.TextBox();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.txbPlayerName = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.btnAddPlayer = new System.Windows.Forms.Button();
            this.cmbTeams = new System.Windows.Forms.ComboBox();
            this.btnAddTeam = new System.Windows.Forms.Button();
            this.grpbEditPlayer = new System.Windows.Forms.GroupBox();
            this.grpbEditPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(198, 352);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "button1";
            this.btnFinish.UseVisualStyleBackColor = true;
            // 
            // lstbPlayers
            // 
            this.lstbPlayers.FormattingEnabled = true;
            this.lstbPlayers.Location = new System.Drawing.Point(15, 109);
            this.lstbPlayers.Name = "lstbPlayers";
            this.lstbPlayers.Size = new System.Drawing.Size(183, 186);
            this.lstbPlayers.TabIndex = 1;
            // 
            // lblTeamName
            // 
            this.lblTeamName.AutoSize = true;
            this.lblTeamName.Location = new System.Drawing.Point(12, 47);
            this.lblTeamName.Name = "lblTeamName";
            this.lblTeamName.Size = new System.Drawing.Size(35, 13);
            this.lblTeamName.TabIndex = 2;
            this.lblTeamName.Text = "label1";
            // 
            // txbTeamName
            // 
            this.txbTeamName.Location = new System.Drawing.Point(98, 44);
            this.txbTeamName.Name = "txbTeamName";
            this.txbTeamName.Size = new System.Drawing.Size(100, 20);
            this.txbTeamName.TabIndex = 3;
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(12, 93);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(35, 13);
            this.lblPlayers.TabIndex = 4;
            this.lblPlayers.Text = "label2";
            // 
            // txbPlayerName
            // 
            this.txbPlayerName.Location = new System.Drawing.Point(80, 80);
            this.txbPlayerName.Name = "txbPlayerName";
            this.txbPlayerName.Size = new System.Drawing.Size(100, 20);
            this.txbPlayerName.TabIndex = 6;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(6, 83);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(35, 13);
            this.lblPlayerName.TabIndex = 5;
            this.lblPlayerName.Text = "label1";
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Location = new System.Drawing.Point(67, 301);
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddPlayer.TabIndex = 8;
            this.btnAddPlayer.Text = "button1";
            this.btnAddPlayer.UseVisualStyleBackColor = true;
            // 
            // cmbTeams
            // 
            this.cmbTeams.FormattingEnabled = true;
            this.cmbTeams.Location = new System.Drawing.Point(12, 12);
            this.cmbTeams.Name = "cmbTeams";
            this.cmbTeams.Size = new System.Drawing.Size(121, 21);
            this.cmbTeams.TabIndex = 10;
            // 
            // btnAddTeam
            // 
            this.btnAddTeam.Location = new System.Drawing.Point(139, 10);
            this.btnAddTeam.Name = "btnAddTeam";
            this.btnAddTeam.Size = new System.Drawing.Size(59, 23);
            this.btnAddTeam.TabIndex = 11;
            this.btnAddTeam.Text = "button1";
            this.btnAddTeam.UseVisualStyleBackColor = true;
            // 
            // grpbEditPlayer
            // 
            this.grpbEditPlayer.Controls.Add(this.lblPlayerName);
            this.grpbEditPlayer.Controls.Add(this.txbPlayerName);
            this.grpbEditPlayer.Location = new System.Drawing.Point(207, 109);
            this.grpbEditPlayer.Name = "grpbEditPlayer";
            this.grpbEditPlayer.Size = new System.Drawing.Size(186, 186);
            this.grpbEditPlayer.TabIndex = 12;
            this.grpbEditPlayer.TabStop = false;
            this.grpbEditPlayer.Text = "groupBox1";
            // 
            // EditTeams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 387);
            this.Controls.Add(this.btnAddTeam);
            this.Controls.Add(this.cmbTeams);
            this.Controls.Add(this.btnAddPlayer);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.txbTeamName);
            this.Controls.Add(this.lblTeamName);
            this.Controls.Add(this.lstbPlayers);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.grpbEditPlayer);
            this.Name = "EditTeams";
            this.Text = "EditTeam";
            this.grpbEditPlayer.ResumeLayout(false);
            this.grpbEditPlayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.ListBox lstbPlayers;
        private System.Windows.Forms.Label lblTeamName;
        private System.Windows.Forms.TextBox txbTeamName;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.TextBox txbPlayerName;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Button btnAddPlayer;
        private System.Windows.Forms.ComboBox cmbTeams;
        private System.Windows.Forms.Button btnAddTeam;
        private System.Windows.Forms.GroupBox grpbEditPlayer;
    }
}