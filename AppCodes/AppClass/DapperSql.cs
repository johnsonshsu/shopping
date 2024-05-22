using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

public class DapperSql<TEntity> : BaseClass, IDapperSql<TEntity> where TEntity : class
{
    /// <summary>
    /// Dapper 物件
    /// </summary>
    /// <returns></returns>
    public DapperRepository dpr { get; set; } = new DapperRepository();
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
        string str_query = " ORDER BY ";
        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
        if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        if (OrderByDirection.IndexOf(',') == 0)
        {
            str_query += OrderByColumn;
            if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
        }
        else
        {
            List<string> lst_column = OrderByColumn.Split(',').ToList();
            List<string> lst_order = OrderByDirection.Split(',').ToList();
            if (lst_column.Count == lst_order.Count)
            {
                for (int i = 0; i < lst_column.Count; i++)
                {
                    str_query += lst_column[i];
                    str_query += " ";
                    str_query += lst_order[i];
                    if (i < lst_column.Count - 1) str_query += ", ";
                }
            }
            else
            {
                str_query += OrderByColumn;
                if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
            }
        }
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
    /// <returns></returns>
    public virtual List<TEntity> GetDataList()
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, "", 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm)
    {
        var model = GetDataList(parm, "", 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(int page, int pageSize)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, "", page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm, int page, int pageSize)
    {
        var model = GetDataList(parm, "", page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(string searchString)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, searchString, 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm, string searchString)
    {
        var model = GetDataList(parm, searchString, 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(string searchString, int page, int pageSize)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, searchString, page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm, string searchString, int page, int pageSize)
    {
        List<string> searchColumns = GetSearchColumns();
        var model = new List<TEntity>();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString) && searchColumns.Count() > 0)
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        sql_query += GetSQLOrderBy();
        if (parm.ParameterNames.Count() > 0)
            model = dpr.ReadAll<TEntity>(sql_query, parm);
        else
            model = dpr.ReadAll<TEntity>(sql_query);
        if (page > 0 && pageSize > 0) model = (List<TEntity>)model.ToPagedList(page, pageSize);
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
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync()
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, "", 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm)
    {
        var model = await GetDataListAsync(parm, "", 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(int page, int pageSize)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, "", page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, int page, int pageSize)
    {
        var model = await GetDataListAsync(parm, "", page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(string searchString)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, searchString, 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, string searchString)
    {
        var model = await GetDataListAsync(parm, searchString, 0, 0);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(string searchString, int page, int pageSize)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, searchString, page, pageSize);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <param name="page">當前頁數</param>
    /// <param name="pageSize">每頁筆數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, string searchString, int page, int pageSize)
    {
        List<string> searchColumns = GetSearchColumns();
        var model = new List<TEntity>();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        sql_query += GetSQLOrderBy();
        if (parm.ParameterNames.Count() > 0)
            model = await dpr.ReadAllAsync<TEntity>(sql_query, parm);
        else
            model = await dpr.ReadAllAsync<TEntity>(sql_query, parm);
        if (page > 0 && pageSize > 0) model = (List<TEntity>)model.ToPagedList(page, pageSize);
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