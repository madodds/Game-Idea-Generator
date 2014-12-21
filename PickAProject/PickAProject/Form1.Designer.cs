namespace PickAProject
{
    partial class mainForm
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
            this.generationButton = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.websiteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generationButton
            // 
            this.generationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.generationButton.Location = new System.Drawing.Point(13, 279);
            this.generationButton.Name = "generationButton";
            this.generationButton.Size = new System.Drawing.Size(204, 26);
            this.generationButton.TabIndex = 0;
            this.generationButton.Text = "Generate Project";
            this.generationButton.UseVisualStyleBackColor = true;
            this.generationButton.Click += new System.EventHandler(this.generationButton_Click);
            // 
            // textOutput
            // 
            this.textOutput.BackColor = System.Drawing.SystemColors.Window;
            this.textOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textOutput.Location = new System.Drawing.Point(13, 13);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.Size = new System.Drawing.Size(412, 260);
            this.textOutput.TabIndex = 1;
            // 
            // websiteButton
            // 
            this.websiteButton.Enabled = false;
            this.websiteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.websiteButton.Location = new System.Drawing.Point(221, 279);
            this.websiteButton.Name = "websiteButton";
            this.websiteButton.Size = new System.Drawing.Size(204, 26);
            this.websiteButton.TabIndex = 2;
            this.websiteButton.Text = "Open the URL";
            this.websiteButton.UseVisualStyleBackColor = true;
            this.websiteButton.Click += new System.EventHandler(this.websiteButton_Click);
            // 
            // mainForm
            // 
            this.AcceptButton = this.generationButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 317);
            this.Controls.Add(this.websiteButton);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.generationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Pick a Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generationButton;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Button websiteButton;
    }
}

