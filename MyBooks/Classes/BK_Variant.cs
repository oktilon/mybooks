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
        public int ItemId = 0;
        public string Name = "";
        public int Param = 0;
        public BK_Device Device = BK_Device.NullDevice;
        public List<BK_VarItem> Items = new List<BK_VarItem>();

        public static BK_Variant Default = new BK_Variant();

        public static List<BK_Variant> cache;

        const int K_PARAM_DEFAULT = 0x01;

        public BK_Variant() { }
        public BK_Variant(denSQL.denReader r)
        {
            Id = r.GetInt("v_id");
            ItemId = r.GetInt("v_item");
            Name = r.GetString("v_name");
            Param = r.GetInt("v_param");
            Device = BK_Device.getDevice(r, "v_device");
        }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_variant", "v_id");
            t.AddFld("v_item", ItemId);
            t.AddFld("v_name", Name);
            t.AddFld("v_param", Param);
            t.AddFld("v_device", Device.Id);
            return t.Store(ref Id);
        }

        public string Amount { get { return string.Format("{0} ед.", Items.Count); } }
        public Image ParamImg
        {
            get
            {
                int x = 0;
                Bitmap ret = new Bitmap(1, 16);
                if(isDefault)
                {
                    Bitmap bmp = new Bitmap(x + 16, 16);
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawImage(ret, 0, 0);
                    g.DrawImage(Properties.Resources.ok_16, x, 0, 12, 12);
                    g.Dispose();
                    ret.Dispose();
                    ret = bmp;
                }
                return ret;
            }
        }

        public static void initVariants()
        {
            if (BK_Item.getAll().Count == 0) BK_Item.initItems();
            List<BK_Item> items = new List<BK_Item>();
            if(cache == null) cache = new List<BK_Variant>();
            cache.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_variant");
            while (r.Read())
            {
                BK_Variant v = new BK_Variant(r);
                if (items.Find(x => x.Id == v.ItemId) == null) items.Add(BK_Item.getItem(v.ItemId));
                cache.Add(v);
            }
            r.Close();
            foreach (BK_Variant v in cache) BK_VarItem.getVariantItems(v);
            foreach (BK_Item i in items)
            {
                i.Variants = cache.FindAll(x => x.ItemId == i.Id);
                i.DefaultVariant = cache.Find(x => x.ItemId == i.Id && x.isDefault);
            }
        }

        public bool isDefault
        {
            get { return Param.HasBit(K_PARAM_DEFAULT); }
            set { Param.SetBit(K_PARAM_DEFAULT, value);  }
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
        public static List<BK_Variant> getItemVariants(BK_Item i) { return (from v in cache where v.ItemId == i.Id select v).ToList(); }

        public void SetButton(Button b) { b.Text = Name; b.Tag = this; }

        public override string ToString() { return Name; }
        public override int GetHashCode() { return Id; }

        public IEnumerator GetEnumerator() { return new BK_VarItem_Enum(Items); }
    }
}
