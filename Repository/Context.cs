using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Context : IdentityDbContext<UsuarioLogado>
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Cozinha> cozinhas { get; set; }
        public DbSet<ItemPedido> itemPedidos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
    }
}
