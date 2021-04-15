namespace Testare_Angajati
{
    partial class Motiv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Motiv));
            this.ok_btn = new System.Windows.Forms.Button();
            this.motiv_txt = new System.Windows.Forms.TextBox();
            this.mesaj_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ok_btn
            // 
            this.ok_btn.BackColor = System.Drawing.Color.Yellow;
            this.ok_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ok_btn.ForeColor = System.Drawing.Color.Black;
            this.ok_btn.Location = new System.Drawing.Point(203, 216);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(127, 36);
            this.ok_btn.TabIndex = 2;
            this.ok_btn.Text = "Ok";
            this.ok_btn.UseVisualStyleBackColor = false;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // motiv_txt
            // 
            this.motiv_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motiv_txt.Location = new System.Drawing.Point(29, 80);
            this.motiv_txt.Multiline = true;
            this.motiv_txt.Name = "motiv_txt";
            this.motiv_txt.Size = new System.Drawing.Size(476, 118);
            this.motiv_txt.TabIndex = 1;
            // 
            // mesaj_txt
            // 
            this.mesaj_txt.BackColor = System.Drawing.SystemColors.Control;
            this.mesaj_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mesaj_txt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mesaj_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesaj_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(159)))));
            this.mesaj_txt.Location = new System.Drawing.Point(29, 19);
            this.mesaj_txt.Multiline = true;
            this.mesaj_txt.Name = "mesaj_txt";
            this.mesaj_txt.ReadOnly = true;
            this.mesaj_txt.Size = new System.Drawing.Size(476, 43);
            this.mesaj_txt.TabIndex = 4;
            this.mesaj_txt.Text = "Mesaj";
            this.mesaj_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Motiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.ControlBox = false;
            this.Controls.Add(this.mesaj_txt);
            this.Controls.Add(this.motiv_txt);
            this.Controls.Add(this.ok_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Motiv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testare Angajati";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok_btn;
        public System.Windows.Forms.TextBox motiv_txt;
        public System.Windows.Forms.TextBox mesaj_txt;
    }
}