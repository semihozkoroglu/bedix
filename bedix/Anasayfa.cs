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
    public partial class Anasayfa : Form
    {
        int Admin;
        string name;
        Help HP;

        public Anasayfa(int a, string n)
        {
            InitializeComponent();

            this.Admin = a;
            this.name = n;

            if (Admin == 0)
            {
                button6.Visible = false;
                button7.Visible = false;
            }
            HP = new Help();
            //HP.addPanel(ref panel1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (close() == 1)
                this.Dispose();
        }

        public int close()
        {
            DialogResult result = MessageBox.Show("Çıkmak İstediğinizden Eminmisiniz?", "Çıkış",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                return 1;
            }
            return 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (close() == 1)
            {
                HP.returnLogin();
                this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            groupBox1.Text = "Satış";
            Satis s = new Satis();
            s.TopLevel = false;
            s.Visible = true;
            s.Dock = DockStyle.Fill;
            s.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(s);
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            button1_Click(null,null);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            groupBox1.Text = "Ürün Girişi";
            Giris s = new Giris();
            s.TopLevel = false;
            s.Visible = true;
            s.Dock = DockStyle.Fill;
            s.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(s);
        }
    }
}
