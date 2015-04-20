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
    public partial class kayitForm : Form
    {
        public kayitForm()
        {
            InitializeComponent();
        }
        PortfoyDataContext database = new PortfoyDataContext();
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\Portfoy.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");

        private void ekleButton_Click(object sender, EventArgs e)
        {
            if (bireyselRadioButton.Checked == true)
            {
                

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
                    {
                        var pas = from x in database.BIREYSEL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == textBox1.Text.ToString() ||
                                  x.email.ToString() == textBox4.Text.ToString())
                                  select x.kullanici_adi;
                        if (!pas.Any())
                        {
                            SqlCommand komut = new SqlCommand("insert into BIREYSEL_KULLANICI(kullanici_adi,ad,soyad,email,dogum_tarihi,sifre) values('" + textBox1.Text.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text.ToString() + "')", conn);

                            DataTable dt = new DataTable();
                            dataGridView1.DataSource = dt;
                            komut.ExecuteNonQuery();
                            label7.Text = "Kayıt başarıyla eklenmiştir";
                            clearTextBox();
                            kayitlariGetir();
                            conn.Close();


                        }
                        else
                        {
                            label7.Text = "Girdiğiniz kullanıcı adına veya email e sahip bir bireysel kullanıcı vardır";
                            textBox1.Text = "";
                            textBox4.Text = "";
                            conn.Close();


                        }


                    }
                    else
                    {
                        label7.Text = "* olan alanları doldurmanız gerekmektedir";
                        conn.Close();
                    }

                }

            }
            else if (kurumsalRadioButton.Checked == true)
            {
                

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "")
                    {
                        var pas = from x in database.KURUMSAL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == textBox1.Text.ToString() ||
                                  x.email.ToString() == textBox2.Text.ToString())
                                  select x.kullanici_adi;
                        if (!pas.Any())
                        {
                            SqlCommand komut = new SqlCommand("insert into KURUMSAL_KULLANICI(kullanici_adi,email,sirket_adi,sirket_adresi,kurulus_tarihi,sifre) values('" + textBox1.Text.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text.ToString() + "')", conn);

                            DataTable dt = new DataTable();
                            dataGridView1.DataSource = dt;
                            komut.ExecuteNonQuery();
                            label7.Text = "Kayıt başarıyla eklenmiştir";
                            clearTextBox();
                            kayitlariGetir2();
                            conn.Close();


                        }
                        else
                        {
                            label7.Text = "Girdiğiniz kullanıcı adına veya email e sahip bir kurumsal kullanıcı vardır";
                            textBox1.Text = "";
                            textBox2.Text = "";
                            conn.Close();


                        }


                    }
                    else
                    {
                        label7.Text = "* olan alanları doldurmanız gerekmektedir";
                        conn.Close();
                    }

                }
            }
        }
        void clearTextBox()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox1.Text = "";
        }

        private void kayitlariGetir()
        {
            if (conn.State==ConnectionState.Closed)
            conn.Open();
            string kayit = "SELECT * from BIREYSEL_KULLANICI";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, conn);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            conn.Close();
        }

        private void kayitlariGetir2()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string kayit = "SELECT * from KURUMSAL_KULLANICI";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, conn);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            conn.Close();
        }

        private void listeleButton_Click(object sender, EventArgs e)
        {
            if (bireyselRadioButton.Checked == true)
                kayitlariGetir();
            else if (kurumsalRadioButton.Checked == true)
                kayitlariGetir2();
        }

        private void geriButton_Click(object sender, EventArgs e)
        {
            girisForm grsfrm = new girisForm();
            grsfrm.Show();
            this.Hide();
            
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            if (bireyselRadioButton.Checked == true)
            {
                SqlCommand komut = new SqlCommand("delete from BIREYSEL_KULLANICI where kullanici_adi=@tcm", conn);
                komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                if (conn.State == ConnectionState.Closed) conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
                kayitlariGetir();
            }
            else if (kurumsalRadioButton.Checked == true)
            {
                SqlCommand komut = new SqlCommand("delete from KURUMSAL_KULLANICI where kullanici_adi=@tcm", conn);
                komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                if (conn.State == ConnectionState.Closed) conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
                kayitlariGetir();
            }
          
        }


        private void guncelleButton_Click(object sender, EventArgs e)
        {
            if (bireyselRadioButton.Checked == true)
            {


                if (textBox2.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set ad=@adi where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@adi", textBox2.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox3.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set soyad=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox3.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox4.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set email=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox4.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox5.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set dogum_tarihi=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox5.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox6.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set sifre=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox6.Text.ToString());


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox1.Text != "")
                    label7.Text = "Kullanıcı adı kısmı güncellenemez.";
                kayitlariGetir();
                clearTextBox();

            }
            else if (kurumsalRadioButton.Checked == true)
            {
                if (textBox2.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set email=@adi where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@adi", textBox2.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox3.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set sirket_adi=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox3.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox4.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set sirket_adresi=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox4.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox5.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set kurulus_tarihi=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox5.Text);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox6.Text != "")
                {
                    SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set sifre=@x where kullanici_adi=@tcm", conn);
                    komut.Parameters.AddWithValue("@tcm", dataGridView1.CurrentRow.Cells[0].Value);
                    komut.Parameters.AddWithValue("@x", textBox6.Text.ToString());


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();


                }
                if (textBox1.Text != "")
                    label7.Text = "Kullanıcı adı kısmı güncellenemez.";
                kayitlariGetir2();
                clearTextBox();
            }

        }

        private void bireyselRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Kullanıcı Adı:*";
            label2.Text = "Ad:*";
            label3.Text = "Soyad:*";
            label4.Text = "Email:*";
            label5.Text = "Doğum Tarihi:";
            label6.Text = "Şifre ata:*";

            kayitlariGetir();
        }

        private void kurumsalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Kullanıcı Adı:*";
            label2.Text = "Email:*";
            label3.Text = "Şirket Adı:*";
            label4.Text = "Şirket Adresi:";
            label5.Text = "Kuruluş Tarihi:";
            label6.Text = "Şifre ata:*";

            kayitlariGetir2();
        }
      
    }
}
