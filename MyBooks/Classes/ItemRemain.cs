﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class ItemRemain
	{
		public int ItemId = 0;
		public BK_Point Pnt = BK_Point.Unknown;
		public decimal Cnt = 0m;

		public ItemRemain() { }
		public ItemRemain(denSQL.denReader r)
		{
			ItemId = r.GetInt("w_item");
			Pnt = BK_Point.getPoint(r, "w_point");
			Cnt = r.GetDecimal("w_cnt");
		}

		public int Store()
		{
			if (ItemId == 0 || Pnt.Id == 0) return 0;
			object[] oPar = new object[] { ItemId, Pnt.Id, Cnt };
			return denSQL.Command("INSERT INTO bk_ware VALUES ({0},{1},{2}) " +
                                "ON DUPLICATE KEY UPDATE " +
                                "w_cnt = {2}", oPar);
		}
		public override string ToString()
		{
			return String.Format("[item:{0}] {1}:{2}pcs.", ItemId, Pnt.Name, Cnt);
		}
		public override int GetHashCode() { return ToString().GetHashCode(); }

	}
}
