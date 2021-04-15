using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Testare_Angajati
{
    public partial class Form1 : Form
    {
        private SqlConnection myConnection = new SqlConnection(Program.myConnectionString);

        public string rezultat = "";
        public string nr_marca = "";
        public string nr_cartela = "";
        public string nr_telefon_superv = "";
        public string mesaj = "";
        public string acord = "";
        public string vaccinat = "";
        public string FFP2 = "";
        public string data_retestare = "";
        DateTime data;

        Timer timer = new Timer
        {
            Interval = 60000
        };

        public Form1()
        {
            InitializeComponent();

            timer.Tick += new System.EventHandler(OnTimerEvent);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            bool error = false;
            this.textBox1.Focus();
            try
            {
                this.myConnection.Open();
                this.myConnection.Close();
            }
            catch (Exception ex)
            {
                error = true;
            }
            
            if (error == false)
            {
                populateGrid();
                timer.Enabled = true;
            }
            else
            {
                MessageBox.Show("Check you internet connection!");
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nr_cartela = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                testare_angajat(nr_cartela);
            }
        }

        public void populateGrid()
        {
            string numar1 = "";
            string numar2 = "";

            myConnection.Close();
            myConnection.Open();
            SqlDataReader sqlDataReader = new SqlCommand("SELECT COUNT (DISTINCT t1.nr_cartela) as Numar FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare, 101) = CONVERT(varchar, GETDATE(), 101) AND (DATEDIFF(day, t2.data_testare, GetDate()) > 2 OR t2.data_testare is null) AND t3.vaccinat != 'da' AND t3.FFP2 != 'da' AND (DATEDIFF(day, t3.data_retestare, GetDate()) >= 1 OR t3.data_retestare is null) AND t3.plecat != 'da'", myConnection).ExecuteReader();
            bool flag = sqlDataReader.Read();
            if (flag)
            {
                numar1 = sqlDataReader.GetValue(0).ToString();
            }
            myConnection.Close();

            this.label1.Text = numar1 + " - PERSOANE CARE TREBUIE TESTATE";

            myConnection.Open();
            SqlDataReader sqlDataReader2 = new SqlCommand("SELECT COUNT (DISTINCT nr_cartela) as Numar FROM rezultat WHERE CONVERT(varchar, data_testare, 101) = CONVERT(varchar, GETDATE(), 101)", myConnection).ExecuteReader();
            bool flag2 = sqlDataReader2.Read();
            if (flag2)
            {
                numar2 = sqlDataReader2.GetValue(0).ToString();
            }
            myConnection.Close();

            this.label2.Text = numar2 +  " - PERSOANE TESTATE ASTAZI " + DateTime.Now.ToString("dd-MM-yyyy HH:mm");

            // populate grid 1
            var select = "SELECT DISTINCT t1.nr_cartela, t3.nr_marca as 'Nr. marca', t3.nr_marca_superv as 'Nr. marca superior', t2.data_testare as 'Data testare' FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND (DATEDIFF(day, t2.data_testare, GetDate()) > 2 OR t2.data_testare is null) AND t3.vaccinat != 'da' AND t3.FFP2 != 'da' AND (DATEDIFF(day, t3.data_retestare, GetDate()) >= 1 OR t3.data_retestare is null) AND t3.plecat != 'da'";
            var dataAdapter = new SqlDataAdapter(select, myConnection);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ClearSelection();

            // populate grid 2
            var select2 = "SELECT DISTINCT t2.nr_cartela, t3.nr_marca as 'Nr. marca', t3.nr_marca_superv as 'Nr. marca superior',  'Disponibil' as Rezultat FROM rezultat t1 LEFT JOIN scanare t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t2.nr_cartela WHERE CONVERT(varchar, t1.data_testare, 101) = CONVERT(varchar, GETDATE(), 101)";
            var dataAdapter2 = new SqlDataAdapter(select2, myConnection);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.ClearSelection();
        }

        private void OnTimerEvent(object source, EventArgs e)
        {
            bool error = false;
            try
            {
                this.myConnection.Open();
                this.myConnection.Close();
            }
            catch (Exception)
            {
                error = true;
            }

            if (error == false)
            {
                populateGrid();
                this.textBox1.Focus();
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {           
                if (row.Cells[3].Value.ToString() == "Disponibil")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = (int)new Parola().ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            bool error = false;
            try
            {
                this.myConnection.Open();
                this.myConnection.Close();
            }
            catch (Exception ex)
            {
                error = true;
            }

            if (textBox1.Text.StartsWith("61"))
            {
                nr_marca = textBox1.Text;
            }
            else
            {
                nr_cartela = textBox1.Text;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.StartsWith("+"))
                {
                    mesaj = "Număr de telefon superior:" + "\r\n" + textBox1.Text;

                    Rezultat rezultat = new Rezultat();
                    rezultat.mesaj_txt.Text = mesaj;
                    rezultat.ShowDialog();

                    //update rezultat test
                    myConnection.Open();
                    SqlDataReader sqlDataReader = new SqlCommand("SELECT nr_cartela FROM personal WHERE nr_telefon_superv = '" + textBox1.Text + "'", myConnection).ExecuteReader();
                    bool flag = sqlDataReader.Read();
                    if (flag)
                    {
                        nr_cartela = sqlDataReader.GetValue(0).ToString();
                    }
                    myConnection.Close();

                    myConnection.Open();
                    string query = "UPDATE rezultat SET rezultat = 'rapid pozitiv' WHERE nr_cartela = '" + nr_cartela + "'";
                    SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
                else
                {
                    if (error == false)
                    {
                        scanare_angajat();
                    }
                    else
                    {
                        MessageBox.Show("Check you internet connection!");
                    }
                }
                textBox1.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public void scanare_angajat()
        {
            acord = "";
            vaccinat = "";
            FFP2 = "";
            data_retestare = "";
            data = new DateTime(2000, 02, 20, 0, 0, 0);

            if (textBox1.Text.StartsWith("61"))
            {
                //check nr_marca exist
                check_nr(nr_marca);

                myConnection.Open();
                SqlDataReader sqlDataReader = new SqlCommand("SELECT DISTINCT t1.nr_cartela, t3.acord, t3.vaccinat, t3.FFP2, t3.data_retestare FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND (DATEDIFF(day, t2.data_testare, GetDate()) > 2 OR t2.data_testare is null) AND t3.plecat != 'da' AND t3.nr_marca='" + nr_marca + "'", myConnection).ExecuteReader();
                bool flag = sqlDataReader.Read();
                if (flag)
                {
                    nr_cartela = sqlDataReader.GetValue(0).ToString();
                    acord = sqlDataReader.GetValue(1).ToString();
                    vaccinat = sqlDataReader.GetValue(2).ToString();
                    FFP2 = sqlDataReader.GetValue(3).ToString();
                    data_retestare = sqlDataReader.GetValue(4).ToString();
                    if (data_retestare != "")
                    {
                        data = DateTime.Parse(data_retestare);
                    }

                    myConnection.Close();

                    if (vaccinat != "da" && FFP2 != "da" && (data.Date < DateTime.Now.Date || data_retestare is null))
                    {
                        if (acord == "nu")
                        {
                            mesaj = "Acordul nu este semnat! " + "\r\n" + "Vă rugăm să il semnați!";

                            Rezultat rezultat = new Rezultat();
                            rezultat.mesaj_txt.Text = mesaj;
                            rezultat.ShowDialog();
                        }
                        testare_angajat(nr_cartela);
                    }
                    else if (vaccinat == "da")
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " este deja vaccinată!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                    else if (FFP2 == "da")
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " are costum FFP2!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                    else if (data.Date >= DateTime.Now.Date)
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " încă are anticorpi!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                }
                myConnection.Close();
            }
            else
            {
                //check nr_cartela exist
                check_cartela(nr_cartela);

                myConnection.Open();
                SqlDataReader sqlDataReader3 = new SqlCommand("SELECT DISTINCT t3.nr_marca, t3.acord, t3.vaccinat, t3.FFP2, t3.data_retestare FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND (DATEDIFF(day, t2.data_testare, GetDate()) > 2 OR t2.data_testare is null) AND t3.plecat != 'da' AND t1.nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
                bool flag3 = sqlDataReader3.Read();
                if (flag3)
                {
                    nr_marca = sqlDataReader3.GetValue(0).ToString();
                    acord = sqlDataReader3.GetValue(1).ToString();
                    vaccinat = sqlDataReader3.GetValue(2).ToString();
                    FFP2 = sqlDataReader3.GetValue(3).ToString();
                    data_retestare = sqlDataReader3.GetValue(4).ToString();
                    if (data_retestare != "")
                    {
                        data = DateTime.Parse(data_retestare);
                    }

                    myConnection.Close();

                    if (vaccinat != "da" && FFP2 != "da" && (data.Date < DateTime.Now.Date || data_retestare is null))
                    {
                        if (acord == "nu")
                        {
                            mesaj = "Acordul nu este semnat! " + "\r\n" + "Vă rugăm să îl semnați!";

                            Rezultat rezultat = new Rezultat();
                            rezultat.mesaj_txt.Text = mesaj;
                            rezultat.ShowDialog();
                        }
                        testare_angajat(nr_cartela);
                    }
                    else if (vaccinat == "da")
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " este deja vaccinată!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                    else if (FFP2 == "da")
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " are costum FFP2!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                    else if (data.Date >= DateTime.Now.Date)
                    {
                        mesaj = "ATENȚIE!!! " + "\r\n\r\n" + nr_marca + " încă are anticorpi!";

                        Retestare retestare = new Retestare();
                        retestare.mesaj_txt.Text = mesaj;
                        retestare.ShowDialog();

                        if (retestare.retestare == "DA")
                        {
                            retestare_angajat(nr_cartela);
                        }
                    }
                }
                myConnection.Close();
            }
        }

        public void testare_angajat(string nr_cartela)
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string data2 = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            string data3 = data2.Substring(0, 10);
            string ora = data2.Substring(data2.Length - 5);

            myConnection.Open();
            SqlDataReader sqlDataReader2 = new SqlCommand("SELECT DISTINCT t2.nr_marca, t2.nr_telefon_superv FROM scanare t1 LEFT JOIN personal t2 ON t2.nr_cartela = t1.nr_cartela WHERE t2.plecat != 'da' AND t1.nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
            bool flag2 = sqlDataReader2.Read();
            if (flag2)
            {
                nr_marca = sqlDataReader2.GetValue(0).ToString();
                nr_telefon_superv = sqlDataReader2.GetValue(1).ToString();
            }
            myConnection.Close();

            mesaj = "Vrei să începi testarea pentru:" + "\r\n" + nr_marca + "?";

            Testare testare = new Testare();
            testare.mesaj_txt.Text = mesaj;
            testare.ShowDialog();

            if (testare.start == "DA")
            {
                StreamWriter streamWriter = new StreamWriter("textToPrinter.txt", false);
                streamWriter.WriteLine("e PCX;*");
                streamWriter.WriteLine("e IMG;*");
                streamWriter.WriteLine("mm");
                streamWriter.WriteLine("zO");
                streamWriter.WriteLine("J");
                streamWriter.WriteLine("O R,P");
                streamWriter.WriteLine("H100,0,T");
                streamWriter.WriteLine("D 0.0,0.0");
                streamWriter.WriteLine("Sl1;0.0,0.0,10.0,10.0,25.0,25.0,1");
                streamWriter.WriteLine("T4.2,4.0,0,-3,x1,y1;" + nr_marca);
                streamWriter.WriteLine("T2.6,6.6,0,-3,x1,y1;" + data3);
                streamWriter.WriteLine("T6.1,9.2,0,-3,x1,y1;" + ora);
                streamWriter.WriteLine("B17.0,2.5,0,DATAMATRIX,0.50;" + nr_telefon_superv);
                streamWriter.WriteLine("A 1");
                streamWriter.Close();
                RawPrinterHelper.SendFileToPrinter(new PrintDocument().PrinterSettings.PrinterName, "textToPrinter.txt");

                myConnection.Open();
                SqlDataReader sqlDataReader = new SqlCommand("SELECT nr_cartela FROM rezultat WHERE nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
                bool flag = sqlDataReader.Read();
                if (flag)
                {
                    myConnection.Close();
                    myConnection.Open();
                    string query = "UPDATE rezultat SET data_testare = '" + data + "', rezultat = 'rapid negativ', alte_motive = '' WHERE nr_cartela = '" + nr_cartela + "'";
                    SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    populateGrid();
                }
                else
                {
                    myConnection.Close();
                    myConnection.Open();
                    string query = "INSERT INTO rezultat (nr_cartela, data_testare, rezultat, alte_motive) VALUES ('" + nr_cartela + "', '" + data + "', 'rapid negativ' , '')";
                    SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    populateGrid();
                }
                myConnection.Close();
                this.textBox1.Focus();
            }
            else if (testare.start == "NU")
            {
                Motiv motiv = new Motiv();
                motiv.ShowDialog();

                myConnection.Open();
                SqlDataReader sqlDataReader = new SqlCommand("SELECT nr_cartela FROM rezultat WHERE nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
                bool flag = sqlDataReader.Read();
                if (flag)
                {
                    myConnection.Close();
                    myConnection.Open();
                    string query = "UPDATE rezultat SET alte_motive = '" + motiv.motiv_txt.Text + "' WHERE nr_cartela = '" + nr_cartela + "'";
                    SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    populateGrid();
                }
                else
                {
                    myConnection.Close();
                    myConnection.Open();
                    string query = "INSERT INTO rezultat (nr_cartela, alte_motive) VALUES ('" + nr_cartela + "', '" + motiv.motiv_txt.Text + "')";
                    SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    populateGrid();
                }
                myConnection.Close();

                this.textBox1.Focus();
                dataGridView1.ClearSelection();
            }
        }

        public void retestare_angajat(string nr_cartela)
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string data2 = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            string data3 = data2.Substring(0, 10);
            string ora = data2.Substring(data2.Length - 5);

            myConnection.Open();
            SqlDataReader sqlDataReader2 = new SqlCommand("SELECT DISTINCT t2.nr_marca, t2.nr_telefon_superv FROM scanare t1 LEFT JOIN personal t2 ON t2.nr_cartela = t1.nr_cartela WHERE t2.plecat != 'da' AND t1.nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
            bool flag2 = sqlDataReader2.Read();
            if (flag2)
            {
                nr_marca = sqlDataReader2.GetValue(0).ToString();
                nr_telefon_superv = sqlDataReader2.GetValue(1).ToString();
            }
            myConnection.Close();

            StreamWriter streamWriter = new StreamWriter("textToPrinter.txt", false);
            streamWriter.WriteLine("e PCX;*");
            streamWriter.WriteLine("e IMG;*");
            streamWriter.WriteLine("mm");
            streamWriter.WriteLine("zO");
            streamWriter.WriteLine("J");
            streamWriter.WriteLine("O R,P");
            streamWriter.WriteLine("H100,0,T");
            streamWriter.WriteLine("D 0.0,0.0");
            streamWriter.WriteLine("Sl1;0.0,0.0,10.0,10.0,25.0,25.0,1");
            streamWriter.WriteLine("T4.2,4.0,0,-3,x1,y1;" + nr_marca);
            streamWriter.WriteLine("T2.6,6.6,0,-3,x1,y1;" + data3);
            streamWriter.WriteLine("T6.1,9.2,0,-3,x1,y1;" + ora);
            streamWriter.WriteLine("B17.0,2.5,0,DATAMATRIX,0.50;" + nr_telefon_superv);
            streamWriter.WriteLine("A 1");
            streamWriter.Close();
            RawPrinterHelper.SendFileToPrinter(new PrintDocument().PrinterSettings.PrinterName, "textToPrinter.txt");

            myConnection.Open();
            SqlDataReader sqlDataReader = new SqlCommand("SELECT nr_cartela FROM rezultat WHERE nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
            bool flag = sqlDataReader.Read();
            if (flag)
            {
                myConnection.Close();
                myConnection.Open();
                string query = "UPDATE rezultat SET data_testare = '" + data + "', rezultat = 'rapid negativ', alte_motive = '' WHERE nr_cartela = '" + nr_cartela + "'";
                SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                populateGrid();
            }
            else
            {
                myConnection.Close();
                myConnection.Open();
                string query = "INSERT INTO rezultat (nr_cartela, data_testare, rezultat, alte_motive) VALUES ('" + nr_cartela + "', '" + data + "', 'rapid negativ' , '')";
                SqlCommand myCommand = new SqlCommand(query, this.myConnection);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                populateGrid();
            }
            myConnection.Close();
            this.textBox1.Focus();
        }

        public void check_nr(string nr_marca)
        {
            myConnection.Open();
            SqlDataReader sqlDataReader = new SqlCommand("SELECT DISTINCT t1.nr_cartela FROM scanare t1 LEFT JOIN personal t2 on t2.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND t2.plecat != 'da' AND t2.nr_marca='" + nr_marca + "'", myConnection).ExecuteReader();
            bool flag = sqlDataReader.Read();
            if (!flag)
            {
                mesaj = "Număr de marcă greșit!" + "\r\n" + "Vă rugăm luați legătura cu HR.";

                Rezultat rezultat = new Rezultat();
                rezultat.mesaj_txt.Text = mesaj;
                rezultat.ShowDialog();
            }
            else //check are test
            {
                myConnection.Close();
                myConnection.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT CONVERT(varchar(10),t2.data_testare,105), t1.nr_cartela FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND DATEDIFF(day, t2.data_testare, GetDate()) <= 2 AND t3.plecat != 'da' AND t3.nr_marca='" + nr_marca + "'", myConnection).ExecuteReader();
                bool flag2 = sqlDataReader2.Read();
                if (flag2)
                {
                    string data_primire_rezultat = sqlDataReader2.GetValue(0).ToString();
                    nr_cartela = sqlDataReader2.GetValue(1).ToString();
                    mesaj = "Test făcut în data de: " + "\r\n" + data_primire_rezultat + "\r\n" + "Vă mulțumim!";

                    Retestare retestare = new Retestare();
                    retestare.mesaj_txt.Text = mesaj;
                    retestare.ShowDialog();

                    if (retestare.retestare == "DA")
                    {
                        myConnection.Close();
                        retestare_angajat(nr_cartela);
                    }
                }
                myConnection.Close();
            }
            myConnection.Close();
        }

        public void check_cartela(string nr_cartela)
        {
            myConnection.Open();
            SqlDataReader sqlDataReader = new SqlCommand("SELECT DISTINCT t1.nr_cartela FROM scanare t1 LEFT JOIN personal t2 ON t2.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND t2.plecat != 'da' AND t1.nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
            bool flag = sqlDataReader.Read();
            if (!flag)
            {
                mesaj = "Cartelă neînregistrată la intrare!" + "\r\n" + "Vă rugăm să vă scanați la poartă.";

                Rezultat rezultat = new Rezultat();
                rezultat.mesaj_txt.Text = mesaj;
                rezultat.ShowDialog();
            }
            else //check are test
            {
                myConnection.Close();
                myConnection.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT CONVERT(varchar(10),t2.data_testare,105) FROM scanare t1 LEFT JOIN rezultat t2 ON t2.nr_cartela = t1.nr_cartela LEFT JOIN personal t3 ON t3.nr_cartela = t1.nr_cartela WHERE CONVERT(varchar, t1.data_scanare,101) = CONVERT(varchar, GETDATE(), 101) AND DATEDIFF(day, t2.data_testare, GetDate()) <= 2 AND t3.plecat != 'da' AND t1.nr_cartela='" + nr_cartela + "'", myConnection).ExecuteReader();
                bool flag2 = sqlDataReader2.Read();
                if (flag2)
                {
                    string data_primire_rezultat = sqlDataReader2.GetValue(0).ToString();
                    mesaj = "Test făcut în data de: " + "\r\n" + data_primire_rezultat + "\r\n" + "Vă mulțumim!";

                    Retestare retestare = new Retestare();
                    retestare.mesaj_txt.Text = mesaj;
                    retestare.ShowDialog();

                    if (retestare.retestare == "DA")
                    {
                        myConnection.Close();
                        retestare_angajat(nr_cartela);
                    }
                }
                myConnection.Close();
            }
            myConnection.Close();
        }
    }
}
