using System;
using Autransoft.Template.EntityFramework.PostgreSQL.Lib.DTOs;
using Autransoft.Template.EntityFramework.PostgreSQL.Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Data
{
    public class AutranSoftContext : DbContext, IAutranSoftContext
    {
        private readonly PosgreSQL _posgreSQL;

        public AutranSoftContext(IOptions<Autransoft.Template.EntityFramework.PostgreSQL.Lib.DTOs.Autransoft> autransoftAppSettings) => _posgreSQL = autransoftAppSettings?.Value?.Database?.PosgreSQL;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = !string.IsNullOrEmpty(_posgreSQL.LocalConnectionString) ? _posgreSQL.LocalConnectionString : GetConnectionString();

                try
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Message={ex.Message}|ConnectionString={connectionString}");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
                builder.ApplyConfigurationsFromAssembly(assembly);
            }
        }

        private string GetConnectionString() =>
            $"Server={_posgreSQL?.EndPoint};Database={_posgreSQL?.DataBaseName};Uid={_posgreSQL?.User};Pwd={_posgreSQL?.Pass}";
    }
}