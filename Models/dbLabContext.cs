using System;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Models;

public partial class dbLabContext : DbContext
{
    public dbLabContext(DbContextOptions<dbLabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChemicalOrderDetails> ChemicalOrderDetails { get; set; }

    public virtual DbSet<ChemicalRecords> ChemicalRecords { get; set; }

    public virtual DbSet<Chemicals> Chemicals { get; set; }

    public virtual DbSet<ConsumableOrderDetails> ConsumableOrderDetails { get; set; }

    public virtual DbSet<ConsumableRecords> ConsumableRecords { get; set; }

    public virtual DbSet<Consumables> Consumables { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<SampleRecords> SampleRecords { get; set; }

    public virtual DbSet<Samples> Samples { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<SampleType> SampleType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChemicalOrderDetails>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.ChemicalID }).HasName("PK__Chemical__BA62C1A084360780");

            entity.HasOne(d => d.Chemical).WithMany(p => p.ChemicalOrderDetails)
                .HasForeignKey(d => d.ChemicalID)
                .HasConstraintName("FK__ChemicalO__Chemi__4AB81AF0");

            entity.HasOne(d => d.Order).WithMany(p => p.ChemicalOrderDetails)
                .HasForeignKey(d => d.OrderID)
                .HasConstraintName("FK__ChemicalO__Order__4BAC3F29");
        });

        modelBuilder.Entity<ChemicalRecords>(entity =>
        {
            entity.HasKey(e => e.RecordID).HasName("PK__Chemical__FBDF78C9B3B2C7A1");
            entity.ToTable(tb => tb.HasTrigger("checkChemicalQty"));
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Notes).HasMaxLength(100);

            entity.HasOne(d => d.Chemical).WithMany(p => p.ChemicalRecords)
                .HasForeignKey(d => d.ChemicalID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ChemicalR__Chemi__4CA06362");

            entity.HasOne(d => d.Employee).WithMany(p => p.ChemicalRecords)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ChemicalR__Emplo__4D94879B");
        });

        modelBuilder.Entity<Chemicals>(entity =>
        {
            entity.HasKey(e => e.ChemicalID).HasName("PK__Chemical__9F29A0F6F49F9657");

            entity.Property(e => e.CASNo).HasMaxLength(20);
            entity.Property(e => e.CabinetName).HasMaxLength(10);
            entity.Property(e => e.ChineseName).HasMaxLength(60);
            entity.Property(e => e.EnglishName).HasMaxLength(60);
            entity.Property(e => e.StorageCondition);
            //entity.Property(e => e.Type).HasMaxLength(10);
            entity.Property(e => e.Unit);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Chemicals)
                .HasForeignKey(d => d.SupplierID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chemicals__Suppl__4E88ABD4");

            entity.HasOne(d => d.Categories).WithMany(p => p.Chemicals)
                .HasForeignKey(d => d.CategoryID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Chemicals_Categories");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryID).HasName("PK__Categori__19093A2B4F0368DE");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<ConsumableOrderDetails>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.ConsumableID }).HasName("PK__Consumab__B69EBA8262918CDF");

            entity.HasOne(d => d.Consumable).WithMany(p => p.ConsumableOrderDetails)
                .HasForeignKey(d => d.ConsumableID)
                .HasConstraintName("FK__Consumabl__Consu__4F7CD00D");

            entity.HasOne(d => d.Order).WithMany(p => p.ConsumableOrderDetails)
                .HasForeignKey(d => d.OrderID)
                .HasConstraintName("FK__Consumabl__Order__5070F446");
        });

        modelBuilder.Entity<ConsumableRecords>(entity =>
        {
            entity.HasKey(e => e.RecordID).HasName("PK__Consumab__FBDF78C94C7DBE2C");
            entity.ToTable(tb => tb.HasTrigger("checkConsumableQty"));
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Notes).HasMaxLength(100);

            entity.HasOne(d => d.Consumable).WithMany(p => p.ConsumableRecords)
                .HasForeignKey(d => d.ConsumableID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Consumabl__Consu__5165187F");

            entity.HasOne(d => d.Employee).WithMany(p => p.ConsumableRecords)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Consumabl__Emplo__52593CB8");
        });

        modelBuilder.Entity<Consumables>(entity =>
        {
            entity.HasKey(e => e.ConsumableID).HasName("PK__Consumab__50EE12D618CD6DFD");

            entity.Property(e => e.Cabinet).HasMaxLength(5);
            entity.Property(e => e.ConsumableName).HasMaxLength(60);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.Shelf).HasMaxLength(5);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Consumables)
                .HasForeignKey(d => d.SupplierID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Consumabl__Suppl__534D60F1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF1344AA461");

            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Role);
            entity.Property(e => e.password).HasMaxLength(12);
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("PK__Orders__C3905BAF54A24751");
            entity.ToTable(e => e.HasTrigger("orderQty"));
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Status);

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__Employee__5441852A");
        });

        modelBuilder.Entity<SampleRecords>(entity =>
        {
            entity.HasKey(e => e.RecordID).HasName("PK__SampleRe__FBDF78C9A23AC1A2");
            entity.ToTable(tb => tb.HasTrigger("checkSampleQty"));
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.TransactionType).HasColumnType("bit");

            entity.HasOne(d => d.Employee).WithMany(p => p.SampleRecords)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SampleRec__Emplo__5535A963");

            entity.HasOne(d => d.Sample).WithMany(p => p.SampleRecords)
                .HasForeignKey(d => d.SampleID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SampleRec__Sampl__5629CD9C");
        });

        modelBuilder.Entity<Samples>(entity =>
        {
            entity.HasKey(e => e.SampleID).HasName("PK__Samples__8B99EC0AC9FCEEFD");

            entity.Property(e => e.BoxName).HasMaxLength(40);
            entity.Property(e => e.Genotype).HasMaxLength(40);
            entity.Property(e => e.Refrigerator).HasMaxLength(4);
            entity.Property(e => e.SampleName).HasMaxLength(60);
            //entity.Property(e => e.SampleType).HasMaxLength(10);
            entity.Property(e => e.Species).HasMaxLength(40);

            entity.HasOne(d => d.SampleType).WithMany(p => p.Samples)
                .HasForeignKey(d => d.TypeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Samples_SampleType");
        });

        modelBuilder.Entity<SampleType>(entity =>
        {
            entity.HasKey(e => e.TypeID).HasName("PK__SampleTy__516F03951D96D9ED");

            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(10);
        });


        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierID).HasName("PK__Supplier__4BE666940EFEB76B");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(20);
            entity.Property(e => e.ContactTel).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
