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
        List<BoundLabel> labels;
        public Form1()
        {
            omList = new OMIClist();
            InitializeComponent();
        }

        void displayList()
        {
            labels = new List<BoundLabel>();
            int count = 0;
            int labelHeight = 60;
            foreach (OMIC om in omList)
            {
                BoundLabel bl = new BoundLabel();
                bl.Location = new Point(0, count * labelHeight-(1*count));
                bl.Font = new Font("Microsoft sans serif", 20, FontStyle.Regular);
                bl.Size = new Size(500, labelHeight);
                bl.TextAlign = ContentAlignment.MiddleLeft;
                bl.Text = om.ToString("OM",10);
                bl.BorderStyle = BorderStyle.FixedSingle;
                bl.Data = om;
                bl.Click += omicLabel_click;
                panel1.Controls.Add(bl);
                ++count;
            }
        }
        void removeLabels()
        {
            foreach (BoundLabel l in labels)
            {
                panel1.Controls.Remove(l);
                labels.Remove(l);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReleaseStickerDialog rsd = new ReleaseStickerDialog();
            if (rsd.ShowDialog() == DialogResult.OK)
            {
                if (!omList.AddNewRecord(rsd.OMIC))
                {
                    return;
                }

                Debug.WriteLine("Adding OMIC: " + rsd.OMIC);
                OMIC o = omList.GetRecord(rsd.OMIC);
                o.StickersOut.Other = rsd.StickersPrinted;
                o.StickersOut.Partial = rsd.StickersPartial;
                
            }
            displayList();
            rsd.Dispose();
        }
        private void omicLabel_click(object sender, EventArgs e)
        {
            BoundLabel b = (BoundLabel)sender;
            OMIC o = (OMIC)b.Data;
            MessageBox.Show("Selection has " + o.StickersOut.Other + " stickers printed.");
        }

        private void BSaveLoad_Click(object sender, EventArgs e)
        {
            SaveLoadMenu svl = new SaveLoadMenu(omList);
            svl.ShowDialog();
        }
    }
}
