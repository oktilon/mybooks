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
    public partial class frmMyOrders : Form
    {
        public List<BK_Order> lstSup;
        private SourceGrid.Cells.Views.Cell vLft = new SourceGrid.Cells.Views.Cell();
        private SourceGrid.Cells.Views.Cell vRgt = new SourceGrid.Cells.Views.Cell();
        private SourceGrid.Cells.Views.Cell vCen = new SourceGrid.Cells.Views.Cell();
        private SourceGrid.Cells.Editors.DateTimePicker eDate = new SourceGrid.Cells.Editors.DateTimePicker();

        public frmMyOrders()
        {
            InitializeComponent();
            BK_Order.initOrders();
            grid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            grid.Selection.EnableMultiSelection = false;
            lstSup = new List<BK_Order>();
            vLft.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
            vRgt.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            vCen.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            eDate.Control.Format = DateTimePickerFormat.Custom;
            eDate.Control.CustomFormat = "dd.MM.yy";
            eDate.EditableMode = SourceGrid.EditableMode.None;
        }

        void FillOrders()
        {
            Company cSup = (Company)cbSupplier.SelectedItem;
            lstSup = BK_Order.getBySupplier(cSup, chkUnReady.Checked);
            grid.Redim(1, 5);
            int iRow = 1, iCol;
            foreach (BK_Order ord in lstSup)
            {
                iCol = 0;
                grid.Rows.Insert(iRow);
                grid.Rows[iRow].Tag = ord;

                grid[iRow, iCol] = new SourceGrid.Cells.ColumnHeader(ord.Number) { View = vLft };
                grid[iRow, ++iCol] = new SourceGrid.Cells.Cell(ord.Supplier.Name) { View = vLft };
                grid[iRow, ++iCol] = new SourceGrid.Cells.Cell(ord.dtOrder) { View = vLft, Editor = eDate };
                grid[iRow, ++iCol] = new SourceGrid.Cells.Cell(ord.Summ) { View = vRgt };
                grid[iRow, ++iCol] = new SourceGrid.Cells.Cell(ord.StatusText) { View = vLft };
                iRow++;
            }
            grid.AutoSizeCells();
        }

        void FillSuppliers()
        {
            cbSupplier.Items.Clear();
            IEnumerable<Company> ies = Company.getSuppliers();
            cbSupplier.Items.Add(Company.AllSuppliers);
            cbSupplier.Items.AddRange(ies.ToArray());
            cbSupplier.SelectedIndex = 0;
        }

        private void frmMyOrders_Load(object sender, EventArgs e)
        {
            grid.SetHeaders(new string[] { "Номер", "Поставщик", "Дата", "Сумма", "Статус" }, new int[] { 100, 250, 100, 50, 50 }, false, true);
            FillSuppliers();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            BK_Order ord = BK_Order.addOrder((Company)cbSupplier.SelectedItem);
            ord.Edit();
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e) { FillOrders(); }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.Clicks != 2) return;
            editOrder();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editOrder();
        }

        private bool editOrder()
        {
            if (grid.Selection.IsEmpty() || grid.Selection.GetSelectionRegion()[0].Start.Row < 1) return false;
            BK_Order ord = (BK_Order)grid.Rows[grid.Selection.GetSelectionRegion()[0].Start.Row].Tag;
            if (ord != null) return ord.Edit();
            return false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }

        private void chkUnReady_CheckedChanged(object sender, EventArgs e) { FillOrders(); }
    }

}
