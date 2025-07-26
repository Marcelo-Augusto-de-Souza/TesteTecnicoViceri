using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.SuperheroisRotas;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ViceriTesteTecnicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
var MyCors = "_myCors";
builder.Services.AddCors(o =>
    o.AddPolicy(MyCors, p => p
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));
var app = builder.Build();


app.UseCors(MyCors);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RotasSuperherois_();

app.Run();



