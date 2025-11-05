using Microsoft.EntityFrameworkCore;

namespace ComandasApi
{
    public class ComandaDBContext : DbContext
    {
        public ComandaDBContext(DbContextOptions<ComandaDBContext> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>()
               .HasData(
                new Models.Usuario
                {
                    Id = 1,
                    Nome = "admin",
                    Email = "email",
                    Senha = "admin"
                }
                );
            modelBuilder.Entity<Models.Mesa>().HasData(
                new Models.Mesa
                {
                    Id = 1,
                    NumeroMesa = 1,
                    SituaçãoMesa = (int)Models.SituacaoMesa.Livre
                }
            );
            modelBuilder.Entity<Models.Comanda>().HasData(
                new Models.Comanda
                {
                    Id = 1,
                    MesaId = 1,
                    ClienteNome = "Cliente Exemplo",
                    SituaçãoComanda = 1
                }
                );
            modelBuilder.Entity<Models.CardapioItem>().HasData(
                new Models.CardapioItem
                {
                    Id = 1,
                    Titulo = "X bacon",
                    Descricao = "que x tudo que x bacon...",
                    Preco = 19.90m,
                    PossuiPreparo = true,
                }
                )
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItens { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidosCozinha { get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> PedidosCozinhaItens { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItens { get; set; } = default!;

    }
}
