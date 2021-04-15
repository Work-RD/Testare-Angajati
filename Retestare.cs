using System;
using System.Windows.Forms;

namespace Testare_Angajati
{
    public partial class Retestare : Form
    {
        public string retestare = "";
        public Retestare()
        {
            InitializeComponent();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void retestare_btn_Click(object sender, EventArgs e)
        {
            retestare = "DA";
            this.Close();
        }
    }
}
