using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using Sgc = SourceGrid.Cells;
using RsRc = MyBooks.Properties.Resources;

namespace MyBooks
{
	public partial class frmPrice : Form
	{
		BK_Item m_Item;
		// Owner draws
		private LinearGradientBrush lgbrSelect;
		private Pen penSel;
		// Cells
		Sgc.Views.RowHeader rh, rhUnit;
		Sgc.Views.Cell vRight;
		Sgc.Views.Cell vLeft;
		Sgc.Editors.TextBoxCurrency eMoney;
		Sgc.Editors.TextBoxCurrency eMoneyRO;
        Sgc.Editors.TextBoxNumeric eDecimal;
        Sgc.Editors.TextBoxNumeric eCons;
		Sgc.Editors.TextBox eUnit;
		string[] hdrStrService = new string[] { "Точка", "Бумага", "Цена", "Расх.", "Ед." };
		string[] hdrStrItem = new string[] { "Точка", "Остаток", "Цена", "Ед." };
		int[] hdrInt = new int[] { 0, 0, 0, 0, 0 };
		bool bGridUnits_Click = false;


		public frmPrice(BK_Item bi = null)
		{
			InitializeComponent();
			m_Item = bi;
			cbIcon.Items.AddRange(BK_Icon.getAll().ToArray());
			imgl.Images.AddRange((from x in BK_Icon.getAll() orderby x.Id select x.Image).ToArray());
			Color clrSelect = Color.AliceBlue;
			lgbrSelect = new LinearGradientBrush(new Rectangle(0, 0, 18, 18), Color.FromArgb(255, clrSelect), Color.FromArgb(200, clrSelect), LinearGradientMode.Vertical);
			penSel = new Pen(Brushes.CornflowerBlue, 1);
			// GRID
			rh = new Sgc.Views.RowHeader();
            DevAge.Drawing.VisualElements.RowHeader rhR = new DevAge.Drawing.VisualElements.RowHeader()
            {
                BackColor = Color.Lavender
            };
            rh.Background = rhR;
			rh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
			rhUnit = new Sgc.Views.RowHeader();
            rhR = new DevAge.Drawing.VisualElements.RowHeader()
            {
                BackColor = Color.Lavender
            };
            rhUnit.Background = rhR;
			rhUnit.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            gridCat.SetHeaders(new string[] { "Код", "Поставщик", "Цена", "Ед." }, new int[] { 0, 0, 0, 0 }, false);
            gridUnits.SetHeaders(new string[] { "Доп.ед.", "Кол.", "Баз.ед." }, new int[] { 0, 0, 0 }, false);
			frmPriceGridEvents ctrlEventsPrc = new frmPriceGridEvents(this, "price");
			grid.Controller.AddController(ctrlEventsPrc);
            frmPriceGridEvents ctrlEventsUnit = new frmPriceGridEvents(this, "unit");
            gridUnits.Controller.AddController(ctrlEventsUnit);
		}

		private void frmPrice_Load(object sender, EventArgs e)
		{
			if (m_Item == null) { DialogResult = System.Windows.Forms.DialogResult.Cancel; Close(); return; }

            vRight = new Sgc.Views.Cell()
            {
                TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight
            };
            vLeft = new Sgc.Views.Cell()
            {
                TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft
            };
            Text += string.Format(" (Id:{0})", m_Item.Id);

            eMoney = new Sgc.Editors.TextBoxCurrency(typeof(decimal))
            {
                CultureInfo = Program.m_cif
            };
            eMoneyRO = new Sgc.Editors.TextBoxCurrency(typeof(decimal))
            {
                CultureInfo = Program.m_cif,
                EditableMode = SourceGrid.EditableMode.None
            };
            eDecimal = new Sgc.Editors.TextBoxNumeric(typeof(decimal))
            {
                CultureInfo = Program.m_cif
            };
            eCons = new Sgc.Editors.TextBoxNumeric(typeof(int));
            //eCons.CultureInfo = Program.m_cif;
            eUnit = new Sgc.Editors.TextBox(typeof(Unit))
            {
                EditableMode = SourceGrid.EditableMode.None
            };
            eUnit.ConvertingValueToDisplayString += eUnit_ConvertingValueToDisplayString;
			txtName.Text = m_Item.Name;
			txtShort.Text = m_Item.Short;
			txtNameUa.Text = m_Item.NameUa;
			txtArt.Text = m_Item.Articul;
			chkUse.Checked = m_Item.InUse;
			//
			lblType.Text = m_Item.TypeCaption;
			lblFmt.Enabled = m_Item.IsService || m_Item.IsCarrier;
			cmdFmt.Enabled = m_Item.IsService || m_Item.IsCarrier;
			if (m_Item.IsCarrier)
			{
				lblDen.Visible = true;
				cmdDen.Visible = true;
				lblSurf.Visible = m_Item.IsCarrier;
				cmdSurf.Visible = m_Item.IsCarrier;
				m_Item.Carrier.Density.SetButton(cmdDen);
				m_Item.Carrier.Surface.SetButton(cmdSurf);
				m_Item.Carrier.Format.SetButton(cmdFmt);
			}
			else
				m_Item.ServiceFormat.SetButton(cmdFmt);

            lblDevice.Visible = m_Item.IsService;
            cmdDevice.Visible = m_Item.IsService;
            m_Item.Device.SetButton(cmdDevice);

            cmdMin.Enabled = !m_Item.IsService;
            cmdBase.Enabled = !m_Item.IsService;
            gridUnits.Visible = !m_Item.IsService;

            lblVen.Visible = !m_Item.IsService;
            cmdVen.Visible = !m_Item.IsService;
            m_Item.Manufacturer.SetButton(cmdVen);
			//
			m_Item.MinUnit.SetButton(cmdMin);
			m_Item.BaseUnit.SetButton(cmdBase);
			BK_Icon bki = m_Item.Ico;
			if (!cbIcon.Items.Contains(bki)) cbIcon.Items.Add(bki);
			cbIcon.SelectedItem = bki;
			if (m_Item.MinUnit != m_Item.BaseUnit) ItemUnit.AddDefaultBaseToMin(m_Item);
			FillPrices();
			FillCatalogs();
			FillUnits();
		}

		void eUnit_ConvertingValueToDisplayString(object sender, DevAge.ComponentModel.ConvertingObjectEventArgs e)
		{
			e.Value = string.Format("1 {0} =", e.Value);
			e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Completed;
		}

		private void FillPrices()
		{
            grid.SetHeaders(m_Item.IsService ? hdrStrService : hdrStrItem, hdrInt, false);
			int iRow = 1;
			foreach (ItemPrice ip in m_Item.Prices)
			{
                if (ip.bDeleted) continue;
				grid.Rows.Insert(iRow);
				grid.Rows[iRow].Tag = ip;

                grid[iRow, 0] = new Sgc.RowHeader(ip.Pnt)
                {
                    View = rh
                };
                if (m_Item.IsService)
                {
                    BK_Item itCar = BK_Item.getItem(ip.Carrier.ItemId);
                    grid[iRow, 1] = new Sgc.Cell(ip.Carrier); // CARRIER
                    grid[iRow, 3] = new Sgc.Cell(ip.Consumption, eCons);
                    grid[iRow, 4] = new Sgc.Cell(itCar.MinUnit);
                }
                else
                {
                    grid[iRow, 1] = new Sgc.Cell(ip.CalculateRemain())
                    {
                        View = vRight
                    };
                    grid[iRow, 3] = new Sgc.Cell(ip.Unit);
                }
				grid[iRow, 1].View = vRight;

                grid[iRow, 2] = new Sgc.Cell(ip.Prc, eMoney)
                {
                    View = vRight
                };
                

				iRow++;
			}
			grid.AutoSizeCells();
		}

		private void FillCatalogs()
		{
			gridCat.Redim(1, 4);
			int iRow = 1;
			denSQL.denReader r = denSQL.Query("SELECT ct_id, ct_code, ct_com, ct_price, ct_unit FROM bk_cat WHERE ct_item={0}", m_Item.Id);
			while (r.Read())
			{
				gridCat.Rows.Insert(iRow);
				gridCat.Rows[iRow].Tag = r.GetInt(0);
				Company cp = Company.getCompany(r, "ct_com");
				Unit u = Unit.getUnit(r, "ct_unit");

                gridCat[iRow, 0] = new Sgc.RowHeader(r.GetString(1))
                {
                    View = rh
                };
                gridCat[iRow, 1] = new Sgc.Cell(cp.Name);

                gridCat[iRow, 2] = new Sgc.Cell(r.GetDecimal(3), eMoneyRO)
                {
                    View = vRight
                };
                gridCat[iRow, 3] = new Sgc.Cell(u);
				iRow++;
			}
			r.Close();
		}

		private void FillUnits()
		{
			gridUnits.Redim(1, 3);
			int iRow = 0;
			foreach (ItemUnit iu in m_Item.SubUnits)
			{
				gridUnits.Rows.Insert(++iRow);
				gridUnits.Rows[iRow].Tag = iu;

                gridUnits[iRow, 0] = new Sgc.RowHeader(iu.SubUnit)
                {
                    View = rh,
                    Editor = eUnit
                };
                gridUnits[iRow, 1] = new Sgc.Cell(iu.Cnt, eDecimal)
                {
                    View = vRight
                };
                gridUnits[iRow, 2] = new Sgc.Cell(iu.CntUnit);
			}
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			if (grid.RowsCount > 1)
			{
				for (int i = 1; i < grid.RowsCount; i++)
				{
					ItemPrice ip = (ItemPrice)grid.Rows[i].Tag;
					ip.Prc = (decimal)grid[i, 2].Value;
				}
			}
			if (gridUnits.RowsCount > 1)
			{
				for (int i = 1; i < gridUnits.RowsCount; i++)
				{
					ItemUnit iu = (ItemUnit)gridUnits.Rows[i].Tag;
					iu.Cnt = (decimal)gridUnits[i, 1].Value;
				}
			}
			m_Item.Name = txtName.Text;
			m_Item.Short = txtShort.Text;
			m_Item.NameUa = txtNameUa.Text;
			m_Item.Articul = txtArt.Text;
			m_Item.InUse = chkUse.Checked;
			m_Item.Ico = (BK_Icon)cbIcon.SelectedItem;
			m_Item.BaseUnit = (Unit)cmdBase.Tag;
			m_Item.MinUnit = (Unit)cmdMin.Tag;
			if (m_Item.IsCarrier)
			{
				m_Item.Carrier.Format = (BK_Format)cmdFmt.Tag;
				m_Item.Carrier.Density = (BK_Density)cmdDen.Tag;
				m_Item.Carrier.Surface = (BK_Surface)cmdSurf.Tag;
			}
			else
				m_Item.ServiceFormat = (BK_Format)cmdFmt.Tag;
            if(m_Item.IsService)
            {
                m_Item.Device = (BK_Device)cmdDevice.Tag;
            }

			m_Item.Manufacturer = (Company)cmdVen.Tag;
			ItemUnit.AddDefaultBaseToMin(m_Item);
			m_Item.Store();
		}

		private void cbIcon_DrawItem(object sender, DrawItemEventArgs e)
		{
			BK_Icon ico = (BK_Icon)((ComboBox)sender).Items[e.Index];
			bool bSelect = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
			if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit) bSelect = false;
			Rectangle rtImg = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, 16, 16);
			Rectangle rtFill = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
			//RectangleF rtText = new RectangleF(e.Bounds.X + rtImg.Width + 5, e.Bounds.Y, e.Bounds.Width - e.Bounds.X - rtImg.Width - 5, e.Bounds.Height);

			// new White item
			//Brush b = SystemBrushes.
			e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
			// if selected - draw rounded rectangle // FillRoundedRectangle
			if (bSelect)
			{
				e.Graphics.FillRoundedRectangle(lgbrSelect, rtFill, 4);
				e.Graphics.DrawRoundedRectangle(penSel, rtFill, 4);
			}
			// draw image
			e.Graphics.DrawImage(ico.Image, rtImg);
			// draw text
			//SizeF sz = e.Graphics.MeasureString(ico, fntNorm);
			//e.Graphics.DrawString("TEXT", fntNorm, Brushes.Black, rtText.X, rtText.Y + (rtText.Height - sz.Height) / 2f);
		}

		public void Grid_OnEditEnded(SourceGrid.CellContext cntx, string tag, EventArgs e)
		{
            if (!cntx.Position.IsEmpty())
            {
                cntx.Grid.Columns.AutoSizeColumn(cntx.Position.Column, true);
                if (tag == "price")
                {
                    ItemPrice ip = (ItemPrice)grid.Rows[cntx.Position.Row].Tag;
                    switch(cntx.Position.Column)
                    {
                        case 2:
                            ip.setPrice(cntx.DisplayText);
                            break;
                        case 4:
                            ip.setConsumption(cntx.DisplayText);
                            break;
                    }
                    
                    //grid[cntx.Position].Value = ip.Prc;
                }
            }
		}

        public void Grid_OnMouseUp(SourceGrid.CellContext cntx, string tag, MouseEventArgs e)
		{
			if (cntx.Grid == gridUnits)
			{
				bGridUnits_Click = false;
				if (cntx.Position.IsEmpty()) return;
				if (cntx.Position.Column == 2 && cntx.Position.Row > 0 && e.Button == MouseButtons.Left) bGridUnits_Click = Unit.UnitsMenu(cntx, cmdChngUnit_Click);
			}
		}
		private void cmdChngUnit_Click(object sender, EventArgs e) { }

		private ToolStripMenuItem ListPriceUnits(BK_Point p, BK_Carrier car = null)
		{
            ToolStripMenuItem tsmi = new ToolStripMenuItem()
            {
                Text = car == null ? p.ToString() : car.ToString(),
                Image = car == null ? RsRc.home_16 : RsRc.documents16
            };
            List<PrcAddTag> lmt = new List<PrcAddTag>();
			List<Unit> lst = m_Item.ItemUnits;
			foreach (Unit u in lst)
			{
				if (car == null)
				{ if (m_Item.Prices.Any(x => x.Pnt == p && x.Unit == u)) continue; }
				else
				{ if (m_Item.Prices.Any(x => x.Pnt == p && x.Carrier == car && x.Unit == u)) continue; }

				PrcAddTag mt = new PrcAddTag(p, u, car);
				tsmi.DropDownItems.Add(u.Short, RsRc.meas_len_16, cmdAddPrice_MenuItem_Click).Tag = mt;
				lmt.Add(mt);
			}
			tsmi.Tag = lmt;
			tsmi.Click += cmdAddPrices_MenuItem_Click;
			return tsmi;
		}

		private void cmdAddPrice_Click(object sender, EventArgs e)
		{
			int iUnits = m_Item.UnitsCount;
			ContextMenuStrip cms = new ContextMenuStrip();
            foreach (BK_Point p in BK_Point.getAll())
			{
				ToolStripMenuItem tsmi = null;
				if (!m_Item.IsService && m_Item.Prices.Count(x => x.Pnt == p) >= iUnits) continue;
				if (m_Item.IsService)
				{
					tsmi = new ToolStripMenuItem(p.ToString(), RsRc.home_16);
					foreach (BK_Carrier car in BK_Carrier.getAll())
					{
						if (car.Format != m_Item.ServiceFormat) continue;
						if (m_Item.Prices.Count(x => x.Pnt == p && x.Carrier == car) >= iUnits) continue;
						ToolStripMenuItem msub = ListPriceUnits(p, car);
						if (msub.HasDropDownItems) tsmi.DropDownItems.Add(msub);
					}
				}
				else
					tsmi = ListPriceUnits(p);
				if (tsmi.HasDropDownItems) cms.Items.Add(tsmi);
			}
			if (cms.Items.Count < 1) return;
			cms.Show(cmdAddPrice, 0, cmdAddPrice.Height);
		}

		private void cmdAddPrice_MenuItem_Click(object sender, EventArgs e) { cmdAddPrices_AddPrices(new PrcAddTag[] { (PrcAddTag)((ToolStripItem)sender).Tag }); }
		private void cmdAddPrices_MenuItem_Click(object sender, EventArgs e) { cmdAddPrices_AddPrices((List<PrcAddTag>)((ToolStripItem)sender).Tag); }
		private void cmdAddPrices_AddPrices(IEnumerable<PrcAddTag> lmt)
		{
			if (lmt == null || lmt.Count() == 0 || lmt.First() == null) return;
			m_Item.AddPrices(lmt);
			FillPrices();
		}

        private void cmdDelPrice_Click(object sender, EventArgs e)
        {
            SourceGrid.RangeRegion rr = grid.Selection.GetSelectionRegion();
            if (rr.IsEmpty()) return;
            int rowPrc = rr[0].Start.Row;
            if (rowPrc < 1) return;
            ItemPrice ip = (ItemPrice)grid.Rows[rowPrc].Tag;
            //if(DialogBox)
            if (MessageBox.Show("Удалить цену на " + ip.Carrier.Caption + "\nдля точки " + ip.Pnt.Name + "?", "Удаление цены", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ip.Delete();
                FillPrices();
            }
        }

        private void cmdMin_Click(object sender, EventArgs e) { Unit.UnitsMenu(cmdMin); }
		private void cmdBase_Click(object sender, EventArgs e) { Unit.UnitsMenu(cmdBase); }

		private void gridUnits_MouseClick(object sender, MouseEventArgs e)
		{
			if (bGridUnits_Click) { bGridUnits_Click = false; return; }
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				ContextMenuStrip cms = new ContextMenuStrip();
				foreach (Unit u in Unit.getAll())
					if (!m_Item.HasUnit(u)) cms.Items.Add(u.Name, RsRc.Add16, Menu_AddUnitClick).Tag = u;
				if (cms.Items.Count == 0) return;
				cms.Show(gridUnits, gridUnits.PointToClient(MousePosition));
			}
		}

		private void Menu_AddUnitClick(object sender, EventArgs e)
		{
			m_Item.AddSubUnit((Unit)((ToolStripItem)sender).Tag);
			FillUnits();
		}

		private void cmdMenu_Click(Button btn, Type tAdd, string sMethod = "getAll")
		{
			ContextMenuStrip cms = new ContextMenuStrip();
            IEnumerable<IBkObject> par = (IEnumerable<IBkObject>)tAdd.GetMethod(sMethod).Invoke(null, null);

            foreach (IBkObject o in par)
				if (o != btn.Tag) cms.Items.Add(o.I_Short, o.I_Icon, menuObj_Click).Tag = new ButtonTag(btn, o);
			if (tAdd != null)
			{
				cms.Items.Add("-");
				cms.Items.Add("Правка...", RsRc.modify_16, menuObjMdf_Click).Tag = new ButtonTag(btn, (IBkObject)btn.Tag, tAdd);
				cms.Items.Add("Добавить...", RsRc.Add16, menuObjAdd_Click).Tag = new ButtonTag(btn, null, tAdd);
			}
			if (cms.Items.Count > 0) cms.Show(btn, 0, btn.Height);
		}

		private void menuObjMdf_Click(object sender, EventArgs e) { ((ButtonTag)((ToolStripItem)sender).Tag).Edit(); }
		private void menuObjAdd_Click(object sender, EventArgs e) { ((ButtonTag)((ToolStripItem)sender).Tag).Make(); }
		private void menuObj_Click(object sender, EventArgs e) { ((ButtonTag)((ToolStripMenuItem)sender).Tag).Apply(); }

		private void cmdFmt_Click(object sender, EventArgs e) { cmdMenu_Click((Button)sender, typeof(BK_Format)); }
		private void cmdSurf_Click(object sender, EventArgs e) { cmdMenu_Click((Button)sender, typeof(BK_Surface)); }
		private void cmdDen_Click(object sender, EventArgs e) { cmdMenu_Click((Button)sender, typeof(BK_Density)); }

        private void cmdCancel_Click(object sender, EventArgs e) { m_Item.reload(); }

        private void cmdDevice_Click(object sender, EventArgs e) { cmdMenu_Click((Button)sender, typeof(BK_Device)); }

        private void cmdVen_Click(object sender, EventArgs e) { cmdMenu_Click((Button)sender, typeof(Company), "getCustomers"); }

    }

	/// <summary>
	/// Дополнительный Обработчик событий в frmPrice
	/// </summary>
	public class frmPriceGridEvents : Sgc.Controllers.ControllerBase
	{
		private frmPrice m_Frm;
        private string m_Tag = "";

		public frmPriceGridEvents(frmPrice frm, string tag) { m_Frm = frm; m_Tag = tag; }

		public override void OnEditEnded(SourceGrid.CellContext sender, EventArgs e) { m_Frm.Grid_OnEditEnded(sender, m_Tag, e); }
		public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e) { m_Frm.Grid_OnMouseUp(sender, m_Tag, e); }
	}

	public class PrcObjTag
	{
		public Button btn;
		public IBkObject ibo;
		public Type type;
		public PrcObjTag(Button b, IBkObject v, Type t = null) { btn = b; ibo = v; type = t; }
		public void Apply() { btn.Tag = ibo; btn.Text = ibo.I_Name; }
		public void Make()
		{
			System.Reflection.ConstructorInfo ci = type.GetConstructor(new Type[] { });
			ibo = (IBkObject)ci.Invoke(new object[] { });
			if (!ibo.I_OnNew()) return;
			Apply();
		}
		public void Edit() { if (ibo.I_Edit()) Apply(); }
	}

	public class PrcAddTag
	{
		public BK_Point p;
		public BK_Carrier c;
		public Unit u;
		public PrcAddTag(BK_Point pp, Unit pu, BK_Carrier pc = null) { p = pp; c = pc; u = pu; }
	}
}
