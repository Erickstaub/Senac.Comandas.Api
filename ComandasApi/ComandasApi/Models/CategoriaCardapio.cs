using System.ComponentModel.DataAnnotations;

namespace ComandasApi.Models
{
    public class CategoriaCardapio
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string? Descrição { get; set; } = default!;
       
    }
}
