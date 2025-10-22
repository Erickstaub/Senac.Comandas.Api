using Microsoft.EntityFrameworkCore;

namespace ComandasApi
{
    public class ComandaDBContext : DbContext
    {
        public ComandaDBContext(DbContextOptions<ComandaDBContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItens { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidosCozinha { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItens { get; set; } = default!;

    }
}
