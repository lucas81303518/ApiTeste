using ApiTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<PioresFilmes> pioresFilmes { get; set; }
    }
}
