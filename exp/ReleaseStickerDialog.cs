using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exp
{
    public partial class ReleaseStickerDialog : Form
    {
        public string OMIC { private set; get; }
        public int StickersPrinted { private set; get; }
        public int StickersPartial { private set; get; }
        public ReleaseStickerDialog()
        {
            InitializeComponent();
        }

        private void tb_OMIC_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            OMIC = tb.Text;
        }

        private void nud_Printed_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            StickersPrinted = (int)nud.Value;
        }

        private void nud_Partial_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            StickersPartial = (int)nud.Value;
        }
    }
}
