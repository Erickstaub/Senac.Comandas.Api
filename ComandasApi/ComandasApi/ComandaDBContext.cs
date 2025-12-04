using Microsoft.EntityFrameworkCore;
using ComandasApi.Models;

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
                },
                new Models.Mesa
                {
                    Id = 2,
                    NumeroMesa = 2,
                    SituaçãoMesa = (int)Models.SituacaoMesa.Livre
                },
                new Models.Mesa
                {
                    Id = 3,
                    NumeroMesa = 3,
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
                },
                new Models.Comanda
                {
                    Id = 2,
                    MesaId = 2,
                    ClienteNome = "Outro Cliente",
                    SituaçãoComanda = 0
                },
                new Models.Comanda
                {
                    Id = 3,
                    MesaId = 3,
                    ClienteNome = "Terceiro Cliente",
                    SituaçãoComanda = 0
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
                },
                new Models.CardapioItem
                {
                    Id = 2,
                    Titulo = "Coca cola 300ml",
                    Descricao = "Refrigerante geladinho",
                    Preco = 6.00m,
                    PossuiPreparo = false,
                },
                new Models.CardapioItem
                {
                    Id = 3,
                    Titulo = "Batata frita",
                    Descricao = "Batata frita crocante",
                    Preco = 12.00m,
                    PossuiPreparo = true,
                }
                );
            modelBuilder.Entity<Models.CategoriaCardapio>().HasData(
                new Models.CategoriaCardapio
                {
                    Id = 1,
                    Nome = "Lanches",
                },
                new Models.CategoriaCardapio
                {
                    Id = 2,
                    Nome = "Bebidas",
                },
                new Models.CategoriaCardapio
                {
                    Id = 3,
                    Nome = "Acompanhamentos",
                }
                );
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
        public DbSet<Models.CategoriaCardapio> CategoriaCardapio { get; set; } = default!;
        public DbSet<Models.Funcionario> Funcionario { get; set; } = default!;
    }
}
