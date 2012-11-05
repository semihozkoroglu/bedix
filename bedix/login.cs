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
    public partial class login : Form
    {
        Database DB;
        Help HP;
        int Admin = 0;

        public login()
        {
            InitializeComponent();
            DB = new Database();
            HP = new Help();
        }
        
        private void login_Load(object sender, EventArgs e)
        {
            DataTable usertable;
            usertable = DB.getUser();

            foreach (DataRow row in usertable.Rows)
            {
                comboBox1.Items.Add(row["kullaniciAdi"]);
            }
        }
        public void close()
        {
            DialogResult result = MessageBox.Show("Çıkmak İstediğinizden Eminmisiniz?", "Çıkış",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                this.Dispose();
            }
        }
        public void error(string e)
        {
            DialogResult result = MessageBox.Show(e, "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button2);
        }
        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && textBox1.Text != "")
            {
                string name = comboBox1.SelectedItem.ToString();
                string pass = textBox1.Text;

                if (DB.userControl(name, pass) == 1)
                {
                    if (DB.userIsAdmin(name) == 1)
                    {
                        Admin = 1;
                        HP.startThread("Anasayfa", Admin,name);
                        this.Dispose();
                    }
                    else
                    {
                        Admin = 0;
                        HP.startThread("Anasayfa", Admin,name);
                        this.Dispose();
                    }
                }
                else
                {
                    error("Kullanıcı Adınızı veya Şifrenizi Kontrol Ediniz");
                }
            }
            else
            {
                error("Kullanıcı Adınızı veya Şifrenizi Girmediniz");
            }
        }
    }
}
