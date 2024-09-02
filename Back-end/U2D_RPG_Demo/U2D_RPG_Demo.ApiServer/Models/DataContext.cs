using Microsoft.EntityFrameworkCore;

namespace U2D_RPG_Demo.ApiServer.Models;

public partial class DataContext : DbContext {
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }


    public virtual DbSet<PlayerAttributes> PlayerAttributes { get; set; }

    public virtual DbSet<UserInfo> UserInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<PlayerAttributes>(entity => {
            entity.HasKey(e => e.PAID).HasName("PK__PlayerAt__5986FD6DB9B6F188");

            entity.Property(e => e.PAID).HasColumnName("PAID");
            entity.Property(e => e.ATK)
                .HasDefaultValue(5)
                .HasColumnName("ATK");
            entity.Property(e => e.DEF)
                .HasDefaultValue(2)
                .HasColumnName("DEF");
            entity.Property(e => e.DR)
                .HasDefaultValue(0.0)
                .HasColumnName("DR");
            entity.Property(e => e.Experience).HasDefaultValue(0);
            entity.Property(e => e.FormattedPAID)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasComputedColumnSql("(right('00000000'+CONVERT([varchar](8),[PAID]),(8)))", true)
                .HasColumnName("Formatted_PAID");
            entity.Property(e => e.Hp)
                .HasDefaultValue(100)
                .HasColumnName("HP");
            entity.Property(e => e.Level).HasDefaultValue(1);
            entity.Property(e => e.MaxHp)
                .HasDefaultValue(100)
                .HasColumnName("MaxHP");
            entity.Property(e => e.MaxMp)
                .HasDefaultValue(100)
                .HasColumnName("MaxMP");
            entity.Property(e => e.Mp)
                .HasDefaultValue(100)
                .HasColumnName("MP");
            entity.Property(e => e.SPD)
                .HasDefaultValue(5)
                .HasColumnName("SPD");
            entity.Property(e => e.SPDMult)
                .HasDefaultValue(0.0)
                .HasColumnName("SPD_MULT");
            entity.Property(e => e.UID).HasColumnName("UID");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.PlayerAttributes)
                .HasForeignKey(d => d.UID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerAttri__UID__40F9A68C");
        });

        modelBuilder.Entity<UserInfo>(entity => {
            entity.HasKey(e => e.UID).HasName("PK__UserInfo__C5B196020D417FD6");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UID).HasColumnName("UID");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeleteTime).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.LastUpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(18)
                .HasDefaultValue("nobody");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasDefaultValue("Aa123456");
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}