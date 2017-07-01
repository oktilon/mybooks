using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    public class BK_Format : IBkObject
    {
        public int Id = 0;
        public string Name = "н/д";
        public int Width = 0;
        public int Height = 0;

        public BK_Format() { }
        public BK_Format(int i, string s) { Id = i; Name = s; }
        public BK_Format(denSQL.denReader r)
        {
            Id = r.GetInt("id_fmt");
            Name = r.GetString("fmt_name");
            Width = r.GetInt("fmt_width");
            Height = r.GetInt("fmt_height");
        }

        public static BK_Format Unknown = new BK_Format();

        private static List<BK_Format> cache = null;

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_formats", "id_fmt");
            t.AddFld("fmt_name", Name);
            t.AddFld("fmt_width", Width);
            t.AddFld("fmt_height", Height);
            return t.Store(ref Id);
        }

		public void SetButton(Button b) { b.Text = Name; b.Tag = this; }

        public static void initFormats()
        {
            cache = new List<BK_Format>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_formats ORDER BY id_fmt");
            while (r.Read()) cache.Add(new BK_Format(r));
            r.Close();
        }

        public static void addFormat(denSQL.denReader rd)
        {
            if (cache == null) cache = new List<BK_Format>();
            cache.Add(new BK_Format(rd));
        }

        public static BK_Format getFormat(int id)
        {
            if (cache == null) initFormats();
            BK_Format f = cache.FirstOrDefault(x => x.Id == id);
            return f ?? Unknown;
        }
        public static BK_Format getFormat(denSQL.denReader rd, string sField = "car_fmt")
        {
            return getFormat(rd.GetInt(sField));
        }

        public static List<BK_Format> getAll() { return cache; }

        public override string ToString() { return string.Format("{0} ({1}x{2})", Name, Width, Height); }
        public override int GetHashCode() { return Name.GetHashCode(); }

		#region IBkObject
        private bool Edit(bool bNew)
        {
            if(bNew)
            {
                Name = "A";
                Width = 100;
                Height = 200;
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
		public string I_Short { get { return Name; } }
		public System.Drawing.Image I_Icon { get { return MyBooks.Properties.Resources.format16; } }
		public List<BO_Field> I_Params()
		{
			List<BO_Field> l = new List<BO_Field>();
			l.Add(new BO_Field("", Name, 0, 45));
            l.Add(new BO_Field("Ширина", Width, 0, 10));
            l.Add(new BO_Field("Высота", Height, 0, 10));
            return l;
		}
		public void I_SetParam(int idx, object val)
		{
			switch (idx)
			{
				case 0: Name = (string)val; return;
                case 1: if (!int.TryParse((string)val, out Width)) { Width = 0; } return;
                case 2: if (!int.TryParse((string)val, out Height)) { Height = 0; } return;
            }
		}
		public bool I_OnNew() { return Edit(true); }
		public bool I_Edit() { return Edit(false); }
		public bool I_Store() { return Store() != 0; }
		#endregion
    }
}
