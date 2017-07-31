using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using Sgc = SourceGrid.Cells;
using RsRc = MyBooks.Properties.Resources;
using SourceGrid;

namespace MyBooks
{
	public partial class frmPrice : Form
	{
		BK_Item m_Item;
		// Owner draws
		private LinearGradientBrush lgbrSelect;
		private Pen penSel;
        // Grids
        private string sTopTag = "";
        private string sBotTag = "";
        // Cells
        Sgc.Views.RowHeader rh, rhUnit;
		Sgc.Views.Cell vRight;
		Sgc.Views.Cell vCenter;
		Sgc.Editors.TextBoxCurrency eMoney;
		Sgc.Editors.TextBoxCurrency eMoneyRO;
        Sgc.Editors.TextBoxNumeric eInt;
        Sgc.Editors.TextBoxNumeric eIntRO;
		Sgc.Editors.TextBox eUnit;
		string[] hdrStrService = new string[] { "Точка", "Расходник", "Вариант", "Цена", "Расход", "Ед." };
		string[] hdrStrItem = new string[] { "Точка", "Остаток", "Ед.", "Цена" };
		int[] hdrInt = new int[] { 0, 0, 0, 0, 0, 0 };
		bool bGridUnits_Click = false;
        private int ixTopDef = 0;
        private int ixBotDef = 0;

        const int COL_PNT = 0;
        const int COL_CAR = 1;
        const int COL_REM = 1;
        const int COL_VAR = 2;
        const int COL_PRCUN = 2;
        const int COL_PRC = 3;
        const int COL_CON = 4;
        const int COL_CONUN = 5;


        public frmPrice(BK_Item bi = null)
		{
			InitializeComponent();
			m_Item = bi;
            if (bi == null) return;
			cbIcon.Items.AddRange(BK_Icon.getAll().ToArray());
			imgl.Images.AddRange((from x in BK_Icon.getAll() orderby x.Id select x.Image).ToArray());
			Color clrSelect = Color.AliceBlue;
			lgbrSelect = new LinearGradientBrush(new Rectangle(0, 0, 18, 18), Color.FromArgb(255, clrSelect), Color.FromArgb(200, clrSelect), LinearGradientMode.Vertical);
			penSel = new Pen(Brushes.CornflowerBlue, 1);
			// GRID
			rh = new Sgc.Views.RowHeader();
            DevAge.Drawing.VisualElements.RowHeader rhR = new DevAge.Drawing.VisualElements.RowHeader() { BackColor = Color.Lavender };
            rh.Background = rhR;
			rh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
			rhUnit = new Sgc.Views.RowHeader();
            rhR = new DevAge.Drawing.VisualElements.RowHeader() { BackColor = Color.Lavender };
            rhUnit.Background = rhR;
			rhUnit.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;

            if (m_Item.IsService)
            {
                sTopTag = "var";
                lblTop.Text = "Варианты:";
                gridTop.SetHeaders(new string[] { "Наимен.", "Состав", "Парам." }, new int[] { 0, 0, 0 }, false);
                sBotTag = "dev";
                lblBot.Text = "Устройства:";
                gridBot.SetHeaders(new string[] { "Устройство", "Расход", "По.умолч." }, new int[] { 0, 0, 0 }, false);
                
            }
            else
            {
                sTopTag = "cat";
                lblTop.Text = "Код в каталогах:";
                gridTop.SetHeaders(new string[] { "Код", "Поставщик", "Цена", "Ед." }, new int[] { 0, 0, 0, 0 }, false);
                sBotTag = "unit";
                lblBot.Text = "Единицы измерения:";
                gridBot.SetHeaders(new string[] { "Доп.ед.", "Кол.", "Баз.ед.", "Мин.ед." }, new int[] { 0, 0, 0, 0 }, false);
            }
            frmPriceGridEvents ctrlEventsTop = new frmPriceGridEvents(this, sTopTag);
            frmPriceGridEvents ctrlEventsBot = new frmPriceGridEvents(this, sBotTag);
            gridTop.Controller.AddController(ctrlEventsTop);
            gridBot.Controller.AddController(ctrlEventsBot);

            frmPriceGridEvents ctrlEventsPrc = new frmPriceGridEvents(this, "price");
            grid.Controller.AddController(ctrlEventsPrc);            
		}

		private void frmPrice_Load(object sender, EventArgs e)
		{
			if (m_Item == null) { DialogResult = DialogResult.Cancel; Close(); return; }

            vRight = new Sgc.Views.Cell() { TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight };
            vCenter = new Sgc.Views.Cell() { TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter };
            Text += string.Format(" (Id:{0})", m_Item.Id);

            eMoney = new Sgc.Editors.TextBoxCurrency(typeof(decimal)) { CultureInfo = Program.m_cif };
            eMoneyRO = new Sgc.Editors.TextBoxCurrency(typeof(decimal)) { CultureInfo = Program.m_cif, EditableMode = EditableMode.None };
            eInt = new Sgc.Editors.TextBoxNumeric(typeof(int));
            eIntRO = new Sgc.Editors.TextBoxNumeric(typeof(int)) { EditableMode = EditableMode.None };
            eUnit = new Sgc.Editors.TextBox(typeof(Unit)) { EditableMode = SourceGrid.EditableMode.None };
            eUnit.ConvertingValueToDisplayString += eUnit_ConvertingValueToDisplayString;
			txtName.Text = m_Item.Name;
			txtShort.Text = m_Item.Short;
			txtNameUa.Text = m_Item.NameUa;
			txtArt.Text = m_Item.Articul;
			chkUse.Checked = m_Item.InUse;
			//
			lblType.Text = m_Item.TypeCaption;
			lblFmt.Visible = m_Item.IsService || m_Item.IsCarrier;
			cmdFmt.Visible = m_Item.IsService || m_Item.IsCarrier;
			if (m_Item.IsCarrier)
			{
				lblDen.Visible = true;
				cmdDen.Visible = true;
				lblSurf.Visible = true;
				cmdSurf.Visible = true;
				m_Item.Carrier.Density.SetButton(cmdDen);
				m_Item.Carrier.Surface.SetButton(cmdSurf);
				m_Item.Carrier.Format.SetButton(cmdFmt);
			}
            if (m_Item.IsService)
            {
                m_Item.ServiceFormat.SetButton(cmdFmt);
            }

            lblMin.Visible = !m_Item.IsService;
            cmdMin.Visible = !m_Item.IsService;
            lblBase.Visible = !m_Item.IsService;
            cmdBase.Visible = !m_Item.IsService;
            gridBot.Top = m_Item.IsService ? 16 : 45;
            gridBot.Height = split2.Height -
                split2.SplitterDistance -
                split2.SplitterWidth -
                gridBot.Top;

			//
			m_Item.MinUnit.SetButton(cmdMin);
			m_Item.DefaultUnit.SetButton(cmdBase);
			BK_Icon bki = m_Item.Ico;
			if (!cbIcon.Items.Contains(bki)) cbIcon.Items.Add(bki);
			cbIcon.SelectedItem = bki;
			if (m_Item.MinUnit != m_Item.DefaultUnit) ItemUnit.AddDefaultBaseToMin(m_Item);
			FillPrices();
			FillTopGrid();
			FillBotGrid();
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

                grid[iRow, COL_PNT] = new Sgc.RowHeader(ip.Pnt)
                {
                    View = rh
                };
                if (m_Item.IsService)
                {
                    BK_Item itCar = BK_Item.getItem(ip.Carrier.ItemId);
                    grid[iRow, COL_CAR] = new Sgc.Cell(ip.Carrier); // CARRIER
                    grid[iRow, COL_VAR] = new Sgc.Cell(ip.Variant); // VARIANT
                    grid[iRow, COL_CON] = new Sgc.Cell(ip.Consumption, eInt);
                    grid[iRow, COL_CONUN] = new Sgc.Cell(itCar.MinUnit);
                }
                else
                {
                    grid[iRow, COL_REM] = new Sgc.Cell(ip.CalculateRemain()) { View = vRight };
                    grid[iRow, COL_PRCUN] = new Sgc.Cell(ip.Unit);
                }

                grid[iRow, COL_PRC] = new Sgc.Cell(ip.Prc, eMoney) { View = vRight };                

				iRow++;
			}
			grid.AutoSizeCells();
		}

        private Sgc.Image DefaultImage(bool bDef)
        {
            Sgc.Image def = bDef ? 
                new Sgc.Image(RsRc.ok_16) { Tag = 1, View = vCenter } : 
                new Sgc.Image() { Tag = 0, View = vCenter };
            def.Editor.EditableMode = SourceGrid.EditableMode.None;
            return def;
        }

        private void FillTopGrid()
		{
            int iCols = gridTop.ColumnsCount;
			gridTop.Redim(1, iCols);
			int iRow = 1;
            switch (sTopTag)
            {
                case "cat":
                    denSQL.denReader r = denSQL.Query("SELECT ct_id, ct_code, ct_com, ct_price, ct_unit FROM bk_cat WHERE ct_item={0}", m_Item.Id);
                    while (r.Read())
                    {
                        gridTop.Rows.Insert(iRow);
                        gridTop.Rows[iRow].Tag = r.GetInt(0);
                        Company cp = Company.getCompany(r, "ct_com");
                        Unit u = Unit.getUnit(r, "ct_unit");

                        gridTop[iRow, 0] = new Sgc.RowHeader(r.GetString(1)) { View = rh };
                        gridTop[iRow, 1] = new Sgc.Cell(cp.Name);
                        gridTop[iRow, 2] = new Sgc.Cell(r.GetDecimal(3), eMoneyRO) { View = vRight };
                        gridTop[iRow, 3] = new Sgc.Cell(u);
                        iRow++;
                    }
                    r.Close();
                    break;

                case "var":
                    foreach (BK_Variant v in m_Item.Variants)
                    {
                        gridTop.Rows.Insert(iRow);
                        gridTop.Rows[iRow].Tag = v;
                        gridTop.Rows[iRow].Height = 21;
                        gridTop[iRow, 0] = new Sgc.RowHeader(v.Name) { View = rh, Image = RsRc.var_16 };
                        gridTop[iRow, 1] = new Sgc.Cell(v.Amount);
                        gridTop[iRow, 2] = DefaultImage(v == m_Item.DefaultVariant);
                        ixTopDef = 2;
                        iRow++;
                    }
                    break;
            }
            gridTop.AutoSizeCells();
        }

		private void FillBotGrid()
		{
            int iCols = gridTop.ColumnsCount;
            gridBot.Redim(1, iCols);
			int iRow = 0;
            switch (sBotTag)
            {
                case "unit":
                    foreach (ItemUnit iu in m_Item.SubUnits)
                    {
                        gridBot.Rows.Insert(++iRow);
                        gridBot.Rows[iRow].Tag = iu;

                        gridBot[iRow, 0] = new Sgc.RowHeader(iu.SubUnit) { View = rh, Editor = eUnit };
                        gridBot[iRow, 1] = new Sgc.Cell(iu.Cnt, eInt) { View = vRight };
                        gridBot[iRow, 2] = new Sgc.Cell(iu.CntUnit);
                        gridBot[iRow, 3] = new Sgc.Cell(iu.MinCnt, eIntRO) { View = vRight };
                    }
                    break;

                case "dev":
                    foreach (BK_Device d in m_Item.Devices)
                    {
                        gridBot.Rows.Insert(++iRow);
                        gridBot.Rows[iRow].Tag = d;
                        gridBot.Rows[iRow].Height = 21;

                        gridBot[iRow, 0] = new Sgc.RowHeader(d.Name) { View = rh, Image = RsRc.device_16 };
                        gridBot[iRow, 1] = new Sgc.Cell("-");
                        gridBot[iRow, 2] = DefaultImage(d == m_Item.DefaultDevice);
                        ixBotDef = 2;
                    }
                    break;
            }
            gridBot.AutoSizeCells();
        }

		private void cmdOk_Click(object sender, EventArgs e)
		{
			if (grid.RowsCount > 1)
			{
				for (int i = 1; i < grid.RowsCount; i++)
				{
					ItemPrice ip = (ItemPrice)grid.Rows[i].Tag;
					ip.Prc = (decimal)grid[i, COL_PRC].Value;
				}
			}
			if (gridBot.RowsCount > 1)
			{
				for (int i = 1; i < gridBot.RowsCount; i++)
				{
                    switch (sBotTag)
                    {
                        case "unit":
                            ItemUnit iu = (ItemUnit)gridBot.Rows[i].Tag;
                            iu.Cnt = (int)gridBot[i, 1].Value;
                            break;
                    }
				}
			}
			m_Item.Name = txtName.Text;
			m_Item.Short = txtShort.Text;
			m_Item.NameUa = txtNameUa.Text;
			m_Item.Articul = txtArt.Text;
			m_Item.InUse = chkUse.Checked;
			m_Item.Ico = (BK_Icon)cbIcon.SelectedItem;
			m_Item.DefaultUnit = (Unit)cmdBase.Tag;
			m_Item.MinUnit = (Unit)cmdMin.Tag;
			if (m_Item.IsCarrier)
			{
				m_Item.Carrier.Format = (BK_Format)cmdFmt.Tag;
				m_Item.Carrier.Density = (BK_Density)cmdDen.Tag;
				m_Item.Carrier.Surface = (BK_Surface)cmdSurf.Tag;
			}
				
            if(m_Item.IsService)
            {
                m_Item.ServiceFormat = (BK_Format)cmdFmt.Tag;
                //m_Item.DefaultDevice = (BK_Device)cmdDevice.Tag;
            }

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
                        case COL_PRC:
                            ip.setPrice(cntx.DisplayText);
                            break;
                        case COL_CON:
                            if (!m_Item.IsService) return;
                            ip.setConsumption(cntx.DisplayText);
                            break;
                    }
                    
                    //grid[cntx.Position].Value = ip.Prc;
                }
            }
		}

        public void Grid_OnMouseUp(CellContext cntx, string tag, MouseEventArgs e)
        {
            if (cntx.Position.IsEmpty()) return;
            int iRow = cntx.Position.Row;
            if (iRow < 1) return;
            int iCol = cntx.Position.Column;
            switch (tag)
            {
                case "cat":
                    break;
                case "var":
                    break;
                case "unit":
                    bGridUnits_Click = false;
                    if (iCol == 2 && e.Button == MouseButtons.Left) bGridUnits_Click = Unit.UnitsMenu(cntx, cmdChngUnit_Click);
                    break;
                case "dev":
                    break;
            }
        }
        private void cmdChngUnit_Click(object sender, EventArgs e) { }

        public void Grid_OnDoubleClick(CellContext cntx, string tag, EventArgs e)
        {
            if (cntx.Position.IsEmpty()) return;
            int iRow = cntx.Position.Row;
            if (iRow < 1) return;
            int iCol = cntx.Position.Column;
            switch (tag)
            {
                case "cat":
                    break;
                case "var":
                    if (ixTopDef > 0 && iCol == ixTopDef)
                    {
                        for (int ix = 1; ix < gridTop.RowsCount; ix++)
                            gridTop[ix, ixTopDef] = DefaultImage(ix == iRow);
                        gridTop.Refresh();
                    }
                    break;
                case "unit":
                    break;
                case "dev":
                    if (ixBotDef > 0 && iCol == ixBotDef)
                    {
                        for (int ix = 1; ix < gridBot.RowsCount; ix++)
                            gridBot[ix, ixBotDef] = DefaultImage(ix == iRow);
                        gridBot.Refresh();
                    }
                    break;
            }
        }


        private ToolStripMenuItem Menu_ListItemUnits(BK_Point p)
		{
            ToolStripMenuItem tsmi = new ToolStripMenuItem() { Text = p.ToString(), Image = RsRc.home_16 };
            List<PrcAddTag> lmt = new List<PrcAddTag>();
			List<Unit> lst = m_Item.ItemUnits;
			foreach (Unit u in lst)
			{
				if (m_Item.Prices.Any(x => x.Pnt == p && x.Unit == u)) continue;

				PrcAddTag mt = new PrcAddTag(p, u);
				tsmi.DropDownItems.Add(u.Short, RsRc.meas_len_16, cmdAddPrice_MenuItem_Click).Tag = mt;
				lmt.Add(mt);
			}
			tsmi.Tag = lmt;
			tsmi.Click += cmdAddPrices_MenuItem_Click;
			return tsmi;
		}

        private ToolStripMenuItem Menu_ListServiceVariants(BK_Point p, BK_Carrier car)
        {
            Unit pcs = Unit.getUnit(1);
            Image imgCar = RsRc.documents16;
            switch(car.Type.Id)
            {
                case 2: imgCar = RsRc.bank16; break;
                case 3: imgCar = RsRc.device_16; break;
            }
            ToolStripMenuItem tsmi = new ToolStripMenuItem() { Text = car.ToString(), Image = imgCar };
            List<PrcAddTag> lmt = new List<PrcAddTag>();
            foreach (BK_Variant v in m_Item.Variants)
            {
                if (m_Item.Prices.Any(x => x.Pnt == p && x.Carrier == car && x.Variant == v)) continue;

                PrcAddTag mt = new PrcAddTag(p, pcs, car, v);
                tsmi.DropDownItems.Add(v.Name, RsRc.var_16, cmdAddPrice_MenuItem_Click).Tag = mt;
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
            foreach (BK_Point p in BK_Point.getAll().OrderBy(p=>p.Name))
			{
				ToolStripMenuItem tsmi = null;
                if (m_Item.IsService)
                {
                    tsmi = new ToolStripMenuItem(p.ToString(), RsRc.home_16);
                    foreach (BK_Carrier car in BK_Carrier.getByType(m_Item.CarrierType))
                    {
                        if (car.Format != m_Item.ServiceFormat) continue;
                        if (m_Item.Prices.Count(x => x.Pnt == p && x.Carrier == car) >= m_Item.Variants.Count()) continue;
                        ToolStripMenuItem msub = Menu_ListServiceVariants(p, car);
                        if (msub.HasDropDownItems) tsmi.DropDownItems.Add(msub);
                    }
                }
                else
                {
                    if (m_Item.Prices.Count(x => x.Pnt == p) >= iUnits) continue;
                    tsmi = Menu_ListItemUnits(p);
                }
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
            RangeRegion rr = grid.Selection.GetSelectionRegion();
            if (rr.IsEmpty()) return;
            int rowPrc = rr[0].Start.Row;
            if (rowPrc < 1) return;
            ItemPrice ip = (ItemPrice)grid.Rows[rowPrc].Tag;
            //if(DialogBox)
            string var = ip.Variant.isDefault ? "" : string.Format("\nвариант {0}", ip.Variant.Name);
            if (MessageBox.Show("Удалить цену на " + ip.Carrier.Caption +
                                var + 
                                "\nдля точки " + ip.Pnt.Name + "?",
                                "Удаление цены",
                                MessageBoxButtons.YesNo
                          )
                 == DialogResult.Yes
                )
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
			if (e.Button == MouseButtons.Right)
			{
				ContextMenuStrip cms = new ContextMenuStrip();
				foreach (Unit u in Unit.getAll())
					if (!m_Item.HasUnit(u)) cms.Items.Add(u.Name, RsRc.Add16, Menu_AddUnitClick).Tag = u;
				if (cms.Items.Count == 0) return;
				cms.Show(gridBot, gridBot.PointToClient(MousePosition));
			}
		}

		private void Menu_AddUnitClick(object sender, EventArgs e)
		{
			m_Item.AddSubUnit((Unit)((ToolStripItem)sender).Tag);
			FillBotGrid();
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

		public override void OnEditEnded(CellContext sender, EventArgs e) { m_Frm.Grid_OnEditEnded(sender, m_Tag, e); }
		public override void OnMouseUp(CellContext sender, MouseEventArgs e) { m_Frm.Grid_OnMouseUp(sender, m_Tag, e); }
        public override void OnDoubleClick(CellContext sender, EventArgs e) { m_Frm.Grid_OnDoubleClick(sender, m_Tag, e); }
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
        public BK_Variant v;
        public PrcAddTag(BK_Point pp, Unit pu, BK_Carrier pc = null, BK_Variant pv = null) { p = pp; c = pc; u = pu; v = pv; }
    }
}
