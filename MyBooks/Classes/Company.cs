using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
	public class Company : IBkObject
	{
		public int Id = 0;
		public string Name = "Неизвестная компания";
		public string Short = "Неизв.комп.";
		public int Role = 0;
		public string MFO = "";
		public string AccountNum = "";
		public string EDRPOU = "";
		public string Certificate = "";
		public string Adr = "";
        public decimal Coeff = 1m;

        public const int K_ROLE_OWNER = 0x100;
		public const int K_ROLE_SUPPLIER = 0x001;
        public const int K_ROLE_CUSTOMER = 0x002;

        public static Dictionary<int, string> mRoleNames = new Dictionary<int, string>()
        {
            { K_ROLE_OWNER, "Своя" },
            { K_ROLE_SUPPLIER, "Поставщик" },
            { K_ROLE_CUSTOMER, "Покупатель" }
        };
        private static Dictionary<int, string> mRoleShortNames = new Dictionary<int, string>()
        {
            { K_ROLE_OWNER, "Своя" },
            { K_ROLE_SUPPLIER, "Пост." },
            { K_ROLE_CUSTOMER, "Покуп." }
        };

        public static Company AllSuppliers = new Company() { Role = K_ROLE_SUPPLIER, Name = "Все поставщики" };
		public static Company Unknown = new Company();
        public static Company self = Unknown;

        private static List<Company> cache = null;

        public Company() { }
		public Company(denSQL.denReader r)
		{
			Id = r.GetInt("c_id");					// c_id
			Name = r.GetString("c_name");			// c_name
			Short = r.GetString("c_short");			// c_short
			Role = r.GetInt("c_role");				// c_role
			MFO = r.GetString("c_mfo");				// c_mfo
			EDRPOU = r.GetString("c_edrpou");			// c_edrpou
			Certificate = r.GetString("c_cert");		// c_cert
			AccountNum = r.GetString("c_account_n");		// c_account_n
            Adr = r.GetString("c_adr");				// c_adr
            Coeff = r.GetDecimal("c_coeff");
        }

		public int Store()
		{
			denSQL.denTable t = denSQL.CreateTable("bk_company", "c_id");
			t.AddFld("c_name", Name);
			t.AddFld("c_short", Short);
			t.AddFld("c_role", Role);
			t.AddFld("c_mfo", MFO);
			t.AddFld("c_edrpou", EDRPOU);
			t.AddFld("c_cert", Certificate);
			t.AddFld("c_account_n", AccountNum);
			t.AddFld("c_adr", Adr);
            t.AddFld("c_coeff", Coeff);
			return t.Store(ref Id);
		}

		public void SetButton(System.Windows.Forms.Button b) { b.Text = Short; b.Tag = this; }

        public static string GetRoleCaption(int role, string delim = ", ") {
            IEnumerable<string> ie = from c in mRoleShortNames where (c.Key & role) > 0 select c.Value;
            return string.Join(delim, ie);
        }
		public string RoleCaption { get { return GetRoleCaption(Role); } }

		public bool IsOwn { get { return (Role & K_ROLE_OWNER) > 0; } }
		public bool IsSupplier { get { return (Role & K_ROLE_SUPPLIER) > 0; } }
		public bool IsCustomer { get { return (Role & K_ROLE_CUSTOMER) > 0; } }
		public List<User> Users = new List<User>();
		public int UsersCnt { get { return Users.Count; } }
		public override string ToString() { return Name; }
		public override int GetHashCode() { return Name.GetHashCode(); }

        public bool Edit() { return I_Edit(); }
        private static bool addNew(int iRole)
        {
            Company c = new Company()
            {
                Role = iRole
            };
            return c.I_OnNew();
        }
        public static bool addNewSupplier() { return addNew(K_ROLE_SUPPLIER);  }

        public static void initCompanies()
        {
            cache = new List<Company>();
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_company ORDER BY c_id");
            while (r.Read())
            {
                Company co = new Company(r);
                cache.Add(co);
                if (co.IsOwn) self = co;
            }
            r.Close();
        }

        public static Company getCompany(int id)
        {
            if (cache == null) initCompanies();
            Company c = cache.FirstOrDefault(x => x.Id == id);
            return c ?? Unknown;
        }
        public static Company getCompany(denSQL.denReader rd, string sField = "u_com")
        {
            return getCompany(rd.GetInt(sField));
        }

        public static List<Company> getAll() { return cache; }

        public static IEnumerable<Company> getSuppliers() {  return from c in cache where c.IsSupplier orderby c.Name select c; }
        public static IEnumerable<Company> getCustomers() { return from c in cache where c.IsCustomer orderby c.Name select c; }

        #region IBkObject
        private bool intEdit(bool bNew)
        {
            if (bNew)
            {
                if (IsCustomer)
                {
                    Name = "Новый покупатель";
                    Short = "Нов.покуп.";
                }
                else
                {
                    Name = "Новый поставщик";
                    Short = "Нов.пост.";
                    if (!IsSupplier) Role = K_ROLE_SUPPLIER;
                }
            }
            frmCompany f = new frmCompany(this);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return false;
            Store();
            if (Id == 0) return false;
            if (bNew) cache.Add(this);
            return true;
        }
        public int I_Id { get { return Id; } }
		public string I_Name { get { return Name; } }
		public string I_Short { get { return Short; } }
		public System.Drawing.Image I_Icon { get { return MyBooks.Properties.Resources.bank16; } }
		public List<BO_Field> I_Params() { return new List<BO_Field>() {
            new BO_Field("Название", Name, 0, 45),
            new BO_Field("Кратко", Short, 0, 45),
            new BO_Field("Роли",
                Role, 
                2, 
                45, 
                System.Windows.Forms.HorizontalAlignment.Center, 
                (from flg in mRoleNames select new BO_Flag(flg.Key, flg.Value, mRoleShortNames[flg.Key])).ToList()
            ),
            new BO_Field("МФО Банка", MFO, 0, 45),
            new BO_Field("Номер счета", AccountNum, 0, 45),
            new BO_Field("ЕДРПОУ", EDRPOU, 0, 45),
            new BO_Field("Сертификат", Certificate, 0, 45),
            new BO_Field("Адрес", Adr, 0, 45)
        }; }
		public void I_SetParam(int idx, object val) { }
		public bool I_OnNew() { return intEdit(true); }
		public bool I_Edit() { return intEdit(false); }
		public bool I_Store() { return Store() != 0; }
		#endregion
	}
}
