using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;

namespace StudentVacations.Models
{
    public class ApplicationContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
        {
            //Configuration = configuration;

            //var conn = (Microsoft.Data.SqlClient.SqlConnection)Database.GetDbConnection();
            ////conn.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            //string userAssignedClientId = "a83e866f-939c-4bd5-a434-aed6e543a248"; // MIH-WE-MIH-CYGH-Q-APP-01
            //var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
            
            //var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
            //conn.AccessToken = token.Token;

            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=tcp:mih-we-mih-cygh-q-db.database.windows.net,1433;Database=mihjrs;"); // MIH-WE-MIH-CYGH-Q-APP-01
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Сourse> Сourses { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}
