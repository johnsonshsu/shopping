using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DapperSql<TEntity> : BaseClass, IDapperSql<TEntity> where TEntity : class
{
    /// <summary>
    /// Dapper 物件
    /// </summary>
    /// <returns></returns>
    public DapperRepository dpr = new DapperRepository();
    /// <summary>
    /// Entity Object
    /// </summary>
    /// <returns></returns>
    public TEntity EntityObject { get { return (TEntity)Activator.CreateInstance(typeof(TEntity)); } }
    /// <summary>
    /// Entity Name
    /// </summary>
    /// <returns></returns>
    public string EntityName { get { return typeof(TEntity).Name; } }
    /// <summary>
    /// 連線字串名稱
    /// </summary>
    public string ConnName { get; set; } = "dbconn";
    /// <summary>
    /// 預設 SQL 排序指令
    /// </summary>
    public string DefaultOrderByColumn { get; set; } = "";
    /// <summary>
    /// 預設 SQL 排序方式指令
    /// </summary>
    public string DefaultOrderByDirection { get; set; } = "";
    /// <summary>
    /// OrderBy 排序指令
    /// </summary>
    /// <value></value>
    public string OrderByColumn { get; set; } = "";
    /// <summary>
    /// OrderBy 排序方式
    /// </summary>
    public string OrderByDirection { get; set; } = "";
    /// <summary>
    /// SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLSelect()
    {
        //自動由表格 Class 產生 SQL 查詢指令
        string str_query = "";
        str_query = dpr.GetSQLSelectCommand(EntityObject);
        return str_query;
    }
    /// <summary>
    /// 取得模擬搜尋的欄位集合
    /// </summary>
    /// <returns></returns>
    public virtual List<string> GetSearchColumns()
    {
        //由系統自動取得文字欄位的集合
        List<string> searchColumn;
        searchColumn = dpr.GetStringColumnList(EntityObject);
        return searchColumn;
    }
    /// <summary>
    /// SQL 查詢條件式指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLWhere()
    {
        string str_query = "";
        return str_query;
    }
    /// <summary>
    /// SQL 查詢排序指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLOrderBy()
    {
        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
        if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        string str_query = $" ORDER BY {OrderByColumn}";
        if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
        return str_query;
    }
    /// <summary>
    /// 取得下拉式選單資料集
    /// </summary>
    /// <param name="valueColumn">資料欄位名稱</param>
    /// <param name="textColumn">顯示欄位名稱</param>
    /// <param name="orderColumn">排序欄位名稱</param>/// 
    /// <param name="textIncludeValue">顯示欄位名稱是否顯示資料欄位</param>
    /// <returns></returns>
    public virtual List<SelectListItem> GetDropDownList(string valueColumn, string textColumn, string orderColumn, bool textIncludeValue = false)
    {
        string str_query = "SELECT ";
        if (textIncludeValue) str_query += $"{valueColumn} + ' ' + ";
        str_query += $"{textColumn} AS Text , {valueColumn} AS Value FROM {EntityName} ORDER BY {orderColumn}";
        var model = dpr.ReadAll<SelectListItem>(str_query);
        return model;
    }
    /// <summary>
    /// SQL 新增指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLInsert()
    {
        //自動由表格 Class 產生 Insert 查詢指令
        return dpr.GetSQLInsertCommand(EntityObject);
    }
    /// <summary>
    /// SQL 刪除指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLDelete()
    {
        //自動由表格 Class 產生 Delete 查詢指令
        return dpr.GetSQLDeleteCommand(EntityObject);
    }
    /// <summary>
    /// SQL 修改指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLUpdate()
    {
        //自動由表格 Class 產生 Update 查詢指令
        return dpr.GetSQLUpdateCommand(EntityObject);
    }
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual TEntity GetData(int id)
    {
        var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
        if (id == 0)
        {
            //新增預設值
        }
        else
        {
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = dpr.ReadSingle<TEntity>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<TEntity>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = dpr.ReadAll<TEntity>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    public virtual void CreateEdit(TEntity model, int id = 0)
    {
        if (id == 0)
            Create(model);
        else
            Edit(model);
    }
    /// <summary>
    /// 新增資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public virtual void Create(TEntity model)
    {
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 更新資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public virtual void Edit(TEntity model)
    {
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 刪除資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    public virtual void Delete(int id = 0)
    {
        string str_query = dpr.GetSQLDeleteCommand(EntityObject);
        DynamicParameters parm = dpr.GetSQLDeleteParameters(EntityObject, id);
        dpr.Execute(str_query, parm);
    }
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetDataAsync(int id)
    {
        var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
        if (id == 0)
        {
            //新增預設值
        }
        else
        {
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            model = await dpr.ReadSingleAsync<TEntity>(sql_query, parm);
        }
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(string searchString = "")
    {
        List<string> searchColumns = GetSearchColumns();
        DynamicParameters parm = new DynamicParameters();
        var model = new List<TEntity>();
        using var dpr = new DapperRepository();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        if (!string.IsNullOrEmpty(sql_where))
        {
            //自定義的 Weher Parm 參數
            //parm.Add("參數名稱", "參數值");
        }
        sql_query += GetSQLOrderBy();
        model = await dpr.ReadAllAsync<TEntity>(sql_query, parm);
        return model;
    }
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task CreateEditAsync(TEntity model, int id = 0)
    {
        if (id == 0)
            await CreateAsync(model);
        else
            await EditAsync(model);
    }
    /// <summary>
    /// 新增資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public virtual async Task CreateAsync(TEntity model)
    {
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        await dpr.ExecuteAsync(str_query, parm);
    }
    /// <summary>
    /// 更新資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public virtual async Task EditAsync(TEntity model)
    {
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        await dpr.ExecuteAsync(str_query, parm);
    }
    /// <summary>
    /// 刪除資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(int id = 0)
    {
        string str_query = dpr.GetSQLDeleteCommand(EntityObject);
        DynamicParameters parm = dpr.GetSQLDeleteParameters(EntityObject, id);
        await dpr.ExecuteAsync(str_query, parm);
    }
}