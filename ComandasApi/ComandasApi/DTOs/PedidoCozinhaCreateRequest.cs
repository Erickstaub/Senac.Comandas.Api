using ComandasApi.Models;

namespace ComandasApi.DTOs
{
    public class PedidoCozinhaCreateRequest
    {
        public int ComandaItemId { get; set; }

        public int SituaçãoPedido { get; set; }
    }
}
