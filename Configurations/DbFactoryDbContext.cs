using CursoBackEnd.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CursoBackEnd.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDBContext>
    {
        public CursoDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDBContext>();
            optionsBuilder.UseSqlServer("");
            CursoDBContext contexto = new CursoDBContext(optionsBuilder.Options);

            return contexto;
        }

       
    }

       
}
