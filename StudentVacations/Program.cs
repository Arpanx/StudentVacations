using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using StudentVacations.Models;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddConsole();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection2"));
//*builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer());
//*builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ArpanxDB"));

var app = builder.Build();

app.UseHsts();
app.UseDeveloperExceptionPage();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();