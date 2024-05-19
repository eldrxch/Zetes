using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zetes.API;
using Zetes.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Zetes.Data.ZetesDBContext>( p => {
    var c = new ZetesDBContext(builder.Configuration.GetConnectionString("ZetesSQLliteDB"));
    c.Database.Migrate();
    return c;
});
builder.Services.AddDbContext<Zetes.Data.ZetesDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ZetesSQLliteDB")));
builder.Services.AddScoped<Zetes.API.Projects>();
builder.Services.AddScoped<Zetes.API.Customers>();
builder.Services.AddScoped<Zetes.API.Users>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(
    options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;        
    })     
    .AddEntityFrameworkStores<Zetes.Data.ZetesDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder =>
        {
            builder.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DefaultFilesOptions options = new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "index.html" }    
};

app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseCors("AllowAll");   
app.MapFallbackToFile("index.html");
app.MapIdentityApi<AppUser>();

var customerMaps = app.MapGroup("/api/customers");
customerMaps.MapGet("", async (Customers customers) => await customers.GetCustomers());
customerMaps.MapGet("{id}", async (Customers customers, int id) => await customers.GetCustomer(id));
customerMaps.MapPost("", async (Customers customers, Zetes.Data.Customer customer) => await customers.AddCustomer(customer));
customerMaps.MapPut("{id}", async (Customers customers, int id, Zetes.Data.Customer customer) => await customers.UpdateCustomer(customer));
customerMaps.MapDelete("{id}", async (Customers customers, int id) => await customers.DeleteCustomer(id));

var projectMaps = app.MapGroup("/api/projects");
projectMaps.MapGet("", async (Projects projects) => await projects.GetProjects());
projectMaps.MapGet("{id}", async (Projects projects, int id) => await projects.GetProject(id));
projectMaps.MapPost("", async (Projects projects, Zetes.Data.Project project) => await projects.AddProject(project));
projectMaps.MapPut("{id}", async (Projects projects, int id, Zetes.Data.Project project) => await projects.UpdateProject(project));
projectMaps.MapDelete("{id}", async (Projects projects, int id) => await projects.DeleteProject(id));

var usersMaps = app.MapGroup("/api/users");
usersMaps.MapGet("", async (Users users) => await users.GetUsers());
usersMaps.MapDelete("{id}", async (Users users, string id) => await users.DeleteUser(id));

app.Run();

// Allows for easier testing of the API
public partial class Program
{

}