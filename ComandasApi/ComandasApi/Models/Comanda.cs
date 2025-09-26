namespace ComandasApi.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string ClienteNome { get; set; } = default!;
        public int SituaçãoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();

    }
}
