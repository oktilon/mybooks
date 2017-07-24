using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class BK_OrderItem
    {
        public int OrderId;
        public BK_Item Item;
        public decimal Price;
        public int Count;
        public Unit Unit;

        public bool ItemChanged = false;

        public BK_OrderItem() { OrderId = 0; Item = BK_Item.Unset; Price = 0m; Count = 1; Unit = Unit.getUnit(1); }

        public BK_OrderItem(BK_Order ord, CatalogItem ci)
        {
            OrderId = ord.Id;
            Item = ci.Item;
            Price = ci.Price;
            Count = 1;
            Unit = ci.Unit;
        }

        public BK_OrderItem(denSQL.denReader r)
        {
            OrderId = r.GetInt("sl_sup");
            Item = BK_Item.getItem(r, "sl_item");
            Price = r.GetDecimal("sl_prc");
            Count = r.GetInt("sl_cnt");
            Unit = Unit.getUnit(r, "sl_unit");
        }

        public int Store(BK_Order o)
        {
            if (OrderId == 0) OrderId = o.Id;
            string p = Price.ToString(denSQL.Cif_sql);
            string c = Count.ToString(denSQL.Cif_sql);
            int ret = denSQL.Command("INSERT INTO bk_slist " +
                                "VALUES ({0}, {1}, {2}, {3}, {4}) " +
                                "ON DUPLICATE KEY UPDATE " +
                                "sl_prc = {2}," +
                                "sl_cnt = {3}," +
                                "sl_unit = {3}",
                                OrderId, Item.Id, p, c, Unit.Id);
            Item.updateRemains(o.Point);
            return ret;
        }

        public ItemPrice addNewPrice(CatalogItem ci)
        {
            ItemPrice ip = new ItemPrice(Item, ci);
            Item.Prices.Add(ip);
            return ip;
        }

        public decimal Total { get { return Price * Count; } }

        internal void SetPrice(string displayText)
        {
            decimal.TryParse(displayText, out Price);
        }
        internal void SetCount(string displayText)
        {
            int.TryParse(displayText, out Count);
        }

    }
}
