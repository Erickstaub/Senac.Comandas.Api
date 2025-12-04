namespace ComandasApi.DTOs
{
    public class FuncionarioCreateRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public int Salario { get; set; }
    }
}
