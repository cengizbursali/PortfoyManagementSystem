using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bugeceson
{
    public partial class girisForm : Form
    {
        public girisForm()
        {
            InitializeComponent();
        }
        PortfoyDataContext database = new PortfoyDataContext();

        private void kullaniciRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            
                label1.Text = "Kullanıcı Adı:";
                label2.Text = "Şifre:";
                this.Text = "KULLANICI GİRİŞ FORMU";
                kullaniciTextBox.Text = "";
                sifreTextBox.Text = "";
                secimComboBox.Visible = true;
                label5.Visible = true;
            

        }



        private void yoneticiRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            if (yoneticiRadioButton.Checked == true)
            {
                label1.Text = "Yönetici:";
                label2.Text = "Şifre:";
                this.Text = "YÖNETİCİ GİRİŞ FORMU";
                kullaniciTextBox.Text = "";
                sifreTextBox.Text = "";


                secimComboBox.Visible = false;
                label5.Visible = false;

                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox1.Visible = false;
                pictureBox4.Visible = false;
                label6.Visible = false;
                label7.Visible = false
;
            }
        }
        public static string veri;
        public static string user_name;
        public void girisForm_Load(object sender, EventArgs e)
        {
            if (yoneticiRadioButton.Checked == true)
            {
                label1.Text = "Yönetici:";
                label2.Text = "Şifre:";
                this.Text = "YÖNETİCİ GİRİŞ FORMU";

            }

        }

        private void girisButton_Click(object sender, EventArgs e)
        {
            if (yoneticiRadioButton.Checked == true)
            {

                if (kullaniciTextBox.Text != "" && sifreTextBox.Text != "")
                {

                    var row = from x in database.YONETICIs
                              where (x.kullanici_adi.ToString() == kullaniciTextBox.Text &&
                              x.sifre.ToString() == sifreTextBox.Text)
                              select x.sifre;
                    if (row.Any())
                    {
                        
                        kayitForm kyt = new kayitForm();
                        kyt.Show();
                        this.Hide();
                    }
                    else
                    {
                        label3.Text = "Kullanıcı adı veya şifreyi yanlış girdiniz";
                        kullaniciTextBox.Text = "";
                        sifreTextBox.Text = "";
                    }
                }
                else
                {
                    label3.Text = "Alanlara değer girmeden giriş yapamazsınız";
                    kullaniciTextBox.Text = "";
                    sifreTextBox.Text = "";
                }
            }
            else if (kullaniciRadioButton.Checked == true)
            {

                if (secimComboBox.SelectedIndex == 0)
                {
                    if (kullaniciTextBox.Text != "" && sifreTextBox.Text != "")
                    {

                        var row = from x in database.BIREYSEL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == kullaniciTextBox.Text &&
                                  x.sifre.ToString() == sifreTextBox.Text)
                                  select x.kullanici_adi;
                        if (row.Any())
                        {
                            veri = secimComboBox.Text;
                            user_name = kullaniciTextBox.Text;
                            islemForm islem = new islemForm();
                            islem.Show();
                            this.Hide();
                        }
                        else
                        {
                            label3.Text = "Kullanıcı adını veya şifreyi yanlış girdiniz";
                            kullaniciTextBox.Text = "";
                            sifreTextBox.Text = "";
                        }
                    }
                    else
                    {
                        label3.Text = "Alanlara değer girmeden giriş yapamazsınız";
                        kullaniciTextBox.Text = "";
                        sifreTextBox.Text = "";
                    }
                }

                else if (secimComboBox.SelectedIndex == 1)
                {
                    if (kullaniciTextBox.Text != "" && sifreTextBox.Text != "")
                    {

                        var row = from x in database.KURUMSAL_KULLANICIs
                                  where (x.kullanici_adi.ToString() == kullaniciTextBox.Text &&
                                  x.sifre.ToString() == sifreTextBox.Text)
                                  select x.kullanici_adi;
                        if (row.Any())
                        {
                            veri = secimComboBox.Text;
                            user_name = kullaniciTextBox.Text;
                            islemForm islem = new islemForm();
                            islem.Show();
                            this.Hide();
                        }
                        else
                        {
                            label3.Text = "Kullanıcı adını veya şifreyi yanlış girdiniz";
                            kullaniciTextBox.Text = "";
                            sifreTextBox.Text = "";
                        }
                    }
                    else
                    {
                        label3.Text = "Alanlara değer girmeden giriş yapamazsınız";
                        kullaniciTextBox.Text = "";
                        sifreTextBox.Text = "";
                    }
                }
                else
                {
                    label3.Text = "Seçim yapmadan giriş yapamazsınız!";
                }
            }

        }
        private void cikisButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void secimComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secimComboBox.SelectedIndex == 0)
            {
                pictureBox1.Visible = true;
                pictureBox4.Visible = true;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                label6.Visible = true;
                label7.Visible = true;

            }
            else if (secimComboBox.SelectedIndex == 1)
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox1.Visible = false;
                pictureBox4.Visible = false;
                label6.Visible = true;
                label7.Visible = true;
            }

        }

      

        


    }
}

