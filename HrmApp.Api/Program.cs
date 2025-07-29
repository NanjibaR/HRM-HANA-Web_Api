using HrmApp;
using HrmApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<HrmDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();



var app = builder.Build();




if (app.Environment.IsDevelopment())

{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(op => {
        op.SwaggerEndpoint("/swagger/v1/swagger.json", "My App");
        op.RoutePrefix ="";

    });
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();






















//builder.Services.AddDbContext<HrmDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwagger();
//    app.UseSwaggerUI(op => {
//        op.SwaggerEndpoint("/swagger/v1/swagger.json", "My App");
//        op.RoutePrefix ="";

//    });
//}

//app.UseHttpsRedirection();

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();

