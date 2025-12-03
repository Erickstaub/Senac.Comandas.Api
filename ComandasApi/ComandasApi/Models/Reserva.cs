using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComandasApi.Models
{
    public class reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int NumeroMesa { get; set; }
        public string Telefone { get; set; }
        public DateTime _dataHoraReserva { get; set; } = DateTime.Now;
    }
}
