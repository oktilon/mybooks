using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace MyBooks
{
    public class BK_Variant : IEnumerable
    {
        public int Id = 0;
        public string Name = "";
        public int Param = 0;
        public List<BK_VarItem> Items = new List<BK_VarItem>();

        public static BK_Variant Default = new BK_Variant();

        public static List<BK_Variant> cache;

        public BK_Variant() { }
        public BK_Variant(denSQL.denReader r)
        {
            Id = r.GetInt("v_id");
            Name = r.GetString("v_name");
            Param = r.GetInt("v_param");
        }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_variant", "v_id");
            t.AddFld("v_name", Name);
            t.AddFld("v_param", Param);
            return t.Store(ref Id);
        }

        public bool isDefault { get { return Id == 0; } }

        public string Amount { get { return string.Format("{0} ед.", Items.Count); } }

        public static void initVariants()
        {
            if (BK_Item.getAll().Count == 0) BK_Item.initItems();
            if(cache == null) cache = new List<BK_Variant>();
            cache.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_variant");
            while (r.Read()) cache.Add(new BK_Variant(r));
            r.Close();
        }

        public static BK_Variant getVariant(int id)
        {
            if (cache == null) initVariants();
            BK_Variant dev = cache.FirstOrDefault(x => x.Id == id);
            return dev ?? Default;
        }
        public static BK_Variant getVariant(denSQL.denReader rd, string sField = "var")
        {
            return getVariant(rd.GetInt(sField));
        }

        public static List<BK_Variant> getAll() { return cache; }

        public void SetButton(Button b) { b.Text = Name; b.Tag = this; }

        public override string ToString() { return Name; }
        public override int GetHashCode() { return Id; }

        public IEnumerator GetEnumerator() { return new BK_VarItem_Enum(Items); }
    }
}
