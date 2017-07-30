using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class BillItem
    {
        /// <summary>
        /// Bill item Id in lst
        /// </summary>
        //public int Id = 0;
		public Decimal Count = 0m;
		public Decimal Price = 0m;
		public BK_Item Item = BK_Item.Unset;
		public BK_Carrier Car = BK_Carrier.NotCarrier;
        public BK_Device Device = BK_Device.NullDevice;
        public BK_Variant Variant = BK_Variant.Default;
		public Unit Unit = Unit.Unknown;
		public int BillId = 0;
        /// <summary>
        /// Item was changed
        /// </summary>
		public Boolean m_mod = false;
        public bool Stored = false;
		public BillItem() { }

        /// <summary>
        /// SELECT * FROM bk_list
        /// </summary>
        /// <param name="r">Reader</param>
        public BillItem(denSQL.denReader r, int iOffset = 0)
        {
			int i = iOffset;
			BillId = r.GetInt("bill");
			Item = BK_Item.getItem(r);   // item
            Price = r.GetDecimal("prc");
            Count = r.GetDecimal("cnt");
			Unit = Unit.getUnit(r);   // unit
			Car = BK_Carrier.getCarrier(r); // car
            Device = BK_Device.getDevice(r); // dev
            Variant = BK_Variant.getVariant(r); // var
            Stored = true;
        }

        /// <summary>
        /// New empty BillItem for item
        /// </summary>
        public BillItem(BK_Item it)
        {
            //Id = -1;
			Item = it;
            Device = it.DefaultDevice;
        }

        public void Delete()
        {
            if (!Stored) return;
            denSQL.Command("DELETE FROM bk_list " +
                    "WHERE bill={0}, item={1}, unit={2}, car={3}",
                        BillId, Item.Id, Unit.Id, Car.ItemId);
        }

        public void Save()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_list", "");
            //int r = denSQL.Command("INSERT INTO");
        }

        public Boolean IsChanged { get { return m_mod; } set { m_mod = value; } }
        public decimal Summ { get { return Price * Count; } }

        public override string ToString()
        {
            if (Count == 0) return Item.Name + "=0.00";
			if (Count == 1) return String.Format("{0}={1:0.00}", Item.Name, Price);
			return String.Format("{0}={1:0.00}x{2}{3}", Item.Name, Price, Count, Unit);
        }
        public override int GetHashCode() { return ToString().GetHashCode(); }
        public override bool Equals(object obj)
        {
            if (obj is BillItem) return Item.Id.Equals(((BillItem)obj).Item.Id);
            return base.Equals(obj);
        }
    }
}
