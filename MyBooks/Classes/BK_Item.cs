using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class BK_Item
	{
		public int Id = 0;
		public string Name = "не указан";
		public string Short = "н/у";
		public string NameUa = "не вказано";
		public Unit DefaultUnit = Unit.Unknown;
		public Unit MinUnit = Unit.Unknown;
		public Page Tab = Page.NullPage;
		public int TabPos = 0;
		public int Param = 0;
		public BK_Icon Ico = BK_Icon.Default;
		public string Desc = "";
		public string Articul = "";
		public Company Manufacturer = Company.Unknown;
		public BK_Format ServiceFormat = BK_Format.Unknown;
        public int Group = 0;
		public BK_Carrier Carrier = BK_Carrier.NotCarrier;
        public List<BK_Device> Devices = new List<BK_Device>();
		public List<ItemPrice> Prices = new List<ItemPrice>();
		public List<ItemRemain> Remains = new List<ItemRemain>();
		public List<ItemUnit> SubUnits = new List<ItemUnit>();

        public BK_Carrier DefaultCarrier = BK_Carrier.NotCarrier;
        public BK_Device DefaultDevice = BK_Device.NullDevice;
        public BK_Variant DefaultVariant = BK_Variant.Default;

		public static BK_Item Unset = new BK_Item();

        private static List<BK_Item> cache = null;

        const int K_PARAM_IN_USE  = 0x01;
        const int K_PARAM_SERVICE = 0x02;

        public BK_Item() { }
        public BK_Item(denSQL.denReader r) { readSelf(r); }

		/// <summary>
		/// New 0-Item, 1-Paper, 2-Print, 3-Service
		/// </summary>
		/// <param name="iWhat">0-Item, 1-Paper, 2-Print</param>
		public BK_Item(Page pPage, int iWhat)
		{
			Tab = pPage;
			TabPos = pPage.Grid.RowsCount;
			Param = 1; // In_use
			DefaultUnit = Unit.getUnit(1); // шт
			MinUnit = Unit.getUnit(1); // шт
            string sNewId = getNextId();

			switch (iWhat)
			{
				case 0:
					Name = "Новый товар";
					Short = "Нов.тов.";
					NameUa = "Новий товар";
					Articul = "ID_" + sNewId;
					break;
				case 1:
					Name = "Бумага НОВАЯ";
					Short = "Бум.НОВ.";
					NameUa = "Папір НОВИЙ";
					Articul = "PAP_" + sNewId;
					Carrier = new BK_Carrier(1, 1, 1);
					break;
				case 2:
					Name = "Печать НОВАЯ";
					Short = "Печ.НОВ.";
					NameUa = "Печать НОВА";
					ServiceFormat = BK_Format.getFormat(1); // A4
					Articul = "PRN_" + sNewId;
					IsService = true;
					break;
			}
		}

        /// <summary>
        /// New Item
        /// </summary>
        /// <param name="sName">Item name</param>
        public BK_Item(string sName)
        {
            Tab = Page.NullPage;
            //TabPos = Tab.Grid.RowsCount;
            Param = 1; // In_use
            DefaultUnit = Unit.getUnit(1); // шт
            MinUnit = Unit.getUnit(1); // шт

            Name = sName;
            Short = sName;
            NameUa = sName;
            Articul = "ITM_" + getNextId();
        }

        public static BK_Item createItem(string sName)
        {
            BK_Item i = new BK_Item(sName);
            cache.Add(i);
            return i;
        }

        public void readSelf(denSQL.denReader r)
        {
            Id = r.GetInt("i_id");
            Name = r.GetString("i_name");
            Short = r.GetString("i_short");
            NameUa = r.GetString("i_name_ua");
            Tab = Page.getPage(r); // i_tab
            Ico = BK_Icon.getIcon(r); // i_ico
            Param = r.GetInt("i_param");
            TabPos = r.GetInt("i_pos");
            DefaultUnit = Unit.getUnit(r, "i_unit");
            MinUnit = Unit.getUnit(r, "i_min_unit");
            ServiceFormat = BK_Format.getFormat(r, "i_fmt");  // i_fmt
            Desc = r.GetString("i_desc");
            Articul = r.GetString("i_art");
            Manufacturer = Company.getCompany(r, "i_man");
            Group = r.GetInt("i_group");
            DefaultDevice = BK_Device.getDevice(r, "i_device");
            if (r.Good("car_iid")) Carrier = new BK_Carrier(r);
        }

        public void reload()
        {
            if (Id == 0) return;
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_items LEFT JOIN bk_carrier ON car_iid = i_id WHERE i_id={0}", Id);
            if(r.Read()) readSelf(r);
            r.Close();
            ReadTables();
        }


        public void readRemains()
        {
            Remains.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_ware WHERE w_item={0} ORDER BY w_point", Id);
            while (r.Read()) Remains.Add(new ItemRemain(r));
            r.Close();
        }

        public void readSubUnits()
        {
            SubUnits.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_iunits WHERE iu_item={0} ORDER BY iu_item, iu_unit", Id);
            while (r.Read()) SubUnits.Add(new ItemUnit(r));
            r.Close();
        }

        public void ReadTables()
		{
            ItemPrice.readItemPrices(this);
            readRemains();
            readSubUnits();
		}

		public bool HasPriceChanged
		{
			get
			{
				foreach (ItemPrice p in Prices)
					if (p.HasChanged) return true;
				return false;
			}
		}

		public int UnitsCount
		{
			get
			{
				if (SubUnits.Count == 0 && MinUnit == DefaultUnit) return 1;
				if (MinUnit == DefaultUnit) return SubUnits.Count + (SubUnits.FirstOrDefault(x => x.SubUnit == DefaultUnit) == null ? 1 : 0);
				if (SubUnits.Count == 0) ItemUnit.AddDefaultBaseToMin(this);
				return SubUnits.Count + 1;
			}
		}

		public List<Unit> ItemUnits
		{
			get
			{
                List<Unit> lst = new List<Unit>
                {
                    MinUnit
                };
                lst.AddRange(from x in SubUnits select x.SubUnit);
				if (!lst.Contains(DefaultUnit)) { ItemUnit.AddDefaultBaseToMin(this); lst.Add(DefaultUnit); }
				return lst;
			}
		}

        public List<ItemPrice> getPointPrices(BK_Point pt = null) {
            BK_Point upt = pt ?? BK_Point.self;
            return new List<ItemPrice>(from x in Prices where x.Pnt == upt select x);
        }
        public List<ItemRemain> getPointRemains(BK_Point pt = null)
        {
            BK_Point upt = pt ?? BK_Point.self;
            return new List<ItemRemain>(from x in Remains where x.Pnt == upt select x);
        }

        public void AddPrices(IEnumerable<PrcAddTag> lmt)
		{
			foreach(PrcAddTag mt in lmt)
				Prices.Add(new ItemPrice(this, 1, mt));
		}

        public void updateRemains(BK_Point p)
        {
            // Make units table
            string sCase = string.Format(" WHEN {0} THEN 1", MinUnit.Id);
            foreach(ItemUnit u in SubUnits)
            {
                sCase += string.Format(" WHEN {0} THEN {1}", u.SubUnit.Id, u.MinCnt);
            }

            // Eval income
            object inc = denSQL.Scalar("SELECT SUM(sl_cnt * CASE sl_unit {0} ELSE 0 END) AS s " +
                                    "FROM bk_slist " +
                                    "LEFT JOIN bk_supply ON s_id = sl_sup " +
                                    "WHERE s_status = {1} AND s_point = {2} AND sl_item = {3}",
                                    sCase, BK_Order.K_STATUS_DELIVERED, p.Id, Id);
            System.Diagnostics.Debug.WriteLine("Inc={0}, {1}", inc, inc.GetType().Name);
            
            // Eval expenses            
        }

        private static string getNextId() { return denSQL.Scalar("SELECT 1+MAX(i_id) FROM bk_items").ToString(); }

        public void ResetChangeFlag() { foreach (ItemPrice ip in Prices) ip.HasChanged = false; }

		public bool InUse { get { return Param.HasBit(K_PARAM_IN_USE); } set { Param = Param.SetBit(K_PARAM_IN_USE, value); } }
		public bool IsService { get { return Param.HasBit(K_PARAM_SERVICE); } set { Param = Param.SetBit(K_PARAM_SERVICE, value); } }
		public bool HasVariants { get { return Variants.Count > 0; } }
		public bool IsCarrier { get { return Carrier != BK_Carrier.NotCarrier; } }

		public bool HasUnit(Unit u)
		{
			if (MinUnit == u || DefaultUnit == u) return true;
			return SubUnits.Any(x => x.SubUnit == u);
		}

		public void AddSubUnit(Unit u)
		{
			if (HasUnit(u)) return;
			SubUnits.Add(new ItemUnit(this, u));
		}

        public bool MatchInCat(string sTxt, List<CatalogItem> lExclude)
        {
            return Name.ToLower().Contains(sTxt.ToLower()) && !lExclude.Any(ci => ci.Item.Id == Id);
        }

		public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_items", "i_id");
			t.AddFld("i_name", Name);
			t.AddFld("i_short", Short);
			t.AddFld("i_name_ua", NameUa);
			t.AddFld("i_tab", Tab.Id);
			t.AddFld("i_ico", Ico.Id);
			t.AddFld("i_param", Param);
			t.AddFld("i_pos", TabPos);
			t.AddFld("i_unit", DefaultUnit.Id);
			t.AddFld("i_min_unit", MinUnit.Id);
			t.AddFld("i_fmt", ServiceFormat.Id);
			t.AddNul("i_desc", Desc);
			t.AddNul("i_art", Articul);
			t.AddFld("i_man", Manufacturer.Id);
            t.AddFld("i_device", DefaultDevice.Id);
			int ret = t.Store(ref Id);
			if (Id == 0) return ret;
            storePrices();
			foreach (ItemUnit iu in SubUnits) { iu.ItemId = Id; iu.Store(); }
			if (!Carrier.IsNotCarrier) { Carrier.ItemId = Id; Carrier.Store(); }
			//foreach (BK_SubItem si in SubItems) { si.ItemId = Id; si.Store(); }
			return ret;
		}

        public void storePrices()
        {
            if (Id == 0) return;
            foreach (ItemPrice ip in Prices) { ip.ItemId = Id; ip.Store(); }
        }

        public static IEnumerable<BK_Item> getPageItems(Page page)
        {
            return from x in cache where x.Tab.Id == page.Id orderby x.TabPos select x;
        }

        public static void updateChangedItems(bool bUndo = false)
        {
            foreach (BK_Item it in cache)
            {
                if (bUndo)
                {
                    it.ResetChangeFlag();
                }
                else
                {
                    foreach (ItemPrice ip in it.Prices)
                        if (ip.HasChanged) ip.Store();
                }
            }
        }

        public static void initItems()
        {
            cache = new List<BK_Item>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_items LEFT JOIN bk_carrier ON car_iid = i_id ORDER BY i_id");
            while (r.Read())
            {
                BK_Item it = new BK_Item(r);
                if (it.IsCarrier) BK_Carrier.addCarrier(r);
                cache.Add(it);
            }
            r.Close();
            foreach (BK_Item it in cache) it.ReadTables();
        }

        public static BK_Item getItem(int id)
        {
            if (cache == null) initItems();
            BK_Item it = cache.FirstOrDefault(x => x.Id == id);
            return it ?? Unset;
        }
        public static BK_Item getItem(denSQL.denReader rd, string sField = "item")
        {
            return getItem(rd.GetInt(sField));
        }
        public static List<BK_Item> findItems(string sTxt, List<CatalogItem> lstExclude)
        {
            if (cache == null) initItems();
            return cache.FindAll(x => x.MatchInCat(sTxt, lstExclude));
        }

        public static List<BK_Item> getAll() { return cache; }

        public static void purgeItems() { if(cache != null) cache.RemoveAll(m => m.Id == 0); }

        public string TypeCaption { get { return IsCarrier ? "Бумага" : IsService ? "Услуга" : "Товар"; } }
		public override string ToString() { return String.Format("{0} [{1}]", Short, Id); }
		public override int GetHashCode() { return Name.GetHashCode(); }
	}
}
