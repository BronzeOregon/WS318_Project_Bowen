using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WS318_Project_Bowen;

public partial class GSchoolCWs318ProjectBowenDatabase1MdfContext : DbContext
{
    public GSchoolCWs318ProjectBowenDatabase1MdfContext()
    {
    }

    public GSchoolCWs318ProjectBowenDatabase1MdfContext(DbContextOptions<GSchoolCWs318ProjectBowenDatabase1MdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MeleeWeapon> MeleeWeapons { get; set; }

    public virtual DbSet<MeleeWeaponSpecialRule> MeleeWeaponSpecialRules { get; set; }

    public virtual DbSet<NonVehicleModel> NonVehicleModels { get; set; }

    public virtual DbSet<NonVehicleModelMeleeWeapon> NonVehicleModelMeleeWeapons { get; set; }

    public virtual DbSet<NonVehicleModelRangedWeapon> NonVehicleModelRangedWeapons { get; set; }

    public virtual DbSet<NonVehicleModelSpecialRule> NonVehicleModelSpecialRules { get; set; }

    public virtual DbSet<RangedWeapon> RangedWeapons { get; set; }

    public virtual DbSet<RangedWeaponSpecialRule> RangedWeaponSpecialRules { get; set; }

    public virtual DbSet<SpecialRule> SpecialRules { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitNonVehicleModel> UnitNonVehicleModels { get; set; }

    public virtual DbSet<UnitVehicleModel> UnitVehicleModels { get; set; }

    public virtual DbSet<VehicleModel> VehicleModels { get; set; }

    public virtual DbSet<VehicleModelRangedWeapon> VehicleModelRangedWeapons { get; set; }

    public virtual DbSet<VehicleModelSpecialRule> VehicleModelSpecialRules { get; set; }

    //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\School_C#\WS318_Project_Bowen\Database1.mdf;Integrated Security=True",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                        );
                });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeleeWeapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MeleeWea__3214EC0762AECA8A");

            entity.ToTable("MeleeWeapon");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Am)
                .HasMaxLength(3)
                .HasColumnName("AM");
            entity.Property(e => e.Ap).HasColumnName("AP");
            entity.Property(e => e.As)
                .HasMaxLength(3)
                .HasColumnName("AS");
            entity.Property(e => e.Im)
                .HasMaxLength(3)
                .HasColumnName("IM");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MeleeWeaponSpecialRule>(entity =>
        {
            entity.ToTable("MeleeWeapon_SpecialRules");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MeleeWeapon).WithMany(p => p.MeleeWeaponSpecialRules)
                .HasForeignKey(d => d.MeleeWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeleeWeaponID");

            entity.HasOne(d => d.SpecialRule).WithMany(p => p.MeleeWeaponSpecialRules)
                .HasForeignKey(d => d.SpecialRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecialRuleID");
        });

        modelBuilder.Entity<NonVehicleModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NonVehic__3214EC079F04CBCA");

            entity.ToTable("NonVehicleModel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Bs).HasColumnName("BS");
            entity.Property(e => e.Cl).HasColumnName("CL");
            entity.Property(e => e.Int).HasColumnName("INT");
            entity.Property(e => e.Inv)
                .HasMaxLength(3)
                .HasColumnName("INV");
            entity.Property(e => e.Ld).HasColumnName("LD");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Sv).HasMaxLength(3);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Wp).HasColumnName("WP");
            entity.Property(e => e.Ws).HasColumnName("WS");
        });

        modelBuilder.Entity<NonVehicleModelMeleeWeapon>(entity =>
        {
            entity.ToTable("NonVehicleModel_MeleeWeapons");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MeleeWeaponIdtoModelNavigation).WithMany(p => p.NonVehicleModelMeleeWeaponMeleeWeaponIdtoModelNavigations)
                .HasForeignKey(d => d.MeleeWeaponIdtoModel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeleeWeaponIdtoModel");

            entity.HasOne(d => d.NonVehicleModel).WithMany(p => p.NonVehicleModelMeleeWeaponNonVehicleModels)
                .HasForeignKey(d => d.NonVehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonVehicleModelI");
        });

        modelBuilder.Entity<NonVehicleModelRangedWeapon>(entity =>
        {
            entity.ToTable("NonVehicleModel_RangedWeapons");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.NonVehicleModel).WithMany(p => p.NonVehicleModelRangedWeapons)
                .HasForeignKey(d => d.NonVehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonVehicleModel_RangedWeapons_NonVehicleModelId");

            entity.HasOne(d => d.RangedWeapon).WithMany(p => p.NonVehicleModelRangedWeapons)
                .HasForeignKey(d => d.RangedWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonVehicleModel_RangedWeapons_RangedWeaponId");
        });

        modelBuilder.Entity<NonVehicleModelSpecialRule>(entity =>
        {
            entity.ToTable("NonVehicleModel_SpecialRules");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.NonVehicleModel).WithMany(p => p.NonVehicleModelSpecialRules)
                .HasForeignKey(d => d.NonVehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonVehicleModel_SpecialRules_NonVehicleModelId");

            entity.HasOne(d => d.SpecialRules).WithMany(p => p.NonVehicleModelSpecialRules)
                .HasForeignKey(d => d.SpecialRulesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonVehicleModel_SpecialRules_SpecialRulesId");
        });

        modelBuilder.Entity<RangedWeapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RangedWe__3214EC075C14E693");

            entity.ToTable("RangedWeapon");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ap).HasColumnName("AP");
            entity.Property(e => e.Fp).HasColumnName("FP");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Rs).HasColumnName("RS");
        });

        modelBuilder.Entity<RangedWeaponSpecialRule>(entity =>
        {
            entity.ToTable("RangedWeapon_SpecialRules");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.RangedWeapon).WithMany(p => p.RangedWeaponSpecialRules)
                .HasForeignKey(d => d.RangedWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RangedWeapon_ID");

            entity.HasOne(d => d.SpecialRule).WithMany(p => p.RangedWeaponSpecialRules)
                .HasForeignKey(d => d.SpecialRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecialRules_ID");
        });

        modelBuilder.Entity<SpecialRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpecialR__3214EC076644DAAB");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unit__3214EC0782BA3512");

            entity.ToTable("Unit");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ModelCount).HasColumnName("Model Count");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UnitNonVehicleModel>(entity =>
        {
            entity.ToTable("Unit_NonVehicleModels");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.NonVehicleModel).WithMany(p => p.UnitNonVehicleModels)
                .HasForeignKey(d => d.NonVehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Unit_NonVehicleModels_NonVehicleModelId");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitNonVehicleModels)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Unit_NonVehicleModels_UnitId");
        });

        modelBuilder.Entity<UnitVehicleModel>(entity =>
        {
            entity.ToTable("Unit_VehicleModels");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitVehicleModels)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Unit_VehicleModels_UnitId");

            entity.HasOne(d => d.VehicleModel).WithMany(p => p.UnitVehicleModels)
                .HasForeignKey(d => d.VehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Unit_VehicleModels_NonVehicleModelId");
        });

        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleM__3214EC07FB2C74B1");

            entity.ToTable("VehicleModel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Bs).HasColumnName("BS");
            entity.Property(e => e.FrontAv).HasColumnName("FrontAV");
            entity.Property(e => e.Hp).HasColumnName("HP");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RearAv).HasColumnName("RearAV");
            entity.Property(e => e.SideAv).HasColumnName("SideAV");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<VehicleModelRangedWeapon>(entity =>
        {
            entity.ToTable("VehicleModel_RangedWeapon");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.RangedWeapon).WithMany(p => p.VehicleModelRangedWeapons)
                .HasForeignKey(d => d.RangedWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModel_RangedWeapon_RangedWeaponId");

            entity.HasOne(d => d.VehicleModel).WithMany(p => p.VehicleModelRangedWeapons)
                .HasForeignKey(d => d.VehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModel_RangedWeapon_VehicleModelId");
        });

        modelBuilder.Entity<VehicleModelSpecialRule>(entity =>
        {
            entity.ToTable("VehicleModel_SpecialRules");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.SpecialRules).WithMany(p => p.VehicleModelSpecialRules)
                .HasForeignKey(d => d.SpecialRulesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModel_SpecialRules_SpecialRulesId");

            entity.HasOne(d => d.VehicleModel).WithMany(p => p.VehicleModelSpecialRules)
                .HasForeignKey(d => d.VehicleModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModel_SpecialRules_VehicleModelId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
