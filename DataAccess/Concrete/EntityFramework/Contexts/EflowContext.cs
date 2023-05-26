using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete.Uretim;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public partial class EflowContext : DbContext
{
    public EflowContext()
    {
    }
    IConfiguration Configuration { get; set; }
    public EflowContext(DbContextOptions<EflowContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<UretilecekKoli> UretilecekKolis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("EflowConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UretilecekKoli>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_URETILECEK_KOLI");

            entity.Property(e => e.Ciid).HasColumnName("CIID");
            entity.Property(e => e.Did).HasColumnName("DID");
            entity.Property(e => e.Value).HasColumnName("VALUE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
