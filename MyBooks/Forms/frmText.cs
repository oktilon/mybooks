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
	public partial class frmText : Form
	{
		public string TText = "";
		//private Image mImage = null;
		public frmText(string sText, string sCap = "", bool bMultyLine = false, int iLimit = 0, Image Img = null)
		{
			InitializeComponent();
			TText = sText;
			if (sCap != "") Text = sCap;
			txtText.Multiline = bMultyLine;
			txtText.AcceptsReturn = bMultyLine;
			Height = bMultyLine ? 154 : 114;
			if (iLimit > 0) txtText.MaxLength = iLimit;
		}

		private void frmText_Load(object sender, EventArgs e)
		{
			txtText.Text = TText;
			txtText.SelectAll();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			TText = txtText.Text;
		}
	}
}
