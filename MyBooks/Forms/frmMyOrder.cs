using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SourceGrid;
using sgc = SourceGrid.Cells;
using sgcv = SourceGrid.Cells.Views;
using RsRc = MyBooks.Properties.Resources;

namespace MyBooks
{
    public partial class frmMyOrder : Form
    {
        public BK_Order m_Order = null;
        public Company m_Supplier = null;
        private List<CatalogItem> lstCat;

        private sgcv.Cell vLft = new sgcv.Cell();
        private sgcv.Cell vRgt = new sgcv.Cell();
        private sgcv.Cell vCen = new sgcv.Cell();
        private sgcv.Cell vMyPrcNew = new sgcv.Cell();
        private sgc.Editors.TextBoxCurrency ePrc = new sgc.Editors.TextBoxCurrency(typeof(decimal));
        private sgc.Editors.ComboBox eUnits = new sgc.Editors.ComboBox(typeof(Unit));
        private sgc.Editors.TextBox eName;
        private sgc.Editors.TextBoxNumeric eCnt;
        private sgc.Editors.TextBoxButton eTab;

        private const int K_CODE = 0;
        private const int K_NAME = 1;
        private const int K_PRC  = 2;
        private const int K_CNT  = 3;
        private const int K_UNIT = 4;
        private const int K_TOT  = 5;
        private const int K_MINE = 6;
        private const int K_TAB  = 7;

        private const int K_MAX = K_TAB;

        private int m_MenuRow = 0;
        private bool bNewPrice = false;

        public frmMyOrder(BK_Order ord)
        {
            InitializeComponent();
            m_Order = ord;
            m_Supplier = ord.Supplier;
            txtName.Text = ord.Number;
            txtTotal.Text = ord.Total.ToString();
            gridCat.SelectionMode = GridSelectionMode.Row;
            gridCat.Selection.EnableMultiSelection = false;
            gridOrder.SelectionMode = GridSelectionMode.Row;
            gridOrder.Selection.EnableMultiSelection = false;
            CatalogItem.initCatalog();
            lstCat = CatalogItem.getAll();
            vLft.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
            vRgt.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            vCen.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            vMyPrcNew.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            vMyPrcNew.BackColor = Color.PaleVioletRed;
            ePrc.CultureInfo = Program.m_cif;
            //ePrc..Control..CustomFormat = "dd.MM.yy";
            //ePrc.EditableMode = SourceGrid.EditableMode.Default;
            eUnits.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            eUnits.Control.Items.AddRange(Unit.getAll().ToArray());
            eName = new sgc.Editors.TextBox(typeof(string));
            eCnt = new sgc.Editors.TextBoxNumeric(typeof(decimal)) { CultureInfo = Program.m_cif };
            eTab = new sgc.Editors.TextBoxButton(typeof(Page));
            //eTab.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            ///eTab.Control.Items.AddRange(Page.getAll().ToArray());
            gridCat.SetHeaders(new string[] { "Код", "Наименование", "Цена", "Ед." }, new int[] { 100, 250, 100, 50 }, false, true);
            gridOrder.SetHeaders(new string[] { "Код", "Наименование", "Цена", "Кол", "Ед.изм.", "Сумма", "Моя цена", "Закладка" }, new int[] { 100, 250, 100, 50, 50, 50, 50, 0 }, false, true);
            MyOrderGridEvent cEv = new MyOrderGridEvent(this);
            gridCat.Controller.AddController(cEv);
            // Events
            gridOrder.Controller.AddController(new frmMyOrderGridEvents(this, "order"));
        }

        void FillSuppliers()
        {
            cbSupplier.Items.Clear();
            //cbSupplier.Items.Add(Company.AllSuppliers);
            Company[] lstCom = Company.getSuppliers().ToArray();
            cbSupplier.Items.AddRange(lstCom);
            if (lstCom.Count() < 1) { cbSupplier.SelectedIndex = 0; return; }
            if (m_Supplier == null || m_Supplier.Id == 0)
            {
                m_Supplier = lstCom.First();
                cbSupplier.SelectedItem = m_Supplier;
            }
            else
            {
                if (lstCom.Contains(m_Supplier)) cbSupplier.SelectedItem = m_Supplier;
                else cbSupplier.SelectedIndex = 0;
            }
        }

        ItemPrice findPrice(CatalogItem ci, BK_OrderItem oi)
        {
            bNewPrice = false;
            ItemPrice ip = oi.Item.getPointPrices(BK_Point.self).FirstOrDefault(x => x.Unit.Id == oi.Unit.Id);
            if (ip == null)
            {
                ip = oi.addNewPrice(ci);
                bNewPrice = true;
            }
            return ip;
        }

        int addOrderRow(int iRow, CatalogItem ci, BK_OrderItem oi = null)
        {
            bool reload = false;
            if (ci == null) return -1;
            if (oi == null)
            {
                oi = m_Order.createItem(ci);
                reload = true;
            }

            int hash = ci.GetHashCode();
            for(int i = 1; i < gridOrder.RowsCount; i++)
            {
                CatalogItem t = (CatalogItem)gridOrder[i, K_CODE].Tag;
                if (t.GetHashCode() == hash) return i;
            }

            gridOrder.Rows.Insert(iRow);
            gridOrder.Rows[iRow].Tag = oi;

            sgcv.Cell vMyPrc = vRgt;

            ItemPrice ip = findPrice(ci, oi);
            if (bNewPrice) vMyPrc = vMyPrcNew;

            gridOrder[iRow, K_CODE] = new sgc.RowHeader(ci.Code) { Tag = ci };
            gridOrder[iRow, K_NAME] = new sgc.Cell(ci.Item.Name) { Editor = eName };
            gridOrder[iRow, K_PRC]  = new sgc.Cell(oi.Price) { Editor = ePrc };
            gridOrder[iRow, K_CNT]  = new sgc.Cell(oi.Count) { Editor = eCnt } ;
            gridOrder[iRow, K_UNIT] = new sgc.Cell(oi.Unit) { Editor = eUnits };
            gridOrder[iRow, K_TOT]  = new sgc.Cell(oi.Total);
            gridOrder[iRow, K_MINE] = new sgc.Cell(ip.Prc) { Tag = ip, View = vMyPrc, Editor = ePrc };
            gridOrder[iRow, K_TAB] = new sgc.Button(ci.Item.Tab) { /* Editor = eTab */ };
            if (reload)
            {
                gridOrder.AutoSizeCells();
                txtTotal.Text = m_Order.Total.ToString();
            }
            return 0;
        }

        void FillOrder()
        {
            m_Order.readItems();
            gridOrder.Redim(1, K_MAX+1);
            int iRow = 1;
            foreach(BK_OrderItem oi in m_Order.Items)
            {
                CatalogItem ci = lstCat.FirstOrDefault(c => c.Item.Id == oi.Item.Id);
                if(ci == null)
                {
                    ci = new CatalogItem(oi, (Company)cbSupplier.SelectedItem);
                }
                addOrderRow(iRow, ci, oi);
                iRow++;
            }
            gridOrder.AutoSizeCells();
            txtTotal.Text = m_Order.Total.ToString();
        }

        public void Grid_OnClick(CellContext cntx, EventArgs e)
        {
            if (cntx.Position.Row == 0) return;
            if (cntx.Position.Column == 0) // Select row
            {
                Position p = new Position(cntx.Position.Row, 1);
                ((Grid)cntx.Grid).ShowCell(p, true);
                gridCat.Selection.FocusRow(cntx.Position.Row);
                return;
            }
        }

        void FillCatalog()
        {
            gridCat.Redim(1, 4);
            int iR = 1;
            foreach(CatalogItem ci in lstCat)
            {
                if (ci.Com.Id == m_Supplier.Id)
                {
                    gridCat.Rows.Insert(iR);
                    gridCat.Rows[iR].Tag = ci;
                    gridCat[iR, 0] = new sgc.RowHeader(ci.Code);
                    gridCat[iR, 1] = new sgc.Cell(ci.Item.Name);
                    gridCat[iR, 2] = new sgc.Cell(ci.Price);
                    gridCat[iR, 3] = new sgc.Cell(ci.Unit.Short);
                    iR++;
                }
            }
            gridCat.AutoSizeCells();
        }

        private void frmMyOrder_Load(object sender, EventArgs e)
        {
            FillSuppliers();
            FillCatalog();
            if (m_Order.Id != 0)
            {
                FillOrder();
            }
            cbStatus.Items.AddRange(BK_Order.getStatuses());
            cbStatus.SelectedIndex = m_Order.Status;
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Company c = (Company)cbSupplier.SelectedItem;
            if (c == null) return;
            if (c == m_Supplier) return;
            m_Supplier = c;
            FillCatalog();
        }

        private void btnCatDel_Click(object sender, EventArgs e) { }
        private void btnCatEd_Click(object sender, EventArgs e) { }
        private void btnCatAdd_Click(object sender, EventArgs e) { }

        private void btnSupAdd_Click(object sender, EventArgs e)
        {
            if(Company.addNewSupplier()) updateSuppliersList();
        }

        private void btnSupEd_Click(object sender, EventArgs e)
        {
            Company sup = (Company)cbSupplier.SelectedItem;
            if (sup == null) return;
            if(sup.Edit()) updateSuppliersList();
        }

        private void updateSuppliersList()
        {
            int ixSel = cbSupplier.SelectedIndex;
            FillSuppliers();
            cbSupplier.SelectedIndex = ixSel;
        }

        private void btnAdd_Click(object sender, EventArgs e) { }
        private void btnDel_Click(object sender, EventArgs e) { }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string term = txtFind.Text;
            lstFind.Items.Clear();
            if (term.Trim() == "")
            {
                lstFind.Hide();
                return;
            }
            Company c = (Company)cbSupplier.SelectedItem;
            List<CatalogItem> filteredItems = lstCat.Where(x => x.Match(term, c)).ToList();
            lstFind.Items.AddRange(filteredItems.ToArray());

            List<BK_Item> lst = BK_Item.findItems(term, filteredItems);
            if (filteredItems.Count > 0 && lst.Count > 0) lstFind.Items.Add("-");
            filteredItems.Clear();
            foreach (BK_Item it in lst) filteredItems.Add(new CatalogItem(it, c));
            lstFind.Items.AddRange(filteredItems.ToArray());

            if (lstFind.Items.Count > 0)
                lstFind.Show();
            else
                lstFind.Hide();
        }

        private void addOrderItem(CatalogItem ci)
        {
            if (ci != null)
            {
                int iRet = addOrderRow(1, ci);
                if (iRet > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Duplicate item");
                    gridOrder.Selection.SelectRow(iRet, true);
                }
                txtFind.Text = "";
                txtFind.Focus();
            }
        }

        private void addNewItem()
        {
            string sName = txtFind.Text;
            CatalogItem ci = null;
            if (sName.Length < 3) return;
            if(lstFind.Items.Count == 1)
            {
                ci = (CatalogItem)lstFind.Items[0];
            }
            else
            {
                Company sup = (Company)cbSupplier.SelectedItem;
                BK_Item bi = BK_Item.createItem(sName);
                ci = new CatalogItem(bi, sup);
            }
            addOrderItem(ci);
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down:
                    lstFind.Focus();
                    if (lstFind.SelectedIndex < 0 && lstFind.Items.Count > 0) lstFind.SelectedIndex = 0;
                    break;
                case Keys.Enter:
                    addNewItem();
                    break;
            }
        }

        private void lstFind_KeyUp(object sender, KeyEventArgs e)
        {
            bool bOut = false;
            CatalogItem ci = null;
            switch(e.KeyCode)
            {
                case Keys.Up:
                    if(lstFind.SelectedIndex == 0) txtFind.Focus();
                    break;
                case Keys.Escape:
                    bOut = true;
                    break;
                case Keys.Enter:
                    ci = (CatalogItem)lstFind.SelectedItem;
                    bOut = true;
                    break;
            }
            if (bOut)
            {
                lstFind.Hide();
                txtFind.Focus();
            }
            if (ci != null)
            {
                addOrderItem(ci);
            }
        }

        private void lstFind_MouseClick(object sender, MouseEventArgs e)
        {
            CatalogItem ci = (CatalogItem)lstFind.SelectedItem;
            addOrderItem(ci);
        }

        public void GridOrder_OnEditEnded(CellContext cntx, EventArgs e)
        {
            int row = cntx.Position.Row;
            CatalogItem ci = (CatalogItem)gridOrder[row, K_CODE].Tag;
            BK_OrderItem oi = (BK_OrderItem)gridOrder.Rows[row].Tag;
            ItemPrice ip = (ItemPrice)gridOrder[row, K_MINE].Tag;
            switch (cntx.Position.Column)
            {
                case K_PRC:
                    oi.SetPrice(cntx.DisplayText);
                    ci.SetPrice(oi);
                    gridOrder[row, K_TOT].Value = oi.Total;
                    if(m_Order.evalMine(oi, ip))
                    {
                        gridOrder[row, K_MINE].Value = ip.Prc;
                    }
                    txtTotal.Text = m_Order.Total.ToString();
                    break;

                case K_CNT:
                    oi.SetCount(cntx.DisplayText);

                    gridOrder[row, K_TOT].Value = oi.Total;
                    txtTotal.Text = m_Order.Total.ToString();
                    break;

                case K_UNIT:
                    Unit u = (Unit)gridOrder[row, K_UNIT].Value;
                    oi.Unit = u;
                    ItemPrice ipn = findPrice(ci, oi);
                    if (bNewPrice) oi.ItemChanged = true;
                    gridOrder[row, K_MINE].Tag = ipn;
                    m_Order.evalMine(oi, ipn);
                    gridOrder[row, K_MINE].Value = ipn.Prc;
                    txtTotal.Text = m_Order.Total.ToString();
                    break;

            }
        }

        public void GridOrder_OnClick(CellContext cntx, EventArgs e)
        {
            //string msg = string.Format("click on [{0}, {1}] = {2}", cntx.Position.Row, cntx.Position.Column, cntx.Value);
            //System.Diagnostics.Debug.WriteLine(msg);
            if (cntx.Position.Row <= 0) return;
            switch(cntx.Position.Column)
            {
                case K_TAB:
                    ContextMenuStrip cms = new ContextMenuStrip();
                    foreach(Page p in Page.getAll().OrderBy(p => p.Caption))
                    {
                        ToolStripItem tsi = cms.Items.Add(p.Caption, RsRc.Tab16, GridOrder_TabMenuItem_Click);
                        tsi.Tag = p;
                    }
                    m_MenuRow = cntx.Position.Row;
                    Grid g = (Grid)cntx.Grid;
                    Rectangle r = g.PositionToRectangle(cntx.Position);
                    Point pt = new Point(r.Left, r.Bottom);
                    cms.Show(g, pt);
                    break;
            }
        }

        public void GridOrder_TabMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            Page p = (Page)tsmi.Tag;
            if(m_MenuRow > 0)
            {
                gridOrder[m_MenuRow, K_TAB].Value = p;
                BK_OrderItem oi = (BK_OrderItem)gridOrder.Rows[m_MenuRow].Tag;
                oi.Item.Tab = p;
                oi.ItemChanged = true;
            }
            m_MenuRow = 0;
        }

        private void frmMyOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            BK_Item.purgeItems();
            m_Order.Items.Clear();
            m_Order = null;
            m_Supplier = null;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            m_Order.Number = txtName.Text;
            m_Order.Status = cbStatus.SelectedIndex;
            //m_Order.Supplier = (Company)cbSupplier.SelectedItem;
            for(int r=1; r< gridOrder.RowsCount; r++)
            {
                CatalogItem ci = (CatalogItem)gridOrder[r, K_CODE].Tag;
                if (ci.Id == 0 || ci.HasChanged) ci.Store();
            }
            m_Order.Store();
        }
    }

    public class frmMyOrderGridEvents : sgc.Controllers.ControllerBase
    {
        private frmMyOrder m_Frm;
        private string m_GridTag = "";

        public frmMyOrderGridEvents(frmMyOrder frm, string gridTag) { m_Frm = frm;  m_GridTag = gridTag; }

        public override void OnClick(CellContext cntx, EventArgs e)
        {
            switch (m_GridTag)
            {
                case "order": m_Frm.GridOrder_OnClick(cntx, e); break;
            }
            base.OnClick(cntx, e);
        }

        public override void OnEditEnded(CellContext cntx, EventArgs e) {
            switch (m_GridTag)
            {
                case "order": m_Frm.GridOrder_OnEditEnded(cntx, e); break;
            }
        }
        //public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e) { m_Frm.Grid_OnMouseUp(sender, m_Tag, e); }
    }
}
