namespace Testare_Angajati
{
    partial class Testare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Testare));
            this.da_btn = new System.Windows.Forms.Button();
            this.nu_btn = new System.Windows.Forms.Button();
            this.mesaj_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // da_btn
            // 
            this.da_btn.BackColor = System.Drawing.Color.Green;
            this.da_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.da_btn.ForeColor = System.Drawing.Color.White;
            this.da_btn.Location = new System.Drawing.Point(102, 222);
            this.da_btn.Name = "da_btn";
            this.da_btn.Size = new System.Drawing.Size(127, 36);
            this.da_btn.TabIndex = 1;
            this.da_btn.Text = "Da";
            this.da_btn.UseVisualStyleBackColor = false;
            this.da_btn.Click += new System.EventHandler(this.da_btn_Click);
            // 
            // nu_btn
            // 
            this.nu_btn.BackColor = System.Drawing.Color.Red;
            this.nu_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nu_btn.ForeColor = System.Drawing.Color.White;
            this.nu_btn.Location = new System.Drawing.Point(300, 222);
            this.nu_btn.Name = "nu_btn";
            this.nu_btn.Size = new System.Drawing.Size(127, 36);
            this.nu_btn.TabIndex = 2;
            this.nu_btn.Text = "Nu";
            this.nu_btn.UseVisualStyleBackColor = false;
            this.nu_btn.Click += new System.EventHandler(this.nu_btn_Click);
            // 
            // mesaj_txt
            // 
            this.mesaj_txt.BackColor = System.Drawing.SystemColors.Control;
            this.mesaj_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mesaj_txt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mesaj_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesaj_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(159)))));
            this.mesaj_txt.Location = new System.Drawing.Point(29, 52);
            this.mesaj_txt.Multiline = true;
            this.mesaj_txt.Name = "mesaj_txt";
            this.mesaj_txt.ReadOnly = true;
            this.mesaj_txt.Size = new System.Drawing.Size(476, 118);
            this.mesaj_txt.TabIndex = 3;
            this.mesaj_txt.Text = "Mesaj";
            this.mesaj_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Testare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.ControlBox = false;
            this.Controls.Add(this.nu_btn);
            this.Controls.Add(this.da_btn);
            this.Controls.Add(this.mesaj_txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Testare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testare Angajati";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button da_btn;
        private System.Windows.Forms.Button nu_btn;
        public System.Windows.Forms.TextBox mesaj_txt;
    }
}