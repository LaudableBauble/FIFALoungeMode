namespace FIFALoungeMode
{
    partial class EditProfiles
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
            this.cmbProfiles = new System.Windows.Forms.ComboBox();
            this.txbProfileName = new System.Windows.Forms.TextBox();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnAddProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbProfiles
            // 
            this.cmbProfiles.FormattingEnabled = true;
            this.cmbProfiles.Location = new System.Drawing.Point(12, 12);
            this.cmbProfiles.Name = "cmbProfiles";
            this.cmbProfiles.Size = new System.Drawing.Size(121, 21);
            this.cmbProfiles.TabIndex = 13;
            // 
            // txbProfileName
            // 
            this.txbProfileName.Location = new System.Drawing.Point(95, 79);
            this.txbProfileName.Name = "txbProfileName";
            this.txbProfileName.Size = new System.Drawing.Size(100, 20);
            this.txbProfileName.TabIndex = 12;
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(9, 82);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(35, 13);
            this.lblProfileName.TabIndex = 11;
            this.lblProfileName.Text = "label1";
            // 
            // cmbTeam
            // 
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(12, 105);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(121, 21);
            this.cmbTeam.TabIndex = 14;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(95, 227);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 15;
            this.btnFinish.Text = "button1";
            this.btnFinish.UseVisualStyleBackColor = true;
            // 
            // btnAddProfile
            // 
            this.btnAddProfile.Location = new System.Drawing.Point(139, 10);
            this.btnAddProfile.Name = "btnAddProfile";
            this.btnAddProfile.Size = new System.Drawing.Size(56, 23);
            this.btnAddProfile.TabIndex = 16;
            this.btnAddProfile.Text = "button2";
            this.btnAddProfile.UseVisualStyleBackColor = true;
            // 
            // EditProfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnAddProfile);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.cmbTeam);
            this.Controls.Add(this.cmbProfiles);
            this.Controls.Add(this.txbProfileName);
            this.Controls.Add(this.lblProfileName);
            this.Name = "EditProfiles";
            this.Text = "EditProfiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProfiles;
        private System.Windows.Forms.TextBox txbProfileName;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.ComboBox cmbTeam;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnAddProfile;
    }
}