
namespace Spiderum
{
    partial class Spiderum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Spiderum));
            this.buttonSpiderum = new System.Windows.Forms.Button();
            this.buttonCrawlUser = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboSpiderum = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSpiderum
            // 
            this.buttonSpiderum.Location = new System.Drawing.Point(12, 62);
            this.buttonSpiderum.Name = "buttonSpiderum";
            this.buttonSpiderum.Size = new System.Drawing.Size(213, 62);
            this.buttonSpiderum.TabIndex = 2;
            this.buttonSpiderum.Text = "Crawl Post";
            this.buttonSpiderum.UseVisualStyleBackColor = true;
            this.buttonSpiderum.Click += new System.EventHandler(this.buttonSpiderum_Click);
            // 
            // buttonCrawlUser
            // 
            this.buttonCrawlUser.Location = new System.Drawing.Point(259, 62);
            this.buttonCrawlUser.Name = "buttonCrawlUser";
            this.buttonCrawlUser.Size = new System.Drawing.Size(213, 62);
            this.buttonCrawlUser.TabIndex = 3;
            this.buttonCrawlUser.Text = "Crawl User";
            this.buttonCrawlUser.UseVisualStyleBackColor = true;
            this.buttonCrawlUser.Click += new System.EventHandler(this.buttonCrawlUser_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(12, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 35);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(239, 22);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(87, 15);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Are you ready?";
            // 
            // comboSpiderum
            // 
            this.comboSpiderum.FormattingEnabled = true;
            this.comboSpiderum.Items.AddRange(new object[] {
            "new",
            "hot",
            "top",
            "controversial",
            "follow"});
            this.comboSpiderum.Location = new System.Drawing.Point(93, 19);
            this.comboSpiderum.Name = "comboSpiderum";
            this.comboSpiderum.Size = new System.Drawing.Size(132, 23);
            this.comboSpiderum.TabIndex = 1;
            // 
            // Spiderum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.comboSpiderum);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonCrawlUser);
            this.Controls.Add(this.buttonSpiderum);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Spiderum";
            this.Text = "Spiderum";
            this.Load += new System.EventHandler(this.Spiderum_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSpiderum;
        private System.Windows.Forms.Button buttonCrawlUser;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboSpiderum;
    }
}

