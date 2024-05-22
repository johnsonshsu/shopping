using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// 使用 Dapper 來執行 SQL 指令功能
/// </summary>
/// <typeparam name="TEntity">泛型表格物件</typeparam>
public interface IDapperSql<TEntity>
{
    /// <summary>
    /// 連線字串名稱
    /// </summary>
    string ConnName { get; set; }
    /// <summary>
    /// 預設 SQL 排序指令
    /// </summary>
    string DefaultOrderByColumn { get; set; }
    /// <summary>
    /// 預設 SQL 排序方式指令
    /// </summary>
    string DefaultOrderByDirection { get; set; }
    /// <summary>
    /// OrderBy 排序指令
    /// </summary>
    /// <value></value>
    string OrderByColumn { get; set; }
    /// <summary>
    /// OrderBy 排序方式
    /// </summary>
    string OrderByDirection { get; set; }
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    string GetSQLSelect();
    /// <summary>
    /// 取得模擬搜尋的欄位集合
    /// </summary>
    /// <returns></returns>
    List<string> GetSearchColumns();
    /// <summary>
    /// SQL 查詢條件式指令
    /// </summary>
    /// <returns></returns>
    string GetSQLWhere();
    /// <summary>
    /// SQL 查詢排序指令
    /// </summary>
    /// <returns></returns>
    string GetSQLOrderBy();
    /// <summary>
    /// 取得下拉式選單資料集
    /// </summary>
    /// <param name="valueColumn">資料欄位名稱</param>
    /// <param name="textColumn">顯示欄位名稱</param>
    /// <param name="orderColumn">排序欄位名稱</param>/// 
    /// <param name="textIncludeValue">顯示欄位名稱是否顯示資料欄位</param>
    /// <returns></returns>
    List<SelectListItem> GetDropDownList(string valueColumn, string textColumn, string orderColumn, bool textIncludeValue = false);
    /// <summary>
    /// SQL 新增指令
    /// </summary>
    /// <returns></returns>
    string GetSQLInsert();
    /// <summary>
    /// SQL 刪除指令
    /// </summary>
    /// <returns></returns>
    string GetSQLDelete();
    /// <summary>
    /// SQL 修改指令
    /// </summary>
    /// <returns></returns>
    string GetSQLUpdate();
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    TEntity GetData(int id);
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    Task<TEntity> GetDataAsync(int id);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <returns></returns>
    List<TEntity> GetDataList();
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    List<TEntity> GetDataList(DynamicParameters parm);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    List<TEntity> GetDataList(int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    List<TEntity> GetDataList(DynamicParameters parm, int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    List<TEntity> GetDataList(string searchString);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    List<TEntity> GetDataList(DynamicParameters parm, string searchString);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    List<TEntity> GetDataList(string searchString, int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    List<TEntity> GetDataList(DynamicParameters parm, string searchString, int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync();
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(DynamicParameters parm);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(string searchString);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, string searchString);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(string searchString, int page, int pageSize);
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, string searchString, int page, int pageSize);
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    void CreateEdit(TEntity model, int id = 0);
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    Task CreateEditAsync(TEntity model, int id = 0);
    /// <summary>
    /// 新增資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    void Create(TEntity model);
    /// <summary>
    /// 新增資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    Task CreateAsync(TEntity model);
    /// <summary>
    /// 更新資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    void Edit(TEntity model);
    /// <summary>
    /// 更新資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    Task EditAsync(TEntity model);
    /// <summary>
    /// 刪除資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    void Delete(int id = 0);
    /// <summary>
    /// 刪除資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    Task DeleteAsync(int id = 0);
}