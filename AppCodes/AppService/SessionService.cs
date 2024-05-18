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
    public static string SubHeaderName
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("SubHeaderName");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("SubHeaderName", value); }
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
    /// <param name="subHeaderName">副標題</param>
    public static void SetPrgInit(string subHeaderName = "")
    {
        SortColumn = "";
        SortDirection = "";
        SearchText = "";
        if (ActionService.Controller == "Home")
        {
            PrgNo = "Home";
            PrgName = "首頁";
            SubHeaderName = "";
            return;
        }
        if (!string.IsNullOrEmpty(subHeaderName))
        {
            SubHeaderName = subHeaderName;
            return;
        }
        SubHeaderName = PrgInfo;
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
        PageInfo = $"({page} / {pageCount})";
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="action">表單動作</param>
    /// <param name="cardSize">卡片寛度大小</param>
    /// <param name="id">Id</param>
    /// <param name="subHeaderName">子標題名稱</param>
    public static void SetActionInfo(enAction action, enCardSize cardSize, int id = 0, string subHeaderName = "")
    {
        if (action == enAction.CreateEdit && id == 0) action = enAction.Create;
        if (action == enAction.CreateEdit && id > 0) action = enAction.Edit;
        List<SelectListItem> actionList = new List<SelectListItem>();
        actionList.Add(new SelectListItem() { Text = "首頁", Value = "Home" });
        actionList.Add(new SelectListItem() { Text = "儀表板", Value = "Dashboard" });
        actionList.Add(new SelectListItem() { Text = "列表", Value = "Index" });
        actionList.Add(new SelectListItem() { Text = "列表", Value = "List" });
        actionList.Add(new SelectListItem() { Text = "明細", Value = "Detail" });
        actionList.Add(new SelectListItem() { Text = "新增", Value = "Create" });
        actionList.Add(new SelectListItem() { Text = "修改", Value = "Edit" });
        actionList.Add(new SelectListItem() { Text = "刪除", Value = "Delete" });
        actionList.Add(new SelectListItem() { Text = "查詢", Value = "Search" });
        actionList.Add(new SelectListItem() { Text = "排序", Value = "Sort" });
        actionList.Add(new SelectListItem() { Text = "列印", Value = "print" });
        actionList.Add(new SelectListItem() { Text = "上傳", Value = "Upload" });
        actionList.Add(new SelectListItem() { Text = "上傳圖片", Value = "UploadImage" });
        actionList.Add(new SelectListItem() { Text = "上傳檔案", Value = "UploadFile" });
        actionList.Add(new SelectListItem() { Text = "下載", Value = "Download" });
        actionList.Add(new SelectListItem() { Text = "確認", Value = "Confirm" });
        actionList.Add(new SelectListItem() { Text = "作廢", Value = "Invalid" });
        actionList.Add(new SelectListItem() { Text = "核准", Value = "Approve" });
        actionList.Add(new SelectListItem() { Text = "駁回", Value = "Reject" });
        actionList.Add(new SelectListItem() { Text = "登入", Value = "Login" });
        actionList.Add(new SelectListItem() { Text = "註冊", Value = "Register" });
        actionList.Add(new SelectListItem() { Text = "忘記密碼", Value = "Forget" });
        ActionNo = Enum.GetName(typeof(enAction), action);
        var data = actionList.Where(m => m.Value == ActionNo).FirstOrDefault();
        ActionName = (data == null) ? ActionNo : data.Text;
        string str_size = Enum.GetName(typeof(enCardSize), cardSize).ToLower();
        CardSize = $"card-size-{str_size}";
        SubHeaderName = "";
    }
}

/// <summary>
/// 表單動作枚舉類型
/// </summary>
public enum enAction
{
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
    Forget
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