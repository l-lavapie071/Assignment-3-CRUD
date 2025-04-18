using Assignment_3_CRUD___Model.Middleware;
using Assignment_3_CRUD.Repositories;
using Assignment_3_CRUD___Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register the repository for dependency injection 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddSingleton<ILoginRepository, LoginRepository>();





var app = builder.Build();

// Register the custom authentication middleware

app.UseSession();  // Enable session
app.UseMiddleware<AuthMiddleware>();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}"); // Redirect to Login by default
app.Run();