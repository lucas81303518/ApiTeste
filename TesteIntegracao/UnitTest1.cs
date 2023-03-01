using ApiTeste;
using ApiTeste.Controllers;
using ApiTeste.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;

namespace TesteIntegracao
{
    [TestFixture]
    public class UnitTest1
    {
        private readonly Context _context;
        public UnitTest1(Context context)
        {
            _context = context;
        }

        [Test]
        public void TestMethod1()
        {
            PioresFilmes pioresFilmes = new PioresFilmes
            {
                Producer = "producerTeste",
                Studios = "studioTeste",
                Year = 1990,
                Title = "titleTeste",
                Winner = true
            };
            _context.pioresFilmes.Add(pioresFilmes);

            PioresFilmes pioresFilmes1 = new PioresFilmes
            {
                Producer = "producerTeste",
                Studios = "studioTeste1",
                Year = 2000,
                Title = "titleTeste1",
                Winner = true
            };
            _context.pioresFilmes.Add(pioresFilmes1);

            PioresFilmes pioresFilmes2 = new PioresFilmes
            {
                Producer = "producerTeste1",
                Studios = "studioTeste1",
                Year = 1990,
                Title = "titleTeste1",
                Winner = true
            };
            _context.pioresFilmes.Add(pioresFilmes2);

            PioresFilmes pioresFilmes3 = new PioresFilmes
            {
                Producer = "producerTeste1",
                Studios = "studioTeste1",
                Year = 1994,
                Title = "titleTeste1",
                Winner = true
            };
            _context.pioresFilmes.Add(pioresFilmes3);

            PioresFilmesController pioresFilmesController = new PioresFilmesController(_context);
            
            List<Min> listaMin = new List<Min>();
            Min min = new Min
            {
                Producer = "producerTeste1",
                Interval = 4,
                PreviousWin = 1990,
                FollowingWin = 1994
            };
            listaMin.Add(min);

            List<Max> listaMax = new List<Max>();
            Max max = new Max
            {
                Producer = "producerTeste",
                Interval = 10,
                PreviousWin = 1990,
                FollowingWin = 2000
            };
            listaMax.Add(max);

            MinMaxIntervaloDePremios minMaxIntervaloDePremios = new MinMaxIntervaloDePremios();
            minMaxIntervaloDePremios.min = listaMin;
            minMaxIntervaloDePremios.max = listaMax;
            string esperado = JsonSerializer.Serialize(minMaxIntervaloDePremios);
            var Result = pioresFilmesController.GetMinMaxInterval();
            Assert.Equals(Result, esperado);
        }
    }
}