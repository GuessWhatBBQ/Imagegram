using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Providers.AuthHandlers.Scheme;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(options => options.DefaultScheme = nameof(SessionHeaderAuthHandler))
    .AddScheme<SessionHeaderAuthSchemeOptions, SessionHeaderAuthHandler>(
        nameof(SessionHeaderAuthHandler),
        options => { }
    );

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding postgres connection
builder.Services.AddDbContext<PostgresContext>(
    optionsBuilder =>
        optionsBuilder.UseNpgsql(
            builder.Configuration.GetConnectionString("PostgreSQLDbConnection")
        )
);

var app = builder.Build();

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
