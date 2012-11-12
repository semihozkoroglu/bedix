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
    public partial class Satis : Form
    {
        DataRow satır;
        DataTable dt;
        Database DB;
        Help HP;
        DataTable depoSatir;

        int error = 0;

        public string kategoriKod = "";
        public string turKod = "";
        public string firmakod = "";
        public string urunkod = "";
        public string renkkod = "";
        public string barkodshort = "";
        public string id = "";
        public string indirim = "";

        public Satis()
        {
            DB = new Database();
            HP = new Help();
            depoSatir = new DataTable();

            InitializeComponent();

            datatable();
        }

        public void datatable()
        {
            dt = new DataTable();
            DataColumn sütun;

            sütun = new DataColumn("Barkod");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("İsim");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Soyisim");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Numara");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Satış Türü");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Fiyatı");
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

            sütun = new DataColumn("Kategori Kodu");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("Tür Kodu");
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

            sütun = new DataColumn("barkodshort");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            sütun = new DataColumn("id");
            sütun.DataType = Type.GetType("System.String");
            dt.Columns.Add(sütun);

            dataGridView1.DataSource = dt;
        }

        private void barkod_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable tmp = new DataTable();

            if (e.KeyCode == Keys.Enter)
            {
                string barkod = barkodtext.Text.ToString().Trim();

                depoSatir = DB.get("depo where id in (select min(id) from depo where durum is NULL and barkod = '" + barkod + "')");

                if (depoSatir.Rows.Count == 0)
                {
                    HP.error("Bu Ürün Sistemde Kayıtlı Değildir");
                    barkodtext.Text = "";
                }
                else
                {
                    foreach (DataRow row in depoSatir.Rows)
                    {
                        kategoriKod = row["kategorikod"].ToString().Trim();
                        turKod = row["turkod"].ToString().Trim();
                        firmakod = row["firmakod"].ToString().Trim();
                        urunkod = row["urunkod"].ToString().Trim();
                        renkkod = row["renkkod"].ToString().Trim();
                        barkodshort = row["barkodshort"].ToString().Trim();
                        id = row["id"].ToString().Trim();
                        
                        firma.Text = row["firmaisim"].ToString().Trim();
                        urun.Text = row["urunisim"].ToString().Trim();
                        renk.Text = row["renkisim"].ToString().Trim();
                        numara.Text = row["numara"].ToString().Trim();
                        tmp = HP.fiyatlar(barkod, DB);

                        foreach (DataRow rowTmp in tmp.Rows)
                        {
                            nakit.Text = rowTmp["nakifiyat"].ToString().Trim();
                            kredi.Text = rowTmp["kredifiyat"].ToString().Trim();
                            indirim = rowTmp["indirimorani"].ToString().Trim();
                        }
                    }
                }
            }
        }

        private void checkFiyat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiyat.Checked)
            {
                textFiyat.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
            }
            else
            {
                textFiyat.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
            }
        }
        public void temizle(int i)
        {
            firma.Clear();
            urun.Clear();
            renk.Clear();
            numara.Clear();
            nakit.Clear();
            kredi.Clear();

            if (i == 1)
            {
                isim.Clear();
                soyisim.Clear();
            }

            if (checkFiyat.Checked)
                checkFiyat.Checked = false;
        }
        private void ekle_Click(object sender, EventArgs e)
        {
            error = 0;

            if (barkodtext.Text.ToString().Trim() == "")
            {
                HP.error("Barkodu Okutunuz.");
            }
            else
            {
                string nakitOdeme = nakit.Text.ToString().Trim();
                string krediOdeme = kredi.Text.ToString().Trim();

                satır = dt.NewRow();
                satır["Barkod"] = barkodtext.Text.ToString().Trim();
                satır["İsim"] = isim.Text.ToString().Trim();
                satır["Soyisim"] = soyisim.Text.ToString().Trim();
                satır["Numara"] = numara.Text.ToString().Trim();

                if (checkFiyat.Checked)
                {
                    if (radioNakit.Checked)
                    {
                        string fiyatGirilen = textFiyat.Text.ToString().Trim();

                        satır["Satış Türü"] = "Nakit";
                        satır["Fiyatı"] = fiyatGirilen;
                        if (HP.hesapla(indirim, fiyatGirilen, nakitOdeme) == 0)
                        {
                            HP.error("Bu Fiyata Satılamaz!");
                            error = 1;
                        }
                    }
                    else
                    {
                        string fiyatGirilen = textFiyat.Text.ToString().Trim();

                        satır["Satış Türü"] = "Kredi";
                        satır["Fiyatı"] = fiyatGirilen;

                        if (HP.hesapla(indirim, fiyatGirilen, krediOdeme) == 0)
                        {
                            HP.error("Bu Fiyata Satılamaz!");
                            error = 1;
                        }
                    }
                }
                else
                {
                    if (radioNakit.Checked)
                    {
                        satır["Satış Türü"] = "Nakit";
                        satır["Fiyatı"] = nakitOdeme;
                    }
                    else
                    {
                        satır["Satış Türü"] = "Kredi";
                        satır["Fiyatı"] = krediOdeme;
                    }
                }

                satır["Firma İsmi"] = firma.Text.ToString().Trim();
                satır["Ürün İsmi"] = urun.Text.ToString().Trim();
                satır["Renk İsmi"] = renk.Text.ToString().Trim();
                satır["Kategori Kodu"] = kategoriKod.Trim();
                satır["Tür Kodu"] = turKod.Trim();
                satır["Firma Kodu"] = firmakod.Trim();
                satır["Ürün Kodu"] = urunkod.Trim();
                satır["Renk Kodu"] = renkkod.Trim();
                satır["barkodshort"] = barkodshort.Trim();
                satır["id"] = id;

                if (error == 0)
                {
                    dt.Rows.Add(satır);
                    DB.update("depo set durum = '" + id + "' where id = '" + id + "'");
                    barkodtext.Clear();
                    temizle(0);
                }
            }
        }

        private void sil_Click(object sender, EventArgs e)
        {
            int secilencell = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (secilencell > 1)
            {
                DB.update("depo set durum = NULL where id = '" + dataGridView1.CurrentRow.Cells[15].Value.ToString().Trim() +"'");
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

        private void gonder_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string sql = "satis (barkod,isim,soyisim," +
                          "numara,nakit,kredi,"+
                          "firmaisim,urunisim,renkisim," +
                          "kategorikod,turkod," +
                          "firmakod,urunkod,renkkod," +
                          "giris,barkodshort) VALUES (" +
                          
                          "'"+ row.Cells[0].Value.ToString().Trim() +"'," +
                          "'" + row.Cells[1].Value.ToString().Trim() + "'," +
                          "'" + row.Cells[2].Value.ToString().Trim() + "'," +

                          "'" + row.Cells[3].Value.ToString().Trim() + "',";
                
                          if ( row.Cells[4].Value.ToString().Trim() == "Nakit")
                              sql += "'" + row.Cells[5].Value.ToString().Trim() + "','',";
                          else
                              sql += "'','" + row.Cells[5].Value.ToString().Trim() + "',";

                          sql += "'" + row.Cells[6].Value.ToString().Trim() + "'," +
                                 "'" + row.Cells[7].Value.ToString().Trim() + "'," +
                                 "'" + row.Cells[8].Value.ToString().Trim() + "'," +

                                 "'" + row.Cells[9].Value.ToString().Trim() + "'," +
                                 "'" + row.Cells[10].Value.ToString().Trim() + "'," +

                                 "'" + row.Cells[11].Value.ToString().Trim() + "'," +
                                 "'" + row.Cells[12].Value.ToString().Trim() + "'," +
                                 "'" + row.Cells[13].Value.ToString().Trim() + "'," +

                                 "'" + DateTime.Now + "'," +
                                 "'" + row.Cells[14].Value.ToString().Trim() + "')";

                          DB.insert(sql);
                          DB.remove("depo where id = '" + row.Cells[15].Value.ToString().Trim() + "'");
            }

            HP.success("Satış İşlemi Başarılı.");
            dataGridView1.DataSource = null;
            datatable();
            temizle(1);
        }
    }
}
