using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class BK_Order
    {
        public int Id = 0;
        public Company Supplier;
        public DateTime dtOrder;
        public DateTime dtDeliver;
        public string Number = "";
        public int Status = K_STATUS_PREPARE;
        public decimal Summ = 0m;
        public BK_Point Point;

        public List<BK_OrderItem> Items = new List<BK_OrderItem>();

        public static BK_Order Unknown = new BK_Order();
        public static List<BK_Order> cache = null;

        public const int K_STATUS_PREPARE   = 0;
        public const int K_STATUS_ORDERED   = 1;
        public const int K_STATUS_DELIVERED = 2;

        private static string[] m_Statuses = new string[] { "не заказан", "заказан", "выполнен" };

        public BK_Order()
        {
            Supplier = new Company();
            dtOrder = Program.DateNone;
            dtDeliver = Program.DateNone;
            Point = BK_Point.Unknown;
        }

        public BK_Order(Company supplier)
        {
            long iOrd = 1 + (long)denSQL.Scalar("SELECT COUNT(*) FROM bk_supply");
            Supplier = supplier;
            dtOrder = Program.DateNone;
            dtDeliver = Program.DateNone;
            Number = string.Format("Заказ №{0}", iOrd);
            Status = DefaultStatus;
            Point = BK_Point.self;
        }

        public BK_Order(denSQL.denReader r)
        {
            Id = r.GetInt("s_id");
            Supplier = Company.getCompany(r, "s_com");
            dtOrder = r.GetDateTime("s_date_order");
            dtDeliver = r.GetDateTime("s_date_deliver");
            Number = r.GetString("s_number");
            Status = r.GetInt("s_status");
            Point = BK_Point.getPoint(r, "s_point");
            Summ = r.GetDecimal("summ"); // Evaluated
        }

        public int Store()
        {
            denSQL.denTable t = denSQL.CreateTable("bk_supply", "s_id");
            t.AddFld("s_com", Supplier.Id);
            t.AddFld("s_date_order", dtOrder);
            t.AddFld("s_date_deliver", dtDeliver);
            t.AddFld("s_number", Number);
            t.AddFld("s_status", Status);
            t.AddFld("s_point", Point.Id);
            int ret = t.Store(ref Id);
            denSQL.Command("DELETE FROM bk_slist WHERE sl_sup = {0}", Id);
            foreach(BK_OrderItem it in Items)
            {
                if (it.Item.Id == 0 || it.ItemChanged) it.Item.Store();
                else if (it.Item.HasPriceChanged) it.Item.storePrices();
                it.Store(this);
            }
            return ret;
        }

        public static int DefaultStatus { get { return K_STATUS_DELIVERED; } }

        public void readItems()
        {
            Items.Clear();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_slist WHERE sl_sup={0}", Id);
            while (r.Read()) Items.Add(new BK_OrderItem(r));
            r.Close();
        }


        public bool Edit()
        {
            frmMyOrder frm = new frmMyOrder(this);
            return frm.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public bool evalMine(BK_OrderItem oi, ItemPrice ip)
        {
            decimal prc = oi.Price * Supplier.Coeff;
            if (prc > ip.Prc)
            {
                ip.setPrice(decimal.Round(prc, 2));
                return true;
            }
            return false;
        }

        public decimal Total
        {
            get
            {
                return decimal.Round(Items.Aggregate(0m, (a, x) => a + x.Total), 2);
            }
        }

        public bool Match(Company sup, bool bUnReady)
        {
            if (sup.Id == 0) return bUnReady ? (Status < K_STATUS_DELIVERED) : true;
            bool ret = sup.Id == Supplier.Id && (bUnReady ? (Status < K_STATUS_DELIVERED) : true);
            return ret;
        }

        public static string[] getStatuses() { return m_Statuses; }

        public string StatusText { get { return m_Statuses.ElementAtOrDefault(Status); } }

        public static string baseQuery(string sWhere = "", string sOrder = "s_date_order DESC")
        {
            if (sWhere != "") sWhere = "WHERE " + sWhere;
            if (sOrder != "") sOrder = "ORDER BY " + sOrder;
            return string.Format("SELECT bk_supply.*, SUM(sl_prc*sl_cnt) summ " +
                                "FROM bk_supply " +
                                "LEFT JOIN bk_slist ON sl_sup=s_id " +
                                "{0} " +
                                "GROUP BY s_id " +
                                "{1}", sWhere, sOrder);
        }

        public BK_OrderItem createItem(CatalogItem ci)
        {
            BK_OrderItem oi = new BK_OrderItem(this, ci);
            Items.Add(oi);
            return oi;
        }

        public static void initOrders()
        {
            if (cache == null) cache = new List<BK_Order>();
            else cache.Clear();

            denSQL.denReader r = denSQL.Query(baseQuery());
            while (r.Read()) cache.Add(new BK_Order(r));
            r.Close();
        }

        public static BK_Order addOrder(Company sup)
        {
            if (cache == null) cache = new List<BK_Order>();
            BK_Order ord = new BK_Order(sup);
            cache.Add(ord);
            return ord;
        }

        public static BK_Order getOrder(int id)
        {
            if (cache == null) initOrders();
            BK_Order d = cache.FirstOrDefault(x => x.Id == id);
            return d ?? Unknown;
        }
        public static BK_Order getOrder(denSQL.denReader rd, string sField = "sl_sup")
        {
            return getOrder(rd.GetInt(sField));
        }

        public static List<BK_Order> getAll() { return cache; }
        public static List<BK_Order> getBySupplier(Company c, bool bUnReady)
        {
            if (c.Id == 0) return getAll();
            return (from x in cache where x.Match(c, bUnReady) select x).ToList();
        }
    }
}
