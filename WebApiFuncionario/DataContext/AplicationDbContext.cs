using Microsoft.EntityFrameworkCore;
using WebApiFuncionario.Models;

namespace WebApiFuncionario.DataContext
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }

    }
}
