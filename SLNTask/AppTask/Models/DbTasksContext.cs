using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AppTask.Models;

namespace AppTask.Models;

public partial class DbTasksContext : DbContext
{
    public DbTasksContext()
    {
    }

    public DbTasksContext(DbContextOptions<DbTasksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CentralCusto> CentralCustos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Incidente> Incidentes { get; set; }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CentralCusto>(entity =>
        {
            entity.HasKey(e => e.CentralId).HasName("PK__CentralC__74F6332241665DAD");

            entity.ToTable("CentralCusto");

            entity.Property(e => e.NomeCusto)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ValorAnualMeta).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId).HasName("PK__Departam__66BB0E3E55411C0F");

            entity.ToTable("Departamento");

            entity.Property(e => e.Ativo)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Descricao)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Funciona__06370DAD559C97B2");

            entity.ToTable("Funcionario");

            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Incidente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Incident__06370DAD4E6C96C0");

            entity.ToTable("Incidente");

            entity.Property(e => e.DataIncidente).HasColumnType("datetime");
            entity.Property(e => e.DescricaoProblema)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Resolvido)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Solucao)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Tarefa__06370DADCFAF76AA");

            entity.ToTable("Tarefa");

            entity.Property(e => e.DataCancelada).HasColumnType("datetime");
            entity.Property(e => e.DataFinalizada).HasColumnType("datetime");
            entity.Property(e => e.DataIniciada).HasColumnType("datetime");
            entity.Property(e => e.DataPlanejada).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Prazo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StatusTarefa)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarefa_Funcionario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<AppTask.Models.Departamento> Departamento { get; set; } = default!;
}
