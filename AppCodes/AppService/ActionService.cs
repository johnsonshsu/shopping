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
    /// 設定目前的 Action 名稱(或自行指定)
    /// </summary>
    /// <param name="action">Action 代號</param>
    public static void SetActionName(enAction action = enAction.None)
    {
        SessionService.ActionName = "";
        if (action == enAction.None)
        {
            var currentAction = (enAction)Enum.Parse(typeof(enAction), Action);
            SessionService.ActionName = SessionService.GetActionName(currentAction);
        }
        else
        {
            SessionService.ActionName = SessionService.GetActionName(action);
        }
    }
    /// <summary>
    /// 設定Action 名稱
    /// </summary>
    /// <param name="actionName">Action 名稱</param>
    public static void SetActionName(string actionName)
    {
        SessionService.ActionName = actionName;
    }
    /// <summary>
    /// 設定事件副標題
    /// </summary>
    /// <param name="subActionName">副標題</param>
    public static void SetSubActionName(string subActionName = "")
    {
        SessionService.SubActionName = subActionName;
    }
    /// <summary>
    /// Action 初始化
    /// </summary>
    public static void ActionInit()
    {
        SessionService.PrgNo = "";
        SessionService.PrgName = "";
        SessionService.ActionName = "";
        SessionService.SubActionName = "";
        SessionService.SearchText = "";
        SessionService.SortColumn = "";
        SessionService.SortDirection = "asc";
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
    /// Home Action 名稱
    /// </summary>
    public static string Home { get { return "Home"; } }

    /// <summary>
    /// Init Action 名稱
    /// </summary>
    public static string Init { get { return "Init"; } }

    /// <summary>
    /// Index Action 名稱
    /// </summary>
    public static string Index { get { return "Index"; } }

    /// <summary>
    /// CreateEdit Action 名稱
    /// </summary>
    public static string CreateEdit { get { return "CreateEdit"; } }

    /// <summary>
    /// Delete Action 名稱
    /// </summary>
    public static string Delete { get { return "Delete"; } }

    /// <summary>
    /// Sort Action 名稱
    /// </summary>
    public static string Sort { get { return "Sort"; } }

    /// <summary>
    /// Search Action 名稱
    /// </summary>
    public static string Search { get { return "Search"; } }

    /// <summary>
    /// SearchText 名稱
    /// </summary>
    public static string SearchText { get { return "SearchText"; } }

    /// <summary>
    /// PageInfo 名稱
    /// </summary>
    public static string PageInfo(int page = 1, int PageCount = 1) { return $"第 {page} 頁，共 {PageCount}頁"; ; }

    /// <summary>
    /// 設定 Action 程式參數
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetPrgInfo(string prgNo, string prgName)
    {
        SessionService.PrgNo = prgNo;
        SessionService.PrgName = prgName;
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