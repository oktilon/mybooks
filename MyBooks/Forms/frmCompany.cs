using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsRc = MyBooks.Properties.Resources;
namespace MyBooks
{
	public partial class frmCompany : Form
	{
		Company Com = null;
		public frmCompany(Company cp = null)
		{
			InitializeComponent();
			Com = cp;
			Text = string.Format("{0} (Id:{1})", cp.Name, cp.Id);
		}

		private void frmCompany_Load(object sender, EventArgs e)
		{
			if (Com == null) { DialogResult = System.Windows.Forms.DialogResult.Cancel; Close(); return; }
			txtName.Text = Com.Name;
			txtShort.Text = Com.Short;
			txtAdr.Text = Com.Adr;
			txtMFO.Text = Com.MFO;
			txtEDRPOU.Text = Com.EDRPOU;
			txtAccNumber.Text = Com.AccountNum;
			txtCert.Text = Com.Certificate;
			cmdRole.Tag = Com.Role;
			cmdRole.Text = Com.RoleCaption;
            txtCoeff.Text = Com.Coeff.ToString("0.0000");
		}

		private void cmdRole_Click(object sender, EventArgs e)
		{
			ContextMenuStrip cms = new ContextMenuStrip();
            foreach (KeyValuePair<int, string> kvp in Company.mRoleNames)
            {
                ToolStripButton tsb = new ToolStripButton(kvp.Value, RsRc.bank16, menuRole_Click)
                {
                    Tag = kvp.Key,
                    Checked = (kvp.Key & Com.Role) > 0
                };
                cms.Items.Add(tsb);
            }
            cms.Show(cmdRole, 0, cmdRole.Height);
		}

		private void menuRole_Click(object sender, EventArgs e)
		{
			int iRole = (int)((ToolStripButton)sender).Tag;
            int cRole = (int)cmdRole.Tag;
            if ((cRole & iRole) > 0) cRole -= iRole;
            else cRole += iRole;
            cmdRole.Text = Company.GetRoleCaption(cRole);
			cmdRole.Tag = cRole;
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			Com.Name = txtName.Text;
			Com.Short=txtShort.Text ;
			Com.Adr = txtAdr.Text;
			Com.MFO = txtMFO.Text;
			Com.EDRPOU = txtEDRPOU.Text;
			Com.AccountNum = txtAccNumber.Text;
			Com.Certificate = txtCert.Text;
			Com.Role = (int)cmdRole.Tag;
            Com.Coeff = 1m;
            decimal.TryParse(txtCoeff.Text, out Com.Coeff);
		}
	}
}
