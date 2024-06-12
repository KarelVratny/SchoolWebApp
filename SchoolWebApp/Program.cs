using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using SchoolWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Connection string
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection"));
	//options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnection"));
	options.UseSqlServer(builder.Configuration.GetConnectionString("MonsterDbConnection"));	
});

//Predani kontroleru instanci service
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<GradeService>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
});
builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // Pro zobrazeni chyb na publishovane appce
    //app.UseDeveloperExceptionPage();
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

app.Run();
