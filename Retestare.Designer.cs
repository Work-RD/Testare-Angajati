namespace Testare_Angajati
{
    partial class Retestare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Retestare));
            this.retestare_btn = new System.Windows.Forms.Button();
            this.ok_btn = new System.Windows.Forms.Button();
            this.mesaj_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // retestare_btn
            // 
            this.retestare_btn.BackColor = System.Drawing.Color.Red;
            this.retestare_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retestare_btn.ForeColor = System.Drawing.Color.White;
            this.retestare_btn.Location = new System.Drawing.Point(300, 222);
            this.retestare_btn.Name = "retestare_btn";
            this.retestare_btn.Size = new System.Drawing.Size(127, 36);
            this.retestare_btn.TabIndex = 6;
            this.retestare_btn.Text = "Retestare";
            this.retestare_btn.UseVisualStyleBackColor = false;
            this.retestare_btn.Click += new System.EventHandler(this.retestare_btn_Click);
            // 
            // ok_btn
            // 
            this.ok_btn.BackColor = System.Drawing.Color.Green;
            this.ok_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ok_btn.ForeColor = System.Drawing.Color.White;
            this.ok_btn.Location = new System.Drawing.Point(102, 222);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(127, 36);
            this.ok_btn.TabIndex = 5;
            this.ok_btn.Text = "Ok";
            this.ok_btn.UseVisualStyleBackColor = false;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
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
            this.mesaj_txt.TabIndex = 7;
            this.mesaj_txt.Text = "Mesaj";
            this.mesaj_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Retestare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.ControlBox = false;
            this.Controls.Add(this.retestare_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.mesaj_txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Retestare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testare Angajati";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button retestare_btn;
        private System.Windows.Forms.Button ok_btn;
        public System.Windows.Forms.TextBox mesaj_txt;
    }
}