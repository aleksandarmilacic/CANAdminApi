using CANAdminApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Data
{
    public class Context : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public Context() : base()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            _loggerFactory = _loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
#if DEBUG
                    .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CANAdminDB;Trusted_Connection=True;MultipleActiveResultSets=True")
#elif RELEASE
                 .UseSqlServer(@"Server=.\SQLEXPRESS;Database=CANAdminDB;Trusted_Connection=True;Integrated Security=SSPI;MultipleActiveResultSets=True")
#endif
                .EnableSensitiveDataLogging().UseLoggerFactory(_loggerFactory);


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

          
            // here is for fluent api, but we dont need it in this case :)

            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

         
        // db sets

        public DbSet<File> Files { get; set; }

        public DbSet<NetworkNode> NetworkNodes { get; set; }

        public DbSet<CanMessage> CanMessages { get; set; }

        public DbSet<CanSignal> CanSignals { get; set; }
    }
}
