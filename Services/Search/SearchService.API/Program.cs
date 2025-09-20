
using Carter;
using SearchService.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCarter();
try
{
    await app.InitDbAsync();
}
catch (Exception ex) { 
    Console.WriteLine(ex.ToString());
}
app.Run();