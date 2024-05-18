using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// .NetFrameWork string 的擴充功能
/// </summary>
public static partial class Extension
{
    /// <summary>
    /// 將字串轉換成整數
    /// <para>範例：</para>
    /// <para>string s = "32";</para>
    /// <para>int i = s.ezToInt32();</para>
    /// <para>回傳：</para>
    /// <para>i = 32</para>
    /// </summary>
    /// <param name="StringName">字串</param>
    /// <returns>整數</returns>
    public static int ezToInt32(this string StringName)
    {
        int intValue = 0;
        if (!StringName.ezIsNullOrEmpty()) intValue = Int32.Parse(StringName);
        return intValue;
    }

    /// <summary>
    /// 取文字右方指定長度字串
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="length">指定長度</param>
    /// <returns>字串</returns>
    public static string ezRight(this string s, int length)
    {
        length = Math.Max(length, 0);
        return (s.Length > length) ? s.Substring(s.Length - length, length) : s;
    }

    /// <summary>
    /// 取文字左方指定長度字串
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="length">指定長度</param>
    /// <returns>字串</returns>
    public static string ezLeft(this string s, int length)
    {
        return (s.Trim().Length > 0) ? s.Substring(0, length > s.Length ? s.Length : length) : s;
    }

    /// <summary>
    /// 取文字指定開始位置起算，並指長度之字串
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="start">開始起算位置</param>
    /// <param name="length">指定長度</param>
    /// <returns>字串</returns>
    public static string ezMid(this string s, int start, int length)
    {
        length = Math.Max(length, 0);
        return (s.Length > (length + start - 1)) ? s.Substring(start, length) : s;
    }

    /// <summary>
    /// 除去文字中指定字元.
    /// <para>範例：</para>
    /// <para>string s = "abcde";</para>
    /// <para>s = s.ezStrip('b');</para>
    /// <para>回傳：</para>
    /// <para>s = "acde"</para>
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="character">指定字元</param>
    /// <returns>除去文字中指定字元後的文字</returns>
    public static string ezStrip(this string s, char character)
    {
        return s.Replace(character.ToString(), "");
    }

    /// <summary>
    /// 除去文字中指定的字元陣列.
    /// <para>範例：</para>
    /// <para>string s = "abcde";</para>
    /// <para>s = s.ezStrip('a', 'd');</para>
    /// <para>回傳：</para>
    /// <para>s = "bce"</para>
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="chars">指定字元陣列</param>
    /// <returns>除去文字中指定字元陣列後的文字</returns>
    public static string ezStrip(this string s, params char[] chars)
    {
        foreach (char c in chars)
        {
            s = s.Replace(c.ToString(), "");
        }
        return s;
    }
    /// <summary>
    /// 除去文字中指定文字
    /// <para>範例：</para>
    /// <para>string s = "abcde";</para>
    /// <para>s = s.ezStrip("bcd");</para>
    /// <para>回傳：</para>
    /// <para>s = "ae"</para>
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="subString">指定文字</param>
    /// <returns>除去文字中指定文字後的文字</returns>
    public static string ezStrip(this string s, string subString)
    {
        s = s.Replace(subString, "");
        return s;
    }

    /// <summary>
    /// 將文字依指定字串分隔成陣列
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="SplitWord">指定字串</param>
    /// <returns>字串陣列</returns>
    public static string[] ezSplit(this string s, string SplitWord)
    {
        return Regex.Split(s, SplitWord);
    }

    /// <summary>
    /// 將文字分隔非字元的字串成陣列
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns>字串陣列</returns>
    public static string[] ezSplitWords(this string s)
    {
        return Regex.Split(s, @"\W+");
    }

    /// <summary>
    /// 將文字分隔斷行字(\r\n)元的字串成陣列
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns>字串陣列</returns>
    public static string[] ezSplitLine(this string s)
    {
        return Regex.Split(s, @"\r\n");
    }

    /// <summary>
    /// 文字指定另一文字來相比對，看是否相符
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="CompareString">比對的文字</param>
    /// <returns>相符為true , 不符為false</returns>
    public static bool ezCompare(this string s, string CompareString)
    {
        if (string.Compare(s, CompareString) == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 文字指定另一文字來相比對，並指定是否需要判斷大小寫，看是否相符
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="CompareString">比對的文字</param>
    /// <param name="ignoreCase">是否判斷大小寫</param>
    /// <returns>相符為true , 不符為false</returns>
    public static bool ezCompare(this string s, string CompareString, bool ignoreCase)
    {
        if (string.Compare(s, CompareString, ignoreCase) == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 查詢字串共有幾段字句
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns>字句數</returns>
    public static int ezWordCount(this string s)
    {
        return s.Split(new char[] { ' ', '.', '?' },
                         StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// 去除字串前後空白及中間空白部份重覆空白僅留一個空白
    /// <para>範例：</para>
    /// <para>string text = "　I'm　　wearing　the　cheese.　It isn't　wearing me!　"</para>
    /// <para>text = text.ezTrimAndReduce();</para>
    /// <para>回傳：</para>
    /// <para>text = "I'm wearing the cheese. It isn't wearing me!";</para>
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns>去除空白後的文字</returns>
    public static string ezTrimAndReduce(this string s)
    {
        return ezConvertWhitespacesToSingleSpaces(s).Trim();
    }

    /// <summary>
    /// 去除字串中間空白部份重覆空白僅留一個空白
    /// <para>範例：</para>
    /// <para>string text = "　I'm　　wearing　the　cheese.　It isn't　wearing me!　"</para>
    /// <para>text = text.ezConvertWhitespacesToSingleSpaces();</para>
    /// <para>回傳：</para>
    /// <para>text = "　I'm wearing the cheese. It isn't wearing me!　";</para>
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns>去除空白後的文字</returns>
    public static string ezConvertWhitespacesToSingleSpaces(this string s)
    {
        return Regex.Replace(s, @"\s+", " ");
    }

    /// <summary>
    /// 判斷文字是否為 Null 值
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Boolean ezIsNull(this string s)
    {
        return s == null;
    }

    /// <summary>
    /// 判斷文字是否為 Null 值或空值
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns></returns>
    public static Boolean ezIsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    /// <summary>
    /// 判斷文字是否為 Null 值或空白字串
    /// </summary>
    /// <param name="s">文字</param>
    /// <returns></returns>
    public static Boolean ezIsNullOrWhiteSpace(this string s)
    {
        return string.IsNullOrWhiteSpace(s);
    }

    /// <summary>
    /// 判斷文字是否為與另一個字串為相等
    /// </summary>
    /// <param name="s">文字</param>
    /// <param name="pattern">比較的字串</param>
    /// <returns>true為相等，false為不相等</returns>
    public static bool ezIsMatch(this string s, string pattern)
    {
        if (s.ezIsNullOrEmpty())
            return false;
        if (pattern.ezIsNullOrEmpty())
            return false;
        return Regex.IsMatch(s, pattern);
    }

    /// <summary>
    /// 字串反轉
    /// </summary>
    /// <param name="s">字串</param>
    /// <returns>反轉後的字串</returns>
    public static string ezReverse(this string s)
    {
        if (String.IsNullOrEmpty(s))
            return "";
        char[] charArray = new char[s.Length];
        int len = s.Length - 1;
        for (int i = 0; i <= len; i++)
        {
            charArray[i] = s[len - i];
        }
        return new string(charArray);
    }

    /// <summary>
    /// 判斷字串是為為整數字串
    /// </summary>
    /// <param name="s">字串</param>
    /// <returns></returns>
    public static bool ezIsInteger(this string s)
    {
        Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
        return regularExpression.Match(s).Success;
    }

    /// <summary>
    /// 判斷字符串是否可以轉換為數字
    /// </summary>
    /// <param name="s">要判斷的字符串</param>
    /// <returns></returns>
    public static bool ezIsDouble(this string s)
    {
        bool bln_result = false;
        double dbl_value = 0;
        if (double.TryParse(s, out dbl_value)) bln_result = true;
        return bln_result;
    }

    /// <summary>
    /// 判斷字符串是否可轉換為DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool ezIsDateTime(this string s)
    {
        bool bln_result = false;
        DateTime dateValue = DateTime.MinValue;
        if (System.DateTime.TryParse(s, out dateValue)) bln_result = true;
        return bln_result;
    }

    /// <summary>
    /// 產生重覆字串
    /// </summary>
    /// <param name="s">重覆的字串</param>
    /// <param name="length">重覆的次數</param>
    /// <returns></returns>
    public static string ezRepeat(this string s, int length)
    {
        // 使用 StringBuilder 加速字串重覆產生的速度 
        StringBuilder dstr = new StringBuilder();
        int icnt = 0;

        for (icnt = 0; icnt <= length - 1; icnt++)
        {
            dstr.Append(s);
        }

        return dstr.ToString();
    }

    /// <summary>
    /// 將多餘的文字字串截掉並用其他符號替代
    /// </summary>
    /// <param name="s">傳入字串</param>
    /// <param name="length">字串的長度</param>
    /// <returns></returns>
    public static string ezCutText(this string s, int length)
    {
        if (s == null || s == "") { return ""; }
        if (System.Text.Encoding.Default.GetByteCount(s) > length)
        {
            int i = 0;
            int j = 0;
            foreach (char myChar in s)
            {
                //如果大於127代表不是英文或數字或符號那些
                //，都是2位元組以上的字，像是中文那些
                if ((int)myChar > 127)
                    i += 2;
                else
                    i++;
                if (i > length)
                {
                    s = s.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
        }
        return s;
    }

    /// <summary>
    /// 去掉有小數的文字後面的 0 , 可指定保留的小數位數
    /// </summary>
    /// <param name="s">傳入字串</param>
    /// <param name="length">保留的小數位數</param>
    public static string ezTrimZero(this string s, int length)
    {
        if (s == null || s == "") { return ""; }
        if (s.IndexOf('.') <= 0) { return s; }
        int int_index = s.IndexOf('.');
        string str_string = s;
        if (s.Length > 0)
        {
            int i = s.Length - 1;
            for (i = s.Length - 1; i >= 0; i--)
            {
                if (s.Substring(i, 1) != "0") break;
                if (s.Substring(i, 1) == ".") { i--; break; }
                if (length > 0)
                {
                    if ((i - int_index) == length) break;
                }
            }
            if (length > 0) i++;
            str_string = s.Substring(0, i);
        }
        return str_string;
    }

    /// <summary>
    /// 取得字元對應的ASCII碼
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <returns>ASCII碼</returns>
    public static int ezAsc(this string s)
    {
        if (s.Trim().Length == 1)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            int intAsciiCode = (int)asciiEncoding.GetBytes(s)[0];
            return (intAsciiCode);
        }
        else
        {
            throw new ApplicationException("字元不合法");
        }
    }

    /// <summary>
    /// 查詢是否為什麼字開頭的字串
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <param name="StringTarget">開頭的字串</param>
    /// <returns></returns>
    public static bool ezStartWith(this string s, string StringTarget)
    {
        return (s.StartsWith(StringTarget, StringComparison.CurrentCultureIgnoreCase)) ? true : false;
    }

    /// <summary>
    /// 截取字串
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <param name="len">長度</param>
    /// <returns></returns>
    public static string ezCutString(this string s, int len)
    {
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] bte_source = ascii.GetBytes(s);
        for (int i = 0; i < bte_source.Length; i++)
        {
            tempLen += ((int)bte_source[i] == 63) ? 2 : 1;

            try
            {
                tempString += s.Substring(i, 1);
            }
            catch
            {
                break;
            }
            if (tempLen > len) break;
        }
        //如果截過則加上半個省略號
        byte[] mybyte = Encoding.Default.GetBytes(s);
        if (mybyte.Length > len) tempString += "…";
        return tempString;
    }

    /// <summary>
    /// 字串右方加入指定字串,可設定有方已有指定字串,則不加入
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <param name="AddString">加入指定字串</param>
    /// <param name="ExistsNonAdd">字串右方已存在則不加入</param>
    /// <returns></returns>
    public static string ezRightAddString(this string s, string AddString, bool ExistsNonAdd)
    {
        string str_value = s;
        if (ExistsNonAdd)
        {
            if (!s.ezIsRightString(AddString)) str_value += AddString;
        }
        else
            str_value += AddString;
        return str_value;
    }

    /// <summary>
    /// 回傳字串右方是否為指定的字串
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <param name="RightString">指定的字串</param>
    /// <returns></returns>
    public static bool ezIsRightString(this string s, string RightString)
    {
        return (s.ezRight(RightString.Length) == RightString) ? true : false;
    }

    /// <summary>
    /// 回傳字串左方是否為指定的字串
    /// </summary>
    /// <param name="s">來源字元</param>
    /// <param name="LeftString">指定的字串</param>
    /// <returns></returns>
    public static bool ezIsLeftString(this string s, string LeftString)
    {
        return (s.ezLeft(LeftString.Length) == LeftString) ? true : false;
    }
}