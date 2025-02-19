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
    public partial class Form2 : Form
    {
        SqlConnection bag;
        SqlCommand komut;
        public Form2()
        {
            InitializeComponent();
        }
        public void baglantı()
        {
            bag = new SqlConnection(@"Data Source=");
            bag.Open();
        }

        public void subeListele()
        {
            baglantı();
            string komut = "select * from sube";
            SqlDataAdapter da = new SqlDataAdapter(komut, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            subelerData.DataSource = dt;
            bag.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            subeListele();

        }

        private void kaydetBtn_Click(object sender, EventArgs e)
        {
           
            baglantı();
            string eklemekomut = "insert into iadetalep(Alici_No,sube_İd,islem_tarihi,Aciklama)values('" + alıcıNoTxt.Text + "','" + subelerData.CurrentRow.Cells[0].Value.ToString() + "','" + tarihTxt.Text + "','" + aciklamaTxt.Text + "')";
            komut = new SqlCommand(eklemekomut, bag);
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("İade Talebiniz alınmıştır", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.None);

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult secim = MessageBox.Show("Formu kapatmak istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (secim == DialogResult.OK)
            {
                Form2 f2 = new Form2();
                f2.Close();

            }

        }
    }
}
