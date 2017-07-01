using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBooks
{
    public class User
    {
        const string AUTH_KEY = "M%v$=";
        const string NO_PWD = "NOT";

        public int Id = 0;
		public string Family = "";
		public string Name = "Гость";
		public string Prenom = "";
		public string Login = "guest";
		public string Hash = NO_PWD;
		public int Role = 0;
        //public Company Company = Company.Unknown;

        private static List<User> cache = null;
        public static User UserGuest = new User();
        public static User Me = UserGuest;


        public User() { }
		public User(string f) { Family = f; }
        public User(denSQL.denReader r)
        {
            Id = r.GetInt("u_id");
            Login = r.GetString("u_login");
			Hash = r.GetString("u_pass").ToUpper();
			Role = r.GetInt("u_role");
			Family = r.GetString("u_family");
			Name = r.GetString("u_name");
			Prenom = r.GetString("u_prenom");
			//Company = Program.GetCompany(r); // u_com
        }
        public string FIO
        {
            get
            {
                return Family +
                    (Name == "" ? "" : (" " + Name.Substring(0, 1) + ".")) +
                    (Prenom == "" ? "" : ("" + Prenom.Substring(0, 1) + "."));
            }
        }

        public bool hasPassword
        {
            get { return Hash != NO_PWD; }
            set { if (!value) Hash = NO_PWD; }
        }

        public void setPassword(string pwd)
        {
            Hash = Program.CalculateSHA1Hash(Login + AUTH_KEY + pwd);
            Store();
        }

        public bool checkPassword(string pwd)
        {
            return Hash == Program.CalculateSHA1Hash(Login + AUTH_KEY + pwd);
        }

        public void Store()
        {
			denSQL.denTable t = denSQL.CreateTable("bk_users", "u_id");
			t.AddFld("u_login", Login);
			t.AddFld("u_pass", Hash.ToLower());
			t.AddFld("u_role", Role);
			t.AddFld("u_family", Family);
			t.AddNul("u_name", Name);
			t.AddNul("u_prenom", Prenom);
			//t.AddFld("u_com", Company.Id);
			t.Store(ref Id);
        }

        public bool IsAdmin { get { return Role > 50; } }

        public override string ToString() { return Login; }
        public override int GetHashCode() { return Login.GetHashCode(); }

        public static void initUsers()
        {
            cache = new List<User>();
            cache.Add(UserGuest);
            denSQL.denReader r = denSQL.Query("SELECT * FROM bk_users ORDER BY u_id");
            while (r.Read()) cache.Add(new User(r));
            r.Close();
        }

        public static User getUser(int id)
        {
            if (cache == null) initUsers();
            User u = cache.FirstOrDefault(x => x.Id == id);
            return u ?? UserGuest;
        }
        public static User getUser(denSQL.denReader rd, string sField = "bill_user")
        {
            return getUser(rd.GetInt(sField));
        }

        public static List<User> getAll() { return cache; }
        public static IEnumerable<User> getOrdered() { return from u1 in cache orderby u1.Name select u1;  }
    }
}
