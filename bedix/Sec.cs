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
    public partial class Sec : Form
    {
        Database DB;
        Help HP;
        DataTable usertable;

        public string fisim = "";
        public string fkod = "";
        public string uisim = "";
        public string ukod = "";
        public string risim = "";
        public string rkod = "";

        public string kkod = "";
        public string tkod = "";

        int table;

        public Sec(int i, string firmakod, string urunkod, string kategorikod, string turkod)
        {
            InitializeComponent();

            this.fkod = firmakod;
            this.ukod = urunkod;
            this.kkod = kategorikod;
            this.tkod = turkod;

            table = i;

            DB = new Database();
            HP = new Help();
        }

        private void Sec_Load(object sender, EventArgs e)
        {
            if( table == 1)
                usertable = DB.select("firma", "firmaismi,firmakodu,id");
            else if( table == 2)
                usertable = DB.select("urun where firmakodu = '" + fkod + "' and kategorikodu = '" + kkod +"' and turkodu = '"+ tkod +"'", "urunismi,urunkodu,nakifiyat,kredifiyat,indirimorani");
            else
                usertable = DB.select("renk where firmakodu = '" + fkod + "' and urunkodu = '" + ukod + "'and kategorikodu = '" + kkod + "' and turkodu = '" + tkod + "'", "renkismi,renkkodu,urunkodu,firmakodu");

            dataGridView1.DataSource = usertable;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            int secilencell = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (secilencell > 1 )
            {
                if (table == 1)
                {
                    fisim = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    fkod = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                else if (table == 2)
                {
                    uisim = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    ukod = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                else
                {
                    risim = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    rkod = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                this.Dispose();
            }
            else
            {
                if (secilencell == 0)
                    HP.error("Satır Seçiniz");
                else
                    HP.error("Tüm Satırı Seçiniz.");
            }
        }
    }
}
