/// <summary>
/// FullCalendar 相關功能類別
/// </summary>
public static class CalendarService
{
    /// <summary>
    /// HttpContextAccessor 物件
    /// </summary>
    /// <returns></returns>
    public static IHttpContextAccessor _contextAccessor { get; set; } = new HttpContextAccessor();
    /// <summary>
    /// HttpContext 物件
    /// </summary>
    public static HttpContext? _context { get { return _contextAccessor.HttpContext; } }
    /// <summary>
    /// 記錄 Id
    /// </summary>
    /// <value></value>
    public static int Id
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("Id");
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        { _context?.Session.Set<string>("Id", value.ToString()); }
    }
    /// <summary>
    /// 行事曆類別
    /// </summary>
    /// <value></value>
    public static string TargetCode
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TargetCode");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TargetCode", value); }
    }
    /// <summary>
    /// 行事曆類別名稱
    /// </summary>
    /// <value></value>
    public static string TargetCodeName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TargetCodeName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TargetCodeName", value); }
    }
    /// <summary>
    /// 行事曆編號
    /// </summary>
    /// <value></value>
    public static string TargetNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TargetNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TargetNo", value); }
    }
    /// <summary>
    /// 行事曆編號名稱
    /// </summary>
    /// <value></value>
    public static string TargetName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TargetName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TargetName", value); }
    }
}

/// <summary>
/// Google 行事曆 Id
/// </summary>
public class GoogleCalendarId
{
    public string? googleCalendarId { get; set; }
    public string? className { get; set; }
}

/// <summary>
/// 行事曆行程
/// </summary>
public class CalendarEvent
{
    /// <summary>
    /// 行程 Id
    /// </summary>
    /// <value></value>
    public int id { get; set; } = 0;
    /// <summary>
    /// 群組 Id
    /// </summary>
    /// <value></value>
    public int groupId { get; set; } = 0;
    /// <summary>
    /// 行程標題
    /// </summary>
    /// <value></value>
    public string title { get; set; } = "";
    /// <summary>
    /// 行程網址
    /// </summary>
    /// <value></value>
    public string url { get; set; } = "";
    /// <summary>
    /// 開始時間
    /// </summary>
    /// <value></value>
    public string start { get; set; } = "";
    /// <summary>
    /// 結束時間
    /// </summary>
    /// <value></value>
    public string end { get; set; } = "";
    /// <summary>
    /// 全天行程
    /// </summary>
    /// <value></value>
    public bool allDay { get; set; } = false;
    /// <summary>
    /// 行程詳細說明
    /// </summary>
    /// <value></value>
    public string description { get; set; } = "";
}