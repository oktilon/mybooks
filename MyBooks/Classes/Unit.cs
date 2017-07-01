using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RsRc = MyBooks.Properties.Resources;
namespace MyBooks
{
	public class Unit : IBkObject
	{
		public int Id = 0;
		public string Name = "<не задана>";
		public string Name2 = "<не задана>";
		public string Name5 = "<не задана>";
		public string Short = "н/д";
		public Unit() { }
		public Unit(denSQL.denReader r) { Id = r.GetInt(0); Name = r.GetString(1); Short = r.GetString(2); Name2 = r.GetString(3); Name5 = r.GetString(4); }

		public static Unit Unknown = new Unit();

        private static List<Unit> cache = null;

		public void SetButton(Button btn)
		{
			btn.Tag = this;
			btn.Text = Short;
		}

		public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_units", "un_id");
			t.AddFld("un_name", Name, 25);
			t.AddFld("un_short", Short, 10);
			t.AddFld("un_name2", Name2, 25);
			t.AddFld("un_name5", Name5, 25);
			return t.Store(ref Id);
		}

		public override string ToString() { return Short; }
		public override int GetHashCode() { return Short.GetHashCode(); }

		private static void UnitsMenu_Sel(object sender, EventArgs e) { ((IBkTag)((ToolStripItem)sender).Tag).Apply(); }
		private static void UnitsMenu_Add(object sender, EventArgs e) { ((IBkTag)((ToolStripItem)sender).Tag).Make(); }
		private static void UnitsMenu_Mdf(object sender, EventArgs e) { ((IBkTag)((ToolStripItem)sender).Tag).Edit(); }

		public static bool UnitsMenu(Button ctrl, EventHandler OnDone = null)
		{
			ContextMenuStrip cms = new ContextMenuStrip();
			Unit uOld = (Unit)ctrl.Tag;
			foreach (Unit u in cache)
			{
				ToolStripMenuItem tsi = new ToolStripMenuItem(u.Name, RsRc.meas_len_16, UnitsMenu_Sel);
				if (u == uOld) tsi.Checked = true;
				tsi.Tag = new ButtonTag(ctrl, u, typeof(Unit), OnDone);
				cms.Items.Add(tsi);
			}
			if (cms.Items.Count == 0) return false;
			cms.Items.Add("-");
			cms.Items.Add("Добавить", RsRc.Add16, UnitsMenu_Add).Tag = new ButtonTag(ctrl, null, typeof(Unit), OnDone);
			cms.Items.Add("Изменить", RsRc.modify_16, UnitsMenu_Mdf).Tag = new ButtonTag(ctrl, uOld, typeof(Unit), OnDone);
			cms.Show(ctrl, 0, ctrl.Height);
			return true;
		}

		public static bool UnitsMenu(SourceGrid.CellContext cntx, EventHandler OnDone = null)
		{
			ContextMenuStrip cms = new ContextMenuStrip();
			foreach (Unit u in cache)
			{
				ToolStripMenuItem tsi = new ToolStripMenuItem(u.Name, RsRc.meas_len_16, UnitsMenu_Sel);
				if (u == (Unit)cntx.Value) tsi.Checked = true;
				tsi.Tag = new ContextTag(cntx, u, typeof(Unit), OnDone);
				cms.Items.Add(tsi);
			}
			if (cms.Items.Count == 0) return false;
			Rectangle r = cntx.Grid.PositionToRectangle(cntx.Position);
			cms.Show(cntx.Grid, new Point(r.Left, r.Bottom));
			return true;
		}

        public static void initUnits()
        {
            cache = new List<Unit>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_units ORDER BY un_id");
            while (r.Read()) cache.Add(new Unit(r));
            r.Close();
        }

        public static void addUnit(denSQL.denReader rd)
        {
            if (cache == null) cache = new List<Unit>();
            cache.Add(new Unit(rd));
        }

        public static Unit getUnit(int id)
        {
            if (cache == null) initUnits();
            Unit it = cache.FirstOrDefault(x => x.Id == id);
            return it ?? Unknown;
        }
        public static Unit getUnit(denSQL.denReader rd, string sField = "unit")
        {
            return getUnit(rd.GetInt(sField));
        }

        public static List<Unit> getAll() { return cache; }

        public bool Edit(bool bNew)
		{
			if (bNew)
			{
				Name = "новая";
				Name2 = "новые";
				Name5 = "новых";
				Short = "нов.";
			}
			frmEditor f = new frmEditor(this);
			if (f.ShowDialog() == DialogResult.Cancel) return false;
			Store();
			if (Id == 0) return false;
			if (bNew) cache.Add(this);
			return true;
		}

		#region IBkObject
		public int I_Id { get { return Id; } }
		public string I_Name { get { return Name; } }
		public string I_Short { get { return Name; } }
		public Image I_Icon { get { return RsRc.meas_len_16; } }
		public List<BO_Field> I_Params()
		{
			List<BO_Field> l = new List<BO_Field>();
			l.Add(new BO_Field("Название", Name, 0, 25));
			l.Add(new BO_Field("Кратко", Short, 0, 10));
			l.Add(new BO_Field("Название (2ед)", Name2, 0, 25));
			l.Add(new BO_Field("Название (5ед)", Name5, 0, 25));
			return l;
		}
		public void I_SetParam(int idx, object val)
		{
			switch (idx)
			{
				case 0: Name = (string)val; return;
				case 1: Short = (string)val; return;
				case 2: Name2 = (string)val; return;
				case 3: Name5 = (string)val; return;
			}
		}
		public bool I_OnNew() { return Edit(true); }
		public bool I_Edit() { return Edit(false); }
		public bool I_Store() { return Store() != 0; }
		#endregion
	}
}
