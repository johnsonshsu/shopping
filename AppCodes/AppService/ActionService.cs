/// <summary>
/// Action 服務類別
/// </summary>
public static class ActionService
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
    /// 取得目前的 Area 名稱
    /// </summary>
    public static string Area
    {
        get
        {
            object value = _context.GetRouteData().Values["Area"];
            return (value == null) ? "" : value.ToString();
        }
    }
    /// <summary>
    /// 取得目前的 Controller 名稱
    /// </summary>
    public static string Controller
    {
        get
        {
            object value = _context.GetRouteData().Values["Controller"];
            return (value == null) ? "" : value.ToString();
        }
    }
    /// <summary>
    /// 取得目前的 Action 名稱
    /// </summary>
    public static string Action
    {
        get
        {
            object value = _context.GetRouteData().Values["Action"];
            return (value == null) ? "" : value.ToString();
        }
    }
    /// <summary>
    /// 取得目前的 id 值
    /// </summary>
    public static string id
    {
        get
        {
            object value = _context.GetRouteData().Values["id"];
            return (value == null) ? "" : value.ToString();
        }
    }
    /// <summary>
    /// 取得目前的頁數
    /// </summary>
    public static string Page
    {
        get
        {
            object value = _context.GetRouteData().Values["id"];
            return (value == null) ? "1" : value.ToString();
        }
    }
    /// <summary>
    /// 取得目前的 Http 通訊協定是 Http 還是 Https，如 https
    /// </summary>
    public static string Http
    {
        get { return _context.Request.Scheme.ToString(); }
    }
    /// <summary>
    /// 取得目前的 Domain Name，如 localhsot:2283
    /// </summary>
    public static string Host
    {
        get { return _context.Request.Host.ToString(); }
    }
    /// <summary>
    /// 取得目前的 Http 及 Domain Name 組合，如 https://localhsot:2283
    /// </summary>
    /// <value></value>
    public static string HttpHost
    {
        get { return $"{Http}://{Host}"; }
    }
    /// <summary>
    /// Row ID
    /// </summary>
    /// <value></value>
    public static int RowId
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("RowId");
            if (str_value == null) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        { _context?.Session.Set<string>("RowId", value.ToString()); }
    }
    /// <summary>
    /// Row Data
    /// </summary>
    /// <value></value>
    public static string RowData
    {
        get
        {
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("RowData");
            if (str_value == null) str_value = "0";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("RowData", value); }
    }
    /// <summary>
    /// View Action 名稱
    /// </summary>
    /// <value></value>
    public static string ViewActionName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ViewActionName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ViewActionName", value); }
    }

    /// <summary>
    /// View 程式代號
    /// </summary>
    /// <value></value>
    public static string ViewPrgNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ViewPrgNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ViewPrgNo", value); }
    }

    /// <summary>
    /// View 程式名稱
    /// </summary>
    /// <value></value>
    public static string ViewPrgName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ViewPrgName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ViewPrgName", value); }
    }

    /// <summary>
    /// View 程式名稱
    /// </summary>
    /// <value></value>
    public static string ViewPrgInfo
    {
        get
        {
            if (string.IsNullOrEmpty(ViewPrgNo)) return ViewPrgName;
            return $"{ViewPrgNo} {ViewPrgName}";
        }
    }

    /// <summary>
    /// 設定 Action 參數
    /// </summary>
    /// <param name="actionName">Action 名稱</param>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetActionData(string actionName, string prgNo, string prgName)
    {
        ViewActionName = actionName;
        ViewPrgNo = prgNo;
        ViewPrgName = prgName;
    }

    /// <summary>
    /// 取得目前控制器的指定 Action 的網址 
    /// </summary>
    /// <param name="actionName">Action 名稱</param>
    /// <returns></returns>
    public static string CurrentActionLinkUrl(string actionName)
    {
        string str_area = string.IsNullOrEmpty(Area) ? "" : $"/{Area}";
        string str_controller = string.IsNullOrEmpty(Controller) ? "" : $"/{Controller}";
        string str_action = $"/{actionName}";
        string str_url = $"{HttpHost}{str_area}{str_controller}{str_action}";
        var location = new Uri(str_url);
        return location.ToString();
    }
}