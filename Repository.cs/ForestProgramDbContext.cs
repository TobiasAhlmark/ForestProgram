using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ForestProgram.Models;

public partial class ForestProgramDbContext : DbContext
{
    string connectionstring = File.ReadAllText(".env");
    public ForestProgramDbContext()
    {
    }

    public ForestProgramDbContext(DbContextOptions<ForestProgramDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DamageAndDisease> DamageAndDiseases { get; set; }

    public virtual DbSet<DamageRepair> DamageRepairs { get; set; }

    public virtual DbSet<Enviroment> Enviroments { get; set; }

    public virtual DbSet<ForestArea> ForestAreas { get; set; }

    public virtual DbSet<PlantingHistory> PlantingHistories { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    public virtual DbSet<Tree> Trees { get; set; }

    public virtual DbSet<TreeManagement> TreeManagements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
    => optionsBuilder.UseSqlServer(connectionstring);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DamageAndDisease>(entity =>
        {
            entity.HasKey(e => e.DamageAndDiseaseId).HasName("PK_DamageAndDiseaseID");

            entity.ToTable("DamageAndDisease");

            entity.Property(e => e.DamageAndDiseaseId).HasColumnName("DamageAndDiseaseID");
            entity.Property(e => e.DamageAndDiseaseType).IsUnicode(false);
            entity.Property(e => e.ForestAreaId).HasColumnName("ForestAreaID");
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Severity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpeciesId).HasColumnName("SpeciesID");
            entity.Property(e => e.Spread)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Symptom).IsUnicode(false);
            entity.Property(e => e.TreeId).HasColumnName("TreeID");
        });

        modelBuilder.Entity<DamageRepair>(entity =>
        {
            entity.HasKey(e => e.DamageRepairId).HasName("PK_DamageRepairID");

            entity.ToTable("DamageRepair");

            entity.Property(e => e.DamageRepairId).HasColumnName("DamageRepairID");
            entity.Property(e => e.Action).IsUnicode(false);
            entity.Property(e => e.DamageAndDiseaseId).HasColumnName("DamageAndDiseaseID");
            entity.Property(e => e.FollowUp).IsUnicode(false);
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Resources).IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Result).IsUnicode(false);
            entity.Property(e => e.Satus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TimeSpan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Enviroment>(entity =>
        {
            entity.HasKey(e => e.EnviromentId).HasName("PK_EnviromentID");

            entity.ToTable("Enviroment");

            entity.Property(e => e.EnviromentId).HasColumnName("EnviromentID");
            entity.Property(e => e.Altitude)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ForestAreaId).HasColumnName("ForestAreaID");
            entity.Property(e => e.GroundType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precipitation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Temperature)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Wind)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ForestArea>(entity =>
        {
            entity.HasKey(e => e.ForestAreId).HasName("PK_ForestAreaID");

            entity.ToTable("ForestArea");

            entity.Property(e => e.ForestAreId).HasColumnName("ForestAreID");
            entity.Property(e => e.AreaSquareMeters)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EcoSystem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ForestType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlantingHistory>(entity =>
        {
            entity.HasKey(e => e.PlantingHistoryId).HasName("PK_PlantingHistoryID");

            entity.ToTable("PlantingHistory");

            entity.Property(e => e.PlantingHistoryId).HasColumnName("PlantingHistoryID");
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.NumberOfTreesPlanted)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlantedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpeciesId).HasColumnName("SpeciesID");
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.SpeciesId).HasName("PK_SpeciesID");

            entity.Property(e => e.SpeciesId).HasColumnName("SpeciesID");
            entity.Property(e => e.Adaptation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LifeSpan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tree>(entity =>
        {
            entity.HasKey(e => e.TreeId).HasName("PK_TreeID");

            entity.ToTable("Tree");

            entity.Property(e => e.TreeId).HasColumnName("TreeID");
            entity.Property(e => e.ForestAreId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ForestAreID");
            entity.Property(e => e.Health)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Height)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.SpeciesId).HasColumnName("SpeciesID");
            entity.Property(e => e.TrunkDiameter)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TreeManagement>(entity =>
        {
            entity.HasKey(e => e.TreeManagementId).HasName("PK_TreeManagementID");

            entity.ToTable("TreeManagement");

            entity.Property(e => e.TreeManagementId).HasColumnName("TreeManagementID");
            entity.Property(e => e.Action).IsUnicode(false);
            entity.Property(e => e.ForestAreId).HasColumnName("ForestAreID");
            entity.Property(e => e.Method).IsUnicode(false);
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.NumberOfTreesTreated).IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpeciesId).HasColumnName("SpeciesID");
            entity.Property(e => e.TreeId).HasColumnName("TreeID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
