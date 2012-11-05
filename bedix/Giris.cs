using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace bedix
{
    public partial class Giris : Form
    {
        Database DB;
        Help HP;
        DataTable barkodSatir;
        DataRow satır;
        DataTable dt;
        PdfPTable table;
        System.Drawing.Bitmap bmpimg;

        public string kategoriKod = "";
        public string turKod = "";
        public string firmaisim = "";
        public string firmakod = "";
        public string urunisim = "";
        public string urunkod = "";
        public string renkisim = "";
        public string renkkod = "";

        public string barkod = "";

        int error = 0;

        int fSecim = 0;
        int uSecim = 0;
        int rSecim = 0;

        public Giris()
        {
            InitializeComponent();
        
            DB = new Database();
            HP = new Help();
            barkodSatir = new DataTable();
            table = new PdfPTable(4);

            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            panel2.Controls.Clear();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            DataTable usertable;
            usertable = DB.get("kategori");

            foreach (DataRow row in usertable.Rows)
            {
                kategori.Items.Add(row["adi"]);
            }

            usertable = DB.get("tur");

            foreach (DataRow row in usertable.Rows)
            {
                tur.Items.Add(row["adi"]);
            }

            datatable();
        }

        public void datatable()
        {
            dt = new DataTable();
            DataColumn sütun;

            sütun = new DataColumn("Barkod");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Firma İsmi");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Ürün İsmi");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Renk İsmi");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Firma Kodu");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Ürün Kodu");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Renk Kodu");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kategori.SelectedItem == null || tur.SelectedItem == null)
                HP.error("Kategori ve tür seçimini yapınız.");
            else
            {
                ekle ekle = new ekle("Firma", "Firma Adı:", 1, "");
                ekle.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (firmakod.Length > 1)
            {
                kategoriVeTur();

                ekle ekle = new ekle("Ürün", "Ürün Adı:", 2, firmakod,"",kategoriKod,turKod);
                ekle.Width = 400;
                ekle.Height = 300;
                ekle.Show();
            }
            else
                HP.error("Firmayı Seçiniz.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (firmakod.Length > 1)
            {
                kategoriVeTur();

                ekle ekle = new ekle("Renk", "Renk Adı:", 3, firmakod, urunkod, kategoriKod, turKod);
                ekle.Show();
            }
            else
                HP.error("Ürün Seçiniz.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kategori.SelectedItem == null || tur.SelectedItem == null)
                HP.error("Kategori ve tür seçimini yapınız.");
            else
            {
                Sec sec = new Sec(1, "", "","","");
                sec.ShowDialog();
                if (sec.fisim != "")
                {
                    label6.Text = sec.fisim;
                    label6.ForeColor = Color.Green;
                    firmaisim = sec.fisim;
                    firmakod = sec.fkod;

                    fSecim = 1;

                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;

                }
                else
                {
                    label6.Text = "Seçilmedi.";
                    label6.ForeColor = Color.Red;

                    fSecim = 0;
                    
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;

                }
                label7.Text = "Seçilmedi.";
                label7.ForeColor = Color.Red;

                label8.Text = "Seçilmedi.";
                label8.ForeColor = Color.Red;

                uSecim = 0;
                rSecim = 0;

                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (firmakod.Length > 1)
            {
                kategoriVeTur();

                Sec sec = new Sec(2, firmakod,"",kategoriKod,turKod);
                sec.ShowDialog();
                if (sec.uisim != "")
                {
                    label7.Text = sec.uisim;
                    label7.ForeColor = Color.Green;
                    urunisim = sec.uisim;
                    urunkod = sec.ukod;

                    uSecim = 1;

                    button7.Enabled = true;
                    button8.Enabled = true;
                    button9.Enabled = true;
                }
                else
                {
                    label7.Text = "Seçilmedi.";
                    label7.ForeColor = Color.Red;

                    uSecim = 0;

                    button7.Enabled = false;
                    button8.Enabled = false;
                    button9.Enabled = false;
                }

                rSecim = 0;
                label8.Text = "Seçilmedi.";
                label8.ForeColor = Color.Red;
            }
            else
                HP.error("Firmayı Seçiniz.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kategori.SelectedItem == null || tur.SelectedItem == null)
                HP.error("Kategori ve tür seçimini yapınız.");
            else
            {
                Sil sil = new Sil(1, "", "","","");
                sil.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (firmakod.Length > 1)
            {
                kategoriVeTur();

                if (error == 0)
                {
                    Sil sil = new Sil(2, firmakod, "", kategoriKod, turKod);
                    sil.ShowDialog();
                }
                else
                    error = 0;
            }
            else
                HP.error("Firmayı Seçiniz.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (urunkod.Length > 1)
            {
                kategoriVeTur();

                if (error == 0)
                {
                    Sil sil = new Sil(3, firmakod, urunkod, kategoriKod, turKod);
                    sil.ShowDialog();
                }
                else
                    error = 0;
            }
            else
                HP.error("Ürünü Seçiniz.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (urunkod.Length > 1)
            {
                kategoriVeTur();

                Sec sec = new Sec(3, firmakod, urunkod,kategoriKod,turKod);
                sec.ShowDialog();
                if (sec.risim != "")
                {
                    label8.Text = sec.risim;
                    label8.ForeColor = Color.Green;
                    renkisim = sec.risim;
                    renkkod = sec.rkod;

                    rSecim = 1;

                    barkod = kategoriKod.Trim() + turKod.Trim() + firmakod.Trim() + urunkod.Trim() + renkkod.Trim();
    
                    button7.Enabled = true;
                    button8.Enabled = true;
                    button9.Enabled = true;
                }
                else
                {
                    rSecim = 0;
                    label8.Text = "Seçilmedi.";
                    label8.ForeColor = Color.Red;
                }
            }
            else
                HP.error("Ürünü Seçiniz.");
        }

        private void kategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            if (kategori.SelectedIndex == 0)
            {
                patik.Dock = DockStyle.Fill;
                panel2.Controls.Add(patik);
            }
            if (kategori.SelectedIndex == 1)
            {
                filet.Dock = DockStyle.Fill;
                panel2.Controls.Add(filet);
            }
            if (kategori.SelectedIndex == 2)
            {
                garson.Dock = DockStyle.Fill;
                panel2.Controls.Add(garson);
            }
            if (kategori.SelectedIndex == 3)
            {
                zenne.Dock = DockStyle.Fill;
                panel2.Controls.Add(zenne);
            }
            if (kategori.SelectedIndex == 4)
            {
                merdane.Dock = DockStyle.Fill;
                panel2.Controls.Add(merdane);
            }
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            label6.Text = "Seçilmedi.";
            label6.ForeColor = Color.Red;

            label7.Text = "Seçilmedi.";
            label7.ForeColor = Color.Red;

            label8.Text = "Seçilmedi.";
            label8.ForeColor = Color.Red;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            kategoriVeTur();

            if (error == 0)
            {
                barkod = kategoriKod.Trim() + turKod.Trim() + firmakod.Trim() + urunkod.Trim() + renkkod.Trim();

                barkodEkleNum(barkod);
                HP.success("Kaydetme İşlemi Başarılı.");
            }
            else
                error = 0;

        }
        public void ekle(decimal value,string num)
        {
            string tmp;
            string q = "";
            kategoriVeTur();

            for (int i = 0; i < value; i++)
            {
                tmp = barkod;
                barkod += num;
                q = "depo (barkod, barkodshort , giris, firmakod, urunkod, renkkod, kategorikod, turkod, numara, firmaisim, urunisim, renkisim ) VALUES ('" + barkod + "','" + tmp +"','" + DateTime.Now + "', '" + firmakod + "', '" + urunkod + "', '" + renkkod +"','" + kategoriKod + "','" + turKod +"', '" + num + "','" + firmaisim + "','" + urunisim + "','" + renkisim + "')";
                DB.insert(q);
                barkod = tmp;
            }
        }
        public void barkodEkleNum(string barkod)
        {

            if (kategori.SelectedIndex == 0)
            {
                if (n17.Value > 0)
                    ekle(n17.Value, "17");
                if (n18.Value > 0)
                    ekle(n18.Value, "18");
                if (n19.Value > 0)
                    ekle(n19.Value, "19");
                if (n20.Value > 0)
                    ekle(n20.Value, "20");
                if (n21.Value > 0)
                    ekle(n21.Value, "21");
                if (n22.Value > 0)
                    ekle(n22.Value, "22");
                if (n23.Value > 0)
                    ekle(n23.Value, "23");
                if (n24.Value > 0)
                    ekle(n24.Value, "24");
                if (n25.Value > 0)
                    ekle(n25.Value, "25");
                if (n26.Value > 0)
                    ekle(n26.Value, "26");
            }
            else if (kategori.SelectedIndex == 1)
            {
                if (nu24.Value > 0)
                    ekle(nu24.Value, "24");
                if (nu25.Value > 0)
                    ekle(nu25.Value, "25");
                if (nu26.Value > 0)
                    ekle(nu26.Value, "26");
                if (nu27.Value > 0)
                    ekle(nu27.Value, "27");
                if (nu28.Value > 0)
                    ekle(nu28.Value, "28");
                if (nu29.Value > 0)
                    ekle(nu29.Value, "29");
                if (nu30.Value > 0)
                    ekle(nu30.Value, "30");
                if (nu31.Value > 0)
                    ekle(nu31.Value, "31");
                if (nu32.Value > 0)
                    ekle(nu32.Value, "32");
                if (nu33.Value > 0)
                    ekle(nu33.Value, "33");
                if (nu34.Value > 0)
                    ekle(nu34.Value, "34");
                if (nu35.Value > 0)
                    ekle(nu35.Value, "35");
                if (nu36.Value > 0)
                    ekle(nu36.Value, "36");
            }
            else if (kategori.SelectedIndex == 2)
            {
                if (num35.Value > 0)
                    ekle(num35.Value, "35");
                if (num36.Value > 0)
                    ekle(num36.Value, "36");
                if (num37.Value > 0)
                    ekle(num37.Value, "37");
                if (num38.Value > 0)
                    ekle(num38.Value, "38");
                if (num39.Value > 0)
                    ekle(num39.Value, "39");
                if (num40.Value > 0)
                    ekle(num40.Value, "40");
                if (num41.Value > 0)
                    ekle(num41.Value, "41");
            }
            else if (kategori.SelectedIndex == 3)
            {
                if (nume35.Value > 0)
                    ekle(nume35.Value, "35");
                if (nume36.Value > 0)
                    ekle(nume36.Value, "36");
                if (nume37.Value > 0)
                    ekle(nume37.Value, "37");
                if (nume38.Value > 0)
                    ekle(nume38.Value, "38");
                if (nume39.Value > 0)
                    ekle(nume39.Value, "39");
                if (nume40.Value > 0)
                    ekle(nume40.Value, "40");
                if (nume41.Value > 0)
                    ekle(nume41.Value, "41");
                if (nume42.Value > 0)
                    ekle(nume42.Value, "42");
            }
            else if (kategori.SelectedIndex == 4)
            {
                if (numer39.Value > 0)
                    ekle(numer39.Value, "39");
                if (numer40.Value > 0)
                    ekle(numer40.Value, "40");
                if (numer41.Value > 0)
                    ekle(numer41.Value, "41");
                if (numer42.Value > 0)
                    ekle(numer42.Value, "42");
                if (numer43.Value > 0)
                    ekle(numer43.Value, "43");
                if (numer44.Value > 0)
                    ekle(numer44.Value, "44");
                if (numer45.Value > 0)
                    ekle(numer45.Value, "45");
                if (numer46.Value > 0)
                    ekle(numer46.Value, "46");
            }
            else
                HP.error("Kategori türünü seçiniz.");  
        }

        public void kategoriVeTur()
        {
            if (tur.SelectedIndex == 0)
                turKod = DB.selectKod("tur", "terlik");
            else if (tur.SelectedIndex == 1)
                turKod = DB.selectKod("tur", "sandelet");
            else if (tur.SelectedIndex == 2)
                turKod = DB.selectKod("tur", "spor");
            else if (tur.SelectedIndex == 3)
                turKod = DB.selectKod("tur", "klasik");
            else if (tur.SelectedIndex == 4)
                turKod = DB.selectKod("tur", "bot");
            else
            {
                HP.error("Türü seçiniz.");
                error = 1;
            }

            if (kategori.SelectedIndex == 0)
                kategoriKod = DB.selectKod("kategori", "patik");
            else if (kategori.SelectedIndex == 1)
                kategoriKod = DB.selectKod("kategori", "filet");
            else if (kategori.SelectedIndex == 2)
                kategoriKod = DB.selectKod("kategori", "garson");
            else if (kategori.SelectedIndex == 3)
                kategoriKod = DB.selectKod("kategori", "zenne");
            else if (kategori.SelectedIndex == 4)
                kategoriKod = DB.selectKod("kategori", "merdane");
            else
            {
                HP.error("Kategori türünü seçiniz.");
                error = 1;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (fSecim == 1)
            {
                if (uSecim == 1)
                {
                    if (rSecim == 1)
                    {
                        satır = dt.NewRow();
                        satır["Barkod"] = barkod;
                        satır["Firma İsmi"] = firmaisim;
                        satır["Firma Kodu"] = firmakod;
                        satır["Ürün İsmi"] = urunisim;
                        satır["Ürün Kodu"] = urunkod;
                        satır["Renk İsmi"] = renkisim;
                        satır["Renk Kodu"] = renkkod;

                        dt.Rows.Add(satır);
                    }
                    else
                        HP.error("Renk seçiniz.");
                }
                else
                    HP.error("Ürünü seçiniz.");
            }
            else
                HP.error("Firmayı seçiniz.");
        }

        private void tur_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            label6.Text = "Seçilmedi.";
            label6.ForeColor = Color.Red;

            label7.Text = "Seçilmedi.";
            label7.ForeColor = Color.Red;

            label8.Text = "Seçilmedi.";
            label8.ForeColor = Color.Red;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int secilencell = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (secilencell > 1)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            else
            {
                if (secilencell == 0)
                    HP.error("Satır Seçiniz");
                else
                    HP.error("Tüm Satırı Seçiniz.");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 1)
                HP.error("Ürün eklemesi yapınız.");
            else
            {
                int i = 0;

                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\sem\\Desktop" + "/barkod.pdf", FileMode.Create));
                doc.Open();
                table = new PdfPTable(4);

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    string nFiyat = "";
                    string kFiyat = "";

                    if (row.Cells[0].Value != null)
                    {
                        barkodSatir = DB.select("depo where barkodshort = '" + row.Cells[0].Value.ToString() + "'", "barkod, firmakod, urunkod, renkkod, kategorikod, turkod, numara, urunisim, renkisim");

                        foreach (DataRow brow in barkodSatir.Rows)
                        {
                            nFiyat = DB.selectFRU("urun where kategorikodu = '" + brow["kategorikod"] + "' and turkodu = '" + brow["turkod"] + "' and firmakodu = '" + brow["firmakod"] + "'", "nakifiyat");
                            kFiyat = DB.selectFRU("urun where kategorikodu = '" + brow["kategorikod"] + "' and turkodu = '" + brow["turkod"] + "' and firmakodu = '" + brow["firmakod"] + "'", "kredifiyat");

                            tabloEkle(brow["barkod"].ToString().Trim(), brow["urunisim"].ToString().Trim(), brow["renkisim"].ToString().Trim(), brow["numara"].ToString().Trim(), nFiyat.Trim(), kFiyat.Trim());

                            i += 1;
                            if (i == 4)
                            {
                                doc.Add(table);
                                table = new PdfPTable(4);
                                i = 0;
                            }
                        }
                    }
                }

                if (i == 1)
                {
                    table.AddCell("");
                    table.AddCell("");
                    table.AddCell("");
                    doc.Add(table);
                    i = 0;
                }
                else if (i == 2)
                {
                    table.AddCell("");
                    table.AddCell("");
                    doc.Add(table);
                    i = 0;
                }
                else if (i == 3)
                {
                    table.AddCell("");
                    doc.Add(table);
                    i = 0;
                }

                doc.Close();
                HP.success("Barkod Oluşturuldu.");
            }
        }
        public void tabloEkle(string prodCode,string urun, string renk, string numara, string nakitFiyat, string krediFiyat)
        {
            bmpimg = HP.barkodUret(prodCode, urun, renk, numara, nakitFiyat, krediFiyat);
            table.AddCell(iTextSharp.text.Image.GetInstance(bmpimg, System.Drawing.Imaging.ImageFormat.Png));
        }
    }
}
