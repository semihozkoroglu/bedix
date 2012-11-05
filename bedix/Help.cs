using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace bedix
{
    class Help
    {
        public void newForm(string pName,int Admin, string name)
        {
            if( pName == "Anasayfa")
                Application.Run(new Anasayfa(Admin,name));
        }
        public void error(string e)
        {
            DialogResult result = MessageBox.Show(e, "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button2);
        }

        public void success(string e)
        {
            DialogResult result = MessageBox.Show(e, "Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button2);
        }

        public void startThread(string pName, int Admin, string name)
        {
            Thread t = new Thread(delegate() { newForm(pName,Admin,name); });
            t.Start();
        }
        public void returnLogin()
        {
            Thread t = new Thread(delegate() { Application.Run(new login()); });
            t.Start();
        }

        public Bitmap barkodUret(string prodCode, string urun, string renk, string numara, string nakitFiyat, string krediFiyat)
        {
            System.Drawing.Bitmap bmpimg;

            //string prodCode = "50101234560841";
            //string urun = "Ürün Adı";
            //string renk = "Renk";
            //string numara = "44";
            //string nakitFiyat = "165";
            //string krediFiyat = "195";

            bmpimg = box(prodCode, urun, renk, numara, nakitFiyat, krediFiyat);

            //Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\sem\\Desktop" + "/barkod.pdf", FileMode.Create));
            //doc.Open();
            //PdfPTable table = new PdfPTable(4);
            //table.AddCell(iTextSharp.text.Image.GetInstance(bmpimg, System.Drawing.Imaging.ImageFormat.Png));
            //table.AddCell(iTextSharp.text.Image.GetInstance(bmpimg, System.Drawing.Imaging.ImageFormat.Png));
            //table.AddCell(iTextSharp.text.Image.GetInstance(bmpimg, System.Drawing.Imaging.ImageFormat.Png));
            //table.AddCell("");
            //doc.Add(table);
            //doc.Add(table);
            //doc.Add(table);
            //doc.Add(table);
            //doc.Add(table);
            //doc.Add(table);
            ////doc.Add(iTextSharp.text.Image.GetInstance(bmpimg, System.Drawing.Imaging.ImageFormat.Png));
            //doc.Close();

            return bmpimg;
        }

        public Bitmap box(string prodCode, string urun, string renk, string numara, string nakitFiyat, string krediFiyat)
        {
            System.Drawing.Bitmap bmpimg;

            Barcode128 code128 = new Barcode128();
            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = false;
            code128.Code = prodCode;
            code128.BarHeight = 60;

            bmpimg = new Bitmap(130, 150); 

            Graphics bmpgraphics = Graphics.FromImage(bmpimg);
            Pen pen = new Pen(Color.Black, 3.0f);
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(92, 2, 32, 27);
            bmpgraphics.Clear(Color.White); 

            bmpgraphics.DrawRectangle(pen, rec);
            bmpgraphics.DrawString(urun, new System.Drawing.Font("Arial", 9, FontStyle.Bold), SystemBrushes.WindowText, new Point(8, 4));
            bmpgraphics.DrawString(renk, new System.Drawing.Font("Arial", 8, FontStyle.Regular), SystemBrushes.WindowText, new Point(8, 18));
            bmpgraphics.DrawString(numara, new System.Drawing.Font("Arial", 15, FontStyle.Bold), SystemBrushes.WindowText, new Point(95, 5));
            bmpgraphics.DrawImage(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White), new Point(10, 40));

            bmpgraphics.DrawString(prodCode, new System.Drawing.Font("Arial", 7, FontStyle.Regular), SystemBrushes.WindowText, new Point(30, 102));
            bmpgraphics.DrawString("Nakit:" + nakitFiyat + " TL", new System.Drawing.Font("Arial", 12, FontStyle.Bold), SystemBrushes.WindowText, new Point(15, 114));
            bmpgraphics.DrawString("Kredi:" + krediFiyat + " TL", new System.Drawing.Font("Arial", 12, FontStyle.Bold), SystemBrushes.WindowText, new Point(15, 132));
            //pictureBox1.Image = bmpimg;

            return bmpimg;
        }

        //public void addPanel(ref Panel p)
        //{
        //    p.Controls.Add(newButton(5));
        //    p.Controls.Add(newButton(55));
        //}
        //public Button newButton(int x)
        //{
        //    Button b = new Button();

        //    b.Width = 100;
        //    b.Height = 50;

        //    b.Location = new Point(5,x);
            
        //    return b;
        //}
    }
}
