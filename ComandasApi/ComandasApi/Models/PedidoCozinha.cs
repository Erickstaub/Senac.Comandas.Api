namespace ComandasApi.Models
{
    public class PedidoCozinha
    {
        public int Id { get; set; }
        public int ComandaItemId { get; set; }
    
        public int SituaçãoPedido { get; set; } // 0 - Pendente, 1 - Em Preparo, 2 - Pronto, 3 - Entregue, 4 - Cancelado
    }
}
