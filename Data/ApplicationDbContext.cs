using Microsoft.EntityFrameworkCore;
using WebReverseEngineering.Models;

namespace WebReverseEngineering.Data // Namespace deve ser consistente com o Program.cs
{
    public class usuariosContext : DbContext
    {
        public usuariosContext(DbContextOptions<usuariosContext> options)
            : base(options)
        {
        }

        public DbSet<pessoa> pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=usuarios;User=root;Password=;",
                    new MariaDbServerVersion(new Version(10, 4, 32)));
            }
        }
    }
}
