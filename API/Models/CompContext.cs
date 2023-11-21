using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompCompany1.Models;

public partial class CompContext : DbContext
{
    public CompContext()
    {
    }

    public CompContext(DbContextOptions<CompContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;user=root;;Database=comp");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CPU");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProducingCountry).HasMaxLength(100);
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("GPU");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProducingСountry)
                .HasMaxLength(100)
                .HasColumnName("Producing_сountry");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
