using System;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CardOrgAPI.Contexts
{
    public partial class CardOrgContext : DbContext
    {
        public CardOrgContext(DbContextOptions<CardOrgContext> options )
            : base(options)
        {
        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<GradeCompany> GradeCompany { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerCard> PlayerCard { get; set; }
        public virtual DbSet<SearchSort> SearchSort { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<Sport> Sport { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamCard> TeamCard { get; set; }
        public virtual DbSet<Year> Year { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.BackCardMainImagePath)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.BackCardThumbnailImagePath)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.CardDescription)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EbayPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FrontCardMainImagePath)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.FrontCardThumbnailImagePath)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.Grade).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.HighestBeckettPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowestBeckettPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowestComcprice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LowestCOMCPrice");

                entity.Property(e => e.PricePaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.GradeCompany)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.GradeCompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cards__GradeComp__5AB9788F");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cards__LocationI__5D95E53A");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.SetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cards__SetId__6442E2C9");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cards__SportId__55F4C372");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cards__YearId__56E8E7AB");
            });

            modelBuilder.Entity<GradeCompany>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<PlayerCard>(entity =>
            {
                entity.HasOne(d => d.Card)
                    .WithMany(p => p.PlayerCards)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PlayerCar__CardI__30C33EC3");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerCards)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PlayerCar__Playe__2FCF1A8A");
            });

            modelBuilder.Entity<SearchSort>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SearchSort");

                entity.Property(e => e.EbayPriceHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EbayPriceLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GradeHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GradeLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HighestBeckettPriceHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HighestBeckettPriceLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowestBeckettPriceHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowestBeckettPriceLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowestComcpriceHigh)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LowestCOMCPriceHigh");

                entity.Property(e => e.LowestComcpriceLow)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LowestCOMCPriceLow");

                entity.Property(e => e.LowestComcpriceSort).HasColumnName("LowestCOMCPriceSort");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PricePaidHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PricePaidLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SearchSortId).ValueGeneratedOnAdd();

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeStampEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStampStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<TeamCard>(entity =>
            {
                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TeamCards)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__TeamCards__CardI__65370702");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCards)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__TeamCards__TeamI__3F115E1A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
