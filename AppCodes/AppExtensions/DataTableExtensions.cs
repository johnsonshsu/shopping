using System.Reflection;

/// <summary>
/// DataTable 型別擴充功能
/// </summary>
public static class DataTableExtensions
{
    public static List<T> DataTableToList<T>(this DataTable table) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        List<T> result = new List<T>();

        foreach (var row in table.Rows)
        {
            var item = CreateItemFromRow<T>((DataRow)row, properties);
            result.Add(item);
        }

        return result;
    }
    /// <summary>
    /// DataTable 型別轉成 List 型別
    /// </summary>
    /// <param name="table">DataTable</param>
    /// <param name="mappings">Dictionary對應設定</param>
    /// <typeparam name="T">泛型型別</typeparam>
    /// <returns></returns>
    public static List<T> DataTableToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        List<T> result = new List<T>();
        foreach (var row in table.Rows)
        {
            var item = CreateItemFromRow<T>((DataRow)row, properties, mappings);
            result.Add(item);
        }
        return result;
    }
    /// <summary>
    /// 從 DataRow 建立 Item
    /// </summary>
    /// <param name="row">DataRow 物件</param>
    /// <param name="properties">PropertyInfo 集合</param>
    /// <typeparam name="T">泛型型別</typeparam>
    /// <returns></returns>
    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
    {
        T item = new T();
        string ErrorMessage = "";
        foreach (var property in properties)
        {
            try
            {
                property.SetValue(item, row[property.Name], null);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        return item;
    }
    /// <summary>
    /// 從 DataRow 建立 Item
    /// </summary>
    /// <param name="row">DataRow 物件</param>
    /// <param name="properties">PropertyInfo 集合</param>
    /// <param name="mappings">Dictionary 對應設定</param>
    /// <typeparam name="T">泛型型別</typeparam>
    /// <returns></returns>
    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, Dictionary<string, string> mappings) where T : new()
    {
        T item = new T();
        string ErrorMessage = "";
        foreach (var property in properties)
        {
            if (mappings.ContainsKey(property.Name))
            {
                try
                {
                    property.SetValue(item, row[mappings[property.Name]], null);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }
        return item;
    }
}