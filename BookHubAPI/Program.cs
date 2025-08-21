using Microsoft.EntityFrameworkCore;
using BookingClients.Services;
using BookingClients.Models;
using BookingClients.Data;   // <-- add this

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddSingleton<BookService>();

builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite("Data Source=books.db"));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
