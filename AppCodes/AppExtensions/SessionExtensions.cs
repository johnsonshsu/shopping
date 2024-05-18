using Newtonsoft.Json;

/// <summary>
/// Session 功能擴充程式
/// </summary>
public static class SessionExtensions
{
    /// <summary>
    /// 設定 Session 值
    /// </summary>
    /// <param name="session">Session 物件</param>
    /// <param name="key">Session 名稱</param>
    /// <param name="value">Session 值</param>
    public static void Set<T>(this ISession session, string key, string value)
    {
        if (value == null) value = "";
        session.SetString(key, value);
    }

    /// <summary>
    /// 取得 Session 值
    /// </summary>
    /// <param name="session">Session 物件</param>
    /// <param name="key">Session 名稱</param>
    /// <returns></returns>
    public static string Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value ?? "";
    }
    /// <summary>
    /// 設定 Session 值
    /// </summary>
    /// <param name="session">Session 物件</param>
    /// <param name="key">Session 名稱</param>
    /// <param name="value">Session 值</param>
    /// <typeparam name="T">泛型物件</typeparam>
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        if (value != null)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }

    /// <summary>
    /// 取得 Session 值
    /// </summary>
    /// <param name="session">Session 物件</param>
    /// <param name="key">Session 名稱</param>
    /// <typeparam name="T">泛型物件</typeparam>
    /// <returns></returns>
    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}