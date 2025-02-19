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
    public partial class YoneticiGiris : Form
    {
        SqlConnection bag;
        SqlCommand komut;
        SqlDataReader dr;
        int i = 0;

        public YoneticiGiris()
        {
            InitializeComponent();
        }
        public void baglantı()
        {
            bag = new SqlConnection(@"Data Source=");
            bag.Open();
        }

        public void baglantı2()
        {
            string bag1 = (@"Data Source=");
            bag = new SqlConnection(bag1);
            bag.Open();
        }

        public void gonderenDataUpdate()
        {
            baglantı();
            string komut = "select * from gonderen";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gonderenData.DataSource = dt;

        }

        public void kuryeDataUpdate()
        {
            baglantı();
            string komut = "select * from kurye";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kuryeData.DataSource = dt;

        }

        public void faturaDataUpdate()
        {
            baglantı();
            string komut = "select * from fatura";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            faturaData.DataSource = dt;
        }

        public void kargoDataUpdate()
        {
            baglantı();
            string komut = "select kargo.Gonderi_no,kargo.Gonderen_No,kargo.Alici_No,kargo.Islem_tarihi,kargo.Durum,kurye.kurye_Adi,kurye.kurye_Soyadi,fiyatlar.Urun,fiyatlar.mesafe,fiyatlar.Ucret from kargo,kurye,fiyatlar where kargo.kurye_İd=kurye.kurye_İd and kargo.fiyat_İd=fiyatlar.fiyat_İd";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kargoData.DataSource = dt;
        }

        public void kuryeListele() //kurye listelemek
        {
            baglantı();
            string komut = "select kurye.kurye_İd,kurye.kurye_Adi,kurye.kurye_Soyadi,sube.sube_Adi from kurye,sube where kurye.sube_İd=sube.sube_İd";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kuryeData.DataSource = dt;

        }

        public void gonderenListele()
        {
            baglantı();
            string komut = "select Gonderen_No,Adi,Soyadi,GSM,E_posta,Adres from gonderen";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gonderenData.DataSource = dt;
        }


        public void subeListele()
        {
            try
            {
                baglantı();
                string komut = "SELECT firma.firma_Adi, sube.sube_Adi, adres.İl, adres.Ilce, adres.Acik_adres " +
                                "FROM sube, adres, firma " +
                                "WHERE sube.adres_İd = adres.adres_İd AND sube.firma_İd = firma.firma_İd";

                SqlDataAdapter da = new SqlDataAdapter(komut, bag);
                DataTable dt = new DataTable();
                da.Fill(dt);
                subeData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                bag.Close(); 
            }
        }

        public void kargoListele() // kargo Listelemek
        {
            baglantı();
            string komut = "select kargo.Gonderi_no,kargo.Gonderen_No,kargo.Alici_No,kargo.Islem_tarihi,kargo.Durum,kurye.kurye_Adi,kurye.kurye_Soyadi,fiyatlar.Urun,fiyatlar.mesafe,fiyatlar.Ucret from kargo,kurye,fiyatlar where kargo.kurye_İd=kurye.kurye_İd and kargo.fiyat_İd=fiyatlar.fiyat_İd";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            kargoData.DataSource = dt;
        }

        public void faturalistele() // fatura listelemek
        {
            baglantı();
            string komut = "select fatura.Fatura_No,kargo.Gonderi_no,kargo.Gonderen_No,kargo.Alici_No,kargo.Islem_tarihi,fatura.Toplam_Ucret from kargo, fatura where kargo.Gonderi_no = fatura.Gonderi_no ";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            faturaData.DataSource = dt;
        }

        public void alıcıListele()
        {
            baglantı();
            string komut = "select * from alici";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            alıcıData.DataSource = dt;
        }

        public void iadeTalepListele()
        {
            baglantı();
            string komut = "select alici.Adi,alici.Soyadi,alici.GSM,alici.E_Posta,alici.Adres,iadetalep.islem_tarihi,iadetalep.Aciklama,sube.sube_Adi from alici, iadetalep, sube where iadetalep.Alici_No = alici.Alici_No and iadetalep.sube_İd = sube.sube_İd";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            iadeData.DataSource = dt;
        }
        public void ucretHesapla()// ücret hesaplama
        {
            baglantı();
            string komut = "select kargo.Gonderi_no,fiyatlar.ucret from kargo,fiyatlar where kargo.fiyat_İd = fiyatlar.fiyat_İd ";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataSet ds = new DataSet();
            da.Fill(ds);
            desiUcret.Text = ds.Tables[0].Rows[i]["ucret"].ToString();
            double e, b, d, desi, toplam;
            e = Convert.ToDouble(enTxt.Text);
            b = Convert.ToDouble(boyTxt.Text);
            d = Convert.ToDouble(derinlikTxt.Text);
            Double ucret = Convert.ToDouble(desiUcret.Text);
            desi = (e * b * d) / 3000;
            toplam = desi * ucret;
            toplamUcretTxt.Text = toplam.ToString();
        }

        public void faturaOlustur()// fatura oluşturma
        {
            
            try
            {
                baglantı2();
                string eklemekomut = "insert into fatura(Fatura_Tipi,Gonderi_no,Gonderen_No,Toplam_ucret) values ('" + faturatipiCmb.Text + "','" + gonderiNoTxt2.Text + "','" + gonderenNoTxt2.Text + "','" + toplamUcretTxt.Text + "')";
                komut = new SqlCommand(eklemekomut, bag);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {

                }
                MessageBox.Show("Yeni faturanız kaydedildi !");

                faturaDataUpdate();
                bag.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YoneticiGiris_Load(object sender, EventArgs e)//form yüklendiğinde çalışıcak olay
        {
            subeListele();
            kuryeListele();
            gonderenListele();
            alıcıListele();
            kargoListele();
            faturalistele();
            iadeTalepListele();
        }

        private void sil1_Click(object sender, EventArgs e)// göndericiyi kaldırmak için kullanılan metot
        {
            
            baglantı();
            string silmekomutu = "delete from gonderen where Gonderen_No='" + this.gonderenNoTxt.Text + "';";
            komut = new SqlCommand(silmekomutu, bag);
            komut.ExecuteNonQuery();
            gonderenDataUpdate();

            bag.Close();
            MessageBox.Show("Seçtiğiniz Gönderici listeden kaldırıldı");
        }

        private void kaydet_Click(object sender, EventArgs e)//kargonun durumunu güncellemek
        {
            ucretHesapla();
            try
            {
                
                baglantı2();
                string guncellemekomutu = "update kargo set Durum ='" + durumTxt.Text + "',Islem_tarihi='" + islemTarihiTxt.Text + "' where Gonderi_no='" + gonderiNoTxt.Text + "';";
                komut = new SqlCommand(guncellemekomutu, bag);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {

                }
                MessageBox.Show("Kargo durumu kaydedildi!");

                kargoDataUpdate();
                bag.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            faturaOlustur();
        }


        private void iadeData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iadeData.CurrentRow.Selected = true;
            aaTxt.Text = iadeData.CurrentRow.Cells[0].Value.ToString();
            asTxt.Text = iadeData.CurrentRow.Cells[1].Value.ToString();
            aTelTxt.Text = iadeData.CurrentRow.Cells[2].Value.ToString();
            aPostaTxt.Text = iadeData.CurrentRow.Cells[3].Value.ToString();
            aadresTxt.Text = iadeData.CurrentRow.Cells[4].Value.ToString();
            tarihTxt.Text = iadeData.CurrentRow.Cells[5].Value.ToString();
            acıklamaTxt.Text = iadeData.CurrentRow.Cells[6].Value.ToString();
            subeAdıTxt.Text = iadeData.CurrentRow.Cells[7].Value.ToString();

        }

        private void kuryeData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            iadeData.CurrentRow.Selected = true;
            aaTxt.Text = iadeData.CurrentRow.Cells[0].Value.ToString();
            asTxt.Text = iadeData.CurrentRow.Cells[1].Value.ToString();
            aTelTxt.Text = iadeData.CurrentRow.Cells[2].Value.ToString();
            aPostaTxt.Text = iadeData.CurrentRow.Cells[3].Value.ToString();
            aadresTxt.Text = iadeData.CurrentRow.Cells[4].Value.ToString();
            tarihTxt.Text = iadeData.CurrentRow.Cells[5].Value.ToString();
            acıklamaTxt.Text = iadeData.CurrentRow.Cells[6].Value.ToString();
            subeAdıTxt.Text = iadeData.CurrentRow.Cells[7].Value.ToString();
        }


        private void alıcıData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            alıcıData.CurrentRow.Selected = true;
            alıcıNoTxt.Text = alıcıData.CurrentRow.Cells[0].Value.ToString();
            alıcıAdTxt.Text = alıcıData.CurrentRow.Cells[1].Value.ToString();
            alıcıSoyadTxt.Text = alıcıData.CurrentRow.Cells[2].Value.ToString();
            alıcıTelTxt.Text = alıcıData.CurrentRow.Cells[3].Value.ToString();
            alıcıPostaTxt.Text = alıcıData.CurrentRow.Cells[4].Value.ToString();
            alıcıAdresTxt.Text = alıcıData.CurrentRow.Cells[5].Value.ToString();
        }

        private void kargoData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kargoData.CurrentRow.Selected = true;
            gonderiNoTxt.Text = kargoData.CurrentRow.Cells[0].Value.ToString();
            gonderiNoTxt2.Text = kargoData.CurrentRow.Cells[0].Value.ToString();
            gonderenNoTxt2.Text = kargoData.CurrentRow.Cells[1].Value.ToString();
            islemTarihiTxt.Text = kargoData.CurrentRow.Cells[3].Value.ToString();
            kaydet.Text = kargoData.CurrentRow.Cells[4].Value.ToString();
        }

        private void gonderenData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gonderenData.CurrentRow.Selected = true;
            gonderenNoTxt.Text = gonderenData.CurrentRow.Cells[0].Value.ToString();
            gonderenAdTxt.Text = gonderenData.CurrentRow.Cells[1].Value.ToString();
            gonderenSoyadTxt.Text = gonderenData.CurrentRow.Cells[2].Value.ToString();
            gonderenGsmTxt.Text = gonderenData.CurrentRow.Cells[3].Value.ToString();
            gonderenPostaTxt.Text = gonderenData.CurrentRow.Cells[4].Value.ToString();
            gonderenAdresTxt.Text = gonderenData.CurrentRow.Cells[5].Value.ToString();
        }

        private void YoneticiGiris_FormClosed(object sender, FormClosedEventArgs e) // form kapanırken çalışıcak olay
        {

            YoneticiGiris y = new YoneticiGiris();
            y.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

    }
}
