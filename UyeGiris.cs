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

namespace KargoTakipOtomasyonu
{
    public partial class UyeGiris : Form
    {
        SqlConnection bag;
        SqlCommand komut;
        SqlDataReader dr;
        int i = 0;
        public UyeGiris()
        {
            InitializeComponent();
        }
        public void baglantı()
        {
            bag = new SqlConnection(@"Data Source=");
            bag.Open();

        }

        public void kargoDataUpdate()// kargo bilgisini güncelleme
        {
            baglantı();
            string komut = "select * from kargo where Gonderen_No like '%" + this.gnoTxt.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kargoData.DataSource = dt;
        }

        public void gonderenbilgisi() // gonderen bilgilerini textboxa çekme
        {
            
            baglantı();
            // gonderileri bağlamak için
            string komut = "Select Gonderen_No,Adi,Soyadi,GSM,E_posta,Adres,sifre from gonderen where E_posta Like '%" + Form1.degisken + "%'";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataSet ds = new DataSet();
            da.Fill(ds);
            // textlere gönderenin bilgilerini yazmak için
            gnoTxt.Text = ds.Tables[0].Rows[i]["Gonderen_No"].ToString();
            gadTxt.Text = ds.Tables[0].Rows[i]["Adi"].ToString();
            gsTxt.Text = ds.Tables[0].Rows[i]["Soyadi"].ToString();
            gTelTxt.Text = ds.Tables[0].Rows[i]["GSM"].ToString();
            gPostaTxt.Text = ds.Tables[0].Rows[i]["E_posta"].ToString();
            gadresTxt.Text = ds.Tables[0].Rows[i]["Adres"].ToString();
            sifreTxt.Text = ds.Tables[0].Rows[i]["sifre"].ToString();
        }

        public void kargoListele()// akrgoları listeleme
        {
            baglantı();
            string komut = "select * from kargo where Gonderen_No like '%" + this.gnoTxt.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kargoData.DataSource = dt;
        }

        public void faturaListele() // gonderen kişinin tüm faturalarını listeleme
        {
            baglantı();
            string komut = "select * from fatura where Gonderen_No='" + gnoTxt.Text + "';";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            faturaData.DataSource = dt;

        }

        public void fiyatlistele() // fiyat listesini gösterme
        {
            baglantı();
            string komut = "select * from fiyatlar";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            fiyatData.DataSource = dt;
        }

        public void subeListele() // şube listelemek
        {
            baglantı();
            string komut = "select kurye.kurye_İd,sube.sube_Adi,adres.Acik_adres from kurye,sube,adres where kurye.sube_İd=sube.sube_İd and sube.adres_İd = adres.adres_İd";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            subeData.DataSource = dt;
        }

        public void alıcıCmb() // alıcı combobox doldurma
        {
            

            baglantı();
            // comboboxa seçenek ekleme
            alıcıNoCmb.Items.Clear();
            SqlCommand komut3 = new SqlCommand();
            komut3.CommandText = "select Alici_No from alici";
            komut3.Connection = bag;
            komut3.CommandType = CommandType.Text;

            dr = komut3.ExecuteReader();
            while (dr.Read()) //veritabanındaki gonderici_no isimli sutundaki verileri sırasıyla ComboBox’a Ekle
            {
                if (true)
                {
                    alıcıNoCmb.Items.Add(dr["Alici_No"]); //çekmek istediğimiz sütunun adi yazılır
                }
            }
            bag.Close();
        }

        public void kargoolustur() // yeni kargo oluşturma
        {
            try
            {
                

                baglantı();
                string eklemekomut = "insert into kargo(Gonderen_No,Alici_No,fiyat_İd,kurye_İd,Teslimat_Turu,Odeme_Sekli,Odeme_Tipi,İslem_Tarihi,Durum) values ('" + gnoTxt.Text + "','" + alıcıNoCmb.SelectedItem.ToString() + "','" + fiyatData.CurrentRow.Cells[0].Value.ToString() + "','" + subeData.CurrentRow.Cells[0].Value.ToString() + "','" + teslimatCmb.SelectedItem.ToString() + "','" + odemesCmb.SelectedItem.ToString() + "','" + odemetCmb.SelectedItem.ToString() + "','" + itTxt.Text + "','" + "-" + "')";
                komut = new SqlCommand(eklemekomut, bag);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                }
                MessageBox.Show("Yeni gonderiniz kaydedildi !");
                kargoDataUpdate();
                bag.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void UyeGiris_Load(object sender, EventArgs e)
        {
            gonderenbilgisi();
            alıcıCmb();
            kargoListele();
            fiyatlistele();
            subeListele();
            faturaListele();

        }

        private void gkaydet_Click(object sender, EventArgs e)// kişisel bilgileri güncelleme
        {
            try
            {
                
                baglantı();
                string guncellemekomutu = "update gonderen set Gonderen_No='" + this.gnoTxt.Text + "'" + ",'Adi ='" + this.gadTxt.Text + "',Soyadi='" + this.gsTxt.Text + ",'GSM='" + this.gTelTxt.Text + "', E_Posta = '" + this.gPostaTxt.Text + "',Adres='" + this.gadresTxt.Text + "',sifre='" + this.sifreTxt.Text + "';";
                komut = new SqlCommand(guncellemekomutu, bag);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                }
                bag.Close();
                MessageBox.Show("Değişiklikler kaydedildi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kaydet_Click(object sender, EventArgs e)// alıcı ekleme
        {
            try
            {
                

                baglantı();
                string eklemekomut = "insert into alici(Adi,Soyadi,GSM,E_Posta,Adres) values ('" + adTxt2.Text + "','" + soyadTxt2.Text + "','" + telTxt.Text + "','" + postTxt2.Text + "','" + adresTxt.Text + "')";
                komut = new SqlCommand(eklemekomut, bag);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                }
                MessageBox.Show("Yeni alıcınız kaydedildi !");

                bag.Close();
                alıcıCmb();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)// şifre göster
        {
            

            //checkBox işaretli ise
            if (checkBox2.Checked)
            {
                //karakteri göster.
                sifreTxt.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                sifreTxt.PasswordChar = '*';
            }
        }

        private void kaydet2_Click(object sender, EventArgs e)
        {
            kargoolustur();

        }

        private void UyeGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            UyeGiris u = new UyeGiris();
            u.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
