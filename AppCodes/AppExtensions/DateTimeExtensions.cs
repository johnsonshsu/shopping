using System.Globalization;

/// <summary>
/// DateTime 擴充方法
/// </summary>
public static partial class Extension
{
    /// <summary>
    /// 將 DateTime 物件格式化成中華民國年份的日期字串(年/月/日 時：分:秒)
    /// <para>範例：</para>
    /// <para>string strDate = DateTime.Now.ezToRocDateString('/');</para>
    /// <para>回傳：</para>
    /// <para>strDate = "102/12/31 23:59:59"</para>
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="separator">分隔字元</param>
    /// <returns>中華民國年份的日期字串</returns>
    public static string ezToRocDateString(this DateTime date, char separator)
    {
        int year = (date.Year - 1911);
        string strDate = year.ToString() + separator + date.Month.ToString().PadLeft(2, '0') + separator + date.Day.ToString().PadLeft(2, '0');
        strDate += " " + date.Hour.ToString().PadLeft(2, '0') + ":" + date.Minute.ToString().PadLeft(2, '0') + ":" + date.Second.ToString().PadLeft(2, '0');
        return strDate;

    }

    /// <summary>
    /// 將 DateTime 物件格式化成中華民國年份的日期字串(年/月/日)
    /// <para>範例：</para>
    /// <para>string strDate = DateTime.Now.ezToRocShortDateString('/');</para>
    /// <para>回傳：</para>
    /// <para>strDate = "102/12/31"</para>
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="separator">分隔字元</param>
    /// <returns>中華民國年份的日期字串</returns>
    public static string ezToRocShortDateString(this DateTime date, char separator)
    {
        int year = (date.Year - 1911);
        return year.ToString() + separator + date.Month.ToString().PadLeft(2, '0') + separator + date.Day.ToString().PadLeft(2, '0');
    }

    /// <summary>
    /// 將 DateTime 物件格式化成西元年份的日期字串(年/月/日 時：分:秒)
    /// <para>範例：</para>
    /// <para>string strDate = DateTime.Now.ezToDateString('/');</para>
    /// <para>回傳：</para>
    /// <para>strDate = "2013/12/31 23:59:59"</para>
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="separator">分隔字元</param>
    /// <returns>西元年份的日期字串</returns>
    public static string ezToDateString(this DateTime date, char separator)
    {
        string strDate = date.Year.ToString() + separator + date.Month.ToString().PadLeft(2, '0') + separator + date.Day.ToString().PadLeft(2, '0');
        strDate += " " + date.Hour.ToString().PadLeft(2, '0') + ":" + date.Minute.ToString().PadLeft(2, '0') + ":" + date.Second.ToString().PadLeft(2, '0');
        return strDate;

    }

    /// <summary>
    /// 將 DateTime 物件格式化成西元年份的日期字串(年/月/日)
    /// <para>範例：</para>
    /// <para>string strDate = DateTime.Now.ezToShortDateString('/');</para>
    /// <para>回傳：</para>
    /// <para>strDate = "2013/12/31"</para>
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="separator">分隔字元</param>
    /// <returns>西元年份的日期字串</returns>
    public static string ezToShortDateString(this DateTime date, char separator)
    {
        string strDate = date.Year.ToString() + separator + date.Month.ToString().PadLeft(2, '0') + separator + date.Day.ToString().PadLeft(2, '0');
        return strDate;
    }


    /// <summary>
    /// 查詢兩個日期差異值
    /// <para>範例：</para>
    /// <para>DateTime date1 = DateTime.Now.AddHours(0);</para>
    /// <para>DateTime date2 = DateTime.Now.AddHours(24).AddMonths(3);</para>
    /// <para>double DiffDays = date1.ezDateDiff("day", date2);</para>
    /// </summary>
    /// <param name="startdate">日期一</param>
    /// <param name="datepart">類別</param>
    /// <param name="enddate">日期二</param>
    /// <returns></returns>
    public static double ezDateDiff(this DateTime startdate, string datepart, DateTime enddate)
    {
        // 參考 SQL Server 2005 線上叢書：http://technet.microsoft.com/zh-tw/library/ms189794.aspx

        double result = 0;

        TimeSpan tsDiff = new TimeSpan(enddate.Ticks - startdate.Ticks);

        switch (datepart.ToLower())
        {
            case "year":
            case "yyyy":
            case "yy":
                result = enddate.Year - startdate.Year;
                break;

            case "quarter":
            case "qq":
            case "q":
                // 每一季的平均天數
                const double AvgQuarterDays = 365 / 4;
                result = Math.Floor(tsDiff.TotalDays / AvgQuarterDays);
                break;

            case "month":
            case "mm":
            case "m":
                // 每一個月的平均天數
                const double AvgMonthDays = 365 / 12;
                result = Math.Floor(tsDiff.TotalDays / AvgMonthDays);
                break;

            case "day":
            case "dd":
            case "d":
                result = tsDiff.TotalDays;
                break;

            case "week":
            case "wk":
            case "ww":
                result = Math.Floor(tsDiff.TotalDays / 7);
                break;

            case "hour":
            case "hh":
                result = tsDiff.TotalHours;
                break;

            case "minute":
            case "mi":
            case "n":
                result = tsDiff.TotalMinutes;
                break;

            case "second":
            case "ss":
            case "s":
                result = tsDiff.TotalSeconds;
                break;

            case "millisecond":
            case "ms":
                result = tsDiff.TotalMilliseconds;
                break;

            default:
                throw new ArgumentException("You input a invalid datepart parameter.");
        }

        return result;
    }

    //在 TimeSpan 成員中，只要是 Total 開頭的屬性，都是回傳兩個時間差的
    //「總天數」、「總時數」、「總分鐘數」、「總秒數」、「總豪秒數」，
    //而且以 double 型別回傳，若無法整除都會有小數點出現。
    //另外 Days 屬性是回傳不足一天者採「無條件刪去法」的天數外 (整數型別)，
    //其餘的像是 Hours, Minutes, Seconds, Milliseconds 等屬性都是以用來描述
    //該差異天數中的餘數進行表示。舉個例子說明會比較清楚：
    //DateTime date1 = new DateTime(2008, 12,31, 23,59,59, DateTimeKind.Local);
    //DateTime date2 = new DateTime(2003, 2, 13, 23, 59, 59, DateTimeKind.Local);
    //TimeSpan s = new TimeSpan(date1.Ticks - date2.Ticks);
    //上述例子中，因為回傳的「差異天數」剛好為「整數」，並沒有時、分、秒、豪秒等餘數，
    //所以 s.Hours, s.Minutes, s.Seconds, s.Milliseconds 等屬性回傳值都會是 0 喔！

    /// <summary>
    /// 取得兩個日期之間的「天數」（不足一天者採「無條件刪去法」） 
    /// </summary>
    /// <param name="date">日期一</param>
    /// <param name="date2">日期二</param>
    /// <returns>天數</returns>
    public static int ezDays(this DateTime date, DateTime date2)
    {
        return new TimeSpan(date.Ticks - date2.Ticks).Days;
    }

    /// <summary>
    /// 取得兩個日期之間的「天數」（回傳型別為 double 雙精確度） 
    /// </summary>
    /// <param name="date">日期一</param>
    /// <param name="date2">日期二</param>
    /// <returns>天數</returns>
    public static double ezTotalDays(this DateTime date, DateTime date2)
    {
        return new TimeSpan(date.Ticks - date2.Ticks).TotalDays;
    }

    /// <summary>
    /// 取得兩個日期之間的「小時數」（回傳型別為 double 雙精確度）
    /// </summary>
    /// <param name="date">日期一</param>
    /// <param name="date2">日期二</param>
    /// <returns>小時數</returns>
    public static double ezTotalHours(this DateTime date, DateTime date2)
    {
        return new TimeSpan(date.Ticks - date2.Ticks).TotalHours;
    }

    /// <summary>
    /// 取得兩個日期之間的「分鐘數」（回傳型別為 double 雙精確度）
    /// </summary>
    /// <param name="date">日期一</param>
    /// <param name="date2">日期二</param>
    /// <returns>分鐘數</returns>
    public static double ezTotalMinutes(this DateTime date, DateTime date2)
    {
        return new TimeSpan(date.Ticks - date2.Ticks).TotalMinutes;
    }

    /// <summary>
    /// 取得週一的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezWeekFirstDate(this DateTime date)
    {
        return date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
    }

    /// <summary>
    /// 取得週日的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezWeekLastDate(this DateTime date)
    {
        DateTime startWeek = date.ezWeekFirstDate();
        return startWeek.AddDays(6);
    }

    /// <summary>
    /// 取得年初的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezYearFirstDate(this DateTime date)
    {
        return DateTime.Parse(string.Format("{0}-01-01", date.Year));
    }

    /// <summary>
    /// 取得年底的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezYearLastDate(this DateTime date)
    {
        return DateTime.Parse(string.Format("{0}-12-31", date.Year));
    }

    /// <summary>
    /// 取得月初的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezMonthFirstDate(this DateTime date)
    {
        return date.AddDays(1 - date.Day);
    }

    /// <summary>
    /// 取得月底的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezMonthLastDate(this DateTime date)
    {
        DateTime startMonth = date.ezMonthFirstDate();
        return startMonth.AddMonths(1).AddDays(-1);
    }

    /// <summary>
    /// 取得當月最大天數
    /// </summary>
    /// <returns></returns>
    public static int ezMonthDays(this DateTime date)
    {
        DateTime startMonth = date.ezMonthFirstDate();
        DateTime lastDay = startMonth.AddMonths(1).AddDays(-1);
        return lastDay.Day;
    }

    /// <summary>
    /// 取得季初的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime ezSeasonFirstDate(this DateTime date)
    {
        return date.AddMonths(0 - (date.Month - 1) % 3).AddDays(1 - date.Day);
    }

    /// <summary>
    /// 取得季末的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime SeasonLastDate(this DateTime date)
    {
        DateTime startQuarter = date.ezSeasonFirstDate();
        return startQuarter.AddMonths(3).AddDays(-1);
    }
    /// <summary>
    /// 取得年初的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime YearFirstDate(this DateTime date)
    {
        DateTime startYear = new DateTime(date.Year, 1, 1);
        return startYear;
    }

    /// <summary>
    /// 取得年末的日期
    /// </summary>
    /// <returns></returns>
    public static DateTime YearLastDate(this DateTime date)
    {
        DateTime endYear = new DateTime(date.Year, 12, 31);
        return endYear;
    }

    /// <summary>
    /// 取得中華民國格式的日期字串
    /// </summary>
    /// <returns></returns>
    public static string TaiwanDate(this DateTime date)
    {
        TaiwanCalendar twC = new TaiwanCalendar();
        return String.Format("民國 {0}/{1}/{2}", twC.GetYear(date), twC.GetMonth(date), twC.GetDayOfMonth(date));
    }

    /// <summary>
    /// 取得指定的日期格式字串
    /// </summary>
    /// <param name="date"></param>
    /// <param name="FormatCode"></param>
    /// <param name="ShowTime"></param>
    /// <returns></returns>
    public static string Today(this DateTime date, string FormatCode, bool ShowTime)
    {
        string strDate = "";
        switch (FormatCode.ToUpper())
        {
            case "C":
                if (ShowTime)
                    strDate = date.ToString("yyyy年MM月dd日HH時mm分ss秒");
                else
                    strDate = date.ToString("yyyy年MM月dd日");
                break;
            case "E":
                if (ShowTime)
                    strDate = date.ToString("yyyy/MM/dd HH:mm:ss");
                else
                    strDate = date.ToString("yyyy/MM/dd");
                break;
            case "M":
                if (ShowTime)
                    strDate = date.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                else
                    strDate = date.ToString("yyyy/MM/dd");
                break;
        }
        return strDate;
    }
}