namespace ESCO.Reference.Data.App
{
    partial class KeyWin
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
            this.keyText = new MetroFramework.Controls.MetroTextBox();
            this.keyBtn = new MetroFramework.Controls.MetroButton();
            this.cancelBtn = new MetroFramework.Controls.MetroButton();
            this.closeBtn = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // keyText
            // 
            this.keyText.Location = new System.Drawing.Point(22, 65);
            this.keyText.Name = "keyText";
            this.keyText.Size = new System.Drawing.Size(811, 23);
            this.keyText.TabIndex = 0;
            this.keyText.TextChanged += new System.EventHandler(this.keyText_TextChanged);
            // 
            // keyBtn
            // 
            this.keyBtn.Enabled = false;
            this.keyBtn.Location = new System.Drawing.Point(721, 104);
            this.keyBtn.Name = "keyBtn";
            this.keyBtn.Size = new System.Drawing.Size(112, 39);
            this.keyBtn.Style = MetroFramework.MetroColorStyle.Blue;
            this.keyBtn.TabIndex = 1;
            this.keyBtn.Text = "Accept";
            this.keyBtn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.keyBtn.Click += new System.EventHandler(this.keyBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Enabled = false;
            this.cancelBtn.Location = new System.Drawing.Point(578, 104);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(112, 39);
            this.cancelBtn.Style = MetroFramework.MetroColorStyle.Silver;
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(22, 104);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 39);
            this.closeBtn.Style = MetroFramework.MetroColorStyle.Silver;
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.Visible = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // KeyWin
            // 
            this.AcceptButton = this.keyBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(856, 166);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.keyBtn);
            this.Controls.Add(this.keyText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyWin";
            this.Resizable = false;
            this.Text = "Suscriptions Key";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox keyText;
        private MetroFramework.Controls.MetroButton keyBtn;
        private MetroFramework.Controls.MetroButton cancelBtn;
        private MetroFramework.Controls.MetroButton closeBtn;
    }
}