using Estoque.DTO;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Data
{
    public class EstoqueDbContext : DbContext
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
 
    }
}