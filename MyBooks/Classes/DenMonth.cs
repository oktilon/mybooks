using System;

namespace MyBooks
{
    public class DenMonth
	{
		static String[] sGen = { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
		public static String GenitiveCase(int iMonth)
		{
			int i = iMonth - 1;
			if (i < 0) i = 0;
			if (i > 11) i = 11;
			return sGen[i];
		}
	}
}

