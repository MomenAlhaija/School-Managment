using Microsoft.EntityFrameworkCore;
using School.BL.Service;
using School.Core.Data;
using School.DL.Repositry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC services
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IClassRepositry, ClassRepositry>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IMaterialRepositry, MaterialRepositry>();
builder.Services.AddTransient<IMaterialService, MaterialService>();
builder.Services.AddTransient<IClassMaterialRepositry, ClassMaterialRepositry>();
builder.Services.AddTransient<IUserRepositrty, UserRepositry>();
builder.Services.AddTransient<IStudentRepositry, StudentRepositry>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITeacherRepositry,TeacherRepositry>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMarkRepositry, MarkRepositry>();
builder.Services.AddTransient<IMarkService, MarkService>();
builder.Services.AddTransient<ILoginService, LoginService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
