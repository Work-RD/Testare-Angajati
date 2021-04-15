using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Testare_Angajati
{
    public partial class Parola : Form
    {
        private SqlConnection myConnection = new SqlConnection(Program.myConnectionString);
        public string data_retestare = "";

        public Parola()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ("Webasto123"))
            {
                int num = (int)new Setari().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please try again.");
                this.textBox1.Text = "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (textBox1.Text == ("Webasto123"))
                {
                    int num = (int)new Setari().ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please try again.");
                    this.textBox1.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Alegeti un fisier pentru import personal";
            openFile.Filter = "Excel file|*.xlsx|Excel file|*.xls";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                upload_excel(openFile.FileName);
            }
        }

        private void upload_excel(string filePath)
        {
            String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);
            OleDbConnection conn = new OleDbConnection(excelConnString);
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From [Upload personal$]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            myConnection.Open();
            new SqlCommand("UPDATE personal SET plecat = 'da'", myConnection).ExecuteReader();
            myConnection.Close();

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() != "")
                {
                    myConnection.Open();
                    SqlDataReader sqlDataReader = new SqlCommand("SELECT nr_marca FROM personal WHERE nr_marca='" + row[0].ToString() + "'", myConnection).ExecuteReader();
                    bool flag = sqlDataReader.Read();
                    myConnection.Close();

                    data_retestare = "";
                    if (row[18].ToString() != "")
                    {
                        DateTime data = (DateTime)row[18];
                        data_retestare = data.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (flag)
                    {
                        myConnection.Open();
                        string query = "UPDATE personal SET nr_cartela = '" + row[1].ToString() + "', nume = '" + row[2].ToString() + "', prenume = '" + row[3].ToString() + "', CNP = '" + row[4].ToString() + "', nr_telefon = '" + row[5].ToString() + "', email = '" + row[6].ToString() + "', nr_marca_superv = '" + row[7].ToString() + "', nume_superv = '" + row[8].ToString() + "', prenume_superv = '" + row[9].ToString() + "', nr_telefon_superv = '" + row[10].ToString() + "', email_superv = '" + row[11].ToString() + "', localitate = '" + row[12].ToString() + "', adresa = '" + row[13].ToString() + "', tesa = '" + row[14].ToString() + "', acord = '" + row[15].ToString() + "', vaccinat = '" + row[16].ToString() + "', FFP2 = '" + row[17].ToString() + "', data_retestare = @data_retestare, plecat = 'nu' WHERE nr_marca = '" + row[0].ToString() + "'";
                        SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                        if (row[18].ToString() != "")
                        {
                            myCommand.Parameters.AddWithValue("@data_retestare", data_retestare);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@data_retestare", DBNull.Value);
                        }
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                    }
                    else
                    {
                        myConnection.Open();
                        string query = "INSERT INTO personal (nr_marca, nr_cartela, nume, prenume, CNP, nr_telefon, email, nr_marca_superv, nume_superv, prenume_superv, nr_telefon_superv,email_superv, localitate, adresa, tesa, acord, vaccinat, FFP2, data_retestare, plecat) VALUES ('" + row[0].ToString() + "', '" + row[1].ToString() + "', '" + row[2].ToString() + "', '" + row[3].ToString() + "', '" + row[4].ToString() + "', '" + row[5].ToString() + "', '" + row[6].ToString() + "', '" + row[7].ToString() + "', '" + row[8].ToString() + "', '" + row[9].ToString() + "', '" + row[10].ToString() + "', '" + row[11].ToString() + "', '" + row[12].ToString() + "', '" + row[13].ToString() + "', '" + row[14].ToString() + "', '" + row[15].ToString() + "', '" + row[16].ToString() + "', '" + row[17].ToString() + "', @data_retestare, 'nu')";
                        SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                        if (row[18].ToString() != "")
                        {
                            myCommand.Parameters.AddWithValue("@data_retestare", data_retestare);
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@data_retestare", DBNull.Value);
                        }
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                    }
                }
            }
        }
    }
}
