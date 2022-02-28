using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dolgozat_2022_02_28
{
    public partial class FrmVizsgazo : Form
    {
        public string connectionString { private set; get; }

        public FrmVizsgazo()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = vizsgazok;";


            InitializeComponent();

        }

        private void FrmVizsgazo_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            textBox2.Text = MainFrm.vizsgazo_adatai.Neve;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var r = new SqlCommand(
                        "SELECT jelentkezesek.sorsz, jelentkezesek.mobil, jelentkezesek.szulev " +
                        "FROM jelentkezesek " +
                       $"WHERE jelentkezesek.nev = \'{MainFrm.vizsgazo_adatai.Neve}\' ;", connection).ExecuteReader();
                    while (r.Read())
                    {
                        textBox1.Text = r[0].ToString();
                        textBox4.Text = r[1].ToString();
                        textBox3.Text = r[2].ToString();
                    }


                }
            }
            catch (Exception E )
            {
                MessageBox.Show("nem megy ez a fos: " + E.Message);
            }
        }

        private void modositBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var r = new SqlCommand(
                        "UPDATE jelentkezesek " +
                       $"SET jelentkezesek.nev = \'{textBox2.Text}\', jelentkezesek.mobil = \'{textBox4.Text}\', jelentkezesek.szulev = \'{textBox3.Text}\' " +
                       $"WHERE jelentkezesek.sorsz = \'{textBox1.Text}\' ;", connection).ExecuteNonQuery();
                }
                MessageBox.Show("Succefull");
            }
            catch (Exception E)
            {
                MessageBox.Show("UnSuccefull" + E.Message);
            }
        }

        private void torolBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
