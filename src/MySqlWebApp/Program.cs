using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var connection = builder.Configuration.GetConnectionString("MyDataConnection");
Console.WriteLine("Connection string: {0}", connection);
builder.Services.AddDbContext<MyDataContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.MapGet("/customers", async (MyDataContext context) => {
    // Open SQL connection and query the database
    var customers = await context.Customers.ToListAsync();
    return customers;
});

app.Run();

public class Customer {
    public int CustomerID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    // Add other properties as needed
}

public class MyDataContext : DbContext {

    public MyDataContext(DbContextOptions<MyDataContext> options) : base(options) {
        
        // Nothing to do here
     }

     protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Customer>().ToTable("Customer", "SalesLT");
    }

    public DbSet<Customer> Customers { get; set; }
}
