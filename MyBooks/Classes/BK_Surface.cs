using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
	public class BK_Surface : IBkObject
	{
		public int Id = 0;
		public string Name = "Любая";
		public string Short = "-";
		public string ShortUa = "-";
        public string Unit = "";

		public BK_Surface() { }
		public BK_Surface(denSQL.denReader r)
		{
			Id = r.GetInt("sf_id");
			Name = r.GetString("sf_name");
			Short = r.GetString("sf_short");
			ShortUa = r.GetString("sf_short_ua");
            Unit = r.GetString("sf_unit");
		}

		public static BK_Surface Any = new BK_Surface();
        private static List<BK_Surface> cache = null;

        public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_surface", "sf_id");
			t.AddFld("sf_name", Name);
			t.AddFld("sf_short", Short);
            t.AddFld("sf_short_ua", ShortUa);
            t.AddFld("sf_unit", Unit);
            return t.Store(ref Id);
		}

		public void SetButton(Button b) { b.Text = Name; b.Tag = this; }

        public static void initSurfaces()
        {
            cache = new List<BK_Surface>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_surface ORDER BY sf_id");
            while (r.Read()) cache.Add(new BK_Surface(r));
            r.Close();
        }

        public static BK_Surface getSurface(int id)
        {
            if (cache == null) initSurfaces();
            BK_Surface s = cache.FirstOrDefault(x => x.Id == id);
            return s ?? Any;
        }
        public static BK_Surface getSurface(denSQL.denReader rd, string sField = "car_surface")
        {
            return getSurface(rd.GetInt(sField));
        }

        public static List<BK_Surface> getAll() { return cache; }

        public override string ToString() { return Name.ToString(); }
		public override int GetHashCode() { return Name.GetHashCode(); }

        #region IBkObject
        private bool Edit(bool bNew)
        {
            if (bNew)
            {
                Name = "Тип";
                Short = "Тип";
                ShortUa = "Тип";
                Unit = "г";
            }
            frmEditor f = new frmEditor(this);
            if (f.ShowDialog() == DialogResult.Cancel) return false;
            Store();
            if (Id == 0) return false;
            if (bNew) cache.Add(this);
            return true;
        }
        public int I_Id { get { return Id; } }
		public string I_Name { get { return Name; } }
		public string I_Short { get { return Short; } }
		public System.Drawing.Image I_Icon { get { return Properties.Resources.surface16; } }
		public List<BO_Field> I_Params()
		{
			List<BO_Field> l = new List<BO_Field>();
			l.Add(new BO_Field("Имя", Name, 0, 45));
            l.Add(new BO_Field("Кратко", Short, 0, 10));
            l.Add(new BO_Field("КраткоДок", ShortUa, 0, 45));
            l.Add(new BO_Field("Ед.", Unit, 0, 5));
            return l;
		}
		public void I_SetParam(int idx, object val)
		{
			switch (idx)
			{
				case 0: Name = (string)val; return;
                case 1: Short = (string)val; return;
                case 2: ShortUa = (string)val; return;
                case 3: Unit = (string)val; return;
            }
		}
		public bool I_OnNew() { return Edit(true); }
		public bool I_Edit() { return Edit(false); }
		public bool I_Store() { return Store() != 0; }
		#endregion
	}
}
