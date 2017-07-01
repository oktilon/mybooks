﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBooks
{
    public class CatalogItem
	{
		public int Id = 0;
		public string Code = "";
		public decimal Price = 0;
		public Unit Unit;
		public Company Com;
		public DateTime dtUpdated;
        public BK_Item Item;

        private static CatalogItem Unknown = new CatalogItem();

        private static List<CatalogItem> cache = null;

        public CatalogItem()
        {
            Item = BK_Item.Unset;
            Com = Company.AllSuppliers;
            Unit = Unit.Unknown;
            dtUpdated = DateTime.Now;
        }
        public CatalogItem(denSQL.denReader r)
		{
			Id = r.GetInt("ct_id");
			Code = r.GetString("ct_code");
			Price = r.GetDecimal("ct_price");
			Unit = Unit.getUnit(r, "ct_unit");
			dtUpdated = r.GetDateTime("ct_updated");
            Com = Company.getCompany(r, "ct_com");
            Item = BK_Item.getItem(r, "ct_item");
		}

        public CatalogItem(BK_OrderItem oi, Company sup)
        {
            Price = oi.Price;
            Unit = oi.Unit;
            Com = sup;
            Item = BK_Item.getItem(oi.ItemId);
            dtUpdated = DateTime.Now;
        }
        public CatalogItem(BK_Item it, Company sup)
        {
            Unit = Unit.getUnit(1);
            Com = sup;
            Item = it;
            dtUpdated = DateTime.Now;
        }

        public bool Match(string sFind, Company sup)
        {
            if (!Item.Name.ToLower().Contains(sFind.ToLower())) return false;
            return sup.Id == 0 ? true : sup.Id == Com.Id;
        }

        public static string queryBase(string sWhere = "", string sOrder = "")
        {
            if (sWhere != "") sWhere = "WHERE " + sWhere;
            if (sOrder != "") sOrder = "ORDER BY " + sOrder;
            string ret = string.Format("SELECT * FROM bk_cat " +
                                    "{0} {1}", sWhere, sOrder);
            return ret;
        }

        public static void initCatalog()
        {
            if (cache == null)
            {
                cache = new List<CatalogItem>();
            }
            else
            {
                cache.Clear();
            }
            denSQL.denReader r = denSQL.Query(queryBase());
            while (r.Read()) cache.Add(new CatalogItem(r));
            r.Close();
        }

        public static CatalogItem getCatalogItem(int id)
        {
            if (cache == null) initCatalog();
            CatalogItem c = cache.FirstOrDefault(x => x.Id == id);
            return c ?? Unknown;
        }
        public static CatalogItem getCatalogItem(denSQL.denReader rd, string sField = "u_com")
        {
            return getCatalogItem(rd.GetInt(sField));
        }

        public static List<CatalogItem> getAll() { return cache; }



        public override string ToString() { return Item.Name; }
		public override int GetHashCode() { return Id; }
	}
}

