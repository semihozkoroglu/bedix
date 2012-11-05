using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bedix
{
    public partial class ekle : Form
    {
        Database DB;
        Help HP;

        private string fkod;
        private string ukod;
        private string kkod;
        private string tkod;

        public ekle(string firma, string firmad, int i, string firmakod, string urunkod="",string kategorikod = "",string turkod="")
        {
            InitializeComponent();

            fkod = firmakod;
            ukod = urunkod;
            kkod = kategorikod;
            tkod = turkod;

            DB = new Database();
            HP = new Help();

            panel1.Controls.Clear();
            if (i == 1)
            {
                groupBox1.Dock = DockStyle.Fill;
                panel1.Controls.Add(groupBox1);
            }
            else if (i == 2)
            {
                groupBox2.Dock = DockStyle.Fill;
                panel1.Controls.Add(groupBox2);
            }
            else
            {
                groupBox3.Dock = DockStyle.Fill;
                panel1.Controls.Add(groupBox3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                HP.error("Firma Adını Boş Bırakmayın.");
            else
            {
                string q = "firma where firmaismi = " + "'" + textBox1.Text.ToString() + "'";

                if (DB.isThere(q) == 0)
                {
                    if (DB.isThere("firma") == 1)
                    {
                        q = "firma (firmakodu ,firmaismi) VALUES ('" + DB.selectFirmaKod() + "', '" + textBox1.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Firma Eklendi.");
                        this.Dispose();
                    }
                    else
                    {
                        q = "firma (firmakodu ,firmaismi) VALUES ('" + 100 + "', '" + textBox1.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Firma Eklendi.");
                        this.Dispose();
                    }
                }
                else
                {
                    HP.error("Bu Firma Kayıtlı");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                HP.error("Firma Adını Boş Bırakmayın.");
            else
            {
                string q = "urun where urunismi = " + "'" + textBox2.Text.ToString() + "' and firmakodu = '" + fkod + "' and kategorikodu = '" + kkod + "' and turkodu = '" + tkod +"'";

                if (DB.isThere(q) == 0)
                {
                    if (DB.isThere("urun where firmakodu = '" + fkod + "' and kategorikodu = '" + kkod + "' and turkodu = '" + tkod + "'") == 1)
                    {
                        q = "urun (urunkodu, firmakodu, kategorikodu, turkodu, urunismi ,indirimorani,nakifiyat,kredifiyat) VALUES ('" + DB.selectUrunKod(fkod) + "','" + fkod + "', '" + kkod + "', '"  + tkod + "', '" + textBox2.Text.ToString() + "' , '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "', '" + textBox5.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Ürün Eklendi.");
                        this.Dispose();
                    }
                    else
                    {
                        q = "urun (urunkodu, firmakodu, kategorikodu, turkodu, urunismi ,indirimorani,nakifiyat,kredifiyat) VALUES ('" + 100 + "','" + fkod + "', '" + kkod + "', '" + tkod + "', '" + textBox2.Text.ToString() + "' , '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "', '" + textBox5.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Ürün Eklendi.");
                        this.Dispose();
                    }
                }
                else
                {
                    HP.error("Bu ürün Kayıtlı");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.ToString() == "")
                HP.error("Renk Adını Boş Bırakmayın.");
            else
            {
                string q = "renk where renkismi = " + "'" + textBox6.Text.ToString() + "' and urunkodu = '" + ukod + "' and firmakodu = '" + fkod + "' and kategorikodu = '" + kkod + "' and turkodu = '" + tkod + "'";

                if (DB.isThere(q) == 0)
                {
                    if (DB.isThere("renk where urunkodu = '" + ukod + "' and firmakodu = '" + fkod + "' and kategorikodu = '" + kkod + "' and turkodu = '" + tkod + "'") == 1)
                    {
                        q = "renk (kategorikodu, turkodu, renkkodu, urunkodu, firmakodu, renkismi) VALUES ('" + kkod + "', '" + tkod +"','" + DB.selectRenkKod(ukod, fkod) + "','" + ukod + "','" + fkod + "', '" + textBox6.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Renk Eklendi.");
                        this.Dispose();
                    }
                    else
                    {
                        q = "renk (kategorikodu, turkodu, renkkodu, urunkodu, firmakodu, renkismi) VALUES ('" + kkod + "', '" + tkod + "','" + 10 + "','" + ukod + "','" + fkod + "', '" + textBox6.Text.ToString() + "')";
                        DB.insert(q);
                        HP.success("Renk Eklendi.");
                        this.Dispose();
                    }
                }
                else
                {
                    HP.error("Bu renk Kayıtlı");
                }
            }
        }
    }
}
