public class AdoNetService : BaseClass, IAdoNetSqlClient
{
    /// <summary>
    /// 連線物件
    /// </summary>
    public SqlConnection conn { get; set; } = new SqlConnection();
    /// <summary>
    /// 命令物件
    /// </summary>
    public SqlCommand cmd { get; set; } = new SqlCommand();
    /// <summary>
    /// 連線設定檔名稱
    /// </summary>
    public string ConnName { get; set; } = "";
    /// <summary>
    /// SQL 指令
    /// </summary>
    public string CommandText
    {
        get { return cmd.CommandText; }
        set { cmd.CommandText = value; }
    }
    /// <summary>
    /// SQL 命令類型
    /// </summary>
    public CommandType CommandType
    {
        get { return cmd.CommandType; }
        set { cmd.CommandType = value; }
    }
    /// <summary>
    /// StoreProcedure 名稱
    /// </summary>
    public string ProcedureName { get; set; } = "";
    /// <summary>
    /// 執行指令後影響的筆數
    /// </summary>
    int RowAffected { get; set; } = 0;
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; } = "";
    /// <summary>
    /// 回傳執行後是否有記錄
    /// </summary>
    public bool HasRows
    {
        get
        {
            bool bln_hasrows = false;
            ErrorMessage = "";
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                bln_hasrows = dr.HasRows;
                dr.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return bln_hasrows;
        }
    }

    int IAdoNetSqlClient.RowAffected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// 建構子
    /// </summary>
    public AdoNetService()
    {
        InitComponents();
        CommandType = CommandType.Text;
        ConnName = "dbconn";
        Open();
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="commName">ConnectionString 名稱</param>
    /// <param name="commType">命令類別</param>
    public AdoNetService(string commName, CommandType commType)
    {
        InitComponents();
        CommandType = commType;
        ConnName = commName;
        Open();
    }
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="commType">命令類別</param>
    public AdoNetService(CommandType commType)
    {
        InitComponents();
        CommandType = commType;
        ConnName = "dbconn";
        Open();
    }
    /// <summary>
    /// 初始化物件
    /// </summary>
    public void InitComponents()
    {
        ErrorMessage = "";
        cmd.Connection = conn;
    }
    /// <summary>
    /// 資料庫連線
    /// </summary>
    public void Open()
    {
        if (conn.State == ConnectionState.Open) Close();
        SetConnectionString();
        conn.Open();
    }
    /// <summary>
    /// 資料庫關閉連線
    /// </summary>
    public void Close()
    {
        conn.Close();
    }
    /// <summary>
    /// 設定連線字串
    /// </summary>
    public void SetConnectionString()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        var config = builder.Build();
        conn.ConnectionString = config.GetConnectionString(ConnName) ?? "";
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(bool closeDb)
    {
        RowAffected = 0;
        ErrorMessage = "";
        try
        {
            RowAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        finally
        {
            if (closeDb) Close();
        }
        return RowAffected;
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(string commandText, bool closeDb)
    {
        RowAffected = 0;
        ErrorMessage = "";
        try
        {
            cmd.CommandText = commandText;
            RowAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        finally
        {
            if (closeDb) Close();
        }
        return RowAffected;
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(CommandType commandType, bool closeDb)
    {
        RowAffected = 0;
        ErrorMessage = "";
        try
        {
            cmd.CommandType = commandType;
            RowAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        finally
        {
            if (closeDb) Close();
        }
        cmd.CommandType = CommandType.Text;
        return RowAffected;
    }
    /// <summary>
    /// 執行 SQL 命令
    /// </summary>
    /// <param name="commandText">SQL 命令</param>
    /// <param name="commandType">命令類型</param>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public int Execute(string commandText, CommandType commandType, bool closeDb)
    {
        RowAffected = 0;
        ErrorMessage = "";
        try
        {
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            RowAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        finally
        {
            if (closeDb) Close();
        }
        cmd.CommandType = CommandType.Text;
        return RowAffected;
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    public DataSet GetDataSet()
    {
        return GetDataSet(true);
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataSet
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataSet GetDataSet(bool closeDb)
    {
        ErrorMessage = "";
        DataSet dsReturn = new DataSet();
        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dsReturn);
            adapter.Dispose();
        }
        catch (SqlException ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        if (closeDb) Close();
        return dsReturn;
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable,並自動關閉資料庫連線
    /// </summary>
    /// <returns></returns>
    public DataTable GetDataTable()
    {
        return GetDataTable(true);
    }
    /// <summary>
    /// 執行 SQL 指令並取回 DataTable
    /// </summary>
    /// <param name="closeDb">執行後關閉資料庫</param>
    /// <returns></returns>
    public DataTable GetDataTable(bool closeDb)
    {
        ErrorMessage = "";
        DataTable dtReturn = new DataTable();
        try
        {
            DataSet dsReturn = GetDataSet(closeDb);
            dtReturn = dsReturn.Tables[0];
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dtReturn;
    }
    /// <summary>
    /// 取得指定欄位的字串型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public string GetValueString(string columnName)
    {
        ErrorMessage = "";
        string str_value = "";
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                str_value = (dr[columnName] == null) ? "" : dr[columnName].ToString();
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return str_value;
    }
    /// <summary>
    /// 取得指定欄位的日期型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public DateTime GetValueDateTime(string columnName)
    {
        ErrorMessage = "";
        DateTime dtm_value = DateTime.MinValue;
        try
        {
            string str_value = GetValueString(columnName);
            if (!DateTime.TryParse(str_value, out dtm_value)) dtm_value = DateTime.MinValue;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dtm_value;
    }
    /// <summary>
    /// 取得指定欄位的數字型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public decimal GetValueDecimal(string columnName)
    {
        ErrorMessage = "";
        decimal dec_value = 0;
        try
        {
            string str_value = GetValueString(columnName);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            if (!decimal.TryParse(str_value, out dec_value)) dec_value = 0;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return dec_value;
    }
    /// <summary>
    /// 取得指定欄位的整數型態值
    /// </summary>
    /// <param name="columnName">指定欄位</param>
    /// <returns></returns>
    public int GetValueInt(string columnName)
    {
        ErrorMessage = "";
        int int_value = 0;
        try
        {
            string str_value = GetValueString(columnName);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            if (!int.TryParse(str_value, out int_value)) int_value = 0;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message.ToString();
        }
        return int_value;
    }
    /// <summary>
    /// 加入參數
    /// </summary>
    /// <param name="parameterName">參數名稱</param>
    /// <param name="value">參數值</param>
    /// <param name="clearFirst">是否先清除所有參數再加入</param>
    public void ParametersAdd(string parameterName, object value, bool clearFirst)
    {
        if (clearFirst) cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue(parameterName, value);
    }
}