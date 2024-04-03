using CRUDPessoas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Infrastructure.Data
{
    public class PessoaDbContext : DbContext
    {
        public PessoaDbContext(DbContextOptions<PessoaDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<PessoaFisica> PessoaFisica { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePessoaJuridica(modelBuilder);
            ConfigurePessoaFisica(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigurePessoaJuridica(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaJuridica>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<PessoaJuridica>().Property(pj => pj.RazaoSocial)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<PessoaJuridica>().Property(pj => pj.NomeFantasia)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<PessoaJuridica>().Property(pj => pj.CNPJ)
                .IsRequired()
                .HasMaxLength(14);
            modelBuilder.Entity<PessoaJuridica>().Property(pj => pj.DataAbertura)
                .IsRequired();
            modelBuilder.Entity<PessoaJuridica>()
                .HasOne(pj => pj.PessoaFisica)
                .WithMany()
                .HasForeignKey(pj => pj.PessoaFisicaId);
        }

        private static void ConfigurePessoaFisica(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaFisica>()
                .HasKey(pf => pf.Id);
            modelBuilder.Entity<PessoaFisica>().Property(pf => pf.Nome)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<PessoaFisica>().Property(pf => pf.RG)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<PessoaFisica>().Property(pf => pf.CPF)
                .IsRequired()
                .HasMaxLength(11);
            modelBuilder.Entity<PessoaFisica>().Property(pf => pf.DataNascimento).IsRequired();
        }

    }
}
