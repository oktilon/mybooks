using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
    public class BK_Device : IBkObject
    {
        public int Id = 0;
        public string Name = "не указан";
        public string Short = "н/у";
        public BK_DeviceType DevType = BK_DeviceType.EmptyDeviceType;
        public string ip = "127.0.0.1";

        private static List<BK_Device> cache;

        public static BK_Device NullDevice = new BK_Device();

        const int K_DEV_DEFAULT = 0x01;


        public BK_Device() { }
        public BK_Device(denSQL.denReader r)
        {
            Id = r.GetInt("d_id");
            Name = r.GetString("d_name");
            Short = r.GetString("d_short");
            DevType = BK_DeviceType.getDeviceType(r);
            ip = r.GetString("d_ip");
        }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_device", "d_id");
            t.AddFld("d_name", Name);
            t.AddFld("d_short", Short);
            t.AddFld("d_type", DevType.Id);
            t.AddFld("d_ip", ip);
            return t.Store(ref Id);
        }

        public bool IsNull {  get { return Id == 0; } }

        public static void initDevices()
        {
            cache = new List<BK_Device>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_device");
            while (r.Read()) cache.Add(new BK_Device(r));
            r.Close();
        }

        public static BK_Device getDevice(int id)
        {
            if (cache == null) initDevices();
            BK_Device dev = cache.FirstOrDefault(x => x.Id == id);
            return dev ?? NullDevice;
        }
        public static BK_Device getDevice(denSQL.denReader rd, string sField = "device")
        {
            return getDevice(rd.GetInt(sField));
        }

        public static List<BK_Device> getAll() { return cache; }

        public static void getItemDevices(BK_Item i)
        {
            i.Devices.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_item_devices " +
                                            "WHERE item_id = {0}", i.Id);
            while(r.Read())
            {
                BK_Device d = getDevice(r, "dev_id");
                if (r.GetInt("param").HasBit(K_DEV_DEFAULT)) i.DefaultDevice = d;
                i.Devices.Add(d);
            }
            r.Close();
        }

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
        public Image I_Icon { get { return Properties.Resources.device_16; } }
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
