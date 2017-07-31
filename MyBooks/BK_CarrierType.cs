using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBooks
{
    public class BK_CarrierType
    {
        public int Id = 0;
        public string Name = "";

        private static List<BK_CarrierType> cache = null;

        public BK_CarrierType() { }
        public BK_CarrierType(denSQL.denReader r)
        {
            Id = r.GetInt("car_tid");
            Name = r.GetString("car_name");
        }

        public static BK_CarrierType Unknown = new BK_CarrierType();

        public bool IsUnknown { get { return Id == 0; } }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_carrier_type", "car_tid");
            t.AddFld("car_name", Name);
            return t.Store(ref Id);
        }

        public static void initCarrierTypes()
        {
            cache = new List<BK_CarrierType>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_carrier_type");
            while (r.Read()) cache.Add(new BK_CarrierType(r));
            r.Close();
        }

        public static BK_CarrierType getCarrierType(int id)
        {
            if (cache == null) initCarrierTypes();
            BK_CarrierType it = cache.FirstOrDefault(x => x.Id == id);
            return it ?? Unknown;
        }
        public static BK_CarrierType getCarrierType(denSQL.denReader rd, string sField = "car_type")
        {
            return getCarrierType(rd.GetInt(sField));
        }

        public static List<BK_CarrierType> getAll() { return cache; }

        public override string ToString() { return Name; }
        public override int GetHashCode() { return Id; }
    }
}
