namespace ComandasApi.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int SituaçãoMesa { get; set; }
        public int NumeroMesa { get; set; }
    }
    public enum SituacaoMesa
    {
        Livre = 0,
        Ocupada = 1

    }
}
