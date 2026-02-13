using Microsoft.EntityFrameworkCore;
using SampleMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// p.43 [Add] データベース接続設定を追加
builder.Services.AddDbContext<MvcdbContext>(options =>
    // ※Postgresqlの場合は、UseNpgsqlメソッドを使う
    options.UseNpgsql(builder.Configuration.GetConnectionString("MvcdbContext")
                        ?? throw new InvalidOperationException("Connection string 'MvcdbContext' not found.")
                     )
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
