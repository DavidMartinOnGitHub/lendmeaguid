using LendMeAGuid;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use the following to get to the swagger page
// https://localhost:7105/swagger/index.html


//app.MapGet("/", () => Results.LocalRedirect("/swagger"));

// Use the following URL to get to an index.html front end
// https://localhost:7105/
app.MapGet("/", async (HttpContext ctx) =>
{
    //sets the content type as html
    ctx.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    await ctx.Response.SendFileAsync("wwwroot/index.html");
});

app.MapGet("/guid", () =>
{
    return Guid.NewGuid().ToString().ToUpper();
})
.WithOpenApi();

app.MapPost("/guid", ([FromBody] GuidRequestOptions options) =>
{
    if (options.Count <= 0) return "";

    StringBuilder sb = new StringBuilder();
    for(int idx = 0; idx < options.Count; idx++)
    {
        string guid = Guid.NewGuid().ToString();
        if (options.IsUpperCaseRequired) guid = guid.ToUpper();
        if (options.IsBracketsRequired) guid = "{"+ guid + "}";
        if (!options.IsHyphensRequired) guid = guid.Replace("-", "");
        sb.AppendLine(guid);
    }

    return sb.ToString();
})
.WithOpenApi();

app.Run();

