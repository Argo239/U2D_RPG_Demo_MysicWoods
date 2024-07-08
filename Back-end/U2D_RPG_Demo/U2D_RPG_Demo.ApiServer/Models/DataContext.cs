using Microsoft.EntityFrameworkCore;

namespace U2D_RPG_Demo.ApiServer.Models;

public partial class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }


    public virtual DbSet<PlayerAttribute> PlayerAttributes { get; set; }

    public virtual DbSet<UserInfo> UserInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerAttribute>(entity =>
        {
            entity.HasKey(e => e.Paid).HasName("PK__PlayerAt__5986FD6D38FACDC1");

            entity.Property(e => e.Paid).HasColumnName("PAID");
            entity.Property(e => e.Atk)
                .HasDefaultValue(5.0)
                .HasColumnName("ATK");
            entity.Property(e => e.Def)
                .HasDefaultValue(2.0)
                .HasColumnName("DEF");
            entity.Property(e => e.Dr)
                .HasDefaultValue(0.1)
                .HasColumnName("DR");
            entity.Property(e => e.Experience).HasDefaultValue(0);
            entity.Property(e => e.Hp)
                .HasDefaultValue(100.0)
                .HasColumnName("HP");
            entity.Property(e => e.Level).HasDefaultValue(1);
            entity.Property(e => e.MaxHp)
                .HasDefaultValue(100.0)
                .HasColumnName("MaxHP");
            entity.Property(e => e.MaxMp)
                .HasDefaultValue(100.0)
                .HasColumnName("MaxMP");
            entity.Property(e => e.Mp)
                .HasDefaultValue(100.0)
                .HasColumnName("MP");
            entity.Property(e => e.Spd)
                .HasDefaultValue(5.0)
                .HasColumnName("SPD");
            entity.Property(e => e.SpdMult)
                .HasDefaultValue(0.0)
                .HasColumnName("SPD_MULT");
            entity.Property(e => e.Uid).HasColumnName("UID");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.PlayerAttributes)
                .HasForeignKey(d => d.Uid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerAttri__UID__09A971A2");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__UserInfo__C5B196020D417FD6");

            entity.ToTable("UserInfo");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeleteTime).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.HasDelete)
                .HasDefaultValue(0);
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
