public interface IAdoNetSqlClient
{
    /// <summary>
    /// 連線物件
    /// </summary>
    SqlConnection conn { get; set; }
    /// <summary>
    /// 命令物件
    /// </summary>
    SqlCommand cmd { get; set; }
    /// <summary>
    /// 連線設定檔名稱
    /// </summary>
    string ConnName { get; set; }
    /// <summary>
    /// SQL 命令
    /// </summary>
    string CommandText { get; set; }
    /// <summary>
    /// SQL 命令類型
    /// </summary>
    CommandType CommandType { get; set; }
    /// <summary>
    /// StoreProcedure 名稱
    /// </summary>
    string ProcedureName { get; set; }
    /// <summary>
    /// 執行指令後影響的筆數
    /// </summary>
    int RowAffected { get; set; }
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    string ErrorMessage { get; set; }
    /// <summary>
    /// 回傳執行後是否有記錄
    /// </summary>
    bool HasRows { get; }
    /// <summary>
    /// 初始化物件
    /// </summary>
    void InitComponents();
    /// <summary>
    /// 資料庫連線
    /// </summary>
    void Open();
    /// <summary>
    /// 資料庫關閉連線
    /// </summary>
    void Close();
    /// <summary>
    /// 設定連線字串
    /// </summary>
    void SetConnectionString();
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    int Execute(bool closeDb);
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    int Execute(string commandText, bool closeDb);
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    int Execute(CommandType commandType, bool closeDb);
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    int Execute(string commandText, CommandType commandType, bool closeDb);
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    DataSet GetDataSet();
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    DataSet GetDataSet(bool closeDb);
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    DataTable GetDataTable();
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    DataTable GetDataTable(bool closeDb);
    /// <summary>
    /// 取得指定欄位的日期型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    DateTime GetValueDateTime(string columnName);
    /// <summary>
    /// 取得指定欄位的數字型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    decimal GetValueDecimal(string columnName);
    /// <summary>
    /// 取得指定欄位的整數型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    int GetValueInt(string columnName);
    /// <summary>
    /// 取得指定欄位的字串型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    string? GetValueString(string columnName);
    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="parameterName">參數名稱</param>
    /// <param name="value">參數值</param>
    /// <param name="clearFirst">是否先清除所有參數再加入</param>
    void ParametersAdd(string parameterName, object value, bool clearFirst);
}