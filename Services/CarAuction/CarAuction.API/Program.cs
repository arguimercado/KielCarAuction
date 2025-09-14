using CarAuction.API.Commons.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddCarter();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.UseMigrationDbAsync();
}

app.UseHttpsRedirection();
app.MapCarter();
app.Run();
