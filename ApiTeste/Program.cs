using ApiTeste;
using ApiTeste.Controllers;
using ApiTeste.Models;
using ApiTeste.Services;
using Microsoft.EntityFrameworkCore;
using static System.IO.StreamReader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Context>();
builder.Services.AddScoped<IPioresFilmesService, PioresFilmesService>();
builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("teste"));
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
var reader = new StreamReader(File.OpenRead("ArquivoCsv/movielist.csv"));
List<PioresFilmes> listaPioresFilmes = new List<PioresFilmes>();
reader.ReadLine();//Ignorando Header
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();//Prox linha
    var values = line.Split(';');//Separa as Colunas
    var producers = new List<string> ();
   
    foreach (var item in values[3].Split(" and "))
    {
        producers.AddRange(item.Split(", "));
    }
    foreach (var item in producers)
    {
        PioresFilmes piorFilme = new PioresFilmes
        {
            Year = Convert.ToInt32(values[0]),
            Title = values[1],
            Studios = values[2],
            Producer = item,
            Winner = values[4] == "yes"
        };
        listaPioresFilmes.Add(piorFilme);
    }   
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();    
    foreach (var piorFilme in listaPioresFilmes)
    {        
        dbContext.pioresFilmes.Add(piorFilme);        
    }
    dbContext.SaveChanges();   
}
app.Run();

