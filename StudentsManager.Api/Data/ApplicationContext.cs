using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StudentsManager.Core.BusinessObjects;
using StudentsManager.Core.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentsManager.Api.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Student> Students => Set<Student>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromSeconds(120));
        }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        int IApplicationContext.SaveChanges()
        {
            return SaveChanges();
        }

        Task<int> IApplicationContext.SaveChangesAsync()
        {
            return SaveChangesAsync();
        }

        Task<int> IApplicationContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
