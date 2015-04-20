using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.Design;

namespace bugeceson
{
    public partial class islemForm : Form
    {
        public islemForm()
        {
            InitializeComponent();
           
        }
        PortfoyDataContext database = new PortfoyDataContext();
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\Portfoy.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");

         

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.Size = System.Drawing.Size.Add(new Size(0, 55), new Size(55, 0));
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Size = System.Drawing.Size.Add(new Size(0, 40), new Size(40, 0));
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Size = System.Drawing.Size.Add(new Size(0, 55), new Size(55, 0));
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = System.Drawing.Size.Add(new Size(0, 40), new Size(40, 0));
        }

        private void degistirButton_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Bireysel Kullanıcı")
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if (kullanici_adiTextBox.Text != "" && eskiTextBox.Text != "" && yeniTextBox.Text != "" && yeni2TextBox.Text != "")
                    {
                        var row = from x in database.BIREYSEL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == kullanici_adiTextBox.Text &&
                                  x.sifre.ToString() == eskiTextBox.Text)
                                  select x.sifre;
                        if (row.Any())
                        {
                            if (yeni2TextBox.Text.ToString() == yeniTextBox.Text.ToString())
                            {
                                SqlCommand komut = new SqlCommand("update BIREYSEL_KULLANICI set sifre=@sifresi where sifre='" + eskiTextBox.Text + "'", conn);
                                komut.Parameters.AddWithValue("@sifresi", yeniTextBox.Text.ToString());
                                if (conn.State == ConnectionState.Closed) conn.Open();
                                komut.ExecuteNonQuery();
                                MessageBox.Show("Şifreniz başarıyla değiştirilmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                conn.Close();
                                girisForm gr = new girisForm();
                                gr.Show();
                                this.Hide();
                            }
                            else
                            {
                                label7.Text = "Girdiğiniz şifreler birbirleriyle uyuşmamaktadır.";
                                yeni2TextBox.Text = "";
                                yeniTextBox.Text = "";
                                conn.Close();
                            }
                        }
                        else
                        {
                            label7.Text = "Kullanıcı adını veya eski şifreyi yanlış girdiniz.";
                            yeni2TextBox.Text = "";
                            yeniTextBox.Text = "";
                            eskiTextBox.Text = "";
                            kullanici_adiTextBox.Text = "";
                            conn.Close();

                        }
                    }
                    else
                    {
                        label7.Text = "Tüm alanları doldurmak zorunludur.";
                        conn.Close();
                    }


                }
            }
            else if (label1.Text == "Kurumsal Kullanıcı")
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if (kullanici_adiTextBox.Text != "" && eskiTextBox.Text != "" && yeniTextBox.Text != "" && yeni2TextBox.Text != "")
                    {
                        var row = from x in database.KURUMSAL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == kullanici_adiTextBox.Text &&
                                  x.sifre.ToString() == eskiTextBox.Text)
                                  select x.sifre;
                        if (row.Any())
                        {
                            if (yeni2TextBox.Text.ToString() == yeniTextBox.Text.ToString())
                            {
                                SqlCommand komut = new SqlCommand("update KURUMSAL_KULLANICI set sifre=@sifresi where sifre='" + eskiTextBox.Text + "'", conn);
                                komut.Parameters.AddWithValue("@sifresi", yeniTextBox.Text.ToString());
                                if (conn.State == ConnectionState.Closed) conn.Open();
                                komut.ExecuteNonQuery();
                                MessageBox.Show("Şifreniz başarıyla değiştirilmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                conn.Close();
                                girisForm gr = new girisForm();
                                gr.Show();
                                this.Hide();
                            }
                            else
                            {
                                label7.Text = "Girdiğiniz şifreler birbirleriyle uyuşmamaktadır.";
                                yeni2TextBox.Text = "";
                                yeniTextBox.Text = "";
                                conn.Close();
                            }
                        }
                        else
                        {
                            label7.Text = "Kullanıcı adını veya eski şifreyi yanlış girdiniz.";
                            yeni2TextBox.Text = "";
                            yeniTextBox.Text = "";
                            eskiTextBox.Text = "";
                            kullanici_adiTextBox.Text = "";
                            conn.Close();

                        }
                    }
                    else
                    {
                        label7.Text = "Tüm alanları doldurmak zorunludur.";
                        conn.Close();
                    }


                }
            
            }
        }
        
        private void islemForm_Load(object sender, EventArgs e)
        {
            label1.Text = girisForm.veri;
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Bireysel Kullanıcı")
            {
                bireyselForm br = new bireyselForm();
                br.Show();
                this.Hide();

            }
            else if (label1.Text == "Kurumsal Kullanıcı")
            {
                
                kurumsalForm kr = new kurumsalForm();
                kr.Show();
                this.Hide();
                
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            girisForm gr = new girisForm();
            gr.Show();
            this.Hide();
        }

       

       
     
        

        

       
        
        

        

      

        

        
    }
}
