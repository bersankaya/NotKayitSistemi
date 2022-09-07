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

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-AGQ4V6UP\\WOLVOX8;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.Tbl_Ders' table. You can move, or remove it, as needed.
            this.tbl_DersTableAdapter.Fill(this.dbNotKayitDataSet.Tbl_Ders);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Ders (OGRNUMARA,OGRAD,OGRSOYAD) VALUES (@p1,@p2,@p3) ", baglanti);
            komut.Parameters.AddWithValue("@p1", msknumara.Text);
            komut.Parameters.AddWithValue("@p2", txtad.Text);
            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tbl_DersTableAdapter.Fill(this.dbNotKayitDataSet.Tbl_Ders);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            msknumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtsinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtsinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void BtnNotGirişi_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3 ;
            string durum;
          

            s1 = Convert.ToDouble(txtsinav1.Text);
            s2 = Convert.ToDouble(txtsinav2.Text);
            s3 = Convert.ToDouble(txtsinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            LblOrt.Text = ortalama.ToString();
            if (ortalama>=50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }
            LblGecenSayisi.Text = dbNotKayitDataSet.Tbl_Ders.Count(x => x.DURUM == true).ToString();
            LblKalanSayisi.Text = dbNotKayitDataSet.Tbl_Ders.Count(x => x.DURUM == false).ToString();
            LblOrt.Text = dbNotKayitDataSet.Tbl_Ders.Sum(y => y.ORTALAMA / (Convert.ToInt32(LblGecenSayisi.Text) + Convert.ToInt32(LblKalanSayisi.Text))).ToString();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Tbl_Ders set OGRS1=@P1 ,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 where OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", txtsinav1.Text);
            komut.Parameters.AddWithValue("@P2", txtsinav2.Text);
            komut.Parameters.AddWithValue("@P3", txtsinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrt.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", msknumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tbl_DersTableAdapter.Fill(this.dbNotKayitDataSet.Tbl_Ders);

        }
    }
}
