global using shopping.Models;
global using Dapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using System.Data;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DI 注入設定
builder.Services.AddSingleton<CssService>();
#endregion

#region Controller設定
//若要將應用程式設定為遵守瀏覽器 Accept 標頭，請將 RespectBrowserAcceptHeader 屬性設定為 true：
builder.Services.AddControllers(options =>
{ options.RespectBrowserAcceptHeader = true; });
//若要設定 System.Text.Json 型格式器的功能，請使用 Microsoft.AspNetCore.Mvc.JsonOptions.JsonSerializerOptions。
// .AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.PropertyNamingPolicy = null;
// });
//預設 JSON 格式器使用 System.Text.Json。 若要使用 Newtonsoft.Json 型格式器，請安裝
// .AddNewtonsoftJson(options =>
// {
//     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
// });
builder.Services.AddRazorPages();
#endregion

#region 解決 Json 格式中文亂碼問題
builder.Services.AddRazorPages()
        .AddJsonOptions(options =>
        {
            //原本是 JsonNamingPolicy.CamelCase，強制頭文字轉小寫，我偏好維持原樣，設為null
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //允許基本拉丁英文及中日韓文字維持原字元
            options.JsonSerializerOptions.Encoder =
                JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
        });
#endregion

#region 環境設定檔設定
var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
var environmentName = builder.Environment.EnvironmentName;
builder.Configuration
    .SetBasePath(currentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
#endregion

#region 資料庫連線設定
builder.Services.AddDbContext<dbEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
#endregion

#region WebAPI設定
builder.Services.AddSingleton<JWTBase, JWTServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "待辦事項 WebAPI",
        Description = "一個 ASP.NET Core 管理待辦事項的 Web API",
        TermsOfService = new Uri("https://localhost:5050/Home/Terms"),
        Contact = new OpenApiContact
        {
            Name = "連絡我們",
            Url = new Uri("https://localhost:5050/Home/Contact")
        },
        License = new OpenApiLicense
        {
            Name = "版權宣告",
            Url = new Uri("https://localhost:5050/Home/License")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion

#region WebAPI JWT 設定
var str_issuer = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("Issuer") ?? "shopping";
var str_audience = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("Audience") ?? "shopping";
var str_signing_key = builder.Configuration.GetSection("JwtSettings")
    .GetValue<string>("SignKey") ?? "123730a1-1e99-428b-9f6d-9f3ed4021234";

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true; // 當驗證失敗時，會顯示失敗的詳細錯誤原因
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 配置驗證發行者
            ValidateIssuer = true, // 是否要啟用驗證發行者
            ValidIssuer = str_issuer, // 簽發者
            // 配置驗證接收者
            // (由於一般沒有區分特別對象，因此通常不太需要設定，也不太需要驗證)
            ValidateAudience = false, //是否要啟用驗證接收者
            ValidAudience = str_audience, // 接收者
            // 是否要啟用驗證有效時間
            ValidateLifetime = true,
            // 是否要啟用驗證金鑰，一般不需要去驗證，因為通常Token內只會有簽章
            ValidateIssuerSigningKey = false,
            // 配置簽章驗證用金鑰
            // 這裡配置是用來解Http Request內Token加密
            // 如果Secret Key跟當初建立Token所使用的Secret Key不一樣的話會導致驗證失敗
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(str_signing_key)
            ),
            // 是不是有過期時間
            RequireExpirationTime = true,
            //時間偏移量（允許誤差時間）
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });
#endregion

#region Session設定
// 需先加入 Nuget Package "Microsoft.AspNetCore.Session"
// 將 Session 存在 ASP.NET Core 記憶體中
builder.Services.AddDistributedMemoryCache();
// 設定加入 AddHttpContextAccessor
builder.Services.AddHttpContextAccessor();
// 設定 Session 參數值
builder.Services.AddSession(options =>
    {
        // 設定 Session 過期時間, 單位為秒 , 20分鐘 = 20*60 = 1,200秒
        //options.IdleTimeout = TimeSpan.FromSeconds(1200);
        // 設定 Session 過期時間, 單位為分鐘
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        // 限制只有在 HTTPS 連線的情況下，才允許使用 Session
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Name = "shopping";
        // 表示此 Cookie 限伺服器讀取設定，document.cookie 無法存取
        options.Cookie.HttpOnly = true;
    });
//enable the session-based TempData provider
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
//註冊 HttpContextAccessor DI 物件
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region 設定使用 Session
app.UseSession();
#endregion

#region WebAPI設定
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});
#endregion

app.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Init}/{id?}");
app.MapAreaControllerRoute(
    name: "UserArea",
    areaName: "User",
    pattern: "User/{controller=Home}/{action=Init}/{id?}");
app.MapAreaControllerRoute(
    name: "MemberArea",
    areaName: "Member",
    pattern: "Member/{controller=Home}/{action=Init}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
