using QuanLyThueDat.Application.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuanLyThueDatDbContext>
(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, AppRole>()
               .AddEntityFrameworkStores<QuanLyThueDatDbContext>()
               .AddDefaultTokenProviders();
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddScoped<IDoanhNghiepService, DoanhNghiepService>();
builder.Services.AddScoped<IQuyetDinhThueDatService, QuyetDinhThueDatService>();
builder.Services.AddScoped<IHopDongThueDatService, HopDongThueDatService>();
builder.Services.AddScoped<IThongBaoDonGiaThueDatService, ThongBaoDonGiaThueDatService>();
builder.Services.AddScoped<IQuyetDinhMienTienThueDatService, QuyetDinhMienTienThueDatService>();
builder.Services.AddScoped<IThongBaoTienThueDatService, ThongBaoTienThueDatService>();
builder.Services.AddScoped<IUserService, UserService>();

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
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
