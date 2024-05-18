/// <summary>
/// Int 擴充方法
/// </summary>
public static partial class Extension
{
    /// <summary>
    /// 取得平方值
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static int ezSquare(this int i)
    {
        return i * i;
    }

    /// <summary>
    /// 取得立方值
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static int ezCube(this int i)
    {
        return i * i * i;
    }

    /// <summary>
    /// 取得ASCII碼對應字元
    /// </summary>
    /// <param name="i">ASCII碼</param>
    /// <returns>對應字元</returns>
    public static string ezChr(this int i)
    {
        if (i >= 0 && i <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)i };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        else
        {
            throw new ApplicationException("ASCII 碼不合法.");
        }
    }
}