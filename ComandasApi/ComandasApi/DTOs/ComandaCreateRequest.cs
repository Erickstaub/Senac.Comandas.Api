namespace ComandasApi.DTOs
{
    public class ComandaCreateRequest
    {
        public int numeroMesa { get; set; }
        public string nomeCliente { get; set; }
        public int[] CardapioItensId { get; set; } = default!;
    }
}
