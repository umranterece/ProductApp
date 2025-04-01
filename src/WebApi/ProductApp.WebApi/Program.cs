
using ProductApp.Domain.Entities;
using ProductApp.Persistence;
using ProductApp.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Products.Any())
    {
        context.Products.AddRange(new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Pen", Value = 10, Quatity = 100 },
            new Product { Id = Guid.NewGuid(), Name = "Paper A4", Value = 200, Quatity = 5 },
            new Product { Id = Guid.NewGuid(), Name = "Book", Value = 45, Quatity = 150 }
        });

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
