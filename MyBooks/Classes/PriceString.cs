using System;

namespace MyBooks
{
    public class PriceString : IComparable
	{
		static String[] sUnits_M_ua = { "нуль", "один", "два", "три", "чотири", "п’ять", "шість", "сім", "вісім", "дев’ять" };
		static String[] sUnits_F_ua = { "нуль", "одна", "дві", "три", "чотири", "п’ять", "шість", "сім", "вісім", "дев’ять" };
		static String[] sUnits_U_ua = { "нуль", "одне", "два", "три", "чотири", "п’ять", "шість", "сім", "вісім", "дев’ять" };
		static String[] sUnits_M_ru = { "ноль", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восем", "девять" };
		static String[] sUnits_F_ru = { "ноль", "одна", "две", "три", "четыре", "пять", "шесть", "семь", "восем", "девять" };
		static String[] sUnits_U_ru = { "ноль", "одно", "два", "три", "четыре", "пять", "шесть", "семь", "восем", "девять" };
		static String[] sMoney_ru = { "ен", "на", "ны", "ны", "ны", "ен", "ен", "ен", "ен", "ен", "ен" };
		static String[] sCoins_ru = { "еек", "ейка", "ейки", "ейки", "ейки", "еек", "еек", "еек", "еек", "еек", "еек" };
		static String[] sMoney_ua = { "ень", "на", "ні", "ні", "ні", "ень", "ень", "ень", "ень", "ень", "ень" };
		static String[] sCoins_ua = { "ійок", "ійка", "ійки", "ійки", "ійки", "ійок", "ійок", "ійок", "ійок", "ійок", "ійок" };
		static String[] sElevens_ua = { "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п’ятнадцять", "шістнадцять", "сімнадцять", "вісімнадцять", "дев’ятнадцять" };
		static String[] sElevens_ru = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
		static String[] sTens_ua = { "", "десять", "двадцять", "тридцять", "сорок", "п’ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев’яносто" };
		static String[] sTens_ru = { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемдесят", "девяносто" };
		static String[] sHundreds_ua = { "", "сто", "двісті", "триста", "чотириста", "п’ятсот", "шістсот", "сімсот", "вісімсот", "дев’ятсот" };
		static String[] sHundreds_ru = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
		static String[] sThousands_ua = { "", "тисяча", "тисячі", "тисячі", "тисячі", "тисяч", "тисяч", "тисяч", "тисяч", "тисяч" };
		static String[] sThousands_ru = { "", "тысяча", "тысячи", "тысячи", "тысячи", "тысяч", "тысяч", "тысяч", "тысяч", "тысяч" };
		static String[] sMillions_ua = { "", "мільйон", "мільйона", "мільйона", "мільйона", "мільйонів", "мільйонів", "мільйонів", "мільйонів", "мільйонів" };
		static String[] sMillions_ru = { "", "миллион", "миллиона", "миллиона", "миллиона", "миллионов", "миллионов", "миллионов", "миллионов", "миллионов" };
		static String[] sBillions_ua = { "", "мільярд", "мільярда", "мільярда", "мільярда", "мільярдів", "мільярдів", "мільярдів", "мільярдів", "мільярдів" };
		static String[] sBillions_ru = { "", "миллиард", "миллиарда", "миллиарда", "миллиарда", "миллиардов", "миллиардов", "миллиардов", "миллиардов", "миллиардов" };

		public static Int32 kLCID_Ru = 0x419;
		public static Int32 kLCID_Ua = 0x422;
		private System.Globalization.CultureInfo m_cult = new System.Globalization.CultureInfo(kLCID_Ua);
		private decimal m_value = 0;
		private String m_Money = "грив";
		private String m_Coin = "коп";
		private Int32 m_Gender = 1;
		private Boolean m_AddMoney = true;
		private Int32 m_MoneyEnd = 0;

		public Int32 LCID { get { return m_cult.LCID; } set { m_cult = new System.Globalization.CultureInfo(value); } }
		public String MoneyName { get { return m_Money; } set { m_Money = value; } }
		public String CoinName { get { return m_Coin; } set { m_Coin = value; } }
		public decimal Value { get { return m_value; } set { m_value = value; } }
		public Int32 Unit { get { return iTripleUnit; } }
		public Int32 Gender { get { return m_Gender; } set { m_Gender = value; } }
		public Boolean AddMoneyName { get { return m_AddMoney; } set { m_AddMoney = value; } }
		public PriceString() { }
		public PriceString(decimal dbl) { m_value = dbl; }
		public static PriceString operator +(PriceString p, decimal d) { p.m_value += d; return p; }
		public static PriceString operator +(PriceString p, Int32 i) { p.m_value += (decimal)i; return p; }

		private Int32 iTripleUnit = 0;
		private String GetTriple(Int32 iGender, String sTriple, Boolean bUseZero)
		{
			String ret = "", st = sTriple;
			Int32 i;
			Boolean bRu = m_cult.LCID == kLCID_Ru;
			iTripleUnit = 9;
			if (Int32.Parse(st) == 0)
			{
				if (bUseZero)
					return bRu ? sUnits_M_ru[0] : sUnits_M_ua[0];
				else
					return "";

			}
			// СОТНИ - HUNDREDS
			if (st.Length == 3)
			{
				i = Int32.Parse(st.Substring(0, 1));
				ret = bRu ? sHundreds_ru[i] : sHundreds_ua[i];
				st = st.Substring(1);
			}
			// ДЕСЯТКИ - TENS
			if (st.Length == 2)
			{
				i = Int32.Parse(st.Substring(0, 1));
				// -надцать
				if (i == 1)
				{
					if (ret != "") ret += " ";
					i = Int32.Parse(st.Substring(1, 1));
					ret += bRu ? sElevens_ru[i] : sElevens_ua[i];
					m_MoneyEnd = 10;
					return ret;
				}
				if (ret != "" && i > 0) ret += " ";
				ret += bRu ? sTens_ru[i] : sTens_ua[i];
				st = st.Substring(1);
			}
			// ЕДИНИЦЫ - UNITS
			if (st.Length == 1)
			{
				i = Int32.Parse(st.Substring(0, 1));
				m_MoneyEnd = i;
				iTripleUnit = i;
				if (i > 0)
				{
					if (ret != "") ret += " ";
					switch (iGender)
					{
						case 0:
							ret += bRu ? sUnits_M_ru[i] : sUnits_M_ua[i]; break;
						case 1:
							ret += bRu ? sUnits_F_ru[i] : sUnits_F_ua[i]; break;
						case 2:
							ret += bRu ? sUnits_U_ru[i] : sUnits_U_ua[i]; break;
					}
				}
				else
					iTripleUnit = 9;
			}
			return ret;
		}
		public String InWords
		{
			get
			{
				Boolean bRu = m_cult.LCID == kLCID_Ru;
				Int32 iMoney = (Int32)Math.Truncate(m_value);
				Int32 iCoin = Convert.ToInt32(Math.Round(m_value - iMoney, 2) * 100m);
				String ret = "", str = iMoney.ToString(), trp;
				if (str.Length > 12) return "##_VERY_BIG_NUMBER_##";
				// BILLIONS 111 000 000 000
				if (str.Length > 9)
				{
					trp = str.Substring(0, str.Length - 9);
					trp = GetTriple(0, trp, false);
					if (trp != "")
					{
						ret = trp + " ";
						ret += bRu ? sBillions_ru[iTripleUnit] : sBillions_ua[iTripleUnit];
					}
					str = str.Substring(str.Length - 9);
				}
				// MILLIONS 111 000 000
				if (str.Length > 6)
				{
					trp = str.Substring(0, str.Length - 6);
					trp = GetTriple(0, trp, false);
					if (trp != "")
					{
						ret += (ret == "" ? "" : " ") + trp + " ";
						ret += bRu ? sMillions_ru[iTripleUnit] : sMillions_ua[iTripleUnit];
					}
					str = str.Substring(str.Length - 6);
				}
				// THOUSANDS 111 000
				if (str.Length > 3)
				{
					trp = str.Substring(0, str.Length - 3);
					trp = GetTriple(1, trp, false);
					if (trp != "")
					{
						ret += (ret == "" ? "" : " ") + trp + " ";
						ret += bRu ? sThousands_ru[iTripleUnit] : sThousands_ua[iTripleUnit];
					}
					str = str.Substring(str.Length - 3);
				}
				// HUNDREDS 111
				trp = GetTriple(m_Gender, str, ret == "" ? true : false);
				if (trp != "")
					ret += (ret == "" ? "" : " ") + trp;

				String sCoin = iCoin.ToString("00");
				Int32 m_CoinEnd = 0;
				if (iCoin > 10 && iCoin < 20)
				{
					m_CoinEnd = 10;
				}
				else
					m_CoinEnd = Int32.Parse(sCoin.Substring(1, 1));
				String sSuffix = m_AddMoney ? (" " + m_Money + (bRu ? sMoney_ru[m_MoneyEnd] : sMoney_ua[m_MoneyEnd]) + " " + sCoin + " " + m_Coin + (bRu ? sCoins_ru[m_CoinEnd] : sCoins_ua[m_CoinEnd])) : "";
				if (ret == "" && m_Gender == 0) return (bRu ? sUnits_M_ru[0] : sUnits_M_ua[0]) + sSuffix;
				if (ret == "" && m_Gender == 1) return (bRu ? sUnits_F_ru[0] : sUnits_F_ua[0]) + sSuffix;
				if (ret == "" && m_Gender == 2) return (bRu ? sUnits_U_ru[0] : sUnits_U_ua[0]) + sSuffix;
				return ret + sSuffix;
			}
		}
		public override string ToString() { return m_value.ToString(); }
		public override bool Equals(object obj) { if (obj is PriceString) return CompareTo(obj) == 0; return false; }
		public override int GetHashCode() { return base.GetHashCode(); }


		public int CompareTo(object obj)
		{
			if (obj is PriceString)
			{
				PriceString p = (PriceString)obj;
				if (p.m_value == m_value) return 0;
				if (p.m_value < m_value) return 1;
				return -1;
			}
			throw new ArgumentException("Argument for CompareTo is not Price class.");
		}

	}
}

