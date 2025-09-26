namespace ComandasApi.Models
{
    public class PedidoCozinhaItem
    {
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItem> Itens { get; set; } = [];


    }
}
