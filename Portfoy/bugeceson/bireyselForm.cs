using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bugeceson
{
    public partial class bireyselForm : Form
    {
        PortfoyDataContext database = new PortfoyDataContext();
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\Portfoy.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public bireyselForm()
        {
            InitializeComponent();
        }
        
      
        
        void clearTextBox()
        {
            telefonTextBox.Text = "";
            hisseTextBox.Text = "";
            artis_azalisTextBox.Text = "";
            hisse_payiTextBox.Text = "";
            tahvilTextBox.Text = "";
            anaparaTextBox.Text = "";
            basitTextBox.Text = "";
            bilesikTextBox.Text = "";
            nakitTextBox.Text = "";
        }
        private void kayitlariGetir()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string kayit = "SELECT * from BIREYSEL_PORTFOY";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, conn);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView3.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            conn.Close();
        }
        string s = girisForm.user_name;
        string ad_soyad;
        private void bireyselForm_Load(object sender, EventArgs e)
        {
            kullanici_adiTextBox.Text = s;

            var isim = from x in database.BIREYSEL_KULLANICIs
                       where (x.kullanici_adi.ToString() == s)
                       select x;


            foreach (var x in isim)
            { ad_soyad = x.ad+" "+x.soyad; }
            ad_soyadTextBox.Text = ad_soyad;
            kayitlariGetir();
        }

        private void ekleButton_Click_1(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

                if (hisseTextBox.Text != "" && artis_azalisTextBox.Text != "" && hisse_payiTextBox.Text != "" && tahvilTextBox.Text != "" && anaparaTextBox.Text != "" && basitTextBox.Text != "" && bilesikTextBox.Text != "" && nakitTextBox.Text != "")
                {
                    var pas = from x in database.BIREYSEL_PORTFOYs
                              where (x.kullanici_adi.ToString() == s)
                              select x;
                    if (!pas.Any())
                    {
                        SqlCommand komut = new SqlCommand("insert into BIREYSEL_PORTFOY(kullanici_adi,ad_soyad,telefon,hisse,artis_azalis,hisse_payi,tahvil,anapara,basit_faiz,bilesik_faiz,nakit_para) values('" + s + "','" + ad_soyad + "','" + telefonTextBox.Text + "','" + hisseTextBox.Text + "','" + artis_azalisTextBox.Text + "','" + hisse_payiTextBox.Text + "','" + tahvilTextBox.Text + "','" + anaparaTextBox.Text + "','" + basitTextBox.Text + "','" + bilesikTextBox.Text + "','" + nakitTextBox.Text + "')", conn);

                        DataTable dt = new DataTable();
                        dataGridView3.DataSource = dt;
                        komut.ExecuteNonQuery();
                        label11.Text = "Kayıt başarıyla eklenmiştir";
                        clearTextBox();
                        kullanici_adiTextBox.Text = "";
                        ad_soyadTextBox.Text = "";
                        kayitlariGetir();
                        conn.Close();

                    }
                    else
                    {
                        label11.Text = "Hatalı giriş lüften tekrar giriniz.";
                    }
                }
                else
                {
                    label11.Text = "Tüm alanları doldurmanız gerekmektedir";
                    conn.Close();
                }


            }
        }

        private void geriButton_Click_1(object sender, EventArgs e)
        {
            islemForm i = new islemForm();
            i.Show();
            this.Hide();
        }

        private void silButton_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from BIREYSEL_PORTFOY where kullanici_adi=@mnom", conn);
            komut.Parameters.AddWithValue("@mnom", dataGridView3.CurrentRow.Cells[0].Value);
            if (conn.State == ConnectionState.Closed) conn.Open();
            komut.ExecuteNonQuery();
            conn.Close();
            kayitlariGetir();
        }
    
        

    }

}