using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

string? connectionString = app.Configuration.GetConnectionString("defaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<Labs.AppContext>();
var options = optionsBuilder.UseSqlServer(connectionString).Options;

using (Labs.AppContext db = new Labs.AppContext(options))
{
    var countWithMiddleNameLINQ = db.Customers
                            .Where(c => !string.IsNullOrEmpty(c.MiddleName))
                            .Count();
    Console.WriteLine(countWithMiddleNameLINQ);

    var countWithMiddleNameSQL = db.Customers
    .FromSqlRaw("SELECT * FROM SalesLT.Customer WHERE MiddleName IS NOT NULL AND MiddleName <> ''")
    .Count();
    Console.WriteLine(countWithMiddleNameSQL);

    var customersWithMiddleName = db.Customers
                              .Where(c => !string.IsNullOrEmpty(c.MiddleName)).ToList();
    foreach (var item in  customersWithMiddleName)
    {
        Console.WriteLine(item.MiddleName);
    }
}

app.Run();

