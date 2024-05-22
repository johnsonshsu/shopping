using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Dapper;
using X.PagedList;
using X.PagedList.Mvc;

/// <summary>
/// Dapper Repository 類別
/// </summary>
public class DapperRepository : BaseClass, IDapperRepository
{
    /// <summary>
    /// 預設連線字串名稱
    /// </summary>
    private readonly string _defuaultConnectionName = "dbconn";
    /// <summary>
    /// Dapper Repository 建構子
    /// </summary>
    public DapperRepository()
    {
        ConnectionName = _defuaultConnectionName;
        ConnectionString = GetConnectionString(ConnectionName);
        CommandType = CommandType.Text;
        ErrorMessage = "";
    }
    /// <summary>
    /// Dapper Repository 建構子
    /// </summary>
    /// <param name="connectionName">連線字串名稱</param>
    public DapperRepository(string connectionName)
    {
        ConnectionName = connectionName;
        ConnectionString = GetConnectionString(ConnectionName);
        CommandType = CommandType.Text;
        ErrorMessage = "";
    }
    /// <summary>
    /// Dapper Repository 建構子
    /// </summary>
    /// <param name="commandType">命令類型 (SQL 語法/預儲程序)</param>
    public DapperRepository(CommandType commandType)
    {
        ConnectionName = _defuaultConnectionName;
        ConnectionString = GetConnectionString(ConnectionName);
        CommandType = commandType;
        ErrorMessage = "";
    }
    /// <summary>
    /// Dapper Repository 建構子
    /// </summary>
    /// <param name="connectionName">連線字串名稱</param>
    /// <param name="commandType">命令類型 (SQL 語法/預儲程序)</param>
    public DapperRepository(string connectionName, CommandType commandType)
    {
        ConnectionName = connectionName;
        ConnectionString = GetConnectionString(ConnectionName);
        CommandType = commandType;
        ErrorMessage = "";
    }

    /// <summary>
    /// 連線字串名稱
    /// </summary>
    public string ConnectionName { get; set; }
    /// <summary>
    /// 連線字串
    /// </summary>
    public string ConnectionString { get; set; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; }
    /// <summary>
    /// 命令類型 (SQL 語法/預儲程序)
    /// </summary>
    public CommandType CommandType { get; set; }
    /// <summary>
    /// 取得連線字串
    /// </summary>
    /// <param name="connectionName">連線字串名稱</param>
    /// <returns></returns>
    public string GetConnectionString(string connectionName)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString(connectionName) ?? "";
        return connectionString;
    }
    /// <summary>
    /// 檢查 connectionName 中的 connectionString 是否連線正常
    /// </summary>
    /// <param name="connectionName"></param>
    /// <returns></returns>
    public string CheckConnectionName(string connectionName)
    {
        string connString = GetConnectionString(connectionName);
        return CheckConnectionString(connString);
    }
    /// <summary>
    /// 檢查 connectionString 是否連線正常
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public string CheckConnectionString(string connectionString)
    {
        ErrorMessage = "";
        using var conn = new System.Data.SqlClient.SqlConnection(ConnectionString);
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ErrorMessage;
    }
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public int Execute(string query)
    {
        int ReturnValue = 0;
        string ErrorMessage = "";
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            ReturnValue = conn.Execute(query);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public int Execute(string query, DynamicParameters parameters)
    {
        int ReturnValue = 0;
        string ErrorMessage = "";
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
                ReturnValue = conn.Execute(query);
            else
                ReturnValue = conn.Execute(query, parameters);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)(非同步)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string query)
    {
        string ErrorMessage = "";
        int ReturnValue = 0;
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            ReturnValue = await conn.ExecuteAsync(query);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 執行命令 (SQL 語法/預儲程序)(非同步)
    /// </summary>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string query, DynamicParameters parameters)
    {
        string ErrorMessage = "";
        int ReturnValue = 0;
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
                ReturnValue = await conn.ExecuteAsync(query);
            else
                ReturnValue = await conn.ExecuteAsync(query, parameters);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取單筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public T ReadSingle<T>(string query)
    {
        string ErrorMessage = "";
        T ReturnValue = (T)Activator.CreateInstance(typeof(T));
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            ReturnValue = conn.QueryFirstOrDefault<T>(query);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取單筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public T ReadSingle<T>(string query, DynamicParameters parameters)
    {
        string ErrorMessage = "";
        T ReturnValue = (T)Activator.CreateInstance(typeof(T));
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
                ReturnValue = conn.QueryFirstOrDefault<T>(query);
            else
                ReturnValue = conn.QueryFirstOrDefault<T>(query, parameters);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取單筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public async Task<T> ReadSingleAsync<T>(string query)
    {
        string ErrorMessage = "";
        T ReturnValue = (T)Activator.CreateInstance(typeof(T));
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            ReturnValue = await conn.QueryFirstOrDefaultAsync<T>(query);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取單筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public async Task<T> ReadSingleAsync<T>(string query, DynamicParameters parameters)
    {
        string ErrorMessage = "";
        T ReturnValue = (T)Activator.CreateInstance(typeof(T));
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
                ReturnValue = await conn.QueryFirstOrDefaultAsync<T>(query);
            else
                ReturnValue = await conn.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;

    }
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string query)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            ReturnValue = conn.Query<T>(query).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string query, int page, int pageSize)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            var values = conn.Query<T>(query).ToList();
            ReturnValue = (List<T>)values.ToPagedList(page, pageSize);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string query, DynamicParameters parameters)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
                ReturnValue = conn.Query<T>(query).ToList();
            else
                ReturnValue = conn.Query<T>(query, parameters).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    public List<T> ReadAll<T>(string query, DynamicParameters parameters, int page, int pageSize)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
            {
                var values = conn.Query<T>(query, parameters).ToList();
                ReturnValue = (List<T>)values.ToPagedList(page, pageSize);
            }

            else
            {
                var values = conn.Query<T>(query, parameters).ToList();
                ReturnValue = (List<T>)values.ToPagedList(page, pageSize);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <returns></returns>
    public async Task<List<T>> ReadAllAsync<T>(string query)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            var data = await conn.QueryAsync<T>(query);
            ReturnValue = data.ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    public async Task<List<T>> ReadAllAsync<T>(string query, int page, int pageSize)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            var data = await conn.QueryAsync<T>(query);
            ReturnValue = (List<T>)data.ToList().ToPagedList(page, pageSize);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;
    }
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <returns></returns>
    public async Task<List<T>> ReadAllAsync<T>(string query, DynamicParameters parameters)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
            {
                var data = await conn.QueryAsync<T>(query);
                ReturnValue = data.ToList();
            }
            else
            {
                var data = await conn.QueryAsync<T>(query, parameters);
                ReturnValue = data.ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;

    }
    /// <summary>
    /// 讀取多筆記錄(非同步)
    /// </summary>
    /// <typeparam name="T">回傳泛型類型</typeparam>
    /// <param name="query">命令字串</param>
    /// <param name="parameters">參數變數物件</param>
    /// <param name="page">目前頁數</param>
    /// <param name="pageSize">筆頁筆數</param>
    /// <returns></returns>
    public async Task<List<T>> ReadAllAsync<T>(string query, DynamicParameters parameters, int page, int pageSize)
    {
        string ErrorMessage = "";
        List<T> ReturnValue = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            int int_count = (parameters == null) ? 0 : parameters.ParameterNames.Count();
            if (int_count == 0)
            {
                var data = await conn.QueryAsync<T>(query);
                ReturnValue = (List<T>)data.ToList().ToPagedList(page, pageSize);
            }
            else
            {
                var data = await conn.QueryAsync<T>(query, parameters);
                ReturnValue = (List<T>)data.ToList().ToPagedList(page, pageSize);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return ReturnValue;

    }
    /// <summary>
    /// 檢查目前 Entity 內指定欄位值是否重覆(同步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columnName"></param>
    /// <param name="conditional"></param>
    /// <returns></returns>
    public bool IsDuplicated<T>(T entity, string columnName, string conditional = "")
    {
        bool bln_value = false;
        string str_entity_name = entity.GetType().Name;
        string str_key_name = GetKeyColumnName(entity);
        var dataProp = entity.GetType().GetProperties().Where(m => m.Name == columnName).FirstOrDefault();
        var keyProp = entity.GetType().GetProperties().Where(m => m.Name == str_key_name).FirstOrDefault();

        if (dataProp != null && keyProp != null)
        {
            var dataValue = dataProp.GetValue(entity, null);
            var keyValue = keyProp.GetValue(entity, null);
            if (dataValue != null && keyValue != null)
            {
                string str_query = $"SELECT {columnName} FROM {str_entity_name} WHERE ";
                str_query += $"{str_key_name} <> @{str_key_name} AND ";
                str_query += $"{columnName} = @{columnName} ";
                if (!string.IsNullOrEmpty(conditional)) str_query += $"AND ({conditional})";
                DynamicParameters parm = new DynamicParameters();
                parm.Add(str_key_name, keyValue);
                parm.Add(columnName, dataValue);
                var model = ReadSingle<T>(str_query, parm);
                if (model != null) bln_value = true;
            }
        }
        return bln_value;
    }
    /// <summary>
    /// 檢查目前 Entity 內指定欄位值是否重覆(非同步)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="columnName"></param>
    /// <param name="conditional"></param>
    /// <returns></returns>
    public async Task<bool> IsDuplicatedAsync<T>(T entity, string columnName, string conditional = "")
    {
        bool bln_value = false;
        string str_entity_name = entity.GetType().Name;
        string str_key_name = GetKeyColumnName(entity);
        var dataProp = entity.GetType().GetProperties().Where(m => m.Name == columnName).FirstOrDefault();
        var keyProp = entity.GetType().GetProperties().Where(m => m.Name == str_key_name).FirstOrDefault();

        if (dataProp != null && keyProp != null)
        {
            var dataValue = dataProp.GetValue(entity, null);
            var keyValue = keyProp.GetValue(entity, null);
            if (dataValue != null && keyValue != null)
            {
                string str_query = $"SELECT {columnName} FROM {str_entity_name} WHERE ";
                str_query += $"{str_key_name} <> @{str_key_name} AND ";
                str_query += $"{columnName} = @{columnName} ";
                if (!string.IsNullOrEmpty(conditional)) str_query += $"AND ({conditional})";
                DynamicParameters parm = new DynamicParameters();
                parm.Add(str_key_name, keyValue);
                parm.Add(columnName, dataValue);
                var model = await ReadSingleAsync<T>(str_query, parm);
                if (model != null) bln_value = true;
            }
        }
        return bln_value;
    }
    /// <summary>
    /// 動態取得 SQL Select 語法 (不含 Join 的簡單語法)
    /// </summary>
    /// <typeparam name="TEntity">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public string GetSQLSelectCommand<TEntity>(TEntity entity)
    {
        int int_count = 0;
        bool bln_notmapped = false;
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        string str_command = "SELECT ";
        var props = entity.GetType().GetProperties();
        foreach (var prop in props)
        {
            bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
            if (!bln_notmapped)
            {
                int_count++;
                if (int_count > 1) str_command += ", ";
                str_command += $"{str_entity_name}.{prop.Name} ";
            }
        }
        str_command += $" FROM {str_entity_name} ";
        return str_command;
    }
    /// <summary>
    /// 動態取得 SQL Select 語法 (不含 Join 的簡單語法)
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <param name="rowLength">每列長度</param>
    /// <returns></returns>
    public List<string> GetSQLSelectCommandList(string entityName, int rowLength = 100)
    {
        List<string> values = new List<string>();
        var obj = Type.GetType(entityName);
        object entity = Activator.CreateInstance(obj);

        int int_count = 0;
        bool bln_notmapped = false;
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        string str_command = "SELECT ";
        var props = entity.GetType().GetProperties();
        foreach (var prop in props)
        {
            bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
            if (!bln_notmapped)
            {
                if (rowLength == 0)
                {
                    str_command = $"{str_entity_name}.{prop.Name} ";
                    values.Add(str_command);
                }
                else
                {
                    int_count++;
                    if (int_count > 1) str_command += ", ";
                    str_command += $"{str_entity_name}.{prop.Name} ";
                    if (str_command.Length >= rowLength)
                    {
                        values.Add(str_command);
                        str_command = "";
                    }
                }
            }
        }
        if (rowLength > 0)
        {
            if (!string.IsNullOrEmpty(str_command)) values.Add(str_command);
            str_command = $" FROM {str_entity_name} ";
            values.Add(str_command);
        }
        return values;
    }
    /// <summary>
    /// 以表格主鍵的值取得 SQL Select WHERE 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="sqlWhere">目前的 Where 條件式</param>
    /// <returns></returns>
    public string GetSQLSelectWhereById<T>(T entity, string sqlWhere = "")
    {
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        string str_key_name = GetKeyColumnName(entity);
        string str_command = " ";
        if (!string.IsNullOrEmpty(sqlWhere))
            str_command += $" {sqlWhere} AND (";
        else
            str_command += "WHERE (";
        str_command += $"{str_entity_name}.{str_key_name} = ";
        str_command += $"@{str_key_name}) ";
        return str_command;
    }
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public List<string> GetStringColumnList<T>(T entity)
    {
        List<string> columns = new List<string>();
        string str_prop_name = "";
        string str_entity_name = entity.GetType().Name;
        var props = entity.GetType().GetProperties();
        foreach (var prop in props)
        {
            string type_name = GetPropertyType(str_entity_name, prop.Name);
            bool isNotMapped = IsNotMappedAttribute(str_entity_name, prop.Name);
            if (type_name == "string" && !isNotMapped)
            {
                str_prop_name = $"{str_entity_name}.{prop.Name}";
                columns.Add(str_prop_name);
            }
        }
        return columns;
    }
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <returns></returns>
    public List<string> GetStringColumnList(string entityName)
    {
        List<string> columns = new List<string>();
        var obj = Type.GetType(entityName);
        object entity = Activator.CreateInstance(obj);
        string str_prop_name = "";
        string str_entity_name = entity.GetType().Name;
        var props = entity.GetType().GetProperties();
        foreach (var prop in props)
        {
            string type_name = GetPropertyType(str_entity_name, prop.Name);
            bool isNotMapped = IsNotMappedAttribute(str_entity_name, prop.Name);
            if (type_name == "string" && !isNotMapped)
            {
                str_prop_name = $"{str_entity_name}.{prop.Name}";
                columns.Add(str_prop_name);
            }
        }
        return columns;
    }
    /// <summary>
    /// 取得表格內所有是文字型態的欄位集合
    /// </summary>
    /// <param name="entityName">物件變數</param>
    /// <param name="rowLength">每列長度</param>
    /// <returns></returns>
    public List<string> GetStringColumnList(string entityName, int rowLength = 100)
    {
        List<string> values = new List<string>();
        List<string> columns = columns = GetStringColumnList(entityName);
        if (columns.Count > 0)
        {
            string str_datas = "";
            string str_data = "";
            foreach (var col in columns)
            {
                if (!string.IsNullOrEmpty(str_datas))
                {
                    str_datas += ",";
                    str_data += ",";
                }
                str_datas += col;
                str_data += col;
                if (str_data.Length > rowLength)
                {
                    values.Add(str_data);
                    str_data = "";
                }
            }
            if (!string.IsNullOrEmpty(str_data)) values.Add(str_data);
        }
        return values;
    }
    /// <summary>
    /// 取得查詢模糊搜尋條件參數
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="columns">欄位</param>
    /// <param name="sqlWhere">目前的 Where 條件</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public string GetSQLWhereBySearchColumn<T>(T entity, List<string> columns, string sqlWhere, string searchText)
    {
        string str_query = "";
        string str_where = "";
        if (columns.Count > 0 && !string.IsNullOrEmpty(searchText))
        {
            string str_entity_name = entity.GetType().Name;
            if (string.IsNullOrEmpty(sqlWhere))
                str_query += " WHERE ";
            else
                str_query += " AND ";
            str_query += " (";
            foreach (var col in columns)
            {
                if (!string.IsNullOrEmpty(str_where)) str_where += " OR ";
                str_where += $"{col} LIKE '%{searchText}%'";
            }
            str_query += str_where;
            str_query += ") ";
        }
        return str_query;
    }
    /// <summary>
    /// 動態取得 SQL Insert 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public string GetSQLInsertCommand<T>(T entity)
    {
        int int_count = 0;
        bool bln_iskey = false;
        bool bln_notmapped = false;
        string str_columns = "";
        string str_parameters = "";
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        var props = entity.GetType().GetProperties();
        foreach (var prop in props)
        {
            bln_iskey = IsKeyAttribute(str_entity_name, prop.Name);
            bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
            if (!bln_iskey && !bln_notmapped)
            {
                int_count++;
                if (int_count > 1)
                {
                    str_columns += ", ";
                    str_parameters += ", ";
                }
                str_columns += prop.Name;
                str_parameters += $"@{prop.Name}";
            }
        }
        string str_command = $"INSERT INTO {str_entity_name} ({str_columns}) VALUES ({str_parameters})";
        return str_command;
    }
    /// <summary>
    /// 動態取得 SQL Insert 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    public DynamicParameters GetSQLInsertParameters<T>(T entity)
    {
        DynamicParameters parm = new DynamicParameters();
        string str_key_name = GetKeyColumnName(entity);
        if (!string.IsNullOrEmpty(str_key_name))
        {
            bool bln_iskey = false;
            bool bln_notmapped = false;
            string str_entity_name = entity.GetType().Name;
            string str_entity_full_name = entity.GetType().FullName;
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                bln_iskey = IsKeyAttribute(str_entity_name, prop.Name);
                bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
                if (!bln_iskey && !bln_notmapped)
                {
                    parm.Add(prop.Name, prop.GetValue(entity, null));
                }
            }
        }
        return parm;
    }
    /// <summary>
    /// 動態取得 SQL Delete 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public string GetSQLDeleteCommand<T>(T entity)
    {
        string str_command = "";
        string str_key_name = GetKeyColumnName(entity);
        string str_entity_name = entity.GetType().Name;
        if (!string.IsNullOrEmpty(str_key_name))
        {
            str_command = $"DELETE FROM {str_entity_name} WHERE {str_key_name} = @{str_key_name}";
        }
        return str_command;
    }
    /// <summary>
    /// 動態取得 SQL Delete 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    public DynamicParameters GetSQLDeleteParameters<T>(T entity)
    {
        DynamicParameters parm = new DynamicParameters();
        string str_key_name = GetKeyColumnName(entity);
        if (!string.IsNullOrEmpty(str_key_name))
        {
            string str_entity_name = entity.GetType().Name;
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name == str_key_name)
                {
                    parm.Add(prop.Name, prop.GetValue(entity, null));
                    break;
                }
            }
        }
        return parm;
    }
    /// <summary>
    /// 動態取得 SQL Delete 參數
    /// </summary>
    /// <typeparam name="T">物件變數</typeparam>
    /// <param name="entity">型別</param>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public DynamicParameters GetSQLDeleteParameters<T>(T entity, int id)
    {
        string str_key_name = GetKeyColumnName(entity);
        DynamicParameters parm = new DynamicParameters();
        parm.Add(str_key_name, id);
        return parm;
    }
    /// <summary>
    /// 動態取得 SQL Update 語法
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public string GetSQLUpdateCommand<T>(T entity)
    {
        string str_command = "";
        string str_key_name = GetKeyColumnName(entity);
        if (!string.IsNullOrEmpty(str_key_name))
        {
            int int_count = 0;
            bool bln_iskey = false;
            bool bln_notmapped = false;
            string str_entity_name = entity.GetType().Name;
            string str_entity_full_name = entity.GetType().FullName;
            str_command = $"UPDATE {str_entity_name} SET ";
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                bln_iskey = IsKeyAttribute(str_entity_name, prop.Name);
                bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
                if (!bln_iskey && !bln_notmapped)
                {
                    int_count++;
                    if (int_count > 1) str_command += ", ";
                    str_command += $"{prop.Name} = @{prop.Name} ";
                }
            }
            str_command += $"WHERE {str_key_name} = @{str_key_name}";
        }
        return str_command;
    }
    /// <summary>
    /// 動態取得 SQL Update 參數
    /// </summary>
    /// <param name="entity">型別</param>
    /// <typeparam name="T">物件變數</typeparam>
    /// <returns></returns>
    public DynamicParameters GetSQLUpdateParameters<T>(T entity)
    {
        DynamicParameters parm = new DynamicParameters();
        string str_key_name = GetKeyColumnName(entity);
        if (!string.IsNullOrEmpty(str_key_name))
        {
            bool bln_notmapped = false;
            string str_entity_name = entity.GetType().Name;
            string str_entity_full_name = entity.GetType().FullName;
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
                if (!bln_notmapped)
                {
                    parm.Add(prop.Name, prop.GetValue(entity, null));
                }
            }
        }
        return parm;
    }
    /// <summary>
    /// 返回新增或修改參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="value">物件變數</param>
    /// <returns></returns>
    public DynamicParameters GetSQLSelectKeyParm<T>(T entity, object value)
    {
        string str_entity_name = entity.GetType().Name;
        DynamicParameters parm = new DynamicParameters();
        string str_key_name = GetKeyColumnName(entity);
        parm.Add(str_key_name, value);
        return parm;
    }
    /// <summary>
    /// 返回新增或修改參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public DynamicParameters GetCreateEditParm<T>(T entity)
    {
        bool bln_iskey = false;
        bool bln_notmapped = false;
        object obj_value = null;
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        DynamicParameters parm = new DynamicParameters();
        foreach (var prop in entity.GetType().GetProperties())
        {
            bln_iskey = IsKeyAttribute(str_entity_name, prop.Name);
            bln_notmapped = IsNotMappedAttribute(str_entity_full_name, prop.Name);
            if (bln_iskey)
            {
                int KeyId = 0;
                string KeyValue = (prop.GetValue(entity, null) == null) ? "0" : prop.GetValue(entity, null).ToString();
                if (!int.TryParse(KeyValue, out KeyId)) KeyId = 0;
                if (KeyId > 0) parm.Add(prop.Name, KeyId);
            }
            if (!bln_notmapped)
            {
                obj_value = prop.GetValue(entity, null);
                parm.Add(prop.Name, obj_value);
            }
        }
        return parm;
    }
    /// <summary>
    /// 返回刪除參數
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="id">Key 值</param>
    /// <returns></returns>
    public DynamicParameters GetDeleteParm<T>(T entity, int id = 0)
    {
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        string str_key_name = GetKeyColumnName(entity);
        DynamicParameters parm = new DynamicParameters();
        if (!string.IsNullOrEmpty(str_key_name) && id > 0)
        {
            parm.Add(str_key_name, id);
        }
        return parm;
    }
    /// <summary>
    /// 使用 Key 值取得指定欄位值
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="keyValue">Key 值</param>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public string GetValueByKey<T>(T entity, object keyValue, string columnName)
    {
        string str_value = "";
        string str_entity_name = entity.GetType().Name;
        string str_key_name = GetKeyColumnName(entity);
        var entityType = entity.GetType();
        ErrorMessage = "";
        if (!string.IsNullOrEmpty(str_key_name))
        {
            string str_query = $"SELECT {columnName} FROM {str_entity_name} WHERE {str_key_name} = {keyValue}";
            using var conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                var data = conn.QueryFirstOrDefault<string>(str_query);
                if (data != null) str_value = data.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        return str_value;
    }
    /// <summary>
    /// 使用指定搜尋欄位值取得另一指定欄位值
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="searchName">搜尋欄位</param>
    /// <param name="searchValue">搜尋欄位值</param>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public string GetValueByColumn<T>(T entity, string searchName, object searchValue, string columnName)
    {
        string str_value = "";
        string str_entity_name = entity.GetType().Name;
        var entityType = entity.GetType();
        ErrorMessage = "";

        string str_query = $"SELECT {columnName} FROM {str_entity_name} WHERE {searchName} = '{searchValue}'";
        using var conn = new SqlConnection(ConnectionString);
        try
        {
            conn.Open();
            var data = conn.QueryFirstOrDefault<string>(str_query);
            if (data != null) str_value = data.ToString();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        return str_value;
    }
    /// <summary>
    /// 取得 Key 欄位名稱
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <returns></returns>
    public string GetKeyColumnName<T>(T entity)
    {
        string str_value = "";
        string str_entity_name = entity.GetType().Name;
        foreach (var prop in entity.GetType().GetProperties())
        {
            if (IsKeyAttribute(str_entity_name, prop.Name))
            {
                str_value = prop.Name;
                break;
            }
        }
        return str_value;
    }
    /// <summary>
    /// Model 中的值填入參數 (參數名與 Model 的屬性值相同)
    /// </summary>
    /// <typeparam name="T">型別</typeparam>
    /// <param name="entity">物件變數</param>
    /// <param name="query">SQL Select 語法</param>
    /// <returns></returns>
    public DynamicParameters ModelToParm<T>(T entity, string query)
    {
        string str_entity_name = entity.GetType().Name;
        string str_entity_full_name = entity.GetType().FullName;
        string str_key_name = GetKeyColumnName(entity);
        string str_parm = "";
        string str_prop_name = "";
        DynamicParameters parm = new DynamicParameters();
        foreach (var prop in entity.GetType().GetProperties())
        {
            str_prop_name = prop.Name;
            str_parm = $"@{str_prop_name}";
            if (query.IndexOf(str_parm) != -1)
            {
                var obj_value = prop.GetValue(entity, null);
                parm.Add(str_prop_name, obj_value);
            }
        }
        return parm;
    }
    /// <summary>
    /// 取得屬性型態
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetPropertyType(string entityName, string propName)
    {
        string str_value = "string";
        string str_meta_name = $"z_meta{entityName}";
        var entity = Type.GetType($"{ProjectName}.Models.{entityName}");
        var metas = Type.GetType(str_meta_name);
        if (metas == null)
        {
            str_meta_name = $"meta{entityName}";
            metas = Type.GetType(str_meta_name);
        }
        if (metas != null)
        {
            str_value = "unknow";
            var prop = metas.GetProperty(propName);
            if (prop == null)
            {
                if (entity != null)
                {
                    prop = entity.GetProperty(propName);
                }
            }
            if (prop != null)
            {
                string str_full_name = prop.PropertyType.FullName;
                string str_type_name = prop.PropertyType.Name;
                if (str_type_name == "Nullable`1")
                {
                    int int_start = str_full_name.IndexOf("[[System.") + 9;
                    int int_end = str_full_name.IndexOf(",");
                    int int_leng = int_end - int_start;
                    str_value = str_full_name.Substring(int_start, int_leng);
                }
                else
                {
                    if (str_type_name == "Int32") str_value = "int";
                    if (str_type_name == "Boolean") str_value = "bool";
                    if (str_type_name == "DateTime") str_value = "DateTime";
                    if (str_type_name == "String") str_value = "string";
                    if (!string.IsNullOrEmpty(str_value)) str_value = str_type_name.ToLower();
                }

            }
        }
        return str_value;
    }
    /// <summary>
    /// 檢查是否為 [Key]
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool IsKeyAttribute(string entityName, string propName)
    {
        bool bln_value = false;
        string str_meta_name = $"z_meta{entityName}";
        var metas = Type.GetType(str_meta_name);
        if (metas == null)
        {
            str_meta_name = $"meta{entityName}";
            metas = Type.GetType(str_meta_name);
        }
        if (metas != null)
        {
            var prop = metas.GetProperty(propName);
            if (prop != null)
            {
                var attr = prop.GetCustomAttributes(typeof(KeyAttribute), false);
                bln_value = (attr.Length == 1);
            }
        }
        return bln_value;
    }
    /// <summary>
    /// 檢查是否為 [NotMapped]
    /// </summary>
    /// <param name="entityName">物件名稱</param>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool IsNotMappedAttribute(string entityName, string propName)
    {
        bool bln_value = false;
        string str_entity = entityName;
        if (str_entity.IndexOf(".") <= 0)
            str_entity = $"{ProjectName}.Models.{entityName}";
        var obj = Type.GetType(str_entity);
        var entity = Activator.CreateInstance(obj);
        string str_entity_name = entity.GetType().Name;
        string str_entity_fullname = entity.GetType().FullName;
        string str_meta_name = $"z_meta{str_entity_name}";

        var metas = Type.GetType(str_meta_name);
        if (metas == null)
        {
            str_meta_name = $"meta{str_entity_name}";
            metas = Type.GetType(str_meta_name);
        }
        if (metas != null)
        {
            var prop = metas.GetProperty(propName);
            if (prop == null)
            {
                if (entity != null)
                {
                    prop = entity.GetType().GetProperty(propName);
                }
            }
            if (prop != null)
            {
                var attr = prop.GetCustomAttributes(typeof(NotMappedAttribute), false);
                bln_value = (attr.Length == 1);
            }
        }
        return bln_value;
    }

    private string ProjectName
    {
        get
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}