/* ■Program.cs */
/* 　⇒ASP.NET Core MVCアプリケーションをWebサービスとして実行するためのメインとなるコード */


//--------------------------------------------------------------------------------------------------------
// builder
//--------------------------------------------------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.（コンテナにサービスを追加する）
builder.Services.AddControllersWithViews();



//--------------------------------------------------------------------------------------------------------
// app
//--------------------------------------------------------------------------------------------------------
var app = builder.Build();

/* Configure the HTTP request pipeline.（HTTPリクエストのパイプラインを設定する）*/

// 開発環境でない場合
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // HSTSのデフォルト値は30日間です。本番環境では変更することをお勧めします。詳細はhttps://aka.ms/aspnetcore-hstsを参照してください。
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
