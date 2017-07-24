using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BK_SubItem
	{
		public int Id = 0;
		public int ItemId = 0;
		public BK_Item Sub = BK_Item.Unset;
		public Unit CntUnit = Unit.Unknown;
		public BK_Format Fmt = BK_Format.Unknown;
		public decimal Count = 0;
		public int Param = 0;

        const int TYPE_CARRIER = 0x2;
        const int TYPE_INK = 0x4;

		public BK_SubItem() { }

		public BK_SubItem(denSQL.denReader r)
		{
			Id = r.GetInt("si_id");
			ItemId = r.GetInt("si_iid");
			Sub = BK_Item.getItem(r, "si_sub");
            CntUnit = Unit.getUnit(r, "si_unit");
			Count = r.GetDecimal("si_cnt");
			Param = r.GetInt("si_param");
		}

		public static BK_SubItem Empty = new BK_SubItem();
		public bool IsEmpty { get { return Id == 0; } }

		public bool IsCarrier { get { return Param.HasBit(TYPE_CARRIER); } set { Param = Param.SetBit(TYPE_CARRIER, value); } }
		public bool IsInk { get { return Param.HasBit(TYPE_INK); } set { Param = Param.SetBit(TYPE_INK, value); } }
	}
}
