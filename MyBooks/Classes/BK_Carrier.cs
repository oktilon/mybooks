using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BK_Carrier
	{
		public int ItemId = 0;
		public BK_Density Density = BK_Density.ZeroDensity;
		public BK_Format Format = BK_Format.Unknown;
		public BK_Surface Surface = BK_Surface.Any;

        private static List<BK_Carrier> cache = null;

        public BK_Carrier() { }
		public BK_Carrier(int pD, int pF, int pS) { Density = BK_Density.getDensity(pD); Format = BK_Format.getFormat(pF); Surface = BK_Surface.getSurface(pS); }
		public BK_Carrier(denSQL.denReader r)
		{
			ItemId = r.GetInt("car_iid");
			Density = BK_Density.getDensity(r); // car_density
            Format = BK_Format.getFormat(r); // car_fmt
			Surface = BK_Surface.getSurface(r); // car_surface
		}

		public static BK_Carrier NotCarrier = new BK_Carrier();

		public bool IsNotCarrier { get { return Format == BK_Format.Unknown; } }

		public int Store()
		{
			if(ItemId == 0) return 0;
			object[] oPar = new object[] { ItemId, Density, Format.Id, Surface.Id };
			denSQL.Command("DELETE FROM bk_carrier WHERE car_iid={0}", ItemId);
			return denSQL.Command("INSERT INTO bk_carrier (car_iid, car_density, car_fmt, car_surface) VALUES ({0}, {1}, {2}, {3})", oPar);
		}

		public string CaptionUa { get { return ItemId == 0 ? "" : String.Format(" ({0} {1})", Surface.ShortUa, Density); } }
		public string Caption { get { return ItemId == 0 ? "-" : String.Format("{0} {1}", Surface, Density); } }
		public string Short { get { return ItemId == 0 ? "" : String.Format(" ({0}{1})", Surface.Short, Density); } }
		public string ShortCaption { get { return ItemId == 0 ? "" : String.Format("{0} {1}", Surface.Short, Density); } }

        public static void initCarriers()
        {
            cache = new List<BK_Carrier>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_items LEFT JOIN bk_carrier ON car_iid = i_id ORDER BY i_id");
            while (r.Read()) cache.Add(new BK_Carrier(r));
            r.Close();
        }

        public static void addCarrier(denSQL.denReader rd)
        {
            if (cache == null) cache = new List<BK_Carrier>();
            cache.Add(new BK_Carrier(rd));
        }

        public static BK_Carrier getCarrier(int id)
        {
            if (cache == null) initCarriers();
            BK_Carrier it = cache.FirstOrDefault(x => x.ItemId == id);
            return it ?? NotCarrier;
        }
        public static BK_Carrier getCarrier(denSQL.denReader rd, string sField = "item")
        {
            return getCarrier(rd.GetInt(sField));
        }

        public static List<BK_Carrier> getAll() { return cache; }

        public override string ToString() { return ShortCaption; }
		public override int GetHashCode() { return Caption.GetHashCode(); }
	}
}
