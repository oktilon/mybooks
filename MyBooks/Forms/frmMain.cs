using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sgc = SourceGrid.Cells;

namespace MyBooks
{
    public partial class frmMain : Form
    {
        private Int32 iNewCnt = 1;
        // Cells Views 
        private List<sgc.Views.RowHeader> m_viewRow = new List<sgc.Views.RowHeader>();
        private List<sgc.Views.Cell> m_viewRight = new List<sgc.Views.Cell>();
        private List<sgc.Views.Cell> m_viewCenter = new List<sgc.Views.Cell>();
        private sgc.Editors.TimePicker edTime;
        private sgc.Editors.TextBoxCurrency edMoney;
        private List<int> lstShowedBills = new List<int>();
        private static string[] lstHeaders = new string[] { "№", "Время", "Сумма" };
		private static int[] lstWidth = new int[] { 30, 75, -1 };

        internal Properties.Settings gSettings = MyBooks.Properties.Settings.Default;

        public frmMain()
        {
            InitializeComponent();
            Text = BK_Point.self.Name + " - " + User.Me.Name;
        }

        private void AddColors(List<Color> lstColor)
        {
            foreach (Color clr in lstColor)
            {
                // Rows headers
                DevAge.Drawing.VisualElements.RowHeader da_rh = new DevAge.Drawing.VisualElements.RowHeader()
                {
                    BackColor = clr
                };
                sgc.Views.RowHeader rh = new sgc.Views.RowHeader()
                {
                    Background = da_rh,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter
                };
                m_viewRow.Add(rh);

                // View CENTER
                sgc.Views.Cell vc = new sgc.Views.Cell()
                {
                    BackColor = clr,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter
                };
                //vc.WordWrap = true;
                m_viewCenter.Add(vc);

                // View RIGHT
                vc = new sgc.Views.Cell()
                {
                    BackColor = clr,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight
                };
                m_viewRight.Add(vc);
            }
        }

		private void btnPrice_Click(object sender, EventArgs e)
		{
			frmBill f = new frmBill();
			f.Show();
		}

        private void btnNew_Click(object sender, EventArgs e)
        {
			Bill b = new Bill(dtCurrent.Value.Date + DateTime.Now.TimeOfDay);
			frmBill f = new frmBill(b, iNewCnt++);
			f.OnDone += OnBill_Done;
			f.OnClose += OnBill_Close;
            f.Show();
        }

		public void gridLst_OnDblClick(int iR)
		{
			int lId = (int)gridLst[iR, 0].Tag;
			if (lstShowedBills.Contains(lId)) return;
			lstShowedBills.Add(lId);
			Bill b = new Bill(lId);
			frmBill f = new frmBill(b);
			f.OnDone += OnBill_Done;
			f.OnClose += OnBill_Close;
			f.Show();
		}

        public void OnBill_Done(object sender, BillEventArgs bea)
        {
			if (bea.Bill.Id != 0 && lstShowedBills.Contains(bea.Bill.Id))
				lstShowedBills.Remove(bea.Bill.Id);
			if (bea.Bill.Count > 0)
            {
				bea.Bill.Save();
                UpdateCurrentBillList();
            }
        }

		public void OnBill_Close(object sender, BillEventArgs bea)
        {
			if (bea.Bill.Id != 0 && lstShowedBills.Contains(bea.Bill.Id))
				lstShowedBills.Remove(bea.Bill.Id);
        }


        public void gridLst_OnClick(int iR)
        {
            gridLst.Selection.Focus(new SourceGrid.Position(iR, 1), true);
        }

        public void OnHotKeyPress(Shortcut key)
        {
            if (!this.Enabled) return;
            if (key == Shortcut.F12) btnNew_Click(null, new EventArgs());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnPrice.Visible = User.Me.IsAdmin;
            dtCurrent.Enabled = User.Me.IsAdmin;

            // ЦВЕТА ДЛЯ РЯДОВ
            AddColors(new List<Color>() { Color.PapayaWhip, Color.Bisque });

            // Редакторы
            edMoney = new sgc.Editors.TextBoxCurrency(typeof(Decimal))
            {
                CultureInfo = Program.m_cif,
                EditableMode = SourceGrid.EditableMode.None
            };
            edTime = new sgc.Editors.TimePicker("HH:mm:ss", new string[] { })
            {
                EditableMode = SourceGrid.EditableMode.None
            };
            gridLst.AutoStretchColumnsToFitWidth = true;
            gridLst.SetHeaders(lstHeaders, lstWidth, true);
            UpdateCurrentBillList();
            sgc.ColumnHeader ch = (sgc.ColumnHeader)gridLst[0, 1];
            ch.SortStyle = DevAge.Drawing.HeaderSortStyle.Descending;
            ListEvent le = new ListEvent(this);
            gridLst.Controller.AddController(le);
        }

        private void UpdateCurrentBillList()
        {
            decimal dSum = 0m;
            Int32 iLine = 1, iView = 0;
            gridLst.Redim(1, 3);
            denSQL.denReader rd = denSQL.Query("SELECT bill_id, bill_num, bill_date, (SELECT SUM(prc*cnt) FROM bk_list WHERE bill=bill_id) FROM bk_bill WHERE bill_date LIKE '{0:yyyy-MM-dd}%' ORDER BY bill_date desc", dtCurrent.Value);
            while (rd.Read())
            {
                gridLst.Rows.Insert(iLine);
                gridLst[iLine, 0] = new sgc.RowHeader(rd.GetInt(1))
                {
                    Tag = rd.GetInt(0),
                    View = m_viewRow[iView]
                };
                gridLst[iLine, 1] = new sgc.Cell(rd.GetDateTime(2))
                {
                    View = m_viewRight[iView],
                    Editor = edTime
                };
                decimal dPrc = rd.GetDecimal(3);
                gridLst[iLine, 2] = new sgc.Cell(dPrc)
                {
                    View = m_viewRight[iView],
                    Editor = edMoney
                };
                dSum += dPrc;
                iLine++;
                iView++;
                if (iView > m_viewRight.Count - 1) iView = 0;
            }
            rd.Close();
            gridLst.AutoSizeCells();
            String sPeriod = String.Format(Program.m_cif, "{0:ddd dd} {1}", dtCurrent.Value, DenMonth.GenitiveCase(dtCurrent.Value.Month));
            if (dtCurrent.Value.Date == DateTime.Now.Date) sPeriod = "сегодня";
            if (dtCurrent.Value.Date == DateTime.Now.AddDays(-1).Date) sPeriod = "вчера";
            if (dtCurrent.Value.Date == DateTime.Now.AddDays(-2).Date) sPeriod = "позавчера";
            stStripLabel.Text = String.Format(Program.m_cif, "За {0} чеков {1}шт. на сумму {2:C} грн.", sPeriod, iLine - 1, dSum);
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {
                this.Hide();
                return;
            }
            if (!this.Visible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Refresh();
                this.Activate();
            }
        }

        private void icoNotify_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.Enabled) return;
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visible)
                {
                    this.Hide();
                    return;
                }
                frmMain_SizeChanged(null, new EventArgs());
            }
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReports frmr = new frmReports();
            frmr.Init(dtCurrent.Value);
            frmr.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtCurrent_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurrentBillList();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            if (User.Me.IsAdmin)
            {
                frmMyOrders f = new frmMyOrders();
                f.ShowDialog();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            int iBadCnt = 0;
            //denSQL.denReader r = denSQL.Query("SELECT * FROM");
            //r.Close();
            e.Cancel = iBadCnt > 0;
        }
    }
}
