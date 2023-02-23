using LoginFPTBook;
using LoginFPTBook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<ApplicationDbContext>();

// builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
// {
//     //previous code removed for clarity reasons
//     opt.Lockout.AllowedForNewUsers = true;
//     // opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
//     // opt.Lockout.MaxFailedAccessAttempts = 3;
// });

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    //.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    //previous code removed for clarity reasons
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    opt.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// app.Use(async (context, next) => {
//     await context.Response.WriteAsync("Welcome to Middleware1, ");
//     await next();
//     await context.Response.WriteAsync("End Middleware1 ");
// });

// app.UseWhen(c => c.Request.Query.ContainsKey("role"), a => a.Use(async (context, next) => {
//     var role = context.Request.Query["role"];
//     await context.Response.WriteAsync($"Role is {role}, ");
//     await next();
// }));

// app.Run(async context => {
//     await context.Response.WriteAsync("Welcome to Middleware2, ");
// });


using (var scope = app.Services.CreateScope()){
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();
