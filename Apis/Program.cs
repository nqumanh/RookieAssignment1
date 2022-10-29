using Microsoft.EntityFrameworkCore;
using Apis.Data;
using Apis.Repository;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5001",
                                                "http://localhost:3000")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                    .AllowCredentials(); // allow credentials
                      });
});

builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreContext") ??
        throw new InvalidOperationException("Connection string 'BookStoreContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
