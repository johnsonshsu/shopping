using Microsoft.Extensions.Localization;
using System.Reflection;

/// <summary>
/// 將共享資源進行分組的虛擬類別
/// </summary>
public class SharedResource { }

/// <summary>
/// 語系物件
/// </summary>
public class CultureObject
{
    /// <summary>
    /// 語系名稱
    /// </summary>
    /// <value></value>
    public string Name { get; set; }
    /// <summary>
    /// 語系名稱標題
    /// </summary>
    /// <value></value>
    public string Caption { get; set; }
    /// <summary>
    /// flag-incons 的國旗代號 
    /// 可至 https://flagicons.lipis.dev/ 網站查詢
    /// </summary>
    /// <value></value>
    public string FlagName { get; set; }
}

/// <summary>
/// 多國語系用類別
/// </summary>
public class LanguageService
{
    /// <summary>
    /// 多國語系字串處理物件
    /// </summary>
    private readonly IStringLocalizer _localizer;

    /// <summary>
    /// 語系名稱陣列
    /// </summary>
    /// <returns></returns>
    public List<CultureObject> CultureList = new List<CultureObject>()
    {
        new CultureObject() { Name = "en-US" , Caption = "English" , FlagName = "us"},
        new CultureObject() { Name = "zh-TW" , Caption = "繁體中文" , FlagName = "tw"}
    };

    /// <summary>
    /// 多國語系建構子，注入 IStringLocalizerFactory
    /// </summary>
    /// <param name="factory"></param>
    public LanguageService(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create(type.Name, assemblyName.Name);
    }

    /// <summary>
    /// 以 Key 值來讀取多國語系文字
    /// </summary>
    /// <param name="keyName">Key 值</param>
    /// <returns></returns>
    public LocalizedString GetResourceValue(string keyName)
    {
        return _localizer[keyName];
    }

    /// <summary>
    /// 取得目前的語系名稱
    /// </summary>
    public string CurrentLanguageName { get { return Thread.CurrentThread.CurrentCulture.Name; } }

    /// <summary>
    /// 取得目前的語系名稱標題
    /// </summary>
    public string CurrentLanguageCaption
    {
        get
        {
            var data = CultureList.Where(m => m.Name == CurrentLanguageName).FirstOrDefault();
            if (data != null) return data.Caption;
            return "undefined";
        }
    }
}