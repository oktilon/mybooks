using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBooks
{
	public class ItemUnit
	{
		public int ItemId = 0;

		/// <summary>
		/// Дополнительная еденица измерения
		/// </summary>
		public Unit SubUnit = Unit.Unknown;

		/// <summary>
		/// Из скольки базовых единиц изм. состоит
		/// </summary>
		public decimal Cnt = 0m;

		/// <summary>
		/// Базовая единица измерения
		/// </summary>
		public Unit CntUnit = Unit.Unknown;

		// Non storable
		public bool HasChanged = false;


		public ItemUnit() { }
		public ItemUnit(BK_Item it, Unit u)
		{
			ItemId = it.Id;
			Cnt = 1m;
			SubUnit = u;
			CntUnit = it.MinUnit;
		}
		public ItemUnit(denSQL.denReader r)
		{
			ItemId = r.GetInt("iu_item");
			SubUnit = Unit.getUnit(r, "iu_unit");
			Cnt = r.GetDecimal("iu_rate");
			CntUnit = Unit.getUnit(r, "iu_rate_unit");
		}
		public static void AddDefaultBaseToMin(BK_Item it)
		{
			if (it.BaseUnit == it.MinUnit) return;
			if (it.SubUnits.FirstOrDefault(x => x.SubUnit == it.BaseUnit) != null) return;
			ItemUnit iu = new ItemUnit();
			iu.ItemId = it.Id;
			iu.SubUnit = it.BaseUnit;
			iu.Cnt = 1m;
			iu.CntUnit = it.MinUnit;
			it.SubUnits.Add(iu);
		}
		public static ItemUnit Unset = new ItemUnit();
		public bool IsUnset { get { return SubUnit == Unit.Unknown; } }

		public int Store()
		{
			HasChanged = false;
			object[] oPar = new object[] { ItemId, SubUnit.Id, Cnt, CntUnit.Id };
			denSQL.Command("DELETE FROM bk_iunits WHERE iu_item={0} AND iu_unit={1}", oPar);
			return denSQL.Command("INSERT INTO bk_iunits (iu_item, iu_unit, iu_rate, iu_rate_unit) VALUES ({0}, {1}, {2}, {3})", oPar);
		}
	}
}
