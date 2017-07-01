using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using gSet = MyBooks.Properties.Settings;
using RsRc = MyBooks.Properties.Resources;

namespace MyBooks
{
    /// <summary>
    /// ЧЕК
    /// </summary>
    public class Bill
    {
        /// <summary>
        /// Дата чека
        /// </summary>
		public DateTime Date = DateTime.Now;
        /// <summary>
        /// Код чека в БД
        /// </summary>
		public int Id = 0;
        /// <summary>
        /// Номер чека
        /// </summary>
		public int Num = -1;
        /// <summary>
        /// Сумма от клиента
        /// </summary>
		public decimal Cash = 0m;
        /// <summary>
        /// Список товаров в чеке
        /// </summary>
		public List<BillItem> Items = new List<BillItem>();
        /// <summary>
        /// Точка, сощздавшая чек
        /// </summary>
		public int m_point = gSet.Default.pointID;
        /// <summary>
        /// Получатель
        /// </summary>
		public string Rcpt = gSet.Default.strRecipient;
        /// <summary>
        /// Плательщик
        /// </summary>
		public string Payer = gSet.Default.strPayer;
        /// <summary>
        /// Основание (в чеке)
        /// </summary>
		public string Reason = gSet.Default.strReason;
        /// <summary>
        /// Условие продажи (в чеке)
        /// </summary>
		public string Cond = gSet.Default.strCondition;
        /// <summary>
        /// Создатель чека
        /// </summary>
		public User m_user = User.Me;

		public Bill() { }
		public Bill(DateTime dtDateTime) { Date = dtDateTime; }
        public Bill(int id)
        {
            Id = id;
            Num = 0;
            // Extract BILL info
            denSQL.denReader r = denSQL.Query(String.Format("SELECT bill_num, bill_date, bill_cash, bill_rcpt, bill_payer, bill_reason, bill_cond, bill_user, bill_point FROM bk_bill WHERE bill_id={0};", Id));
            if (r.Read())
            {
                Num = r.GetInt(0);
                Date = r.GetDateTime(1);
                Cash = r.GetDecimal(2);
                Rcpt = r.GetString(3, gSet.Default.strRecipient);
                Payer = r.GetString(4, gSet.Default.strPayer);
                Reason = r.GetString(5, gSet.Default.strReason);
                Cond = r.GetString(6, gSet.Default.strCondition);
                m_user = User.getUser(r); // bill_user
                m_point = r.GetInt(8);
            }
            else
                Id = 0;
            r.Close();
            if (Id == 0) return;
            // Extract BILL items
            r = denSQL.Query("SELECT * FROM bk_list WHERE bill={0};", Id);
            while (r.Read()) Items.Add(new BillItem(r));
            r.Close();
        }

        public decimal Total
        {
            get
            {
                decimal ret = 0m;
                foreach (BillItem bi in Items) ret += bi.Summ;
                return ret;
            }
        }

        public User User { get { return m_user; } set { m_user = value; } }
        public int Count { get { return Items.Count; } }

        /// <summary>
        /// Возвращает строку чека с заданным кодом
        /// </summary>
        /// <param name="i_id">Код позиции чека</param>
        /// <returns>BillItem</returns>
        public BillItem this[int i_id] { get { return getItem(i_id); } }

        public bool ContainsItemLike(BillItem bi)
        {
            if (Items.Count == 0) return false;
			return Items.Exists(x => x.Item == bi.Item && x.Unit == bi.Unit && x.Car == bi.Car);
        }

		public BillItem GetItemLike(BillItem bi)
		{
			if (Items.Count == 0) return null;
			return Items.FirstOrDefault(x => x.Item == bi.Item && x.Unit == bi.Unit && x.Car == bi.Car);
		}

        private BillItem getItem(int i_id)
        {
            if (Items.Count == 0) return new BillItem();
			IEnumerable<BillItem> ie = from b in Items where b.Item.Id == i_id select b;
            if (ie.Count() > 0) return ie.First();
            return new BillItem();
        }

		public bool RemoveItemLike(BillItem bi)
        {
			if (Items.Count == 0) return false;
			IEnumerable<BillItem> ie = from x in Items where x.Item == bi.Item && x.Unit == bi.Unit && x.Car == bi.Car select x;
            if (ie.Count() == 1) return Items.Remove(ie.First());
            return false;
        }

        public void Add(BillItem bi)
        {
			if (ContainsItemLike(bi)) throw new ArgumentException("Элемент уже есть в списке", "BillItem");
            Items.Add(bi);
        }

        public IEnumerable<BillItem> AllItems { get { return from b in Items orderby b.Item.Tab.Pos, b.Item.TabPos select b; } }

		public void Save()
		{
			if (Id == 0) // New bill
			{
				// Get new number
				object o_num = denSQL.Scalar("SELECT MAX(bill_num) FROM bk_bill WHERE bill_date LIKE '{0:yyyy-MM-dd}%';", Date);
				Num = (o_num is DBNull || o_num == null) ? 0 : (int)(uint)o_num;
				Num++;
			}
			denSQL.denTable t = denSQL.CreateTable("bk_bill", "bill_id");
			t.AddFld("bill_point", m_point);
			t.AddFld("bill_num", Num);
			t.AddFld("bill_date", Date, true);
			t.AddFld("bill_cash", Cash);
			t.AddNul("bill_rcpt", Rcpt, 45, gSet.Default.strRecipient);
			t.AddNul("bill_payer", Payer, 45, gSet.Default.strPayer);
			t.AddNul("bill_reason", Reason, 45, gSet.Default.strReason);
			t.AddNul("bill_cond", Cond, 45, gSet.Default.strCondition);
			t.AddFld("bill_user", m_user.Id);
			t.Store(ref Id);

			if (Id == 0) return;

			denSQL.Command("DELETE FROM bk_list WHERE bill={0}", Id);

			// INSERT all items
			string sql = "INSERT INTO bk_list (`bill`, `item`, `prc`, `cnt`, `unit`, `car`, `device`) VALUES ";
			Boolean bNext = false;
			foreach (BillItem bi in Items)
			{
				if (bNext) sql += ", ";
				else bNext = true;
				sql += String.Format(denSQL.Cif_sql, "({0}, {1}, {2:C}, {3}, {4}, {5}, {6})", Id, bi.Item.Id, bi.Price, bi.Count, bi.Unit.Id, bi.Car.ItemId, bi.Device.Id);
			}
			sql += ";";
			denSQL.Command(sql);
		}
		public override string ToString() { return String.Format("{0:dd.MM.yy HH:mm:ss} №{2} [id:{1}]", Date, Id, Num); }
		public override int GetHashCode() { return ToString().GetHashCode(); }
	}

}
