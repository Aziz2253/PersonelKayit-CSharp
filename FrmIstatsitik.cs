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

namespace PersonelKayıt
{
    public partial class FrmIstatsitik : Form
    {
        public FrmIstatsitik()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(
           "Data Source=DVC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

     

        private void FrmIstatsitik_Load(object sender, EventArgs e)
        {
            //Toplam Personel Sayısı
            baglanti.Open();
            SqlCommand sqlCommand1 = new SqlCommand("Select count(*) from Tbl_Personel",baglanti);
            SqlDataReader dr1= sqlCommand1.ExecuteReader();//ExecuteReader Selecy için sorguyu çalıştırır
            while(dr1.Read())
            {
                labelToplamPersonelSayısı.Text = dr1[0].ToString();
            }
            baglanti.Close();

            //Evli Personel Sayısı
            baglanti.Open();
            SqlCommand sqlCommand2 = new SqlCommand("Select Count( * ) from Tbl_Personel where PerDurum=1", baglanti);
            SqlDataReader dr2 = sqlCommand2.ExecuteReader();//ExecuteReader Select için sorguyu çalıştırır
            while (dr2.Read())
            {
                labelEvliPersonelSayısı.Text = dr2[0].ToString();
            }
            baglanti.Close();

            //Bekar Personel Sayısı
            baglanti.Open();
            SqlCommand sqlCommand3 = new SqlCommand("Select Count( * ) from Tbl_Personel where PerDurum=0", baglanti);
            SqlDataReader dr3 = sqlCommand3.ExecuteReader();//ExecuteReader Select için sorguyu çalıştırır
            while (dr3.Read())
            {
                labelBekarPersonelSayısı.Text = dr3[0].ToString();

            }
            baglanti.Close();


            //Şehir Sayısı
            baglanti.Open();
            SqlCommand sqlCommand4 = new SqlCommand("Select Count( distinct(PerSehir) ) from Tbl_Personel ", baglanti);
            SqlDataReader dr4 = sqlCommand4.ExecuteReader();//ExecuteReader Select için sorguyu çalıştırır
            while (dr4.Read())
            {
                labelSehirSayısı.Text = dr4[0].ToString();
            }
            baglanti.Close();


            //Toplam Maaş 
            baglanti.Open();
            SqlCommand sqlCommand5 = new SqlCommand("Select Sum( PerMaas) from Tbl_Personel ", baglanti);
            SqlDataReader dr5 = sqlCommand5.ExecuteReader();//ExecuteReader Select için sorguyu çalıştırır
            while (dr5.Read())
            {
                labelToplamMaas.Text = dr5[0].ToString();
            }
            baglanti.Close();


            //Ortalama Maaş 
            baglanti.Open();
            SqlCommand sqlCommand6 = new SqlCommand("Select Avg( PerMaas) from Tbl_Personel ", baglanti);
            SqlDataReader dr6 = sqlCommand6.ExecuteReader();//ExecuteReader Select için sorguyu çalıştırır
            while (dr6.Read())
            {
                labelOrtalamaMaas.Text = dr6[0].ToString();
            }
            baglanti.Close();




        }

      
    }
}
