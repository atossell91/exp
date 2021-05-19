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
        List<Label> labels;
        public Form1()
        {
            InitializeComponent();
        }

        void displayList(string[] lines)
        {
            labels = new List<Label>();
            int lHeight = 100;
            foreach(string line in lines)
            {
                Label l = new Label();
                l.Size = new Size(500, lHeight);
                l.Location = new Point(5, lines.Length * lHeight);
                l.Font = new Font("Microsoft sans serif", 14, FontStyle.Regular);
                l.Text = line;
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
                Debug.WriteLine("Dialog is ok");
            }
            rsd.Dispose();
        }
    }
}
