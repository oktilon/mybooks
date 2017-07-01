using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BK_Point : IBkObject
	{
		public int Id = 0;
		public string Name = "Неизвестная точка";
		public string Adr = "не доступен";

        public static BK_Point Unknown = new BK_Point();

        private static List<BK_Point> cache = null;
        public static BK_Point self = Unknown;

        public BK_Point() { }
		public BK_Point(denSQL.denReader r) : this(r, 0) { }
		public BK_Point(denSQL.denReader r, int iOffset)
		{
			int i = iOffset;
			Id = r.GetInt(i++);
			Name = r.GetString(i++);
			Adr = r.GetString(i++);
		}

		public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_point", "pt_id");
			t.AddFld("pt_name", Name);
			t.AddFld("pt_adr", Adr);
			return t.Store(ref Id);
		}

        public static void initPoints()
        {
            Properties.Settings m_Settings = Properties.Settings.Default;

            cache = new List<BK_Point>();
            if (m_Settings.pointID == 0) cache.Add(Unknown);
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_point ORDER BY pt_id");
            while (r.Read())
            {
                BK_Point pt = new BK_Point(r);
                cache.Add(pt);
                if (pt.Id == m_Settings.pointID) self = pt;
            }
            r.Close();
        }

        public static void addPoint(denSQL.denReader rd)
        {
            if (cache == null) cache = new List<BK_Point>();
            cache.Add(new BK_Point(rd));
        }

        public static BK_Point getPoint(int id)
        {
            if (cache == null) initPoints();
            BK_Point pt = cache.FirstOrDefault(x => x.Id == id);
            return pt ?? Unknown;
        }
        public static BK_Point getPoint(denSQL.denReader rd, string sField = "p_point")
        {
            return getPoint(rd.GetInt(sField));
        }

        public static List<BK_Point> getAll() { return cache; }

        public override string ToString() { return Name; }
		public override int GetHashCode() { return Name.GetHashCode(); }

		#region IBkObject
		public int I_Id { get { return Id; } }
		public string I_Name { get { return Name; } }
		public string I_Short { get { return Name; } }
		public System.Drawing.Image I_Icon { get { return MyBooks.Properties.Resources.home_16; } }
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
