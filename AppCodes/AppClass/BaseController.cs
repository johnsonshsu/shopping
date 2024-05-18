using Newtonsoft.Json;

/// <summary>
/// 底層控制器類別
/// </summary>
public class BaseController : Controller
{
    /// <summary>
    /// 讀取 Session 字串值，不使用 Json 反序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="defaultValue">預設值</param>
    /// <returns></returns>
    protected string GetSessionValue(string keyName, string defaultValue = "")
    {
        return HttpContext.Session.Get<string>(keyName) ?? defaultValue;
    }
    /// <summary>
    /// 設定 Session 字串值，不使用 Json 序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="value">Session 字串值</param> 
    protected void SetSessionValue(string keyName, string value)
    {
        HttpContext.Session.Set<string>(keyName, value);
    }
    /// <summary>
    /// 讀取 Session 字串值，使用 Json 反序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <returns></returns>
    protected T GetSessionObjectValue<T>(string keyName)
    {
        return JsonConvert.DeserializeObject<T>(GetSessionValue(keyName));
    }
    /// <summary>
    /// 設定 Session 字串值，使用 Json 序列化
    /// </summary>
    /// <param name="keyName">Session 名稱</param>
    /// <param name="value">Session 字串值</param>
    protected void SetSessionObjectValue<T>(string keyName, string value)
    {
        string str_value = JsonConvert.SerializeObject(value);
        HttpContext.Session.Set<T>(keyName, str_value);

    }
}