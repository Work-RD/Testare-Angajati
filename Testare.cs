using System;
using System.Windows.Forms;

namespace Testare_Angajati
{
    public partial class Testare : Form
    {
        public string start = "";
        public Testare()
        {
            InitializeComponent();
        }

        private void da_btn_Click(object sender, EventArgs e)
        {
            start = "DA";
            this.Close();
        }

        private void nu_btn_Click(object sender, EventArgs e)
        {
            start = "NU";
            this.Close();
        }
    }
}
