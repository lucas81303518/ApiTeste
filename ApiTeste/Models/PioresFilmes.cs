using FileHelpers;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Models
{
    [DelimitedRecord(";")]
    public class PioresFilmes
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Studios { get; set; }
        public string Producer { get; set; }
        public bool Winner { get; set; }
       
    }
}
