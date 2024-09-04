﻿[assembly: System.CLSCompliant(false)]

namespace EisaAwards.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Descendant class of <see cref="DbContext"/> fit for the project's purpose.
    /// </summary>
    public partial class EisaAwardsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EisaAwardsDbContext"/> class.
        /// </summary>
        public EisaAwardsDbContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Brand"/>.
        /// </summary>
        public virtual DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Product"/>.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="ExpertGroup"/>.
        /// </summary>
        public virtual DbSet<ExpertGroup> ExpertGroups { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Member"/>.
        /// </summary>
        public virtual DbSet<Member> Members { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Country"/>.
        /// </summary>
        public virtual DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Configures the DB.
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/> parameter instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), nameof(this.OnConfiguring) + " must not take a null parameter!");
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\EisaAwardsDb.mdf; Integrated Security=True; MultipleActiveResultSets = true");
            }
        }

        /// <summary>
        /// Create the configured database.
        /// </summary>
        /// <param name="modelBuilder"> <see cref="ModelBuilder"/> parameter instance. </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder), nameof(this.OnModelCreating) + " must not take a null parameter!");
            }

            modelBuilder.Entity<ExpertGroup>(entity =>
            {
                entity.HasKey(expertgroup => expertgroup.ExpertGroupID);
                entity.Property(expertgroup => expertgroup.Name).HasMaxLength(64).IsRequired();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(member => member.MemberID);
                entity.Property(member => member.Name).HasMaxLength(64).IsRequired();
                entity.Property(member => member.Website).HasMaxLength(128).IsRequired();
                entity.Property(member => member.OfficeLocation).HasMaxLength(256).IsRequired();
                entity.Property(member => member.Publisher).HasMaxLength(128).IsRequired();
                entity.Property(member => member.ChiefEditor).HasMaxLength(64).IsRequired();
                entity.Property(member => member.PhoneNumber).HasMaxLength(32).IsRequired();

                entity.HasOne(member => member.ExpertGroup)
                    .WithMany(expertgroup => expertgroup.Members)
                    .HasForeignKey(member => member.ExpertGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(member => member.Country)
                    .WithMany(country => country.Members)
                    .HasForeignKey(member => member.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(country => country.CountryID);
                entity.Property(country => country.Name).HasMaxLength(32).IsRequired();
                entity.Property(country => country.CapitalCity).HasMaxLength(32).IsRequired();
                entity.Property(country => country.CallingCode).IsRequired();
                entity.Property(country => country.PPPperCapita).IsRequired();
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(manufacturer => manufacturer.BrandId);
                entity.Property(manufacturer => manufacturer.Name).HasMaxLength(32).IsRequired();
                entity.Property(manufacturer => manufacturer.Address).HasMaxLength(128);
                entity.Property(manufacturer => manufacturer.Homepage).HasMaxLength(64).IsRequired();

                entity.HasOne(brand => brand.Country)
                    .WithMany(country => country.Brands)
                    .HasForeignKey(manufacturer => manufacturer.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.ProductID);
                entity.Property(product => product.Name).HasMaxLength(64).IsRequired();
                entity.Property(product => product.Price).IsRequired();
                entity.Property(product => product.Category).HasMaxLength(128).IsRequired();
                entity.Property(product => product.LaunchDate).IsRequired();
                entity.Property(product => product.EstimatedLifetime).IsRequired();

                entity.HasOne(product => product.Brand)
                    .WithMany(manufacturer => manufacturer.Products)
                    .HasForeignKey(product => product.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(product => product.ExpertGroup)
                    .WithMany(expertgroup => expertgroup.Products)
                    .HasForeignKey(product => product.ExpertGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Brand>().HasData(DataSeeder.Brands);
            modelBuilder.Entity<Country>().HasData(DataSeeder.Countries);
            modelBuilder.Entity<ExpertGroup>().HasData(DataSeeder.ExpertGroups);
            modelBuilder.Entity<Member>().HasData(DataSeeder.Members);
            modelBuilder.Entity<Product>().HasData(DataSeeder.Products);
        }
    }
}
