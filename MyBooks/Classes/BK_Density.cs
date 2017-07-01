using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
	public class BK_Density : IBkObject
	{
		public int Density = 0;

		public BK_Density() { }
		public BK_Density(int i) { Density = i; }
		public BK_Density(denSQL.denReader r) { Density = r.GetInt("car_density"); }

		public static BK_Density ZeroDensity = new BK_Density();

        private static List<BK_Density> cache = null;

        public int Store() { return 0; }

		public void SetButton(Button b) { b.Text = Density.ToString(); b.Tag = this; }

		public override string ToString() { return Density.ToString(); }
		public override int GetHashCode() { return Density.GetHashCode(); }

        public bool Edit(bool bNew)
        {
            frmText f = new frmText(
                ToString(),
                string.Format("{0} плотность", bNew ? "Новая" : "Изменить"),
                false,
                45,
                MyBooks.Properties.Resources.density16
            );
            if (f.ShowDialog() == DialogResult.Cancel) return false;
            if (!int.TryParse(f.TText, out int i)) return false;
            Density = i;
            if (bNew) cache.Add(this);
            return true;
        }

        public static void initDenities()
        {
            cache = new List<BK_Density>();
            denSQL.denReader r = denSQL.Query("SELECT DISTINCT car_density FROM bk_carrier ORDER BY car_density");
            while (r.Read()) cache.Add(new BK_Density(r));
            r.Close();
        }

        private static BK_Density addDensity(int dens)
        {
            if (cache == null) cache = new List<BK_Density>();
            BK_Density d = new BK_Density(dens);
            cache.Add(d);
            return d;
        }

        public static BK_Density getDensity(int id)
        {
            if (cache == null) initDenities();
            BK_Density d = cache.FirstOrDefault(x => x.Density == id);
            return d ?? addDensity(id);
        }
        public static BK_Density getDensity(denSQL.denReader rd, string sField = "car_density")
        {
            return getDensity(rd.GetInt(sField));
        }

        public static List<BK_Density> getAll() { return cache; }

        #region IBkObject
        public int I_Id { get { return Density; } }
		public string I_Name { get { return Density.ToString(); } }
		public string I_Short { get { return Density.ToString(); } }
		public System.Drawing.Image I_Icon { get { return MyBooks.Properties.Resources.density16; } }
		public List<BO_Field> I_Params() { return new List<BO_Field>(); }
        public void I_SetParam(int idx, object val) { }
		public bool I_OnNew() { Density = 10; return Edit(true); }
		public bool I_Edit(){ return Edit(false); }
		public bool I_Store() { return true; }
		#endregion
	}
}
