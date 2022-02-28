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
    public partial class MainFrm : Form
    {
        public string connectionString { private set; get; }
        public MainFrm()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = vizsgazok;";
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var r = new SqlCommand("SELECT nyelvek.id,jelentkezesek.nev,nyelvek.nyelv,vizsgak.szint " +
                    "FROM nyelvek,vizsgak,jelentkezesek " +
                    "WHERE nyelvek.id = vizsgak.nyelvid " +
                    "AND vizsgak.sorsz = jelentkezesek.vizsga;", connection).ExecuteReader();
                while (r.Read())
                {
                    dataGridView1.Rows.Add(r[0], r[1], r[2], r[3]);
                }

            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            KeresoFrm kf = new KeresoFrm();
            kf.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmVizsgazo v = new FrmVizsgazo();
            v.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            vizsgazo_adatai.Neve = dataGridView1.Rows[e.RowIndex].Cells["nev"].Value.ToString();
            FrmVizsgazo v = new FrmVizsgazo();
            v.ShowDialog();
        }

        public class vizsgazo_adatai
        {
             public static string Neve { get; set; }
        }    
    
    }





}
