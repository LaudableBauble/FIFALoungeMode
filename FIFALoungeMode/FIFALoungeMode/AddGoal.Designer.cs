namespace FIFALoungeMode
{
    partial class AddGoal
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
            this.cmbScorer = new System.Windows.Forms.ComboBox();
            this.lblMinute = new System.Windows.Forms.Label();
            this.cmbGoalType = new System.Windows.Forms.ComboBox();
            this.txbMinute = new System.Windows.Forms.TextBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbScorer
            // 
            this.cmbScorer.FormattingEnabled = true;
            this.cmbScorer.Location = new System.Drawing.Point(12, 12);
            this.cmbScorer.Name = "cmbScorer";
            this.cmbScorer.Size = new System.Drawing.Size(121, 21);
            this.cmbScorer.TabIndex = 0;
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(12, 43);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(35, 13);
            this.lblMinute.TabIndex = 1;
            this.lblMinute.Text = "label1";
            // 
            // cmbGoalType
            // 
            this.cmbGoalType.FormattingEnabled = true;
            this.cmbGoalType.Location = new System.Drawing.Point(12, 66);
            this.cmbGoalType.Name = "cmbGoalType";
            this.cmbGoalType.Size = new System.Drawing.Size(121, 21);
            this.cmbGoalType.TabIndex = 2;
            // 
            // txbMinute
            // 
            this.txbMinute.Location = new System.Drawing.Point(95, 39);
            this.txbMinute.Name = "txbMinute";
            this.txbMinute.Size = new System.Drawing.Size(100, 20);
            this.txbMinute.TabIndex = 3;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(80, 117);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 4;
            this.btnFinish.Text = "button1";
            this.btnFinish.UseVisualStyleBackColor = true;
            // 
            // AddGoal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 152);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.txbMinute);
            this.Controls.Add(this.cmbGoalType);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.cmbScorer);
            this.Name = "AddGoal";
            this.Text = "AddGoal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbScorer;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.ComboBox cmbGoalType;
        private System.Windows.Forms.TextBox txbMinute;
        private System.Windows.Forms.Button btnFinish;
    }
}