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
    public partial class frmError : Form
    {
        public frmError()
        {
            InitializeComponent();
        }

        public static void showError(Exception ex, string sCaption, string sBefore = "", string sAfter = "") {
            frmError frm = new frmError();
            frm.Text = sCaption;
            frm.txError.Text = ex.Message;
            string be = sBefore.Length > 0 ? (sBefore + "\r\n") : "";
            string af = sAfter.Length > 0 ? ("\r\n" + sAfter) : "";
            frm.txStack.Text = be + ex.StackTrace + af;
            frm.ShowDialog();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
