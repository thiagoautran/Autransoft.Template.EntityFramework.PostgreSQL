using System;
using Autransoft.Template.EntityFramework.Lib.Interfaces;
using Autransoft.Template.EntityFramework.PostgreSQL.Lib.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Data
{
    public class AutranSoftContext : DbContext, IAutranSoftContext
    {
        private readonly IAutranSoftLogger<AutranSoftContext> _logger;
        private readonly PosgreSQL _posgreSQL;

        public Func<PosgreSQL, string> GetConnectionString;

        public AutranSoftContext
        (
            IAutranSoftLogger<AutranSoftContext> logger, 
            IOptions<DTOs.Autransoft> autransoft
        ) 
        {
            _posgreSQL = autransoft?.Value?.Database?.PosgreSQL;
            _logger = logger;
        } 

        public AutranSoftContext
        (
            IAutranSoftLogger<AutranSoftContext> logger, 
            IOptions<DTOs.Autransoft> autransoft, 
            Func<PosgreSQL, string> getConnectionString
        )
        {
            _posgreSQL = autransoft?.Value?.Database?.PosgreSQL;
            _logger = logger;
            
            GetConnectionString = getConnectionString;
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    optionsBuilder.UseNpgsql(GetConnectionString(_posgreSQL));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message={ex.Message}");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
                builder.ApplyConfigurationsFromAssembly(assembly);
            }
        }

        //private string GetConnectionString() =>
        //    $"Server={_posgreSQL?.EndPoint};Database={_posgreSQL?.DataBaseName};Uid={_posgreSQL?.User};Pwd={_posgreSQL?.Pass}";
    }
}