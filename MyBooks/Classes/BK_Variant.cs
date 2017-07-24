using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class BK_Variant
    {
        public int Id = 0;
        public int ItemId = 0;
        public string Name = "";
        public int Param = 0;
        public List<BK_SubItem> Items = new List<BK_SubItem>();

        public static BK_Variant Default = new BK_Variant();

        const int K_PARAM_DEFAULT = 0x01;

        public BK_Variant() { }
        public BK_Variant(denSQL.denReader r)
        {
            Id = r.GetInt("v_id");
            ItemId = r.GetInt("v_item");
            Name = r.GetString("v_name");
            Param = r.GetInt("v_param");
        }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_variant", "v_id");
            t.AddFld("v_item", ItemId);
            t.AddFld("v_name", Name);
            t.AddFld("v_param", Param);
            return t.Store(ref Id);
        }

        public static List<BK_Variant> getItemVariants(BK_Item it)
        {
            List<BK_Variant> ret = new List<BK_Variant>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_variant WHERE v_item ={0}", it.Id);
            while (r.Read())
            {
                BK_Variant v = new BK_Variant(r);
                if (v.isDefault) it.DefaultVariant = v;
                ret.Add(v);
            }
            r.Close();
            return ret;
        }

        public bool isDefault
        {
            get { return Param.HasBit(K_PARAM_DEFAULT); }
            set { Param.SetBit(K_PARAM_DEFAULT, value);  }
        }

        public static BK_Variant getDevice(int id)
        {
            if (cache == null) initDevices();
            BK_Variant dev = cache.FirstOrDefault(x => x.Id == id);
            return dev ?? NullDevice;
        }
        public static BK_Variant getDevice(denSQL.denReader rd, string sField = "device")
        {
            return getDevice(rd.GetInt(sField));
        }

        public static List<BK_Variant> getAll() { return cache; }

        public void SetButton(Button b) { b.Text = Name; b.Tag = this; }

        public override string ToString() { return Short; }
        public override int GetHashCode() { return Id; }

        #region IBkObject
        private bool Edit(bool bNew)
        {
            if (bNew)
            {
                Name = "Принтер";
                Short = "Принтер";
                ip = "192.168.0.1";
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
        public Image I_Icon { get { return Properties.Resources.bank16; } }
        public List<BO_Field> I_Params()
        {
            List<BO_Field> l = new List<BO_Field>
            {
                new BO_Field("Имя", Name, 0, 45),
                new BO_Field("Кратко", Short, 0, 45),
                new BO_Field("IP-адрес", ip, 0, 45),
                new BO_Field("Тип", DevType, 0, 45)
            };
            return l;
        }
        public void I_SetParam(int idx, object val)
        {
            switch (idx)
            {
                case 0: Name = (string)val; return;
                case 1: Short = (string)val; return;
                case 2: ip = (string)val; return;
                case 3: DevType = (BK_DeviceType)val; return;
            }
        }
        public bool I_OnNew() { return Edit(true); }
        public bool I_Edit() { return Edit(false); }
        public bool I_Store() { return Store() != 0; }
        #endregion
    }
}
