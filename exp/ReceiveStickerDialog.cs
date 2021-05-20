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
    public partial class ReceiveStickerDialog : Form
    {
        OMIClist list;
        public ReceiveStickerDialog(OMIClist oml)
        {
            InitializeComponent();
            list = oml;
            filterOutstandingLoads(oml);
            comboBox1.Refresh();
        }
        OMIClist filterOutstandingLoads(OMIClist oms)
        {
            OMIClist ol =  oms.Filter((r) => { return r.LoadStatus == 'N'; });
            comboBox2.Items.AddRange(ol.Select(r => r.OMICid).ToArray());
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            return ol;
        }
    }
}
