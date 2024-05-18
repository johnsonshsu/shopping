using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 加解密功能服務
/// </summary>
public class CryptographyService : BaseClass
{
    /// <summary>
    /// 一般文字轉成 SHA256 加密文字
    /// </summary>
    /// <param name="input">一般文字</param>
    /// <returns>SHA256 加密文字</returns> 
    public string StringToSHA256(string input)
    {
        using var sha256Hash = SHA256.Create();
        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }
    /// <summary>
    /// 比較一般文字及已加密文字是否相同
    /// </summary>
    /// <param name="normalString">一般文字</param>
    /// <param name="sha256String">已加密文字</param>
    /// <returns></returns>
    public bool CompareSHA256String(string normalString, string sha256String)
    {
        return (StringToSHA256(normalString).Equals(sha256String));
    }
}