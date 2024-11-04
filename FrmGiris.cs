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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(
         "Data Source=DVC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand sqlCommand = new SqlCommand("Select * from Tbl_Yonetici  where KullaniciAd=@p1 and Sifre=@p2", baglanti);
            sqlCommand.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            sqlCommand.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader rd= sqlCommand.ExecuteReader();
            if (rd.Read())
            {

                FrmAnaForm form = new FrmAnaForm();
                form.Show();
            }
            else { 
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı !!!");
                txtKullaniciAdi.Text = "";
                txtSifre.Text = "";
                txtKullaniciAdi.Focus();


            }


            baglanti.Close();
        }
    }
}
