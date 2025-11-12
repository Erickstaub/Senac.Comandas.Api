namespace ComandasApi.DTOs
{
    public class ComandaUpdateRequest
    {
        public int numeroMesa { get; set; }
        public string nomeCliente { get; set; } = default!;
        public ComandaItemUpdateRequest[] Itens { get; set; } = [];
    }
}
public class ComandaItemUpdateRequest
{
     public int Id { get; set; }
     public bool Remove { get; set; }
     public int CardapioItensId { get; set; }

}
