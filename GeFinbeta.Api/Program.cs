using GeFinbeta.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Banco de dados SQLite
builder.Services.AddDbContext<GefinContext>(options =>
    options.UseSqlite("Data Source=../GeFinbeta/gefin.db"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - permite que o frontend Vue acesse a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger disponível durante o desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Permite requisições vindas do frontend Vue
app.UseCors("VueApp");

app.UseAuthorization();

app.MapControllers();

app.Run();