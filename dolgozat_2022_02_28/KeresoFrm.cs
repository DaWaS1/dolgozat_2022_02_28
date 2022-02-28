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
    public partial class KeresoFrm : Form
    {
        public string connectionString { private set; get; }
        public KeresoFrm()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = vizsgazok;";
            InitializeComponent();
        }

        private void KeresoFrm_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var r = new SqlCommand("SELECT nyelv " +
                    "FROM nyelvek;", connection).ExecuteReader();
                while (r.Read())
                {
                    comboBox1.Items.Add(r[0]);
                }


            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                dataGridView1.Rows.Clear();
                connection.Open();
                //idopont nyelv szint
                var r = new SqlCommand(
                    "SELECT vizsgak.idopont, vizsgak.szint " +
                    "FROM vizsgak " +
                    $"WHERE vizsgak.nyelvid = {comboBox1.SelectedIndex +1} ;", connection).ExecuteReader();
                while (r.Read())
                {
                    dataGridView1.Rows.Add(r[0], r[1]);
                }


            }
        }



    }
}
