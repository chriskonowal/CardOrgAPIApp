using System;
using CardOrgAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CardOrgAPI.Contexts
{
    public partial class CardOrgContext : DbContext
    {
        public CardOrgContext()
        {
        }

        public CardOrgContext(DbContextOptions<CardOrgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<GradeCompany> GradeCompanies { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerCard> PlayerCards { get; set; }
        public virtual DbSet<SearchSort> SearchSorts { get; set; }
        public virtual DbSet<SearchSortGradeCompany> SearchSortGradeCompanies { get; set; }
        public virtual DbSet<SearchSortGradeLocation> SearchSortGradeLocations { get; set; }
        public virtual DbSet<SearchSortPlayer> SearchSortPlayers { get; set; }
        public virtual DbSet<SearchSortSet> SearchSortSets { get; set; }
        public virtual DbSet<SearchSortSport> SearchSortSports { get; set; }
        public virtual DbSet<SearchSortTeam> SearchSortTeams { get; set; }
        public virtual DbSet<SearchSortYear> SearchSortYears { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamCard> TeamCards { get; set; }
        public virtual DbSet<Year> Years { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CardOrg;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

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
                    .HasConstraintName("FK_Card_GradeCompanyId");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Card_LocationId");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.SetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Card_SetId");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Card_SportId");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Card_YearId");
            });

            modelBuilder.Entity<GradeCompany>(entity =>
            {
                entity.ToTable("GradeCompany");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<PlayerCard>(entity =>
            {
                entity.ToTable("PlayerCard");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.PlayerCards)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PlayerCard_CardId");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerCards)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PlayerCard_PlayerId");
            });

            modelBuilder.Entity<SearchSort>(entity =>
            {
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

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeStampEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStampStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<SearchSortGradeCompany>(entity =>
            {
                entity.ToTable("SearchSortGradeCompany");

                entity.HasOne(d => d.GradeCompany)
                    .WithMany(p => p.SearchSortGradeCompanies)
                    .HasForeignKey(d => d.GradeCompanyId)
                    .HasConstraintName("FK_SearchSortGradeCompany_GradeCompanyId");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortGradeCompanies)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortGradeCompany_SearchSortId");
            });

            modelBuilder.Entity<SearchSortGradeLocation>(entity =>
            {
                entity.ToTable("SearchSortGradeLocation");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.SearchSortGradeLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_SearchSortGradeLocation_LocationId");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortGradeLocations)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortGradeLocation_SearchSortId");
            });

            modelBuilder.Entity<SearchSortPlayer>(entity =>
            {
                entity.ToTable("SearchSortPlayer");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SearchSortPlayers)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_SearchSortPlayer_PlayerId");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortPlayers)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortPlayer_SearchSortId");
            });

            modelBuilder.Entity<SearchSortSet>(entity =>
            {
                entity.ToTable("SearchSortSet");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortSets)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortSet_SearchSortId");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.SearchSortSets)
                    .HasForeignKey(d => d.SetId)
                    .HasConstraintName("FK_SearchSortSet_SetId");
            });

            modelBuilder.Entity<SearchSortSport>(entity =>
            {
                entity.ToTable("SearchSortSport");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortSports)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortSport_SearchSortId");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.SearchSortSports)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK_SearchSortSport_SportId");
            });

            modelBuilder.Entity<SearchSortTeam>(entity =>
            {
                entity.ToTable("SearchSortTeam");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortTeams)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortTeam_SearchSortId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.SearchSortTeams)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_SearchSortTeam_TeamId");
            });

            modelBuilder.Entity<SearchSortYear>(entity =>
            {
                entity.ToTable("SearchSortYear");

                entity.HasOne(d => d.SearchSort)
                    .WithMany(p => p.SearchSortYears)
                    .HasForeignKey(d => d.SearchSortId)
                    .HasConstraintName("FK_SearchSortYear_SearchSortId");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.SearchSortYears)
                    .HasForeignKey(d => d.YearId)
                    .HasConstraintName("FK_SearchSortYear_YearId");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("Sport");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(254);
            });

            modelBuilder.Entity<TeamCard>(entity =>
            {
                entity.ToTable("TeamCard");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TeamCards)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_TeamCards_CardId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCards)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_TeamCards_TeamId");
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.ToTable("Year");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
