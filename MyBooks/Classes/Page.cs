using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using RsRc = MyBooks.Properties.Resources;
using Sgc = SourceGrid.Cells;

namespace MyBooks
{
    public class Page
    {
		//
		// Store data
		//
		public int Id = 0;
		public string Tag = "unk" + iCnt.ToString();
		public string Caption = "Не задана";
		public int Pos = 0;
        public PageType type = PageType.Default;
        //
        // UI data
        //
        public static int iCnt = 0;
		public TabPage TabPage = null;
		public SourceGrid.Grid Grid = null;
        public Sgc.Editors.TextBoxCurrency EdMoney = null;
        public Sgc.Editors.TextBoxNumeric EdCnt = null;

        private static List<Page> cache = null;

        public Page() { iCnt++; }

		public Page(denSQL.denReader r)
		{
			iCnt++;
			Id = r.GetInt("id_tab");
			Tag = r.GetString("t_tag");
			Caption = r.GetString("t_name");
			Pos = r.GetInt("t_pos");
            type = PageType.getPageType(r);
		}

		public static Page NullPage = new Page();

		public void SetGrid(Size szTabs, int iTabStop)
		{
			TabPage = new TabPage(Caption);
			int dx = 4;
			int dy = 22;
			int wd = szTabs.Width - dx - dx;
			int ht = szTabs.Height - dy - dx;
			Grid = new SourceGrid.Grid();
			//m_Tab
			TabPage.BackColor = Color.Transparent;
			TabPage.Controls.Add(Grid);
			TabPage.Location = new Point(dx, dy);
			TabPage.Name = "tab" + Tag.ToUpper();
			TabPage.Tag = Tag;
			//m_Tab.Padding = new System.Windows.Forms.Padding(3);
			TabPage.Size = new Size(wd, ht);
			TabPage.TabIndex = iTabStop;
			TabPage.Text = Caption;
			TabPage.UseVisualStyleBackColor = true;
			// 
			// grid
			// 
			wd -= dx + dx;
			ht -= dx + dx;
			Grid.Anchor = ((System.Windows.Forms.AnchorStyles)15);
			Grid.Location = new Point(dx, dx);
			Grid.Name = "grid" + Tag.ToUpper();
			Grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
			Grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
			Grid.Size = new Size(wd, ht);
			Grid.TabIndex = iTabStop + 1;
			Grid.TabStop = true;
			Grid.Tag = Tag;
			Grid.ToolTipText = "";
			// Editors for Page
			// Money
			EdMoney = new Sgc.Editors.TextBoxCurrency(typeof(decimal));
			EdMoney.CultureInfo = Program.m_cif;
			EdMoney.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.DoubleClick;
			// Count
			EdCnt = new Sgc.Editors.TextBoxNumeric(typeof(decimal));
			EdCnt.EditableMode = SourceGrid.EditableMode.AnyKey | SourceGrid.EditableMode.SingleClick;
			EdCnt.ConvertingValueToDisplayString += frmBill.Cnt_ConvertingValueToDisplayString;
		}

		public void ResetGrid()
		{
			Grid = null;
			TabPage = null;
			EdMoney = null;
			EdCnt = null;
		}

        public void setGridHeaders(Boolean bAutoSortOn) { setGridHeaders(bAutoSortOn, false); }
        public void setGridHeaders(Boolean bAutoSortOn, bool bAutoSize)
        {
            Int32 iCols = type.Cols;
            Grid.FixedRows = 1;
            Sgc.ColumnHeader ch;
            if (iCols < 1 && iCols > 100) return;
            Grid.Redim(1, iCols);
            for (int iCol = 0; iCol < iCols; iCol++)
            {
                if (type.Width.ElementAt(iCol) > 0)
                {
                    Grid.Columns[iCol].Width = type.Width.ElementAt(iCol);
                    Grid.Columns[iCol].AutoSizeMode = bAutoSize ? SourceGrid.AutoSizeMode.EnableAutoSize : SourceGrid.AutoSizeMode.None;
                }
                ch = new Sgc.ColumnHeader(type.Names.ElementAt(iCol));
                ch.AutomaticSortEnabled = bAutoSortOn;
                //ch.View.Ba
                ch.View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                Grid[0, iCol] = ch;
            }
        }

        public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_tabs", "id_tab");
			t.AddFld("t_tag", Tag);
			t.AddFld("t_name", Caption);
			t.AddFld("t_pos", Pos);
			return t.Store(ref Id);
		}

        public static void initPages()
        {
            cache = new List<Page>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_tabs ORDER BY id_tab");
            while (r.Read()) cache.Add(new Page(r));
            r.Close();
        }

        public static void ResetPages()
        {
            foreach (Page p in cache) p.ResetGrid();
        }

        public static void addPage(denSQL.denReader rd)
        {
            if (cache == null) cache = new List<Page>();
            cache.Add(new Page(rd));
        }

        public static Page getPage(int id)
        {
            if (cache == null) initPages();
            Page p = cache.FirstOrDefault(x => x.Id == id);
            return p ?? NullPage;
        }
        public static Page getPage(denSQL.denReader rd, string sField = "i_tab")
        {
            return getPage(rd.GetInt(sField));
        }
        public static Page getPage(TabPage tab)
        {
            if (cache == null) initPages();
            Page p = cache.FirstOrDefault(x => x.TabPage == tab);
            return p ?? NullPage;
        }
        public static Page getPage(string tag)
        {
            if (cache == null) initPages();
            Page p = cache.FirstOrDefault(x => x.Tag == tag);
            return p ?? NullPage;
        }

        public static List<Page> getAll() { return cache; }
        /// <summary>
        /// Get pages ordered by position
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Page> getOrdered() { return cache.OrderBy(x => x.Pos); }


        public override string ToString() { return Caption + " [" + Tag + "]"; }
        public override int GetHashCode() { return ToString().GetHashCode(); }
    }
}
