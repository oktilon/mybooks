using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsRc = MyBooks.Properties.Resources;
using Sgc = SourceGrid.Cells;

namespace MyBooks
{
	public partial class frmBill : Form
	{
		public Decimal m_bill_total = 0m;
		public Bill m_bill = null;
		private Dictionary<int, Decimal> lst_NewPrice = new Dictionary<int, Decimal>();
		// Cells Views
		private List<Sgc.Views.RowHeader> m_viewRow = new List<Sgc.Views.RowHeader>();
		private List<Sgc.Views.Cell> m_viewsRight = new List<Sgc.Views.Cell>();
		private List<Sgc.Views.Cell> m_viewsCenter = new List<Sgc.Views.Cell>();
		private Sgc.Views.Cell m_viewCenter = new Sgc.Views.Cell();
		private Sgc.Views.Cell m_viewRight = new Sgc.Views.Cell();
		private Sgc.Editors.TextBoxCurrency m_edRoMoney = new Sgc.Editors.TextBoxCurrency(typeof(Decimal));
		private Sgc.Editors.TextBoxCurrency m_edPrice = new Sgc.Editors.TextBoxCurrency(typeof(Decimal));
		private Sgc.Editors.TextBoxNumeric m_edCnt = new Sgc.Editors.TextBoxNumeric(typeof(Decimal));
		private bool m_EditPriceMode = false;
		// Bill grid headers
		private string[] aHdrBill = new string[] { "Наименование", "Цена", "Кол.", "Ед", "Сумма", "Упр." };
		private int[] aWdBill = new int[] { 140, 40, 40, 30, 45, 18 };

		//
		private bool m_DblClickWait = false;
		private Properties.Settings gSettings = Properties.Settings.Default;

		// Declare the delegate (if using non-generic pattern).
		public delegate void BillEventHandler(object sender, BillEventArgs e);

		// Declare the event.
		public event BillEventHandler OnDone;
		public event BillEventHandler OnClose;

		public SourceGrid.CellContext cntxMenu = SourceGrid.CellContext.Empty;

		/// <summary>
		/// Edit PRICES mode
		/// </summary>
		public frmBill()
		{
			InitializeComponent();
			m_EditPriceMode = true;
			SplitV.Panel2Collapsed = true;
			Text = "Редактор цен - " + BK_Point.self.Name;
			frmBill_Init();
		}

		/// <summary>
		/// BILL mode (Edit or New)
		/// </summary>
		/// <param name="billInit">bill to edit</param>
		/// <param name="iNewCnt">>0 for new bill</param>
		public frmBill(Bill billInit, int iNewCnt = 0)
		{
			InitializeComponent();
			m_bill = billInit;
			Text = iNewCnt == 0 ? String.Format("Чек №{0} от {1:ddd dd MMM - HH:mm}", m_bill.Num, m_bill.Date) : String.Format("Новый чек {0} от {1:ddd dd MMM - HH:mm}", iNewCnt, m_bill.Date);
			frmBill_Init();
		}

		private void frmBill_Init()
		{
			// Редакторы
			m_edRoMoney.CultureInfo = Program.m_cif;
			m_edRoMoney.EditableMode = SourceGrid.EditableMode.None;
			m_edPrice.CultureInfo = Program.m_cif;
			m_edPrice.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.DoubleClick;
			m_edCnt.CultureInfo = Program.m_cif;
			m_edCnt.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.DoubleClick;
			m_edCnt.ConvertingValueToDisplayString += Cnt_ConvertingValueToDisplayString;
			// Виды
			m_viewRight.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
			m_viewCenter.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

			int iTabStop = 0;
			// EDIT MODE
			cmdAddItem.Visible = m_EditPriceMode;
			// BILL MODE
			cmdPrint.Visible = !m_EditPriceMode;
			chkReportViewer.Visible = !m_EditPriceMode;
			gridBill.Visible = !m_EditPriceMode;
			lblTot.Visible = !m_EditPriceMode;
			lblCash.Visible = !m_EditPriceMode;
			lblTotal.Visible = !m_EditPriceMode;
			textCash.Visible = !m_EditPriceMode;
			lblBack.Visible = !m_EditPriceMode;

			//
			foreach (Page pg in Page.getOrdered())
			{
				pg.SetGrid(Tabs.Size, iTabStop);
				iTabStop += 2;
				Tabs.TabPages.Add(pg.TabPage);
			}
		}

		public static void Cnt_ConvertingValueToDisplayString(object sender, DevAge.ComponentModel.ConvertingObjectEventArgs e)
		{
			e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Converting;
			decimal d = (decimal)e.Value;
			if (d == 0) e.Value = "-";
			else e.Value = d.ToString("0.#");
			e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Completed;
		}

		private void Money_ConvertingValueToDisplayString(object sender, DevAge.ComponentModel.ConvertingObjectEventArgs e)
		{
			double dbl = (double)e.Value;
			Sgc.Editors.TextBoxCurrency tbc = (Sgc.Editors.TextBoxCurrency)sender;
			if (dbl == 0)
			{
				e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Completed;
				e.Value = "-";
			}
		}

		private bool SetPriceRow(int iLine, BK_Item it, bool bNew = true)
		{
			Page page = it.Tab;
			BillItem bi = null;
			bool bFromList = true;
			if (m_bill != null) bi = m_bill.Items.FirstOrDefault(x => x.Item.Id == it.Id);
			if (bi == null)
			{
				// Skip unused and unnesessery items
				if (!it.InUse) return true;
				bi = new BillItem(it);
				bFromList = false;
			}

			if (bNew)
			{
				page.Grid.Rows.Insert(iLine);
				page.Grid.Rows[iLine].Tag = it;

                // 0 - Full Name
                Sgc.RowHeader rh = new Sgc.RowHeader(it.Name)
                {
                    View = m_viewRow[iLine % 2],
                    Image = it.Ico.Image
                };
                page.Grid[iLine, 0] = rh;
			}

			ItemPrice ip = null;
            List<ItemPrice> prc = it.getPointPrices();
            List<ItemRemain> rmn = it.getPointRemains();

			if (prc.Count == 1) ip = prc.First();
			if (prc.Count > 1)
			{
				if (bFromList)
					ip = prc.FirstOrDefault(x => x.Carrier == bi.Car);
				else
					ip = prc.FirstOrDefault(x => x.Unit == it.DefaultUnit && x.Carrier == it.DefaultCarrier);
				if (ip == null) ip = prc.First();
			}

            for (int iCol = 1; iCol < page.type.Cols; iCol++)
            {
                Object oVal = "-";
                Object oTag = null;
                Sgc.Editors.EditorBase eCell = null;
                Sgc.Views.Cell vCell = m_viewsCenter[iLine % 2];

                if (iCol == page.type.ixCar)
                {
                    oVal = ip == null ? BK_Carrier.NotCarrier : ip.Carrier;
                }
                if(iCol == page.type.ixDev)
                {
                    oVal = ip == null ? BK_Device.NullDevice : bi.Device;
                }
                if (iCol == page.type.ixPrc)
                {
                    if (ip != null)
                    {
                        oVal = ip.Prc;
                        oTag = ip;
                        eCell = m_EditPriceMode ? page.EdMoney : m_edRoMoney;
                        vCell = m_viewsRight[iLine % 2];
                    }
                }
                if (iCol == page.type.ixCnt)
                {
                    if (ip != null)
                    {
                        oVal = bi.Count;
                        oTag = bi;
                        eCell = m_EditPriceMode ? null : page.EdCnt;
                    }
                }
                if (iCol == page.type.ixUnit)
                {
                    if (ip != null)
                    {
                        oVal = ip.Unit;
                    }
                }
                if (iCol == page.type.ixRest)
                {
                    if (ip != null)
                    {
                        oVal = "-";
                    }
                }

                if (iCol == page.type.ixNote)
                {
                    if (ip != null)
                    {
                        oVal = "-";
                    }
                }


                page.Grid[iLine, iCol] = new Sgc.Cell(oVal)
                {
                    View = vCell
                };
                if (oTag != null) page.Grid[iLine, iCol].Tag = oTag;
                if (eCell != null) page.Grid[iLine, iCol].Editor = eCell;               
            }
            page.Grid[iLine, 0].Tag = prc;

            if (!bNew) page.Grid.InvalidateRange(new SourceGrid.Range(iLine, 0, iLine, page.Grid.RowsCount - 1));
			return false;
		}

		private void Grid_LoadPrice(Page page)
		{
			Int32 iLine = 1;
            IEnumerable<BK_Item> ieItems = BK_Item.getPageItems(page);
			foreach (BK_Item it in ieItems)
			{
				if (SetPriceRow(iLine, it)) continue;
				iLine++;
			}
		}

		private void AddColors(Color[] lstColor)
		{
			foreach (Color clr in lstColor)
			{
				// Rows headers
				Sgc.Views.RowHeader rh = new Sgc.Views.RowHeader();
                DevAge.Drawing.VisualElements.RowHeader da_rh = new DevAge.Drawing.VisualElements.RowHeader()
                {
                    BackColor = clr
                };
                rh.Background = da_rh;
				rh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
				m_viewRow.Add(rh);

                // View CENTER
                Sgc.Views.Cell vc = new Sgc.Views.Cell()
                {
                    BackColor = clr,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter
                };
                //vc.WordWrap = true;
                m_viewsCenter.Add(vc);

                // View RIGHT
                vc = new Sgc.Views.Cell()
                {
                    BackColor = clr,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight
                };
                m_viewsRight.Add(vc);
			}
		}

		private void frmBill_Load(object sender, EventArgs e)
		{
			BillEvent beItem = new BillEvent(this, false);
			BillEvent beBill = new BillEvent(this, true);
			bool bEdit = (m_bill != null && m_bill.Count > 0);

			RefreshReport(null); // Init report

			// ЦВЕТА ДЛЯ РЯДОВ
			AddColors(new Color[] { Color.LightCyan, Color.Honeydew });

			// Закладки
			foreach (Page p in Page.getAll())
			{
                p.setGridHeaders(false);
				Grid_LoadPrice(p);
				p.Grid.Controller.AddController(beItem);
			}

            gridBill.SetHeaders(aHdrBill, aWdBill, false);
			gridBill.Controller.AddController(beBill);
			if (bEdit)
			{
				textCash.Text = m_bill.Cash.ToString("C", Program.m_cif);
				RefreshBill();
			}
			else
			{
				cmdPrint.Enabled = false;
				chkReportViewer.Enabled = false;
			}
		}

		private void RefreshReport(PriceString prc)
		{
			bool bInit = prc == null || m_bill == null;

			List<Microsoft.Reporting.WinForms.ReportParameter> lrp = new List<Microsoft.Reporting.WinForms.ReportParameter>();
			if (bInit)
			{
				lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parProvider", Company.self.Name));
				lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parProviderID", string.Format("ЄДРПОУ {0} номер свідоцтва {1}", Company.self.EDRPOU, Company.self.Certificate)));
			}
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parProviderAddr", bInit ? "" : BK_Point.getPoint(m_bill.m_point).Adr));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parBillId", bInit ? "" : String.Format("РН-{0:000000}-{1}", m_bill.Id, m_bill.m_point)));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parDate", bInit ? DateTime.Now.ToShortDateString() : m_bill.Date.ToShortDateString()));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parTotalS", bInit ? "" : prc.InWords));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parRecipient", bInit ? "" : m_bill.Rcpt));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parPayer", bInit ? "" : m_bill.Payer));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parReason", bInit ? "" : m_bill.Reason));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parCondition", bInit ? "" : m_bill.Cond));
			lrp.Add(new Microsoft.Reporting.WinForms.ReportParameter("parUser", bInit ? "" : m_bill.User.FIO));
			rptView.LocalReport.SetParameters(lrp);
		}

		public void Cell_OnEditEnded(SourceGrid.CellContext cntx, bool isBill)
		{
			int iR = cntx.Position.Row;
			if (isBill)
			{
				BillItem bi = (BillItem)gridBill[iR, 0].Tag;
				m_bill_total -= bi.Summ;
				switch (cntx.Position.Column)
				{
					case 1: // PRICE
						bi.Price = (decimal)cntx.Value;
						break;
					case 2: // COUNT
						bi.Count = (decimal)cntx.Value;
						SourceGrid.Grid grid = bi.Item.Tab.Grid;
						for (Int32 i = 1; i < grid.RowsCount; i++)
							if (grid.Rows[i].Tag == bi.Item)
							{
								grid[i, 4].Value = bi.Count;
								break;
							}
						break;
					case 4: // SUMM
						bi.Price = (decimal)cntx.Value / bi.Count;
						gridBill[iR, 1].Value = bi.Price;
						break;
				}
				m_bill_total += bi.Summ;
				gridBill[iR, 4].Value = bi.Summ;
				dsMain.tblBill.Rows[iR - 1][4] = bi.Price;
				dsMain.tblBill.Rows[iR - 1][5] = bi.Summ;
				ShowTotal();
			}
			else
			{
				SourceGrid.Grid gr = (SourceGrid.Grid)cntx.Grid;
                Page page = Page.getPage(gr.Tag.ToString());
                ItemPrice ip = (ItemPrice)gr[iR, page.type.ixPrc].Tag;
				decimal dVal = (decimal)cntx.Value;

				if (m_bill != null) // Usual mode
				{
					BillItem bi = (BillItem)gr[iR, page.type.ixCnt].Tag;
					if (bi == null || ip == null) return;
					bi.Count = dVal;
					bi.Unit = ip.Unit;
					bi.Car = ip.Carrier;

					if (m_bill.ContainsItemLike(bi))
					{
						if (dVal == 0m) m_bill.RemoveItemLike(bi);
					}
					else
					{
						if (dVal != 0m)
						{
							bi.Price = ip.Prc;
							m_bill.Add(bi);
						}
					}
					RefreshBill();
					textCash.Focus();
				}
				else // Edit price mode
				{
					if (ip.Prc != dVal)
					{
						ip.HasChanged = true;
						ip.Prc = dVal;
					}
				}
			}
		}

		public void Cell_OnClick(SourceGrid.CellContext cntx, bool bIsBill)
		{
			if (cntx.Position.Row == 0) return;
			if (bIsBill)
				CellBill_OnClick(cntx);
			else
				CellPrice_OnClick(cntx);
		}

		public void CellBill_OnClick(SourceGrid.CellContext cntx)
		{
			if (cntx.Position.Column != 0) return;
			if (m_EditPriceMode) return;
			BillItem bi = (BillItem)gridBill[cntx.Position.Row, 0].Tag;
			Tabs.SelectTab(bi.Item.Tab.TabPage);
			SourceGrid.Grid grid = bi.Item.Tab.Grid;

			for (Int32 iR = 1; iR < grid.RowsCount; iR++)
			{
				if (grid.Rows[iR].Tag == bi.Item)
				{
					SourceGrid.Position pos = new SourceGrid.Position(iR, 4); // COUNT CELL
					BillItem bic = (BillItem)grid[pos].Tag;
					if (bic != bi)
					{
						List<ItemPrice> prc = (List<ItemPrice>)grid[iR, 0].Tag;
						ItemPrice ip = prc.FirstOrDefault(x => x.Carrier == bi.Car && x.Unit == bi.Unit);
						if (ip == null) return;
						SwitchItemPrice(iR, ip);
					}
					grid.ShowCell(pos, true);
					SourceGrid.CellContext cc = new SourceGrid.CellContext(grid, pos, grid[pos]);
					if (!cc.IsEmpty()) cc.StartEdit();
					break;
				}
			}
		}

		public void CellPrice_OnClick(SourceGrid.CellContext cntx)
		{
			SourceGrid.Grid grid = (SourceGrid.Grid)cntx.Grid;
            Page page = Page.getPage(grid.Tag.ToString());
            List<ItemPrice> prc = (List<ItemPrice>)grid[cntx.Position.Row, 0].Tag;
			ContextMenuStrip cms = new ContextMenuStrip();
            if (cntx.Position.Column == 0)
            {
                // 0-Name (4-Cnt: AutoEnter EditMode)
                if (m_EditPriceMode) return;
                SourceGrid.CellContext cc = new SourceGrid.CellContext(cntx.Grid, new SourceGrid.Position(cntx.Position.Row, page.type.ixCnt));
                if (!cc.IsEmpty())
                {
                    if (cc.IsEditing())
                        cc.EndEdit(false);
                    else
                        cc.StartEdit();
                }
            } else {
                if (cntx.Position.Column == page.type.ixCar)
                {  // Carrier
                    BK_Carrier car = (BK_Carrier)cntx.Value;
                    IEnumerable<ItemPrice> ccx = from x in prc where x.Carrier != car select x;
                    cntxMenu = cntx;
                    foreach (ItemPrice c in ccx)
                        cms.Items.Add(c.Carrier.Caption, RsRc.documents16, MenuItem_Carrier_Click).Tag = c;
                }
                if (cntx.Position.Column == page.type.ixDev)
                {  // Device
                    BK_Device dev = (BK_Device)cntx.Value;
                    cntxMenu = cntx;
                    foreach (BK_Device d in BK_Device.getAll())
                    {
                        Bitmap obj = (Bitmap)RsRc.ResourceManager.GetObject("Ink16", RsRc.Culture);
                        cms.Items.Add(d.Short, obj, MenuItem_Device_Click).Tag = d;
                    }
                }
                if (cms.Items.Count == 0) return;
                cms.Show(MousePosition);
            }
        }

		public void MenuItem_Carrier_Click(object sender, EventArgs e)
		{
			ItemPrice ip = (ItemPrice)((ToolStripMenuItem)sender).Tag;
			if (ip == null || cntxMenu.IsEmpty()) return;
			int iR = cntxMenu.Position.Row;
			SwitchItemPrice(iR, ip);
		}

        public void MenuItem_Device_Click(object sender, EventArgs e)
        {
            BK_Device dev = (BK_Device)((ToolStripMenuItem)sender).Tag;
            if (dev == null || cntxMenu.IsEmpty()) return;
            SourceGrid.Grid grid = (SourceGrid.Grid)cntxMenu.Grid;
            Page page = Page.getPage(grid.Tag.ToString());
            int iR = cntxMenu.Position.Row;
            BillItem bi = (BillItem)grid[iR, page.type.ixCnt].Tag;
            bi.Device = dev;
            if (page.type.ixDev > 0)
            {
                grid[iR, page.type.ixDev].Value = dev;
            }
        }

        public void SwitchItemPrice(int iItemRow, ItemPrice ip)
		{
			BK_Item it = BK_Item.getItem(ip.ItemId);
			SourceGrid.Grid grid = it.Tab.Grid;
			grid[iItemRow, it.Tab.type.ixCar].Value = ip.Carrier;
            if (it.Tab.type.ixPrc > 0)
            {
                grid[iItemRow, it.Tab.type.ixPrc].Value = ip.Prc;
                grid[iItemRow, it.Tab.type.ixPrc].Tag = ip;
            }
            if (it.Tab.type.ixUnit > 0)
            {
                grid[iItemRow, it.Tab.type.ixUnit].Value = ip.Unit;
            }

			if (!m_EditPriceMode)
			{
                BillItem bi = new BillItem(BK_Item.getItem(ip.ItemId))
                {
                    Car = ip.Carrier,
                    Unit = ip.Unit
                };
                if (m_bill.ContainsItemLike(bi)) bi = m_bill.GetItemLike(bi);
				grid[iItemRow, it.Tab.type.ixCnt].Tag = bi;
				grid[iItemRow, it.Tab.type.ixCnt].Value = bi.Count;
                if (it.Tab.type.ixDev > 0)
                {
                    grid[iItemRow, it.Tab.type.ixDev].Value = bi.Device;
                }
            }
        }

		public void CellPrice_OnDblClick(SourceGrid.CellContext cntx)
		{
			if (!m_EditPriceMode) return;
			SourceGrid.CellContext cc = new SourceGrid.CellContext(cntx.Grid, new SourceGrid.Position(cntx.Position.Row, 4));
			if (!cc.IsEmpty())
			{
				if (cc.IsEditing())
					cc.EndEdit(false);
				else
					cc.StartEdit();
			}
		}

		public void Cell_OnDblClick(SourceGrid.CellContext cntx, bool isBill)
		{
			if (m_DblClickWait) return;
			if (cntx.Position.Row == 0) return;
			if (m_EditPriceMode && cntx.Position.Column == 0)
			{
				m_DblClickWait = true;
                BK_Item it = (BK_Item)((SourceGrid.Grid)cntx.Grid).Rows[cntx.Position.Row].Tag;
				//BillItem bi = (BillItem)((SourceGrid.Grid)cntx.Grid)[cntx.Position.Row, it.Tab.type.ixCnt].Tag;
				frmPrice f = new frmPrice(it);
				DialogResult dr = f.ShowDialog();
				tmrDblClk.Start();
				if (dr == DialogResult.Cancel) return;
				SetPriceRow(cntx.Position.Row, it, false);
				return;
			}
			String sTag = cntx.Grid.Tag.ToString();
			if (sTag != "bill") { CellPrice_OnDblClick(cntx); }
		}


		public void CellPrice_OnKeyDown(SourceGrid.CellContext ctx, KeyEventArgs e)
		{
			if (!m_EditPriceMode) return;
            BK_Item it = (BK_Item)((SourceGrid.Grid)ctx.Grid).Rows[ctx.Position.Row].Tag;
            Int32 iCol = ctx.Position.Column;
            if (iCol < 1) return;
			if (e.KeyValue == 13 && iCol == it.Tab.type.ixRest) // REST
			{
				e.Handled = true;
				if (ctx.IsEditing()) ctx.EndEdit(false);
				if (ctx.Position.Row + 1 >= ctx.Grid.Rows.Count) return;
				ctx.Grid.Selection.MoveActiveCell(1, 0);
				return;
			}
			if (e.KeyValue == 13 && iCol == it.Tab.type.ixPrc) // PRICE
			{
				e.Handled = true;
				if (ctx.IsEditing()) ctx.EndEdit(false);
				if (ctx.Position.Row + 1 >= ctx.Grid.Rows.Count) return;
				ctx.Grid.Selection.MoveActiveCell(1, 0);
				return;
			}
		}

		private void RefreshBill()
		{
			decimal dSum = 0m;
			Int32 iLine = 1;
			DataRow dr;
			gridBill.Redim(1, aHdrBill.Count());
			dsMain.tblBill.Clear();
			foreach (BillItem bi in m_bill.AllItems)
			{
				dr = dsMain.tblBill.NewRow();
				dr[0] = iLine;
				dr[1] = bi.Item.NameUa + bi.Car.CaptionUa;
				dr[2] = bi.Unit.ToString();
				dr[3] = bi.Count;
				dr[4] = bi.Price;
				dr[5] = bi.Summ;
				dsMain.tblBill.Rows.Add(dr);
				gridBill.Rows.Insert(iLine);
                gridBill[iLine, 0] = new Sgc.RowHeader(bi.Item.Short + bi.Car.Short)
                {
                    Tag = bi
                };
                gridBill[iLine, 1] = new Sgc.Cell(bi.Price, m_edPrice)
                {
                    View = m_viewRight
                };
                gridBill[iLine, 2] = new Sgc.Cell(bi.Count, m_edCnt)
                {
                    View = m_viewCenter
                };
                gridBill[iLine, 3] = new Sgc.Cell(bi.Unit)
                {
                    View = m_viewCenter
                };
                gridBill[iLine, 4] = new Sgc.Cell(bi.Summ, m_edPrice)
                {
                    View = m_viewRight
                };
                gridBill[iLine, 5] = new Sgc.Button("")
                {
                    Image = RsRc.Exit16
                };
                Sgc.Controllers.Button buttonClickEvent = new Sgc.Controllers.Button();
				buttonClickEvent.Executed += new EventHandler(cmdBillDeleteBtn_Click);
				gridBill[iLine, 5].Controller.AddController(buttonClickEvent);
				dSum += bi.Summ;
				iLine++;
			}
			//Report data
			RefreshReport(new PriceString(dSum));
			m_bill_total = dSum;
			ShowTotal();
		}

		private void ShowTotal()
		{
			lblTotal.Text = String.Format(Program.m_cif, "{0:C}", m_bill_total);
			if (m_bill.Cash <= 0)
			{
				lblBack.Text = "*";
				EnableExit(true);
			}
			else
			{
				lblBack.Text = String.Format(Program.m_cif, "{0:C}", m_bill.Cash - m_bill_total);
				EnableExit(m_bill.Cash >= m_bill_total);
			}
			rptView.RefreshReport();
			lblTotal.Refresh();
			lblBack.Refresh();
			if (!cmdPrint.Enabled && m_bill.Count > 0)
			{
				cmdPrint.Enabled = true;
				chkReportViewer.Enabled = true;
			}
		}

		private void cmdBillDeleteBtn_Click(object sender, EventArgs e)
		{
			SourceGrid.CellContext cc = SourceGrid.CellContext.Empty;
			SourceGrid.CellContext cell = (SourceGrid.CellContext)sender;
			BillItem bi = (BillItem)gridBill[cell.Position.Row, 0].Tag;
			/*cc = CellBill_OnClick(bi.Item.Id, "", m_Pages[bi.Page].Grid);

            if (!cc.IsEmpty())
            {
                cc.Value = 0;
				m_bill.Remove(bi.Item.Id);
                RefreshBill();
            }
            if (m_bill.Count < 1)
            {
                cmdPrint.Enabled = false;
                chkReportViewer.Enabled = false;
            }*/
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			if (OnDone != null && m_bill != null)
				OnDone(this, new BillEventArgs(m_bill));
			if (m_bill == null)
			{
                BK_Item.updateChangedItems(false);
			}
			this.Close();
		}

		private void textCash_TextChanged(object sender, EventArgs e)
		{
			Decimal dCash = 0m;
			if (!Decimal.TryParse(textCash.Text, out dCash)) dCash = 0m;
			m_bill.Cash = dCash;
			if (m_bill.Cash <= 0)
			{
				lblBack.Text = "*";
				EnableExit(true);
			}
			else
			{
				lblBack.Text = String.Format(Program.m_cif, "{0:C}", m_bill.Cash - m_bill_total);
				EnableExit(m_bill.Cash >= m_bill_total);
			}
			lblBack.Refresh();
		}

		private void textCash_Enter(object sender, EventArgs e) { textCash.SelectAll(); }

		private void textCash_MouseHover(object sender, EventArgs e) { textCash.SelectAll(); textCash.Focus(); }

		private void EnableExit(Boolean bEnable)
		{
			if (cmdSave.Enabled && !bEnable) cmdSave.Enabled = false;
			if (!cmdSave.Enabled && bEnable) cmdSave.Enabled = true;
		}

		private void chkReportViewer_CheckedChanged(object sender, EventArgs e)
		{
			rptView.Visible = chkReportViewer.Checked;
			cmdEditCustomer.Visible = chkReportViewer.Checked;
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			rptView.PrintDialog();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			if (m_bill == null) BK_Item.updateChangedItems(true);
			Close();
		}

		private void frmBill_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (OnClose != null && m_bill != null)
				OnClose(this, new BillEventArgs(m_bill));
		}

		private void cmdAddItem_Click(object sender, EventArgs e)
		{
			cntxtCmdAdd.Show(cmdAddItem, 0, cmdAddItem.Height);
		}

		private void cmdEditCustomer_Click(object sender, EventArgs e)
		{
			// frmBillInfo
		}

		private void tmrDblClk_Tick(object sender, EventArgs e)
		{
			tmrDblClk.Stop();
			m_DblClickWait = false;
		}

		private void frmBill_FormClosed(object sender, FormClosedEventArgs e)
		{
			Page.ResetPages();
		}

		private void mnuAddCarrier_Click(object sender, EventArgs e) { AddNewItem(1); }
		private void mnuAddPrint_Click(object sender, EventArgs e) { AddNewItem(2); }
		private void mnuAddItem_Click(object sender, EventArgs e) { AddNewItem(0); }
		private void mnuAddTab_Click(object sender, EventArgs e) { AddNewPage(); }

		/// <summary>
		/// Add 0-Item, 1-Paper, 2-Print
		/// </summary>
		private void AddNewItem(int iWhat)
		{
            Page pSel = Page.getPage(Tabs.SelectedTab);
			if (pSel == null) return;
			BK_Item bi = new BK_Item(pSel, iWhat);
			frmPrice frm = new frmPrice(bi);
			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
			SetPriceRow(pSel.Grid.RowsCount, bi, true);
		}

		private void AddNewPage()
		{
			// NEW TAB PAGE
		}
	}
}

