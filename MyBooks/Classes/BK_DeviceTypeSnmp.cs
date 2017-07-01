namespace MyBooks
{
    public class BK_DeviceTypeSnmp
    {
        public const int COLOR_ANY = 0;
        public const int COLOR_B_W = 1;
        public const int COLOR_COLOR = 2;

        public int Id = 0;
        public string snmpTotalCounter = "";
        public BK_Format fmt = BK_Format.Unknown;
        public int color = COLOR_ANY;

        public BK_DeviceTypeSnmp(denSQL.denReader r)
        {
            Id = r.GetInt("dt_sid");
            fmt = BK_Format.getFormat(r, "dt_fmt");
            snmpTotalCounter = r.GetString("dt_snmp");
            color = r.GetInt("dt_color");
        }
    }
}
