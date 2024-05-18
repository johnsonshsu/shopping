using System.Reflection;

/// <summary>
/// 應用程式參數類別
/// </summary>
public static class AppService
{
    public static string ProjectName { get { return Assembly.GetCallingAssembly().GetName().Name; } }
    /// <summary>
    /// 應用程式名稱
    /// </summary>
    /// <value></value>
    public static string AppName
    {
        get
        {
            return GetApplicationString("AppName");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppVersion
    {
        get
        {
            return GetApplicationString("AppVersion");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppDescription
    {
        get
        {
            return GetApplicationString("AppDescription");
        }
    }
    /// <summary>
    /// 應用程式版本
    /// </summary>
    /// <value></value>
    public static string AppKeywords
    {
        get
        {
            return GetApplicationString("AppKeywords");
        }
    }
    /// <summary>
    /// 網站設計者
    /// </summary>
    /// <value></value>
    public static string Designer
    {
        get
        {
            return GetApplicationString("Designer");
        }
    }
    /// <summary>
    /// 系統管理者名稱
    /// </summary>
    /// <value></value>
    public static string AdminName
    {
        get
        {
            return GetApplicationString("AdminName");
        }
    }
    /// <summary>
    /// 系統管理者電子信箱
    /// </summary>
    /// <value></value>
    public static string AdminEmail
    {
        get
        {
            return GetApplicationString("AdminEmail");
        }
    }
    /// <summary>
    /// 除錯模型
    /// </summary>
    /// <value></value>
    public static bool DebugMode
    {
        get
        {
            string str_value = GetApplicationString("DebugMode");
            return (str_value == "1");
        }
    }
    /// <summary>
    /// 取得連線字串
    /// </summary>
    /// <param name="KeyName">參數名稱</param>
    /// <returns></returns>
    public static string GetApplicationString(string KeyName)
    {
        string str_section = $"Applications:{KeyName}";
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        var config = builder.Build();
        return config.GetValue<string>(str_section) ?? "";
    }
}