using BookingClients.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<BookService>();
builder.Services.AddControllers();  // <-- add this so controllers are recognized

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // show detailed errors in dev
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();  // keep this if you plan to add auth later

app.MapControllers();  // <-- map controller endpoints

app.Run();
