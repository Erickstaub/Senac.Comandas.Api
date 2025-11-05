using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComandasApi.Models
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string ClienteNome { get; set; } = default!;
        public int SituaçãoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();

    }
    public enum SituaçãoComanda
    {
        Aberta = 0,
        Fechada = 1,
        Cancelada = 2
    }
}
