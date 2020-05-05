namespace External_Process_Launcher
{
    partial class FormExternalProcessLauncher
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
            this.buttonRUN = new System.Windows.Forms.Button();
            this.textBoxExternalProcesses = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonRUN
            // 
            this.buttonRUN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRUN.Location = new System.Drawing.Point(513, 12);
            this.buttonRUN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRUN.Name = "buttonRUN";
            this.buttonRUN.Size = new System.Drawing.Size(75, 23);
            this.buttonRUN.TabIndex = 0;
            this.buttonRUN.Text = "RUN";
            this.buttonRUN.UseVisualStyleBackColor = true;
            this.buttonRUN.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxExternalProcesses
            // 
            this.textBoxExternalProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExternalProcesses.Location = new System.Drawing.Point(12, 12);
            this.textBoxExternalProcesses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxExternalProcesses.MaxLength = 0;
            this.textBoxExternalProcesses.Multiline = true;
            this.textBoxExternalProcesses.Name = "textBoxExternalProcesses";
            this.textBoxExternalProcesses.Size = new System.Drawing.Size(495, 216);
            this.textBoxExternalProcesses.TabIndex = 2;
            // 
            // FormMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 242);
            this.Controls.Add(this.textBoxExternalProcesses);
            this.Controls.Add(this.buttonRUN);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMainWindow";
            this.Text = "External Process Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRUN;
        private System.Windows.Forms.TextBox textBoxExternalProcesses;
    }
}

