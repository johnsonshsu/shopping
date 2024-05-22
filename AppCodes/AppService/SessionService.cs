/// <summary>
/// Session 類別
/// </summary>
public static class SessionService
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
    /// 登入使用者代號
    /// </summary>
    /// <value></value>
    public static string UserNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("UserNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("UserNo", value); }
    }
    /// <summary>
    /// 登入使用者姓名
    /// </summary>
    /// <value></value>
    public static string UserName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("UserName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("UserName", value); }
    }
    /// <summary>
    /// 登入使用者角色
    /// </summary>
    /// <value></value>
    public static string RoleNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("RoleNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("RoleNo", value); }
    }
    /// <summary>
    /// 是否已經登入
    /// </summary>
    /// <value></value>
    public static bool IsLogin
    {
        get
        {
            string str_value = "no";
            if (_context != null) str_value = _context.Session.Get<string>("IsLogin");
            if (str_value == null) str_value = "no";
            return (str_value == "yes");
        }
        set
        {
            string str_value = (value) ? "yes" : "no";
            _context?.Session.Set<string>("IsLogin", str_value);
        }
    }
    /// <summary>
    /// 主檔編號
    /// </summary>
    /// <value></value>
    public static string BaseNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("BaseNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("BaseNo", value); }
    }
    /// <summary>
    /// 使用者圖片
    /// </summary>
    /// <value></value>
    public static string UserImage
    {
        get
        {
            string str_value = "~/images/users/";
            //取得目前專案資料夾目錄路徑
            string FileNameOnServer = Directory.GetCurrentDirectory();
            //專案路徑加入要存入的資料夾路徑
            FileNameOnServer += "\\wwwroot\\images\\users\\";
            //以使用者名稱.jpg 存入
            FileNameOnServer += $"{UserNo}.jpg";
            //照片如果不存在則用預設照片
            if (File.Exists(FileNameOnServer))
                str_value += $"{UserNo}.jpg";
            else
                str_value += "User.jpg";
            //除理瀏覽器會在緩存圖片問題
            str_value += $"?t={DateTime.Now.ToString("yyyyMMddHHmmssff")}";
            return str_value;
        }
    }
    /// <summary>
    /// 登入使用者部門代號
    /// </summary>
    /// <value></value>
    public static string DeptNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("DeptNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("DeptNo", value); }
    }
    /// <summary>
    /// 登入使用者部門名稱
    /// </summary>
    /// <value></value>
    public static string DeptName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("DeptName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("DeptName", value); }
    }
    /// <summary>
    /// 登入使用者職稱代號
    /// </summary>
    /// <value></value>
    public static string TitleNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TitleNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TitleNo", value); }
    }
    /// <summary>
    /// 登入使用者職稱
    /// </summary>
    /// <value></value>
    public static string TitleName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("TitleName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("TitleName", value); }
    }

    /// <summary>
    /// 模組代號
    /// </summary>
    /// <value></value>
    public static string ModuleNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ModuleNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ModuleNo", value); }
    }

    /// <summary>
    /// 模組名稱
    /// </summary>
    /// <value></value>
    public static string ModuleName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ModuleName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ModuleName", value); }
    }

    /// <summary>
    /// 程式代號
    /// </summary>
    /// <value></value>
    public static string PrgNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("PrgNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("PrgNo", value); }
    }

    /// <summary>
    /// 程式名稱
    /// </summary>
    /// <value></value>
    public static string PrgName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("PrgName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("PrgName", value); }
    }

    /// <summary>
    /// 程式資訊
    /// </summary>
    /// <value></value>
    public static string PrgInfo
    {
        get
        {
            string str_value = (string.IsNullOrEmpty(PrgNo)) ? "" : $" {PrgNo}";
            str_value += PrgName;
            return str_value;
        }
    }

    /// <summary>
    /// 目前頁數
    /// </summary>
    /// <value></value>
    public static int Page
    {
        get
        {
            string str_value = "1";
            if (_context != null) str_value = _context.Session.Get<string>("Page");
            if (string.IsNullOrEmpty(str_value)) str_value = "1";
            return int.Parse(str_value);
        }
        set
        { _context?.Session.Set<string>("Page", value.ToString()); }
    }

    /// <summary>
    /// 總頁數
    /// </summary>
    /// <value></value>
    public static int PageCount
    {
        get
        {
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("PageCount");
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            return int.Parse(str_value);
        }
        set
        { _context?.Session.Set<string>("PageCount", value.ToString()); }
    }

    /// <summary>
    /// 每頁筆數
    /// </summary>
    /// <value></value>
    public static int PageSize
    {
        get
        {
            string str_value = "10";
            if (_context != null) str_value = _context.Session.Get<string>("PageSize");
            if (string.IsNullOrEmpty(str_value)) str_value = "10";
            return int.Parse(str_value);
        }
        set
        { _context?.Session.Set<string>("PageSize", value.ToString()); }
    }

    /// <summary>
    /// 分頁資訊
    /// </summary>
    /// <value></value>
    public static string PageInfo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("PageInfo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("PageInfo", value); }
    }

    /// <summary>
    /// 動作代號
    /// </summary>
    /// <value></value>
    public static string ActionNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ActionNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ActionNo", value); }
    }

    /// <summary>
    /// 動作名稱
    /// </summary>
    /// <value></value>
    public static string ActionName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("ActionName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("ActionName", value); }
    }

    /// <summary>
    /// 子標題名稱
    /// </summary>
    /// <value></value>
    public static string SubActionName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("SubActionName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("SubActionName", value); }
    }

    /// <summary>
    /// 卡片寛度
    /// </summary>
    /// <value></value>
    public static string CardSize
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("CardSize");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("CardSize", value); }
    }

    /// <summary>
    /// 搜尋文字
    /// </summary>
    /// <value></value>
    public static string SearchText
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("SearchText");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        {
            string str_value = "";
            if (value != null) str_value = value.ToString();
            _context?.Session.Set<string>("SearchText", str_value);
        }
    }
    /// <summary>
    /// 排序欄位
    /// </summary>
    /// <value></value>
    public static string SortColumn
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("SortColumn");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("SortColumn", value); }
    }
    /// <summary>
    /// 排序方式 (asc / desc) 小寫
    /// </summary>
    /// <value></value>
    public static string SortDirection
    {
        get
        {
            string str_value = "asc";
            if (_context != null) str_value = _context.Session.Get<string>("SortDirection");
            if (str_value == null) str_value = "asc";
            return str_value.ToLower();
        }
        set
        { _context?.Session.Set<string>("SortDirection", value.ToLower()); }
    }
    /// <summary>
    /// 是否有分頁功能
    /// </summary>
    /// <value></value>
    public static bool IsPageSize
    {
        get
        {
            string str_value = "no";
            if (_context != null) str_value = _context.Session.Get<string>("IsPageSize");
            if (str_value == null) str_value = "no";
            return (str_value == "yes");
        }
        set
        {
            string str_value = (value) ? "yes" : "no";
            _context?.Session.Set<string>("IsPageSize", str_value);
        }
    }
    /// <summary>
    /// 是否有搜尋功能
    /// </summary>
    /// <value></value>
    public static bool IsSearch
    {
        get
        {
            string str_value = "no";
            if (_context != null) str_value = _context.Session.Get<string>("IsSearch");
            if (str_value == null) str_value = "no";
            return (str_value == "yes");
        }
        set
        {
            string str_value = (value) ? "yes" : "no";
            _context?.Session.Set<string>("IsSearch", str_value);
        }
    }
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
    /// 字串變數1
    /// </summary>
    /// <value></value>
    public static string StringValue1
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("StringValue1");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("StringValue1", value); }
    }
    /// <summary>
    /// 字串變數2
    /// </summary>
    /// <value></value>
    public static string StringValue2
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("StringValue2");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("StringValue2", value); }
    }
    /// <summary>
    /// 字串變數3
    /// </summary>
    /// <value></value>
    public static string StringValue3
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("StringValue3");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("StringValue3", value); }
    }
    /// <summary>
    /// 數字變數1
    /// </summary>
    /// <value></value>
    public static int IntValue1
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("IntValue1");
            if (str_value == null) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        {
            string str_value = value.ToString();
            _context?.Session.Set<string>("IntValue1", str_value);
        }
    }
    /// <summary>
    /// 數字變數2
    /// </summary>
    /// <value></value>
    public static int IntValue2
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("IntValue2");
            if (str_value == null) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        {
            string str_value = value.ToString();
            _context?.Session.Set<string>("IntValue2", str_value);
        }
    }
    /// <summary>
    /// 數字變數3
    /// </summary>
    /// <value></value>
    public static int IntValue3
    {
        get
        {
            int int_value = 0;
            string str_value = "0";
            if (_context != null) str_value = _context.Session.Get<string>("IntValue3");
            if (str_value == null) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
            return int_value;
        }
        set
        {
            string str_value = value.ToString();
            _context?.Session.Set<string>("IntValue3", str_value);
        }
    }
    /// <summary>
    /// Category No
    /// </summary>
    /// <value></value>
    public static string CategoryNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("CategoryNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("CategoryNo", value); }
    }

    /// <summary>
    /// Category Name
    /// </summary>
    /// <value></value>
    public static string CategoryName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("CategoryName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("CategoryName", value); }
    }

    /// <summary>
    /// Category Image
    /// </summary>
    /// <value></value>
    public static string CategoryImage
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("CategoryImage");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("CategoryImage", value); }
    }
    /// <summary>
    /// Message Text
    /// </summary>
    /// <value></value>
    public static string MessageText
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("MessageText");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("MessageText", value); }
    }
    /// <summary>
    /// Message Text
    /// </summary>
    /// <value></value>
    public static string SortNo
    {
        get
        {
            string str_value = "Product";
            if (_context != null) str_value = _context.Session.Get<string>("SortNo");
            if (str_value == null) str_value = "Product";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("SortNo", value); }
    }

    /// <summary>
    /// 設定程式預設事件
    /// </summary>
    /// <param name="subActionName">副標題</param>
    public static void SetPrgInit(string subActionName = "")
    {
        SortColumn = "";
        SortDirection = "";
        SearchText = "";
        if (ActionService.Controller == "Home")
        {
            PrgNo = "Home";
            PrgName = "首頁";
            SubActionName = "";
            return;
        }
        if (!string.IsNullOrEmpty(subActionName))
        {
            SubActionName = subActionName;
            return;
        }
        SubActionName = PrgInfo;
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetProgramInfo(string prgNo, string prgName)
    {
        PrgNo = prgNo;
        PrgName = prgName;
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="prgName">程式名稱</param>
    /// <param name="isPageSize">是否有分頁</param>
    /// <param name="isSearch">是否有搜尋</param>
    /// <param name="pageSize">每頁筆數</param>
    public static void SetProgramInfo(string prgNo, string prgName, bool isPageSize, bool isSearch, int pageSize)
    {
        PrgNo = prgNo;
        PrgName = prgName;
        IsPageSize = isPageSize;
        IsSearch = isSearch;
        PageSize = pageSize;
    }

    /// <summary>
    /// 取得分頁資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <param name="pageCount">總頁數</param>
    /// <returns></returns>
    public static void SetPageInfo(int page, int pageCount)
    {
        Page = page;
        PageCount = pageCount;
        PageInfo = $"({Page} / {PageCount})";
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="action">表單動作</param>
    /// <param name="cardSize">卡片寛度大小</param>
    /// <param name="id">Id</param>
    /// <param name="subActionName">子標題名稱</param>
    public static void SetActionInfo(enAction action, enCardSize cardSize, int id = 0, string subActionName = "")
    {
        if (action == enAction.CreateEdit && id == 0) action = enAction.Create;
        if (action == enAction.CreateEdit && id > 0) action = enAction.Edit;
        List<SelectListItem> actionList = new List<SelectListItem>();
        var actinList = Enum.GetValues(typeof(enAction)).Cast<enAction>().ToList();
        foreach (var item in actinList)
        {
            string str_text = GetActionName(item);
            string str_value = Enum.GetName(typeof(enAction), item);
            actionList.Add(new SelectListItem() { Text = str_text, Value = str_value });
        }
        ActionNo = Enum.GetName(typeof(enAction), action);
        var data = actionList.Where(m => m.Value == ActionNo).FirstOrDefault();
        ActionName = (data == null) ? ActionNo : data.Text;
        string str_size = Enum.GetName(typeof(enCardSize), cardSize).ToLower();
        CardSize = $"card-size-{str_size}";
        SubActionName = subActionName;
    }
    /// <summary>
    /// 取得動作名稱
    /// </summary>
    /// <param name="action">動作</param>
    /// <returns></returns>
    public static string GetActionName(enAction action)
    {
        if (action == enAction.None) return "";
        if (action == enAction.Home) return "首頁";
        if (action == enAction.Dashboard) return "儀表板";
        if (action == enAction.Index) return "列表";
        if (action == enAction.List) return "列表";
        if (action == enAction.Detail) return "明細";
        if (action == enAction.Create) return "新增";
        if (action == enAction.Edit) return "修改";
        if (action == enAction.Delete) return "刪除";
        if (action == enAction.Search) return "查詢";
        if (action == enAction.Sort) return "排序";
        if (action == enAction.print) return "列印";
        if (action == enAction.Upload) return "上傳";
        if (action == enAction.UploadImage) return "上傳圖片";
        if (action == enAction.UploadFile) return "上傳檔案";
        if (action == enAction.Download) return "下載";
        if (action == enAction.Confirm) return "確認";
        if (action == enAction.Invalid) return "作廢";
        if (action == enAction.Approve) return "核准";
        if (action == enAction.Reject) return "駁回";
        if (action == enAction.Login) return "登入";
        if (action == enAction.Register) return "註冊";
        if (action == enAction.Forget) return "忘記密碼";
        if (action == enAction.Reset) return "重設";
        if (action == enAction.ResetPassword) return "重設密碼";
        return "";
    }
}

/// <summary>
/// 表單動作枚舉類型
/// </summary>
public enum enAction
{
    None,
    Home,
    Dashboard,
    Index,
    List,
    Detail,
    Create,
    Edit,
    CreateEdit,
    Delete,
    Search,
    Sort,
    print,
    Upload,
    UploadImage,
    UploadFile,
    Download,
    Confirm,
    Invalid,
    Approve,
    Reject,
    Login,
    Register,
    Forget,
    Reset,
    ResetPassword
}

/// <summary>
/// 卡片寛度大小枚舉類型
/// </summary>
public enum enCardSize
{
    Small,
    Medium,
    Larget,
    Max
}