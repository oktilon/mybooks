using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using RsRc = MyBooks.Properties.Resources;

namespace MyBooks
{
    public class BK_Icon : IBkObject
    {
        public int Id = 0;
        public Image Image = null;

        public BK_Icon() { Image = new Bitmap(16, 16); }
        public BK_Icon(Image i) { Image = i; }
        public BK_Icon(denSQL.denReader r)
        {
            Id = r.GetInt(0);
            Byte[] aPng = (Byte[])r.GetValue(1);
            using (MemoryStream ms = new MemoryStream(aPng))
            {
                Image = Image.FromStream(ms);
            }
        }

        public static BK_Icon Default = new BK_Icon(RsRc.Dot16);
        private static List<BK_Icon> cache = null;

        public override string ToString() { return string.Format("[{0}]={1}x{2}", Id, Image.Width, Image.Height); }
        public override int GetHashCode() { return Image.GetHashCode(); }

        public static void initIcons()
        {
            cache = new List<BK_Icon>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_ico ORDER BY ico_id");
            while (r.Read()) cache.Add(new BK_Icon(r));
            r.Close();
        }

        public static BK_Icon getIcon(int id)
        {
            if (cache == null) initIcons();
            BK_Icon it = cache.FirstOrDefault(x => x.Id == id);
            return it ?? Default;
        }
        public static BK_Icon getIcon(denSQL.denReader rd, string sField = "i_ico")
        {
            return getIcon(rd.GetInt(sField));
        }

        public static List<BK_Icon> getAll() { return cache; }


        #region IBkObject
        public int I_Id { get { return Id; } }
        public string I_Name { get { return ""; } }
        public string I_Short { get { return ""; } }
        public System.Drawing.Image I_Icon { get { return Image; } }
        public List<BO_Field> I_Params() { return new List<BO_Field>(); }
        public void I_SetParam(int idx, object val) { }
        public bool I_OnNew() { return false; }
        public bool I_Edit() { return false; }
        public bool I_Store() { return false; }
        #endregion
    }
}
