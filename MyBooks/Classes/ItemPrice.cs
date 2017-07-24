﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class ItemPrice
	{
		public int ItemId = 0;
		public BK_Point Pnt = BK_Point.self;
		public Unit Unit = Unit.Unknown;
		public BK_Carrier Carrier = BK_Carrier.NotCarrier;
        public BK_Variant Variant = BK_Variant.Default;
		public decimal Prc = 0m;
        public int Consumption = 0;
		// Non storable
		public bool HasChanged = false;
        public bool bDeleted = false;

		public ItemPrice() { }
		public ItemPrice(BK_Item i, decimal p = 1, PrcAddTag mt = null) {
            HasChanged = true;
            ItemId = i.Id;
            Prc = p;
            if (mt == null) return;
            Pnt = mt.p;
            Unit = mt.u;
            if (mt.c == null) return;
            Carrier = mt.c;
            Consumption = 1;
        }
        public ItemPrice(BK_Item i, CatalogItem ci)
        {
            ItemId = i.Id;
            Prc = decimal.Round(ci.Price * ci.Com.Coeff, 2);
            Pnt = BK_Point.self;
            Unit = ci.Unit;
            HasChanged = true;
        }

        /// <summary>
        /// SELECT * FROM bk_price WHERE p_item=BK_Item.Id
        /// </summary>
        public ItemPrice(denSQL.denReader r)
		{
			ItemId = r.GetInt("p_item");
			Pnt = BK_Point.getPoint(r); // p_point
            Unit = Unit.getUnit(r, "p_unit");
            Carrier = BK_Carrier.getCarrier(r, "p_car");
			Prc = r.GetDecimal("p_prc");
            Consumption = r.GetInt("p_car_cnt");
		}

		public int Store(int iItem = -1)
		{
            if (!HasChanged) return 0;
            if (iItem > 0 && ItemId == 0) ItemId = iItem;
            HasChanged = false;
            object[] oPar = new object[] { ItemId, Pnt.Id, Unit.Id, Carrier.ItemId, Prc.ToString(denSQL.Cif_sql), Consumption };
			//denSQL.Command("DELETE FROM bk_price WHERE p_item={0} AND p_point={1} AND p_unit={2} AND p_car={3}", oPar);
			return denSQL.Command("INSERT INTO bk_price (p_item, p_point, p_unit, p_car, p_prc, p_car_cnt) " +
                                "VALUES ({0}, {1}, {2}, {3}, {4}, {5}) " +
                                "ON DUPLICATE KEY UPDATE " +
                                "p_prc = {4}," +
                                "p_car_cnt = {5}", oPar);
		}

        public static void readItemPrices(BK_Item it)
        {
            it.Prices.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_price " +
                        "LEFT JOIN bk_variant ON v_id = p_var " +
                        "WHERE p_item={0} ORDER BY p_point, p_unit, p_car, p_var", it.Id);
            while (r.Read())
            {
                ItemPrice ip = new ItemPrice(r);
                if (!r.IsNull("v_id"))
                {
                    ip.Variant = new BK_Variant(r);
                    if (ip.Variant.isDefault) it.DefaultVariant = ip.Variant;
                }
                it.Prices.Add(ip);
            }
            r.Close();
        }


        public int CalculateRemain()
        {
            return 0;
        }

        public void setPrice(string prc)
        {
            decimal p = 0;
            if (decimal.TryParse(prc, out p)) setPrice(p);
        }

        public void setPrice(decimal prc)
        {
            if(Prc != prc)
            {
                Prc = prc;
                HasChanged = true;
            }
        }

        public void setConsumption(string cnt)
        {
            int c = 0;
            if (int.TryParse(cnt, out c) && Consumption != c)
            {
                Consumption = c;
                HasChanged = true;
            }
        }

        public void Delete()
        {
            bDeleted = true;
        }

		public override string ToString()
		{
			return String.Format("[it:{0},pt:{1}] {2}, {3} per {4}", ItemId, Pnt.Name, Carrier, Prc, Unit);
		}
		public override int GetHashCode() { return ToString().GetHashCode(); }

	}
}
