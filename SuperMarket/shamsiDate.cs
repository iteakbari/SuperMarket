using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SuperMarket
{
    class shamsiDate
    {
        public static String m2sh(DateTime d)
        {
            PersianCalendar pc = new PersianCalendar();
            StringBuilder sb = new StringBuilder();
            sb.Append(pc.GetYear(d).ToString("0000"));
            sb.Append("/");
            sb.Append(pc.GetMonth(d).ToString("00"));
            sb.Append("/");
            sb.Append(pc.GetDayOfMonth(d).ToString("00"));

            return sb.ToString();
        }
    }
}
