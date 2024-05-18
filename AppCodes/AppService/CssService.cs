/// <summary>
/// CSS 統一標準樣式設定
/// </summary>
public class CssService
{
    /// <summary>
    /// Bootstrap Button primary
    /// </summary>
    public string ButtonPrimary { get { return "btn btn-primary"; } }
    /// <summary>
    /// Bootstrap Button success
    /// </summary>
    public string ButtonSuccess { get { return "btn btn-success"; } }
    /// <summary>
    /// Bootstrap Button danger
    /// </summary>
    public string ButtonDanger { get { return "btn btn-danger"; } }
    /// <summary>
    /// Bootstrap Button warning
    /// </summary>
    public string ButtonWarning { get { return "btn btn-warning"; } }
    /// <summary>
    /// Bootstrap Button info
    /// </summary>
    public string ButtonInfo { get { return "btn btn-info"; } }
    /// <summary>
    /// Bootstrap Button secondary
    /// </summary>
    public string ButtonSecondary { get { return "btn btn-secondary"; } }
    /// <summary>
    /// 新增按鈕樣式
    /// </summary>
    public string CreateButton { get { return ButtonPrimary; } }
    /// <summary>
    /// 修改按鈕樣式
    /// </summary>
    public string EditButton { get { return ButtonSuccess; } }
    /// <summary>
    /// 刪除按鈕樣式
    /// </summary>
    public string DeleteButton { get { return ButtonDanger; } }
    /// <summary>
    /// 明細按鈕樣式
    /// </summary>
    public string DetailButton { get { return ButtonWarning; } }
    /// <summary>
    /// 列印按鈕樣式
    /// </summary>
    public string PrintButton { get { return ButtonInfo; } }
    /// <summary>
    /// 上傳按鈕樣式
    /// </summary>
    public string UploadButton { get { return ButtonSecondary; } }
    /// <summary>
    /// 送出按鈕樣式
    /// </summary>
    public string SubmitButton { get { return ButtonPrimary; } }
    /// <summary>
    /// 返回按鈕樣式
    /// </summary>
    public string ReturnButton { get { return ButtonSuccess; } }
}