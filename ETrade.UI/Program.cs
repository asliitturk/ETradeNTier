using Etrade.DAL.Abstract;
using Etrade.DAL.Concreate;
using Etrade.DAL.Conscreate;
using Etrade.Data.Context;
using Etrade.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<EtradeContext>(); //EtradeContext tipi için DbContext'in eklenmesi = Uygulanamanın yaşam süresi boyunca kullanılcak olan 'EtradeContext' tipinin DbContext sınıfını ekler.Genellikler veritabanı işlemleri için kullanılır.
builder.Services.AddScoped<ICategoryDAL, CategoryDAL>();// 'ICategoryDAL' interface ile 'CategoryDAL' sınıfını ilişkilendirir ve her bi istemci isteği için bir kapsamda (scope) bir örneği oluşturur.Yani,her bir istemci isteği için ayrı bir 'CategoryDAL' örneği oluşturulur.
builder.Services.AddScoped<IProductDAL, ProductDAL>();
builder.Services.AddScoped<IOrderDAL, OrderDAL>();

//Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<EtradeContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";//Giriş yapılmadıysa
    options.AccessDeniedPath = "/";//Yetkisi yoksa
    options.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie", //Kimlik doğrulama Cookie Adı
        HttpOnly = false, //Tarayıcı dışında javascript tarafından erişilebilir olmasına izin ver
        SameSite = SameSiteMode.Lax, //Aynı site politikası, tarayıcının cookie'nin kendi alanına gönderilmesini sağlar
        SecurePolicy = CookieSecurePolicy.Always //Sadece HTTPS üzerinden iletilmesini sağlar
    };
    options.SlidingExpiration = true; //Kullanıcı etkin olduğu sürece oturumun süresini yeniler
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15); //Oturumun süresi, kullanıcı etkin olmasa daki 15 dk boyunca geçerli olacak şekilde ayarlar
});

//Oturum eklemek için
builder.Services.AddSession();

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

//Identity ve cookie için
app.UseAuthentication();//Önce giriş kontrolü
app.UseAuthorization();//Sonra yetki kontrolü

//Use Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
