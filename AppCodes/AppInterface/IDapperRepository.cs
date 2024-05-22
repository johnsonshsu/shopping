using System.Data;
using Dapper;

/// <summary>
/// Dapper Repository 介面
/// </summary>
public interface IDapperRepository
{
    /// <summary>
    /// 連線字串名稱
    /// </summary>
    string ConnectionName { get; set; }
    /// <summary>
    /// 連線字串
    /// </summary>
    string ConnectionString { get; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    string ErrorMessage { get; set; }
    /// <summary>
    /// 命令類型 (SQL 語法/預儲程序)
    /// </summary>
    CommandType CommandType { get; }
    /// <summary>
    /// 取得連線字串
    /// </summary>
    /// <param name="connectionName">連線字串名稱</param>
    /// <returns></returns>
    string GetConnectionString(string connectionName);
    /// <summary>
    /// 檢查 connectionName 中的 connectionString 是否連線正常
    /// </summary>
    /// <param name="connectionName"></param>
    /// <returns></returns>
    string CheckConnectionName(string connectionName);
    /// <summary>
    /// 檢查 connectionString 是否連線正常
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    string CheckConnectionString(string connectionString);
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    int Execute(string query);
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    int Execute(string query, DynamicParameters parameters);
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)(非同步)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string query);
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)(非同步)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string query, DynamicParameters parameters);

    /// <summary>
    /// 讀取單筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    T ReadSingle<T>(string query);
    /// <summary>
    /// 讀取單筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    T ReadSingle<T>(string query, DynamicParameters parameters);
    /// <summary>
    /// 讀取單筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    Task<T> ReadSingleAsync<T>(string query);
    /// <summary>
    /// 讀取單筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    Task<T> ReadSingleAsync<T>(string query, DynamicParameters parameters);
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    List<T> ReadAll<T>(string query);
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    List<T> ReadAll<T>(string query, int page, int pageSize);
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    List<T> ReadAll<T>(string query, DynamicParameters parameters);
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param> 
    /// <returns></returns>
    List<T> ReadAll<T>(string query, DynamicParameters parameters, int page, int pageSize);
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    Task<List<T>> ReadAllAsync<T>(string query);
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param> 
    /// <returns></returns>
    Task<List<T>> ReadAllAsync<T>(string query, int page, int pageSize);
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    Task<List<T>> ReadAllAsync<T>(string query, DynamicParameters parameters);
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    Task<List<T>> ReadAllAsync<T>(string query, DynamicParameters parameters, int page, int pageSize);
    /// <summary>
    /// 檢查目前 Entity 內指定欄位值是否重覆(同步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columnName"></param>
    /// <param name="conditional"></param>
    /// <returns></returns>
    bool IsDuplicated<T>(T entity, string columnName, string conditional = "");
    /// <summary>
    /// 檢查目前 Entity 內指定欄位值是否重覆(非同步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columnName"></param>
    /// <param name="conditional"></param>
    /// <returns></returns>
    Task<bool> IsDuplicatedAsync<T>(T entity, string columnName, string conditional = "");
    /// <summary>
    /// 動態取得 SQL Select 語法 (不含 Join 的簡單語法)
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    string GetSQLSelectCommand<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Select 語法 (不含 Join 的簡單語法)
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <param name="rowLength">每列長度</param>
    /// <returns></returns>
    List<string> GetSQLSelectCommandList(string entityName, int rowLength = 100);
    /// <summary>
    /// 以表格主鍵的值取得 SQL Select WHERE 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="sqlWhere">目前的 Where 條件式</param>
    /// <returns></returns>
    string GetSQLSelectWhereById<T>(T entity, string sqlWhere);
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    List<string> GetStringColumnList<T>(T entity);
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <returns></returns>
    List<string> GetStringColumnList(string entityName);
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <param name="rowLength">每列長度</param>
    /// <returns></returns>
    List<string> GetStringColumnList(string entityName, int rowLength = 100);
    /// <summary>
    /// 取得查詢模糊搜尋條件參數
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="columns">欄位</param>
    /// <param name="sqlWhere">目前的 Where 條件</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    string GetSQLWhereBySearchColumn<T>(T entity, List<string> columns, string sqlWhere, string searchText);
    /// <summary>
    /// 動態取得 SQL Insert 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    string GetSQLInsertCommand<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Insert 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    DynamicParameters GetSQLInsertParameters<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Delete 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    string GetSQLDeleteCommand<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Delete 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    DynamicParameters GetSQLDeleteParameters<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Update 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    string GetSQLUpdateCommand<T>(T entity);
    /// <summary>
    /// 動態取得 SQL Update 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    DynamicParameters GetSQLUpdateParameters<T>(T entity);
    /// <summary>
    /// 返回新增或修改參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="value">物件變數</param>
    /// <returns></returns>
    DynamicParameters GetSQLSelectKeyParm<T>(T entity, object value);
    /// <summary>
    /// 返回新增或修改參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    DynamicParameters GetCreateEditParm<T>(T entity);
    /// <summary>
    /// 返回刪除參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="id">Key 值</param>
    /// <returns></returns>
    DynamicParameters GetDeleteParm<T>(T entity, int id = 0);
    /// <summary>
    /// 使用 Key 值取得指定欄位值
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="keyValue">Key 值</param>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    string GetValueByKey<T>(T entity, object keyValue, string columnName);
    /// <summary>
    /// 使用指定搜尋欄位值取得另一指定欄位值
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="searchName">搜尋欄位</param>
    /// <param name="searchValue">搜尋欄位值</param>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    string GetValueByColumn<T>(T entity, string searchName, object searchValue, string columnName);
    /// <summary>
    /// 取得 Key 欄位名稱
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    string GetKeyColumnName<T>(T entity);
    /// <summary>
    /// 取得屬性型態
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    string GetPropertyType(string entityName, string propName);
    /// <summary>
    /// 檢查是否為 [Key]
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    bool IsKeyAttribute(string entityName, string propName);
    /// <summary>
    /// 檢查是否為 [NotMapped]
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    bool IsNotMappedAttribute(string entityName, string propName);
}