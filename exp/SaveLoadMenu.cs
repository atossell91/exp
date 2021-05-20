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
    public partial class SaveLoadMenu : Form
    {
        OMIClist list;
        public SaveLoadMenu(OMIClist oList)
        {
            InitializeComponent();
            list = oList;
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            StorageHandler sh = new StorageHandler();
            sh.Save("text.xml", list);
        }

        private void B_Load_Click(object sender, EventArgs e)
        {
            StorageHandler sh = new StorageHandler();
            sh.Load("text.xml");
        }
    }
}
