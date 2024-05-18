using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// JWT 服務類別
/// </summary>
public class JWTServices : BaseClass, JWTBase
{
    public TokenStatus GetToken(TokenLogin model)
    {
        TokenStatus returnValue = new TokenStatus();
        returnValue.IsSuccess = false;
        returnValue.JWT = "";
        returnValue.MessageText = "";

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        var config = builder.Build();

        var issuer = config.GetSection("JwtSettings").GetValue<string>("Issuer");
        var signKey = config.GetSection("JwtSettings").GetValue<string>("SignKey");
        var password = config.GetSection("JwtSettings").GetValue<string>("UserPassword");

        if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Password))
        {
            returnValue.MessageText = "使用者登入資訊不正確!!";
            return returnValue;
        }

        if (model.Password != password)
        {
            returnValue.MessageText = "使用者密碼不正確!!";
            return returnValue;
        }

        JWTCliam jWTCliam = new JWTCliam();
        //使用者
        jWTCliam.UserName = model.Name;
        //接收者
        jWTCliam.Aud = "Admin";
        //發證者
        jWTCliam.Iss = issuer;
        //簽發時間 = 中原標準時間」（GMT+8）
        jWTCliam.Iat = DateTimeOffset.Now.ToUnixTimeSeconds();
        //生效時間 = 中原標準時間」（GMT+8）
        jWTCliam.Nbf = DateTimeOffset.Now.ToUnixTimeSeconds();
        //過期時間 = 中原標準時間」（GMT+8）+ 30 秒
        jWTCliam.Exp = DateTimeOffset.Now.AddMinutes(model.ExpireMinutes).ToUnixTimeSeconds();
        //主旨內容
        jWTCliam.Sub = model.Name;
        //JWT ID = GUID 亂碼
        jWTCliam.Jti = Guid.NewGuid().ToString().Replace("-", "");
        //產 JWT
        returnValue = GetJWT(jWTCliam, signKey, issuer, model.ExpireMinutes);
        return returnValue;
    }

    public TokenStatus GetJWT(JWTCliam jWTCliam, string secretKey, string issuer, int expireMinutes = 30)
    {
        TokenStatus response = new TokenStatus();

        try
        {
            string str_message = "";
            // Step 1. 取得資訊聲明(claims)集合
            List<Claim> claims = GenCliams(jWTCliam, expireMinutes, ref str_message);
            if (!string.IsNullOrEmpty(str_message))
            {
                response.IsSuccess = false;
                response.JWT = string.Empty;
                response.MessageText = str_message;
                return response;
            }

            // Step 2. 建置資訊聲明(claims)物件實體，依據上面步驟產生Data來做
            ClaimsIdentity userClaimsIdentity = new ClaimsIdentity(claims);

            // Step 3. 建立Token加密用金鑰
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // 建立簽章，依據金鑰, 使用 HmacSha256 進行加密
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Step 5. 建立Token內容實體
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer, // 設置發行者資訊
                Audience = issuer, // 設置驗證發行者對象，如果需要驗證Token發行者，需要設定此項目
                NotBefore = DateTime.Now, // 設置可用時間， 預設值就是 DateTime.Now
                IssuedAt = DateTime.Now, // 設置發行時間，預設值就是 DateTime.Now
                Subject = userClaimsIdentity, // Token 針對User資訊內容物件
                Expires = DateTime.Now.AddMinutes(expireMinutes), // 建立Token有效期限
                SigningCredentials = signingCredentials // Token簽章
            };

            // Step 6. 產生 JWT Token 並轉換成字串
            // 建立一個JWT Token處理容器
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // 將Token內容實體放入JWT Token處理容器
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            // 最後將JWT Token處理容器序列化，這一個就是最後會需要的Token 字串
            string serializeToken = tokenHandler.WriteToken(securityToken);
            // 告訴使用此請求一方Token成功產生
            response.IsSuccess = true;
            // 放置產生的Token字串
            response.JWT = serializeToken;
            // 放置結果訊息
            response.MessageText = "Done.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.JWT = string.Empty;
            response.MessageText = ex.Message;
        }

        return response;
    }

    // https://auth0.com/docs/tokens/json-web-tokens/json-web-token-claims#custom-claims
    /// <summary>
    /// 建置資訊聲明集合
    /// </summary>
    /// <param name="jWTCliam">Token資訊聲明內容物件</param>
    /// <param name="expireMinutes">過期分鐘數</param>
    /// <param name="messateText">錯誤訊息</param>
    /// <returns>一組收集資訊聲明集合</returns>
    private List<Claim> GenCliams(JWTCliam jWTCliam, int expireMinutes, ref string messateText)
    {
        messateText = "";
        List<Claim> claims = new List<Claim>();
        long lng_now = DateTimeOffset.Now.ToUnixTimeSeconds();

        // Token接受者(audience)，用在驗證接收者驗證是否相符
        if (!string.IsNullOrEmpty(jWTCliam.Aud))
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, jWTCliam.Aud));
        else
        { messateText = "驗證接收者未設定!!"; return claims; }

        // Token 過期時間，一但超過這時間此Token就失效
        if (jWTCliam.Exp > lng_now)
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, jWTCliam.Exp.ToString()));
        else
        { messateText = "Token 已過期!!"; return claims; }

        // Token 發行時間(issued at time)，用在後面檢查 Token 發行多久
        if (lng_now >= jWTCliam.Iat)
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, jWTCliam.Iat.ToString()));
        else
        { messateText = "Token 發行時間不正確!!"; return claims; }

        // Token 有效起始時間(not before time)，用來驗證 Token 可用時間
        if (lng_now >= jWTCliam.Nbf)
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, jWTCliam.Nbf.ToString()));
        else
        { messateText = "Token 有效起始時間不正確!!"; return claims; }

        // 發行者資訊 (issuer)
        if (!string.IsNullOrEmpty(jWTCliam.Iss))
            claims.Add(new Claim(JwtRegisteredClaimNames.Iss, jWTCliam.Iss));
        else
        { messateText = "Token 發行者未設定!!"; return claims; }

        // JWT Token ID，避免Token重複在被套用
        if (!string.IsNullOrEmpty(jWTCliam.Jti))
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jWTCliam.Jti));
        else
        { messateText = "Token ID 未設定!!"; return claims; }

        // Token 主旨(subject)，放置該User內容
        if (!string.IsNullOrEmpty(jWTCliam.Sub))
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, jWTCliam.Sub));
        else
        { messateText = "Token 主旨未設定!!"; return claims; }

        return claims;
    }
}

/// <summary>
/// JWT 底層介面
/// </summary>
public interface JWTBase
{
    /// <summary>
    ///  產生JWT Token
    /// </summary>
    /// <param name="jWTCliam">Token 資訊聲明內容物件</param>
    /// <param name="secretKey">加密金鑰，用來做加密簽章用</param>
    /// <param name="issuer">Token 發行者資訊</param>
    /// <param name="expireMinutes">Token 有效期限(分鐘)</param>
    /// <returns>回應內容物件，內容屬性jwt放置Token字串</returns>
    TokenStatus GetJWT(
        JWTCliam jWTCliam,
        string secretKey,
        string issuer,
        int expireMinutes = 3
    );
}

/// <summary>
/// JWT 聲明資訊模型類別
/// </summary>
public class JWTCliam
{
    /// <summary>
    /// 聲明資訊-發行者
    /// </summary>
    /// <value></value>
    public string Iss { set; get; } = "";

    /// <summary>
    /// 聲明資訊-User內容
    /// </summary>
    /// <value></value>
    public string Sub { set; get; } = "";

    /// <summary>
    /// 聲明資訊-接收者
    /// </summary>
    /// <value></value>
    public string Aud { set; get; } = "";

    /// <summary>
    /// 聲明資訊-有效期限
    /// </summary>
    /// <value></value>
    public long Exp { set; get; } = 0;

    /// <summary>
    /// 聲明資訊-起始時間
    /// </summary>
    /// <value></value>
    public long Nbf { set; get; } = 0;

    /// <summary>
    /// 聲明資訊-發行時間
    /// </summary>
    /// <value></value>
    public long Iat { set; get; } = 0;

    /// <summary>
    /// 聲明資訊-獨立識別ID
    /// </summary>
    /// <value></value>
    public string Jti { set; get; } = "";

    /// <summary>
    /// 使用者名稱
    /// </summary>
    public string UserName { get; set; } = "";
}

/// <summary>
/// 驗證 WebAPI 使用者模型
/// </summary>
public class TokenLogin
{
    /// <summary>
    /// 使用者名稱
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = "";
    /// <summary>
    /// 使用者密碼
    /// </summary>
    /// <value></value>
    public string Password { get; set; } = "";
    /// <summary>
    /// Token 過期分鐘數
    /// </summary>
    /// <value></value>
    public int ExpireMinutes { get; set; } = 30;
}

public class TokenStatus
{
    /// <summary>
    /// Token是否有成功產生狀態
    /// </summary>
    /// <value>布林值</value>
    public bool IsSuccess { set; get; } = false;

    /// <summary>
    /// Token
    /// </summary>
    /// <value>字串</value>
    public string JWT { set; get; } = "";

    /// <summary>
    /// 處理訊息
    /// </summary>
    /// <value>字串</value>
    public string MessageText { set; get; } = "";
}