using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace MyBooks
{
    public class BK_DeviceType : IBkObject
    {
        public int Id = 0;
        public string Name = "Generic";
        public string snmpTotalCounter = "1.3.6.1.2.1.43.10.2.1.4.1.1";
        public List<BK_DeviceTypeSnmp> snmpFormatCounter = new List<BK_DeviceTypeSnmp>();

        private static List<BK_DeviceType> cache;

        public static BK_DeviceType EmptyDeviceType = new BK_DeviceType();

        public BK_DeviceType() { }
        public BK_DeviceType(denSQL.denReader r)
        {
            Id = r.GetInt("dt_id");
            Name = r.GetString("dt_name");
            snmpTotalCounter = r.GetString("dt_total");
        }

        public void readFormats()
        {
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_devtype_snmp WHERE dt_type={0}", Id);
            while(r.Read()) { snmpFormatCounter.Add(new BK_DeviceTypeSnmp(r)); }
            r.Close();
        }

        public int Store()
        {
            return 0;
        }

        public int getTotal(string ip)
        {
            int commlength, miblength, datatype = 0, datalength, datastart;
            int total = 0;
            UInt64 upt = 0u;
            SNMP conn = new SNMP();
            byte[] response = new byte[1024];

            // Send a SysUptime SNMP request
            response = conn.get("get", ip, "public", snmpTotalCounter);
            if (response[0] != 0xff)
            {
                // Get the community and MIB lengths of the response
                commlength = Convert.ToInt16(response[6]);
                miblength = Convert.ToInt16(response[23 + commlength]);

                // Extract the MIB data from the SNMp response
                datatype = Convert.ToInt16(response[24 + commlength + miblength]);
                datalength = Convert.ToInt16(response[25 + commlength + miblength]);
                datastart = 26 + commlength + miblength;

                // The sysUptime value may by a multi-byte integer
                // Each byte read must be shifted to the higher byte order
                while (datalength > 0)
                {
                    upt = (upt << 8) + response[datastart++];
                    datalength--;
                }
                switch (datatype)
                {
                    case 67:
                        total = Convert.ToInt32(upt / 100);
                        break;

                    default:
                        total = Convert.ToInt32(upt);
                        break;
                }
            }
            return total;
        }

        public static void initDeviceTypes()
        {
            cache = new List<BK_DeviceType>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_devtype");
            while (r.Read()) cache.Add(new BK_DeviceType(r));
            r.Close();
            foreach (BK_DeviceType dt in cache) dt.readFormats();
        }

        public static BK_DeviceType getDeviceType(int id)
        {
            if (cache == null) initDeviceTypes();
            BK_DeviceType dt = cache.FirstOrDefault(x => x.Id == id);
            return dt == null ? EmptyDeviceType : dt;
        }
        public static BK_DeviceType getDeviceType(denSQL.denReader rd, string sField = "d_type")
        {
            return getDeviceType(rd.GetInt(sField));
        }

        public static List<BK_DeviceType> getAll() { return cache; }

        public override string ToString() { return Name; }
        public override int GetHashCode() { return Id; }

        public bool Edit(bool bNew)
        {
            if (bNew)
            {
                Name = "Марка";
            }
            frmEditor f = new frmEditor(this);
            if (f.ShowDialog() == DialogResult.Cancel) return false;
            Store();
            if (Id == 0) return false;
            if (bNew) cache.Add(this);
            return true;
        }

        #region IBkObject
        public int I_Id { get { return Id; } }
        public string I_Name { get { return Name; } }
        public string I_Short { get { return Name; } }
        public Image I_Icon { get { return Properties.Resources.meas_16; } }
        public List<BO_Field> I_Params()
        {
            List<BO_Field> l = new List<BO_Field>();
            l.Add(new BO_Field("Имя", Name, 0, 54));
            l.Add(new BO_Field("SNMP", snmpTotalCounter, 0, 54));
            return l;
        }
        public void I_SetParam(int idx, object val)
        {
            switch (idx)
            {
                case 0: Name = (string)val; return;
            }
        }
        public bool I_OnNew() { return Edit(true); }
        public bool I_Edit() { return Edit(false); }
        public bool I_Store() { return Store() != 0; }
        #endregion

    }
}
