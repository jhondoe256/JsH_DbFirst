﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JacksSteakHouse_API.Models.DB;

public partial class JacksSteakHouseContext : DbContext
{
    public JacksSteakHouseContext()
    {
    }

    public JacksSteakHouseContext(DbContextOptions<JacksSteakHouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Gremlin> Gremlins { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8DC1C73B6");

            entity.ToTable("Customer");

            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
        });

        modelBuilder.Entity<CustomerOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Customer__C3905BCFDED35916");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerO__Custo__403A8C7D");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.CustomerOrders)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerO__MenuI__412EB0B6");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11A3BEB848");

            entity.ToTable("Employee");

            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(150);

            entity.HasOne(d => d.Location).WithMany(p => p.Employees)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Employee__Locati__398D8EEE");
        });

        modelBuilder.Entity<Gremlin>(entity =>
        {
            entity.ToTable("Gremlin");

            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3213E83F8B30BE32");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.GrandOpening).HasColumnName("grandOpening");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuItem__3213E83F27A90650");

            entity.ToTable("MenuItem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MealDescription)
                .HasMaxLength(150)
                .HasColumnName("mealDescription");
            entity.Property(e => e.MealName)
                .HasMaxLength(150)
                .HasColumnName("mealName");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
