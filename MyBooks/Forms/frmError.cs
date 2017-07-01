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

        public static void showError(Exception ex, string sCaption) {
            frmError frm = new frmError();
            frm.Text = sCaption;
            frm.txError.Text = ex.Message;
            frm.txStack.Text = ex.StackTrace;
            frm.ShowDialog();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
