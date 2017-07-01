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

		public BK_Surface() { }
		public BK_Surface(denSQL.denReader r, int iOffset = 0)
		{
			int i = iOffset;
			Id = r.GetInt(i++);
			Name = r.GetString(i++);
			Short = r.GetString(i++);
			ShortUa = r.GetString(i++);
		}

		public static BK_Surface Any = new BK_Surface();
        private static List<BK_Surface> cache = null;

        public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_surface", "sf_id");
			t.AddFld("sf_name", Name);
			t.AddFld("sf_short", Short);
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
		public int I_Id { get { return Id; } }
		public string I_Name { get { return Name; } }
		public string I_Short { get { return Short; } }
		public System.Drawing.Image I_Icon { get { return MyBooks.Properties.Resources.surface16; } }
		public List<BO_Field> I_Params()
		{
			List<BO_Field> l = new List<BO_Field>();
			l.Add(new BO_Field("", Name, 0, 45));
			return l;
		}
		public void I_SetParam(int idx, object val)
		{
			switch (idx)
			{
				case 0: Name = (string)val; return;
			}
		}
		public bool I_OnNew() { return false; }
		public bool I_Edit() { return false; }
		public bool I_Store() { return Store() != 0; }
		#endregion
	}
}
