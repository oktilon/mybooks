using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class BK_OrderItem
    {
        public int Id;
        public int ItemId;
        public decimal Price;
        public decimal Count;
        public Unit Unit;

        public BK_OrderItem() { Id = 0; ItemId = 0; Price = 0m; Count = 1m; Unit = Unit.getUnit(1); }

        public BK_OrderItem(CatalogItem ci)
        {
            ItemId = ci.Item.Id;
            Price = ci.Price;
            Count = 1;
            Unit = ci.Unit;
        }

        public BK_OrderItem(denSQL.denReader r)
        {
            Id = r.GetInt("sl_id");
            ItemId = r.GetInt("sl_item");
            Price = r.GetDecimal("sl_prc");
            Count = r.GetDecimal("sl_cnt");
            Unit = Unit.getUnit(r, "sl_unit");
        }

        public decimal Total { get { return Price * Count; } }

        internal void SetPrice(string displayText)
        {
            decimal.TryParse(displayText, out Price);
        }
        internal void SetCount(string displayText)
        {
            decimal.TryParse(displayText, out Count);
        }

    }
}
