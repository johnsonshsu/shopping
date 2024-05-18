using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Enum 服務類別
/// </summary>
/// <typeparam name="T">Enum 物件</typeparam>
public class EnumService<T> : BaseClass
{
    /// <summary>
    /// 將 Enum 轉成 SelectListItem 陣列集合
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> DropdownList()
    {
        return Enum.GetNames(typeof(T)).Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
    }
    public List<SelectListItem> EnumList()
    {
        List<SelectListItem> enumList = new List<SelectListItem>();
        var values = ValueList();
        var names = NameList();
        if (values.Count > 0 && names.Count > 0 && values.Count == names.Count)
        {
            for (int i = 0; i < values.Count; i++)
            {
                enumList.Add(new SelectListItem()
                {
                    Value = values[i].ToString(),
                    Text = names[i]
                });
            }
        }
        return enumList;
    }
    public string GetText(int index)
    {
        string str_value = index.ToString();
        string str_text = "";
        var data = EnumList().Where(x => x.Value == str_value).FirstOrDefault();
        if (data != null) str_text = data.Text;
        return str_text;
    }
    /// <summary>
    /// 將 Enum 轉成 SelectListItem 陣列集合
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> DropdownNameValueList()
    {
        var names = NameList();
        var values = ValueList();
        List<SelectListItem> items = new List<SelectListItem>();
        if (names.Count() > 0)
        {
            for (int i = 0; i < names.Count(); i++)
            {
                items.Add(new SelectListItem() { Text = names[i], Value = values[i].ToString() });
            }
        }
        return items;
    }
    /// <summary>
    /// 將 Enum 的 Name 轉成字串集合
    /// </summary>
    /// <returns></returns>
    public List<string> NameList()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
    /// <summary>
    /// 將 Enum 的Value 轉成整數集合
    /// </summary>
    /// <returns></returns>
    public List<int> ValueList()
    {
        List<int> values = new List<int>();
        foreach (int i in Enum.GetValues(typeof(T)))
        {
            values.Add(i);
        }

        return values;
    }
    /// <summary>
    /// 檢查指定的 Name 是否存在
    /// </summary>
    /// <param name="name">指定的 Name</param>
    /// <returns></returns>
    public bool NameExists(string name)
    {
        return Enum.IsDefined(typeof(T), name);
    }
    /// <summary>
    /// 檢查指定的 Value 是否存在
    /// </summary>
    /// <param name="value">指定的 Value</param>
    /// <returns></returns>
    public bool ValueExists(int value)
    {
        return Enum.IsDefined(typeof(T), value);
    }
}