using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    public partial class frmEncrypt : Form
    {
        public frmEncrypt(string sInit = "")
        {
            InitializeComponent();
            txSrc.Text = sInit;
            Do();
        }

        private void Do(){ txDst.Text = Program.Encrypt(txSrc.Text); }
        private void txSrc_TextChanged(object sender, EventArgs e) { Do(); }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txDst.Text);
        }
    }
}
