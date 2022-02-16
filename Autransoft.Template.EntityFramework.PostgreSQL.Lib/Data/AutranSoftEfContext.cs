using System;
using Autransoft.Template.EntityFramework.Lib.Exceptions;
using Autransoft.Template.EntityFramework.Lib.Interfaces;
using Autransoft.Template.EntityFramework.PostgreSQL.Lib.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Data
{
    public class AutranSoftEfContext : DbContext, IAutranSoftEfContext
    {
        private readonly IAutranSoftEfLogger<AutranSoftEfContext> _logger;
        private readonly PosgreSQL _posgreSQL;

        public Func<PosgreSQL, string> GetConnectionString;

        public AutranSoftEfContext
        (
            IAutranSoftEfLogger<AutranSoftEfContext> logger, 
            IOptions<DTOs.Autransoft> autransoft
        ) 
        {
            _posgreSQL = autransoft?.Value?.Database?.PosgreSQL;
            _logger = logger;

            GetConnectionString = (PosgreSQL posgreSQL) => 
            {
                return $"host={posgreSQL?.Host};port={posgreSQL?.Port};database={posgreSQL?.Database};username={posgreSQL?.Username};password={posgreSQL?.Password}";
            };
        } 

        public AutranSoftEfContext
        (
            IAutranSoftEfLogger<AutranSoftEfContext> logger, 
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
                    _logger.LogError(new AutranSoftEfException(ex));
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
    }
}