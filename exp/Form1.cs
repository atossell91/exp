using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exp
{
    public partial class Form1 : Form
    {
        OMIClist omList;
        List<Label> labels;
        public Form1()
        {
            omList = new OMIClist();
            InitializeComponent();
        }

        void displayList(string[] lines)
        {
            labels = new List<Label>();
            int lHeight = 40;
            foreach(string line in lines)
            {
                BoundLabel l = new BoundLabel();
                l.Size = new Size(500, lHeight);
                l.Location = new Point(5, lines.Length * lHeight);
                l.Font = new Font("Microsoft sans serif", 14, FontStyle.Regular);
                l.Text = line;
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Click += label_click;
                panel1.Controls.Add(l);
                labels.Add(l);
            }
        }
        void removeLabels()
        {
            foreach (Label l in labels)
            {
                panel1.Controls.Remove(l);
                labels.Remove(l);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReleaseStickerDialog rsd = new ReleaseStickerDialog();
            if (rsd.ShowDialog() == DialogResult.OK)
            {
                if (!omList.AddRecord(rsd.OMIC))
                {
                    return;
                }

                Debug.WriteLine("Adding OMIC: " + rsd.OMIC);
                OMIC o = omList.GetRecord(rsd.OMIC);
                o.StickersOut.Other = rsd.StickersPrinted;
                o.StickersOut.Partial = rsd.StickersPartial;
                
            }
            rsd.Dispose();
            displayList(omList.ListToString("OM", 10));
        }
        private void label_click(object sender, EventArgs e)
        {

        }
    }
}
