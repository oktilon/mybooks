using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sgc = SourceGrid.Cells;
using sgcv = SourceGrid.Cells.Views;

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

        private const int K_CODE = 0;
        private const int K_NAME = 1;
        private const int K_PRC  = 2;
        private const int K_CNT  = 3;
        private const int K_UNIT = 4;
        private const int K_TOT  = 5;
        private const int K_MINE = 6;

        public frmMyOrder(BK_Order ord)
        {
            InitializeComponent();
            m_Order = ord;
            m_Supplier = ord.Supplier;
            txtName.Text = ord.Number;
            gridCat.SelectionMode = SourceGrid.GridSelectionMode.Row;
            gridCat.Selection.EnableMultiSelection = false;
            gridOrder.SelectionMode = SourceGrid.GridSelectionMode.Row;
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
            gridCat.SetHeaders(new string[] { "Код", "Наименование", "Цена", "Ед." }, new int[] { 100, 250, 100, 50 }, false, true);
            gridOrder.SetHeaders(new string[] { "Код", "Наименование", "Цена", "Кол", "Ед.изм.", "Сумма", "Моя цена" }, new int[] { 100, 250, 100, 50, 50, 50, 50 }, false, true);
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

        void addOrderRow(int iRow, CatalogItem ci, BK_OrderItem oi = null)
        {
            bool reload = false;
            if (ci == null) return;
            if (oi == null)
            {
                oi = new BK_OrderItem(ci);
                m_Order.Items.Add(oi);
                reload = true;
            }
            gridOrder.Rows.Insert(iRow);
            gridOrder.Rows[iRow].Tag = oi;

            sgcv.Cell vMyPrc = vRgt;

            ItemPrice ip = ci.Item.getPointPrices(BK_Point.self).FirstOrDefault(x => x.Unit.Id == oi.Unit.Id);
            if (ip == null)
            {
                PrcAddTag pat = new PrcAddTag(BK_Point.self, ci.Unit);
                ip = new ItemPrice(ci.Item, decimal.Round(ci.Price * ci.Com.Coeff, 2), pat);
                vMyPrc = vMyPrcNew;
            }

            gridOrder[iRow, K_CODE] = new sgc.RowHeader(ci.Code) { Tag = ci };
            gridOrder[iRow, K_NAME] = new sgc.Cell(ci.Item.Name) { Editor = eName };
            gridOrder[iRow, K_PRC]  = new sgc.Cell(oi.Price) { Editor = ePrc };
            gridOrder[iRow, K_CNT]  = new sgc.Cell(oi.Count) { Editor = eCnt } ;
            gridOrder[iRow, K_UNIT] = new sgc.Cell(oi.Unit) { Editor = eUnits };
            gridOrder[iRow, K_TOT]  = new sgc.Cell(oi.Total);
            gridOrder[iRow, K_MINE] = new sgc.Cell(ip.Prc) { Tag = ip, View = vMyPrc, Editor = ePrc };
            if (reload)
            {
                gridOrder.AutoSizeCells();
                txtTotal.Text = m_Order.Total.ToString();
            }
        }

        void FillOrder()
        {
            m_Order.Items.Clear();
            gridOrder.Redim(1, 6);
            int iRow = 1;
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_slist WHERE sl_sup={0}", m_Order.Id);
            while (r.Read())
            {
                BK_OrderItem oi = new BK_OrderItem(r);
                CatalogItem ci = lstCat.FirstOrDefault(c => c.Item.Id == oi.ItemId);
                if(ci == null)
                {
                    ci = new CatalogItem(oi, (Company)cbSupplier.SelectedItem);
                }
                m_Order.Items.Add(oi);
                addOrderRow(iRow, ci, oi);
                iRow++;
            }
            r.Close();
            gridOrder.AutoSizeCells();
            txtTotal.Text = m_Order.Total.ToString();
        }

        public void Grid_OnClick(SourceGrid.CellContext cntx, EventArgs e)
        {
            if (cntx.Position.Row == 0) return;
            if (cntx.Position.Column == 0) // Select row
            {
                SourceGrid.Position p = new SourceGrid.Position(cntx.Position.Row, 1);
                ((SourceGrid.Grid)cntx.Grid).ShowCell(p, true);
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
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Company c = (Company)cbSupplier.SelectedItem;
            if (c == null) return;
            if (c == m_Supplier) return;
            m_Supplier = c;
            FillCatalog();
        }

        private void btnCatDel_Click(object sender, EventArgs e)
        {

        }

        private void btnCatEd_Click(object sender, EventArgs e)
        {

        }

        private void btnCatAdd_Click(object sender, EventArgs e)
        {

        }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            //txtName.AutoCompleteMode = AutoCompleteMode.
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string item = txtFind.Text;
            lstFind.Items.Clear();
            if (item == "")
            {
                lstFind.Hide();
                return;
            }
            Company c = (Company)cbSupplier.SelectedItem;
            List<CatalogItem> filteredItems = lstCat.Where(x => x.Match(item, c)).ToList();
            lstFind.Items.AddRange(filteredItems.ToArray());

            List<BK_Item> lst = BK_Item.findItems(item, filteredItems);
            if (filteredItems.Count > 0 && lst.Count > 0) lstFind.Items.Add("-");
            filteredItems.Clear();
            foreach (BK_Item it in lst) filteredItems.Add(new CatalogItem(it, c));
            lstFind.Items.AddRange(filteredItems.ToArray());

            if (lstFind.Items.Count > 0) lstFind.Show();
        }

        private void addNewItem()
        {
            string sName = txtFind.Text;
            if (sName.Length < 3) return;
            BK_Item.getItem(0);
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down:
                    lstFind.Focus();
                    if (lstFind.SelectedIndex < 0) lstFind.SelectedIndex = 0;
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
                addOrderRow(1, ci);
                txtFind.Text = "";
                txtFind.Focus();
            }
        }

        private void lstFind_MouseClick(object sender, MouseEventArgs e)
        {
            CatalogItem ci = (CatalogItem)lstFind.SelectedItem;
            addOrderRow(1, ci);
            txtFind.Text = "";
            txtFind.Focus();
        }

        public void GridOrder_OnEditEnded(SourceGrid.CellContext cntx, EventArgs e)
        {
            int row = cntx.Position.Row;
            CatalogItem ci = (CatalogItem)gridOrder[row, K_CODE].Tag;
            BK_OrderItem oi = (BK_OrderItem)gridOrder.Rows[row].Tag;
            ItemPrice ip = (ItemPrice)gridOrder[row, K_MINE].Tag;
            switch (cntx.Position.Column)
            {
                case K_PRC:
                    oi.SetPrice(cntx.DisplayText);
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

            }
        }
    }

    public class frmMyOrderGridEvents : sgc.Controllers.ControllerBase
    {
        private frmMyOrder m_Frm;
        private string m_GridTag = "";

        public frmMyOrderGridEvents(frmMyOrder frm, string gridTag) { m_Frm = frm;  m_GridTag = gridTag; }

        public override void OnEditEnded(SourceGrid.CellContext cntx, EventArgs e) {
            switch (m_GridTag)
            {
                case "order": m_Frm.GridOrder_OnEditEnded(cntx, e); break;
            }
        }
        //public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e) { m_Frm.Grid_OnMouseUp(sender, m_Tag, e); }
    }
}
