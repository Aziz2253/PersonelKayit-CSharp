using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PersonelKayıt
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(
           "Data Source=DVC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand sqlCommandg1 = new SqlCommand("Select PerSehir,Count(*) from Tbl_Personel group by PerSehir",baglanti);
            SqlDataReader dr1= sqlCommandg1.ExecuteReader();
            while (dr1.Read())
            {

                chart1.Series["Sehirler"].Points.AddXY(dr1[0], dr1[1]);
            }
            baglanti.Close();


            baglanti.Open();

            SqlCommand sqlCommandg2 = new SqlCommand("Select PerMeslek,Avg(PerMaas) from Tbl_Personel group by PerMeslek", baglanti);
            SqlDataReader dr2 = sqlCommandg2.ExecuteReader();
            while (dr2.Read())
            {

                chart2.Series["MeslekMaas"].Points.AddXY(dr2[0], dr2[1]);
            }

            baglanti.Close();


        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
