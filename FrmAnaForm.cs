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
using System.Configuration;

namespace PersonelKayıt
{
    public partial class FrmAnaForm : Form
    {

        public FrmAnaForm()
        {

            InitializeComponent();
            txtAd.Focus();
        }

        SqlConnection baglanti = new SqlConnection(
            "Data Source=DVC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        void Clear()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtMeslek.Text = "";
            txtSoyad.Text = "";
            mskMaas.Text = "";
            rdbBekar.Checked = false;
            rdbEvli.Checked = false;
            cmbSehir.Text = "";
            txtAd.Focus();

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
            //Tbl_Personel tablosundaki verileri çeker
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand command = new SqlCommand("Insert Into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            //p1 ve p2 atama için köprü görevi görür 
            command.Parameters.AddWithValue("@p1", txtAd.Text);//command nesnesinden gelen parametreleri ekler
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", cmbSehir.Text);
            command.Parameters.AddWithValue("@p4", mskMaas.Text);
            command.Parameters.AddWithValue("@p5", txtMeslek.Text);
            command.Parameters.AddWithValue("@p6", label8.Text);


            command.ExecuteNonQuery();//Sorguyu Çalıştırır(insertte, güncellemede silmede kullanılır)
                                      // yazılmazsa ekleme işlemler gerçekleşmez
            baglanti.Close();
            

            MessageBox.Show("Personel Eklendi");
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
            Clear();




        }

        private void rdbEvli_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "True";

        }

        private void rdbBekar_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text= "False";

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            //Seçilen satırın 0. sütun id sini getirir
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            //Seçilen satırın 0. hücresi=İd

            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();


        }

        private void label8_TextChanged(object sender, EventArgs e)
        {

            //Metin Değiştiği Zaman 
            if (label8.Text == "True")
            {
                rdbEvli.Checked = true;
            }
            if(label8.Text == "False")
            {
                rdbBekar.Checked= true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command_sil = new SqlCommand("Delete From Tbl_Personel  where Perid=@k1",baglanti);
            command_sil.Parameters.AddWithValue("@k1", txtId.Text);
            command_sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command_guncelle = new SqlCommand(
                "Update Tbl_Personel Set PerAd=@k2,PerSoyad=@k3,PerSehir=@k4,PerMaas=@k5,PerDurum=@k6,PerMeslek=@k7 where Perid=@k1",baglanti);
            //Eğer where perid=@k1 yazılmazsa tüm verileri günceller
            
            command_guncelle.Parameters.AddWithValue("@k2", txtAd.Text);
            command_guncelle.Parameters.AddWithValue("@k3", txtSoyad.Text);
            command_guncelle.Parameters.AddWithValue("@k4", cmbSehir.Text);
            command_guncelle.Parameters.AddWithValue("@k5", mskMaas.Text);
            command_guncelle.Parameters.AddWithValue("@k6", label8.Text);
            command_guncelle.Parameters.AddWithValue("@k7", txtMeslek.Text);
            command_guncelle.Parameters.AddWithValue("@k1", txtId.Text);
            command_guncelle.ExecuteNonQuery();
          
            MessageBox.Show("Personel Güncellendi");
            
            baglanti.Close();

            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
            Clear();
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            FrmIstatsitik frmIstatsitik=new FrmIstatsitik();
            frmIstatsitik.ShowDialog();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmGrafikler=new FrmGrafikler();   
            frmGrafikler.ShowDialog();
        }
    }
}
