using ApiTeste.Models;

namespace ApiTeste.Services
{
    public class PioresFilmesService : IPioresFilmesService
    {
        private readonly Context _context;
        public PioresFilmesService(Context context)
        {
            _context = context;
        }
        public MinMaxIntervaloDePremios GetMinMaxInterval()
        {
            List<Max> listaMaior = _context.pioresFilmes.Where(w => w.Winner == true)
                                                       .GroupBy(p => p.Producer.ToUpper())
                                                       .Where(g => g.Count() > 1)
                                                       .Select(w => new Max
                                                       {
                                                           Producer = w.Key,
                                                           PreviousWin = w.Min(f => f.Year),
                                                           FollowingWin = w.Max(f => f.Year),
                                                           Interval = (w.Max(f => f.Year) - w.Min(f => f.Year))
                                                       }).OrderByDescending(i => i.Interval)
                                                         .ToList();

            int maiorIntervalo = listaMaior.FirstOrDefault().Interval;
            List<Max> listaProdutoresMaxIntervalo = listaMaior.Where(f => f.Interval == maiorIntervalo)
                                                              .ToList();

            List<Min> listaMenor = _context.pioresFilmes.Where(w => w.Winner == true)
                                            .GroupBy(p => p.Producer)
                                            .Where(g => g.Count() > 1)
                                            .Select(w => new Min
                                            {
                                                Producer = w.Key,
                                                PreviousWin = w.Min(f => f.Year),
                                                FollowingWin = w.Max(f => f.Year),
                                                Interval = (w.Max(f => f.Year) - w.Min(f => f.Year))
                                            }).OrderBy(i => i.Interval)
                                              .ToList();
            int menorIntervalo = listaMenor.FirstOrDefault()?.Interval ?? 0;
            List<Min> listaProdutoresMinIntervalo = listaMenor.Where(f => f.Interval == menorIntervalo)
                                                              .ToList();

            MinMaxIntervaloDePremios minMax = new MinMaxIntervaloDePremios();
            minMax.max = listaProdutoresMaxIntervalo;
            minMax.min = listaProdutoresMinIntervalo;
            return minMax;
        }
    }
}
