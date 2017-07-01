using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    /// <summary>
    /// Дополнительный Обработчик событий для grid-ов прайсов в frmBill
    /// </summary>
    public class BillEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        private frmBill m_Frm;
        private bool m_IsBill;

        public BillEvent(frmBill frm, bool bGridBill)
        {
            m_Frm = frm;
			m_IsBill = bGridBill;
        }

        public override void OnEditEnded(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnEditEnded(sender, e);
            m_Frm.Cell_OnEditEnded(sender, m_IsBill);
        }

        public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnDoubleClick(sender, e);
			m_Frm.Cell_OnDblClick(sender, m_IsBill);
        }

        public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnClick(sender, e);
			m_Frm.Cell_OnClick(sender, m_IsBill);
        }

        /*public override void OnKeyDown(SourceGrid.CellContext sender, KeyEventArgs e)
        {
            String sTag = sender.Grid.Tag.ToString();
            if (sTag == "bill") { base.OnKeyDown(sender, e); return; }
            m_Frm.CellPrice_OnKeyDown(sender, e);
            if (!e.Handled) base.OnKeyDown(sender, e);
            return;
        }*/
    }

    public class BillMouseEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        private frmBill m_Frm;
        public BillMouseEvent(frmBill frm) { m_Frm = frm; }
        public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnDoubleClick(sender, e);
        }
    }

    /// <summary>
    /// Дополнительный Обработчик событий для gridLst в frmMain
    /// </summary>
    public class ListEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        private frmMain m_Frm;

        public ListEvent(frmMain frm) { m_Frm = frm; }

        public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnDoubleClick(sender, e);
            if (sender.Position.Row == 0) return;
            m_Frm.gridLst_OnDblClick(sender.Position.Row);
        }
        
        public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnClick(sender, e);
            if (sender.Position.Row == 0) return;
            if (sender.Position.Column != 0) return;
            m_Frm.gridLst_OnClick(sender.Position.Row);
        }
    }

    /// <summary>
    /// Дополнительный Обработчик событий в frmMyOrder
    /// </summary>
    public class MyOrderGridEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        private frmMyOrder m_Frm;

        public MyOrderGridEvent(frmMyOrder frm) { m_Frm = frm; }

        public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnClick(sender, e);
            m_Frm.Grid_OnClick(sender, e);
        }
    }


}