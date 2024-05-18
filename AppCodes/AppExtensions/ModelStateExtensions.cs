using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// ModelState 擴充功能
/// </summary>
public static class ModelStateExtensions
{
    /// <summary>
    /// 清除指定欄位錯誤資訊
    /// </summary>
    /// <param name="m">ModelState</param>
    /// <param name="fieldName">指定欄位</param>
    /// <returns></returns>
    public static ModelStateDictionary ClearError(this ModelStateDictionary m, string fieldName)
    {
        if (m.ContainsKey(fieldName))
            m[fieldName].Errors.Clear();
        return m;
    }

    /// <summary>
    /// 清除所有欄位錯誤資訊
    /// </summary>
    /// <param name="m">ModelState</param>
    /// <returns></returns>
    public static ModelStateDictionary ClearAllErrors(this ModelStateDictionary m)
    {
        foreach (var key in m.Keys)
        {
            m[key].Errors.Clear();
        }
        return m;
    }

    /// <summary>
    /// 取得第一個錯誤資訊，錯誤訊息必項是 {欄位名稱},{錯誤代碼}
    /// 例如：[Required(ErrorMessage = "EmpNo,Required")]
    /// </summary>
    /// <param name="m">ModelState</param>
    /// <returns></returns>
    public static List<string> GetFirstErrorData(this ModelStateDictionary m)
    {
        var value = new List<string>();
        var errors = m.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
        if (errors != null)
        {
            string str_message = errors[0].First().ErrorMessage;
            string str_ccolumn = str_message.Split(',')[0];
            string str_code = str_message.Split(',')[1];
            value.Add(str_ccolumn);
            value.Add(str_code);
        }
        return value;
    }
}