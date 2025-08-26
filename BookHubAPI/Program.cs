using BookingClients.Data;   // FIXED namespace
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (SQLite for example)
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<BookingClients.Services.BookService>();

builder.Services.AddControllers();

var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();

app.Run();
