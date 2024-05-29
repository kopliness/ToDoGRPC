using Microsoft.EntityFrameworkCore;
using ToDoGRPC.Data;
using ToDoGRPC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=postgres_db2;Port=5432;Database=ToDoDB;Username=postgres;Password=3568"));

builder.Services.AddGrpc();

var app = builder.Build();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();