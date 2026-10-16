using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace appPetShop.Models;

public partial class DbPetShopContext : DbContext
{
    public DbPetShopContext()
    {
    }

    public DbPetShopContext(DbContextOptions<DbPetShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ReservaServico> ReservaServicos { get; set; }

    public virtual DbSet<Veterinario> Veterinarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Cliente__06370DAD33DCCE70");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Cpf, "UQ__Cliente__C1FF9309746CCE32").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReservaServico>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__ReservaS__06370DAD081FBB2E");

            entity.ToTable("ReservaServico");

            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.StatusServico)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ReservaServicos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservaServico_Cliente");

            entity.HasOne(d => d.Veterinario).WithMany(p => p.ReservaServicos)
                .HasForeignKey(d => d.VeterinarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservaServico_Veterinario");
        });

        modelBuilder.Entity<Veterinario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Veterina__06370DAD949F4619");

            entity.ToTable("Veterinario");

            entity.HasIndex(e => e.Crmv, "UQ__Veterina__AD6F9BA41A7A7CDE").IsUnique();

            entity.Property(e => e.Crmv)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Especialidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
