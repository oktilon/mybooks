using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Net.Mail;
using System.IO;
using System.Net;

using Sgc = SourceGrid.Cells;

namespace MyBooks
{
    static class Program
    {
        private static frmMain frmm = null;
        public static System.Globalization.CultureInfo m_cif;
        private static Properties.Settings m_Settings = Properties.Settings.Default;
        public static DateTime DateNone = new DateTime(1, 1, 1);
        public static bool m_LogEnabled = false;
        private static string m_LogFileName = "";

        public static string g_sRegKeyOB = "Software\\DEST Software\\Books";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>MySqlInfo
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                m_cif = new System.Globalization.CultureInfo("ru-RU");
                m_cif.NumberFormat.CurrencySymbol = "";
                m_cif.NumberFormat.NumberDecimalSeparator = ",";
                m_cif.NumberFormat.CurrencyDecimalSeparator = ",";
                m_cif.NumberFormat.CurrencyDecimalDigits = 2;

                if (args.Length > 0 && args[0] == "encrypt")
                {
                    string sTxt = args.Length > 1 ? args[1] : "";
                    frmEncrypt f = new frmEncrypt(sTxt);
                    f.ShowDialog();
                    return;
                }

                m_LogFileName = RegGetString("LogPath", "");
                m_LogEnabled = m_LogFileName != "";

                string sUser = Decrypt(ProgramSettings.DB_USER);
                string sPwd = Decrypt(ProgramSettings.DB_PASS);

                //string h = CalculateSHA1Hash("111");

                denSQL.init(m_Settings.sqlServer, m_Settings.sqlDatabase, sUser, sPwd);
                if (!denSQL.hasConnection)
                {
                    MessageBox.Show("Не найден сервер БД. Проверьте настройки.", "База", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Command("INSERT INTO bk_string (`key`, `val`) VALUES ('{0:yyyy-MM-dd HH:mm:ss}', 'Перевірка-{0}')", DateTime.Now);
                T.initLanguage(1);
                ReadParameters();
                if (args.Length >= 1 && CalculateSHA1Hash(args[0]) == ProgramSettings.CMD_KEY)
                {
                    User.Me = User.getUser(1); // den
                    RunMainForm();
                }
                else
                {
                    frmPwd frm = new frmPwd();
                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                        RunMainForm();
                    else if (dr == DialogResult.No)
                        MessageBox.Show("В доступе отказано.", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { ProceedError(ex, "Не перехваченная ошибка в программе.", false); }
        }

        public static void ReadParameters()
        {
            BK_Format.initFormats();
            BK_Density.initDenities();
            BK_Surface.initSurfaces();
            User.initUsers();
            Company.initCompanies();
            BK_Icon.initIcons();
            Unit.initUnits();
            PageType.initPageTypes();
            BK_DeviceType.initDeviceTypes();
            BK_Device.initDevices();
            BK_Point.initPoints();
            Page.initPages();
            BK_Item.initItems();
        }

        static void RunMainForm()
        {
            frmm = new frmMain();
            Gma.UserActivityMonitor.HookManager.KeyUp += new KeyEventHandler(HookManager_KeyUp);
            Application.Run(frmm);
        }

		public static bool HasBit(this int @base, int iBit) { return (@base & iBit) == iBit; }
		public static bool HasBit(this uint @base, int iBit) { return (@base & iBit) == iBit; }

		public static int ApplyBit(this int @base, int iBit, bool bValue) { if (bValue) return @base | iBit; else return @base & ~iBit; }
		public static uint ApplyBit(this uint @base, int iBit, bool bValue) { if (bValue) return @base | (uint)iBit; else return @base & ~((uint)iBit); }

        public static void SetHeaders(this SourceGrid.Grid @base, IEnumerable<string> hdr, IEnumerable<int> wd, Boolean bAutoSortOn)
        {
            @base.SetHeaders(hdr, wd, bAutoSortOn, false);
        }
        public static void SetHeaders(this SourceGrid.Grid @base, IEnumerable<string> hdr, IEnumerable<int> wd, Boolean bAutoSortOn, bool bAutoSize)
        {
            Int32 iCols = hdr.Count();
            @base.FixedRows = 1;
            Sgc.ColumnHeader ch;
            if (iCols < 1 && iCols > 100) return;
            @base.Redim(1, iCols);
            for (int iCol = 0; iCol < iCols; iCol++)
            {
                if (wd.ElementAt(iCol) > 0)
                {
                    @base.Columns[iCol].Width = wd.ElementAt(iCol);
                    @base.Columns[iCol].AutoSizeMode = bAutoSize ? SourceGrid.AutoSizeMode.EnableAutoSize : SourceGrid.AutoSizeMode.None;
                }
                ch = new Sgc.ColumnHeader(hdr.ElementAt(iCol));
                ch.AutomaticSortEnabled = bAutoSortOn;
                //ch.View.Ba
                ch.View.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                @base[0, iCol] = ch;
            }
        }


        /// <summary>
        /// Обрезает строку до заданной длины строки
        /// </summary>
        /// <param name="iSizeMax">Допустимая длина строки</param>
        public static string Truncate(this string @base, int iSizeMax, bool bAddElipsis = false) { return @base.Length > iSizeMax ? (@base.Substring(0, iSizeMax) + (bAddElipsis ? "…" : "")) : @base; }

		public static int SetBit(this int @base, int iBit, bool bValue)
		{
			return bValue ? (@base | iBit) : (@base & ~iBit);
		}

		public static bool IsSetBit(this int @base, int iBit)
		{
			return (@base & iBit) == iBit;
		}

        public static Image GetById (this List<BK_Icon> @base, int iId)
        {
            BK_Icon ic = @base.FirstOrDefault(bko => bko.Id == iId);
            if (ic == null) return MyBooks.Properties.Resources.Dot16;
            return ic.Image;
        }

        static void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (frmm != null) frmm.OnHotKeyPress((Shortcut)e.KeyValue);
        }


        public static string CalculateSHA1Hash(string input)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            SHA1 sh = System.Security.Cryptography.SHA1.Create();
            byte[] hash = sh.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

		public static void ProceedError(Exception ex, string sCaption, bool bSilent = false, string sql = "") { ProceedError(ex, 0, sCaption, bSilent, sql); }
		public static void ProceedError(Exception ex, int iCode, string sCaption, bool bSilent, string sql = "")
		{
            frmError.showError(ex, sCaption);
            //MessageBox.Show(string.Format("Возникла ошибка в программе:\n{0}\n{1}", ex.Message, ex.StackTrace), sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			//SendBugReport(ex, iCode, sCaption, bSilent, sql);
		}

		public static void SendBugReport(Exception ex, int iCode, string sCaption, bool bSilent, string sql)
		{
			string msg = "";
			Exception iex = ex.InnerException;
			while (iex != null)
			{
				msg += "<tr><td><b>Inner</b></td><td>" + iex.Message.Replace("\n", "<br />") + "</td></tr>";
				iex = iex.InnerException;
			}
			if (sql != "") sql = string.Format("<tr><td><b>SQL</b></td><td>{0}</td></tr>", System.Web.HttpUtility.HtmlEncode(sql));
			string sBody = string.Format("<html><head><title>Bug Report</title></head><body style=\"font-family:'Courier New';\">" +
						"<table cellspacing='0' cellpadding='0' border='1'><tr><td><b>Caption</b></td><td>{0}</td></tr>" +
						"<tr><td><b>Type</b></td><td>{1}</td></tr>" +
						"<tr><td><b>Code</b></td><td>{2}</td></tr>" +
						"<tr><td><b>Message</b></td><td>{3}</td></tr>{7}" +
						"<tr><td><b>Source</b></td><td>{4}</td></tr>{5}" +
						"<tr><td><b>StackTrace</b></td><td>{6}</td></tr></table></body></html>", sCaption, ex.GetType().ToString(), iCode, ex.Message.Replace("\n", "<br />"), ex.Source, sql, ex.StackTrace.Replace("\n", "<br />"), msg);

			// System.Net.Mail
            MailAddress maFrom = new MailAddress("LikePrint@i.ua", "LikePrint #0", Encoding.UTF8);
            MailAddress maDeveloper = new MailAddress("dest_inbox@ua.fm", "Денис", Encoding.UTF8);

            MailMessage mm = new MailMessage();
            mm.From = maFrom;
            mm.To.Add(maDeveloper);
            //mm.Attachments.Add(new Attachment("Filename"));
            mm.Subject = "BugReport - " + ex.Message.Truncate(75, true);
            mm.Body = sBody;
            mm.IsBodyHtml = true;
            try
            {
                string smUsr = ""; // Decrypt(m_Settings.sSMTP_User)
                string smPwd = ""; // Decrypt(m_Settings.sSMTP_Pass)
                NetworkCredential nc = new NetworkCredential(smUsr, smPwd);
                SmtpClient smtpc = new SmtpClient(m_Settings.sSMTP, m_Settings.iSMTP_Port);
				smtpc.UseDefaultCredentials = false;
				smtpc.Credentials = nc.GetCredential(m_Settings.sSMTP, m_Settings.iSMTP_Port, "Basic");
                //smtpc.EnableSsl = true;
                smtpc.Send(mm);
                if (!bSilent) MessageBox.Show("Отчет отправлен.", "ObjectsBase");
                //smtpc.SendCompleted += new SendCompletedEventHandler(sc_SendCompleted); //void sc_SendCompleted(object sender, AsyncCompletedEventArgs e)
                //smtpc.SendAsync(mm, null);
            }
            catch (SmtpException e)
            {
                MessageBox.Show("Ошибка отправки отчета об ошибке\n" + e.ToString());
            }
		}

        public static string RegGetString(string key) { return RegGetString(key, ""); }
        public static string RegGetString(string key, string def)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(g_sRegKeyOB);
            object o = rk.GetValue(key);
            rk.Close();
            if (o == null) return def;
            return o.ToString();
        }

        public static void RegSetString(string key, string val)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(g_sRegKeyOB);
            rk.SetValue(key, val);
            rk.Close();
        }

        public static int RegGetDword(string key) { return RegGetDword(key, 0); }
        public static int RegGetDword(string key, int def)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(g_sRegKeyOB);
            object o = rk.GetValue(key);
            rk.Close();
            if (o == null) return def;
            return (int)o;
        }

        public static void RegSetDword(string key, int val)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(g_sRegKeyOB);
            rk.SetValue(key, val);
            rk.Close();
        }

        public static void RegDelValue(string key)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(g_sRegKeyOB);
            rk.DeleteValue(key);
            rk.Close();
        }

        public static void WriteLogLine(string sLine)
        {
#if DEBUG
            System.Diagnostics.Debug.Print(sLine);
#endif
            if (!m_LogEnabled) return;
            try
            {
                StreamWriter sw = File.Exists(m_LogFileName) ? System.IO.File.AppendText(m_LogFileName) : File.CreateText(m_LogFileName);
                sw.WriteLine(sLine);
                sw.Close();
            }
            catch { m_LogEnabled = false; }
        }

		public static string Encrypt(string plainText)
		{
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			byte[] keyBytes = new Rfc2898DeriveBytes(ProgramSettings.ENC_HASH, Encoding.ASCII.GetBytes(ProgramSettings.ENC_SALT)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(ProgramSettings.ENC_VIKEY));

			byte[] cipherTextBytes;

			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					cipherTextBytes = memoryStream.ToArray();
					cryptoStream.Close();
				}
				memoryStream.Close();
			}
			return Convert.ToBase64String(cipherTextBytes);
		}

		public static string Decrypt(string encryptedText)
		{
			byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
			byte[] keyBytes = new Rfc2898DeriveBytes(ProgramSettings.ENC_HASH, Encoding.ASCII.GetBytes(ProgramSettings.ENC_SALT)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

			var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(ProgramSettings.ENC_VIKEY));
			var memoryStream = new MemoryStream(cipherTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
		}
    }



}
