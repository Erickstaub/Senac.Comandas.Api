using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComandasApi.Models
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SituaçãoMesa { get; set; }
        public int NumeroMesa { get; set; }
    }
    public enum SituacaoMesa
    {
        Livre = 0,
        Ocupada = 1,
        Reservada = 2

    }
}
