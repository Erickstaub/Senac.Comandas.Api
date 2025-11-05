using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComandasApi.Models
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaItemId { get; set; }
        public virtual Comanda Comanda { get; set; }
        public int SituaçãoPedido { get; set; } // 0 - Pendente, 1 - Em Preparo, 2 - Pronto, 3 - Entregue, 4 - Cancelado
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
    public enum SituaçãoPedido
    {
        Pendente = 0,
        EmPreparo = 1,
        Pronto = 2,
        Entregue = 3,
        Cancelado = 4
    }
}
