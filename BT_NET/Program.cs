using Microsoft.EntityFrameworkCore;
using BT_NET.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Cấu hình Dịch vụ (Services) ---

builder.Services.AddControllersWithViews();

// Lấy chuỗi kết nối SQLite từ appsettings.json
// Lưu ý: Hãy đảm bảo trong appsettings.json có mục "SQLiteConnection"
var sqliteConnectionString = builder.Configuration.GetConnectionString("SQLiteConnection");

// Đăng ký DbContext - Chỉ sử dụng duy nhất SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(sqliteConnectionString);
});

// --- 2. Xây dựng App (Build) ---
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();

    DbInitializer.Initialize(context);
}

// --- 3. Cấu hình Pipeline (Middleware) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// Thêm dòng này vào ngay sau builder.Build()
app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");//Trang thông báo not found 

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();