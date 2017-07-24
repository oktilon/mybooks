using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SGC = SourceGrid.Cells;
using RsRc = MyBooks.Properties.Resources;
namespace MyBooks
{
	public partial class frmEditor : Form
	{
		private IBkObject Item = null;

		private SGC.Views.Cell vLeft, vCenter, vRight;
		private SGC.Views.Button vBtn;
		private SGC.Views.ColumnHeader vH;
		private SGC.Views.RowHeader vR;
		private SGC.Editors.TextBox eTextP, eTextS;
		private SGC.Editors.TextBoxNumeric eIntP, eIntS;
		private SGC.Editors.DateTimePicker eDateP, eDateS;
		private SGC.Controllers.RowFocus ctrlRowFocus;
		private frmEditor_GridEvents ctrlGridEvents;
		private List<BO_Field> Data = null;

		public frmEditor(IBkObject pItem = null)
		{
			InitializeComponent();
			Item = pItem;
			Data = Item.I_Params();

			vLeft = new SGC.Views.Cell();
			vLeft.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
			vCenter = new SGC.Views.Cell();
			vCenter.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
			vRight = new SGC.Views.Cell();
			vRight.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
			vBtn = new SGC.Views.Button();
			vH = new SGC.Views.ColumnHeader();
			vH.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
			vR = new SGC.Views.RowHeader();
			eTextP = new SGC.Editors.TextBox(typeof(string));
			eTextP.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eTextS = new SGC.Editors.TextBox(typeof(string));
			eTextS.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eIntP = new SGC.Editors.TextBoxNumeric(typeof(int));
			eIntP.Control.TextAlign = HorizontalAlignment.Center;
			eIntP.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eIntS = new SGC.Editors.TextBoxNumeric(typeof(int));
			eIntS.Control.TextAlign = HorizontalAlignment.Center;
			eIntS.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eDateP = new SGC.Editors.DateTimePicker();
			eDateP.Control.CustomFormat = "dd MMMM yyyy HH:mm";
			eDateP.Control.Format = DateTimePickerFormat.Custom;
			eDateP.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eDateP.ConvertingValueToDisplayString += eDate_ConvertingValueToDisplayString;
			eDateS = new SGC.Editors.DateTimePicker();
			eDateS.Control.CustomFormat = "dd MMMM yyyy HH:mm";
			eDateS.Control.Format = DateTimePickerFormat.Custom;
			eDateS.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick;
			eDateS.ConvertingValueToDisplayString += eDate_ConvertingValueToDisplayString;
			ctrlRowFocus = new SGC.Controllers.RowFocus();
			ctrlGridEvents = new frmEditor_GridEvents(this);
		}

		void eDate_ConvertingValueToDisplayString(object sender, DevAge.ComponentModel.ConvertingObjectEventArgs e)
		{
			e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Converting;
			if ((DateTime)e.Value < DateTimePicker.MinimumDateTime || (DateTime)e.Value > DateTimePicker.MaximumDateTime)
				e.Value = "-";
			else
				e.Value = string.Format("{0:dd MMMM yyyy HH:mm}", e.Value);
			e.ConvertingStatus = DevAge.ComponentModel.ConvertingStatus.Completed;
		}

		private void frmEditor_Load(object sender, EventArgs e)
		{
			grid.Redim(1, 2);
			SGC.ColumnHeader ch = new SGC.ColumnHeader("Параметр");
			ch.AutomaticSortEnabled = false;
			ch.View = vH;
			grid[0, 0] = ch;
			ch = new SGC.ColumnHeader("Значение");
			ch.AutomaticSortEnabled = false;
			ch.View = vH;
			grid[0, 1] = ch;
			Text = string.Format("{0} (Id:{1})",Item.I_Name, Item.I_Id);

			int iR = 0;
			foreach (BO_Field f in Data)
			{
				grid.Rows.Insert(++iR);
				grid[iR, 0] = new SGC.RowHeader(f.Name);
				grid[iR, 0].View = vR;
				grid[iR, 1] = GetCell(f);
				grid[iR, 1].Tag = f;
			}
			grid_SizeChanged(sender, e);
		}
		private SGC.ICell GetCell(BO_Field f, bool bSub = false)
		{
			SGC.ICell ic = new SGC.Cell(f.Value);
			if (f.FlagField)
			{
				f.TmpFlagValue = (int)f.Value;
				SGC.Button b = new SGC.Button(BO_Flag.GetCaption(f));
				b.View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
				if (!f.ReadOnly) b.AddController(ctrlGridEvents);
				return b;
			}
			if (f.Value is int)
			{
				ic.View = vCenter;
				if (!f.ReadOnly) ic.Editor = bSub ? eIntS : eIntP;
			}
			if (f.Value is string)
			{
				ic.View = vLeft;
				if (!f.ReadOnly) ic.Editor = bSub ? eTextS : eTextP;
			}
			if (f.Value is decimal)
			{
				ic.View = vRight;
			}
			if (// f.Value is BK_Density ||
				f.Value is BK_Format ||
				f.Value is BK_Surface ||
				f.Value is Unit ||
                f.Value is BK_DeviceType ||
				f.Value is Company)
			{
				SGC.Button b = new SGC.Button(f.Value);
				b.View.WordWrap = true;
				if (!f.ReadOnly) b.AddController(ctrlGridEvents);
				ic = b;
			}
			return ic;
		}

		public void grid_OnClick(SourceGrid.CellContext cntx, EventArgs e)
		{
			BO_Field f = (BO_Field)((SGC.Cell)cntx.Cell).Tag;
			if (f.FlagField)
			{
				List<BO_Flag> lst = f.FlagList;
				int v = (int)f.TmpFlagValue;
				if (lst.Count == 0) return;
				ContextMenuStrip cms = new ContextMenuStrip();
				foreach (BO_Flag x in lst)
				{
					ToolStripMenuItem tsi = new ToolStripMenuItem(x.Name);
					tsi.Checked = v.HasBit(x.Bit);
					tsi.CheckOnClick = true;
					tsi.Click += grid_MenuChkClick;
					tsi.Tag = new CatEdit_Flag(cntx, x);
					cms.Items.Add(tsi);
				}
				Rectangle r = cntx.Grid.PositionToRectangle(cntx.Position);
				cms.Show(cntx.Grid, new Point(r.Left, r.Bottom));
			}
			if (cntx.Value is BK_Format) { grid_ShowMenu(cntx, typeof(BK_Format)); }
			if (cntx.Value is BK_Density) { grid_ShowMenu(cntx, typeof(BK_Density)); }
			if (cntx.Value is BK_Surface) { grid_ShowMenu(cntx, typeof(BK_Surface)); }
			if (cntx.Value is Unit) { grid_ShowMenu(cntx, typeof(Unit)); }
			if (cntx.Value is Company) { grid_ShowMenu(cntx, typeof(Company)); }
            if (cntx.Value is BK_DeviceType) { grid_ShowMenu(cntx, typeof(BK_DeviceType)); }
        }

		public void grid_ShowMenu(SourceGrid.CellContext cntx, Type tAdd)
		{
			ContextMenuStrip cms = new ContextMenuStrip();

            IEnumerable < IBkObject > lst = (IEnumerable < IBkObject > )tAdd.GetMethod("getAll").Invoke(null, null);
            cntx.Grid.Tag = lst;

            foreach (IBkObject x in lst) cms.Items.Add(x.ToString(), x.I_Icon, grid_MenuItemClick).Tag = new CatEdit_Value(cntx, x);

			cms.Items.Add("-");
			cms.Items.Add("Правка...", RsRc.modify_16, grid_MenuEditClick).Tag = new CatEdit_Type(cntx, tAdd);
			cms.Items.Add("Добавить...", RsRc.Add16, grid_MenuAddClick).Tag = new CatEdit_Type(cntx, tAdd);

            if (cms.Items.Count > 0)
			{
				Rectangle r = cntx.Grid.PositionToRectangle(cntx.Position);
				cms.Show(cntx.Grid, new Point(r.Left, r.Bottom));
			}
		}

		public void grid_MenuItemClick(object sender, EventArgs e)
		{
			CatEdit_Value cev = (CatEdit_Value)((ToolStripItem)sender).Tag;
			cev.cntx.Grid.Tag = null;
			cev.cntx.Value = cev.value;
			grid_OnEditEnded(cev.cntx, new EventArgs());
		}

		public void grid_MenuAddClick(object sender, EventArgs e)
		{
			CatEdit_Type cet = (CatEdit_Type)((ToolStripItem)sender).Tag;
			System.Reflection.ConstructorInfo ci = cet.type.GetConstructor(new Type[] { });
			IBkObject o = (IBkObject)ci.Invoke(new object[] { });
			if (!o.I_OnNew()) return;
			cet.cntx.Value = o;
			grid_OnEditEnded(cet.cntx, new EventArgs());
		}

		public void grid_MenuEditClick(object sender, EventArgs e)
		{
			CatEdit_Type cet = (CatEdit_Type)((ToolStripItem)sender).Tag;
			IBkObject o = (IBkObject)cet.cntx.Value;
			if (!o.I_Edit()) return;
			cet.cntx.Value = o;
			grid_OnEditEnded(cet.cntx, new EventArgs());
		}

		public void grid_MenuChkClick(object sender, EventArgs e)
		{
			CatEdit_Flag cef = (CatEdit_Flag)((ToolStripItem)sender).Tag;
			BO_Field f = (BO_Field)((SGC.Cell)cef.cntx.Cell).Tag;
			f.TmpFlagValue = f.TmpFlagValue.SetBit(cef.flag.Bit, !f.TmpFlagValue.HasBit(cef.flag.Bit));
			cef.cntx.Value = BO_Flag.GetCaption(f);
			grid_OnEditEnded(cef.cntx, new EventArgs());
		}

		public void grid_OnEditEnded(SourceGrid.CellContext cntx, EventArgs e)
		{
			if (cntx.Grid == grid && cntx.Position.Column == 1) grid.Rows.AutoSizeRow(cntx.Position.Row, true, 0, grid.ColumnsCount - 1);
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			for (int i = 1; i < grid.RowsCount; i++)
			{
				BO_Field f = (BO_Field)grid[i, 1].Tag;
				if (!Data[i - 1].ReadOnly) Item.I_SetParam(i - 1, f.FlagField ? f.TmpFlagValue : grid[i, 1].Value);
			}

			Item.I_Store();
			DialogResult = DialogResult.OK;
		}

		private void grid_SizeChanged(object sender, EventArgs e)
		{
			if (grid.Columns.Count < 2) return;
			grid.Columns.AutoSizeColumn(0);
			if (grid.Width < grid.Columns[0].Width + 100) return;
			grid.Columns[1].Width = grid.Width - grid.Columns[0].Width - 4;
			for (int i = 1; i < grid.RowsCount; i++)
				grid.Rows.AutoSizeRow(i, true, 0, grid.ColumnsCount - 1);
		}
	}
	public class frmEditor_GridEvents : SGC.Controllers.ControllerBase
	{
		frmEditor m_frm = null;
		public frmEditor_GridEvents(frmEditor frm) { m_frm = frm; }
		public override void OnClick(SourceGrid.CellContext sender, EventArgs e) { m_frm.grid_OnClick(sender, e); }
		public override void OnEditEnded(SourceGrid.CellContext sender, EventArgs e) { m_frm.grid_OnEditEnded(sender, e); }
	}

	public class CatEdit_Value { public SourceGrid.CellContext cntx; public object value; public CatEdit_Value(SourceGrid.CellContext c, object v) { cntx = c; value = v; } }
	public class CatEdit_Type { public SourceGrid.CellContext cntx; public Type type; public CatEdit_Type(SourceGrid.CellContext c, Type t) { cntx = c; type = t; } }
	public class CatEdit_Flag { public SourceGrid.CellContext cntx; public BO_Flag flag; public CatEdit_Flag(SourceGrid.CellContext c, BO_Flag f) { cntx = c; flag = f; } }
}
