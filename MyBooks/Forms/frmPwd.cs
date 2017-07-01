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
    public partial class frmPwd : Form
    {
        private int wrongCount = 0;

        public frmPwd()
        {
            InitializeComponent();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            User u = (User)cbU.SelectedItem;
            if (u.Id == 0) return;
            String p = txtP.Text;
            if (p == "")
            {
                txtP.Focus();
                return;
            }
            if (!u.hasPassword)
            {
                if (p.Length < 5)
                {
                    MessageBox.Show(T.Tx("warn_pwd_len"), T.Tx("new_user"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtP.Focus();
                    return;
                }
                u.setPassword(p);
                User.Me = u;
                DialogResult = DialogResult.OK;
                Program.RegSetString("LastLogin", u.Id.ToString());
                return;
            }
            if (u.checkPassword(p))
            {
                User.Me = u;
                DialogResult = DialogResult.OK;
                Program.RegSetString("LastLogin", u.Id.ToString());
            }
            else
            {
                if (wrongCount > 1)
                {
                    DialogResult = DialogResult.No;
                }
                else
                {
                    wrongCount++;
                    txtP.BackColor = Color.LightPink;
                }

            }
        }

        private void txtP_Enter(object sender, EventArgs e) { txtP.SelectAll(); }

        private void frmPwd_Load(object sender, EventArgs e)
        {
            string sLastUserId = Program.RegGetString("LastLogin");
            int iLastId = 0;
            if (sLastUserId != "") int.TryParse(sLastUserId, out iLastId);
            cbU.Items.Clear();
            User uSel = new User(T.Tx("sel_name"));
            if (iLastId == 0) cbU.Items.Add(uSel);
            foreach(User u in User.getOrdered())
            {
                if (u.Id == 0) continue;
                cbU.Items.Add(u);
                if (iLastId == u.Id) uSel = u;
            }
            if (uSel.Id == 0 && iLastId != 0) cbU.Items.Add(uSel);
            cbU.SelectedItem = uSel;
            if (uSel.Id == 0)
                cbU.Focus();
            else
                txtP.Focus();
        }

        private void cbU_SelectedIndexChanged(object sender, EventArgs e)
        {
            User u = (User)cbU.SelectedItem;
            Text = u.hasPassword ? T.Tx("access") : T.Tx("new_pwd");
        }

        private void txtP_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter && txtP.BackColor != SystemColors.Window) txtP.BackColor = SystemColors.Window;
        }
    }
}
