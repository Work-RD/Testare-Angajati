using System;
using System.Windows.Forms;

namespace Testare_Angajati
{
    public partial class Motiv : Form
    {
        public Motiv()
        {
            InitializeComponent();
            mesaj_txt.Text = "Motivul refuzului:";
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (motiv_txt.Text == "")
            {
                MessageBox.Show("Vă rog complectați motivul refuzului.");
                motiv_txt.Focus();
            }
            else
            {
                this.Close();
            }
        }
    }
}
