using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public partial class DemoProjeDbContext : DbContext
    {
        public DemoProjeDbContext()
        {
        }

        public DemoProjeDbContext(DbContextOptions<DemoProjeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Maintenance> Maintenance { get; set; }
        public virtual DbSet<MaintenanceHistory> MaintenanceHistory { get; set; }
        public virtual DbSet<PictureGroup> PictureGroup { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActionTypeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("actiontype_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.ActionTypeModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("actiontype_user_modifiedby_fk");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ExpectedTimeToFix).HasColumnType("date");

                entity.Property(e => e.LocationLatitude).HasMaxLength(20);

                entity.Property(e => e.LocationLongitude).HasMaxLength(20);

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.PictureGroupId)
                    .HasColumnName("PictureGroupID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ResponsibleUserId).HasColumnName("ResponsibleUserID");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.VehicleId)
                    .HasColumnName("VehicleID")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MaintenanceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("maintenance_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MaintenanceModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("maintenance_user_modifiedby_fk");

                entity.HasOne(d => d.PictureGroup)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.PictureGroupId)
                    .HasConstraintName("maintenance_picturegroup_id_fk");

                entity.HasOne(d => d.ResponsibleUser)
                    .WithMany(p => p.MaintenanceResponsibleUser)
                    .HasForeignKey(d => d.ResponsibleUserId)
                    .HasConstraintName("maintenance_user_responsibleuser_id_fk");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_status_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MaintenanceUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_user_id_fk");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_vehicle_id_fk");
            });

            modelBuilder.Entity<MaintenanceHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId)
                    .HasColumnName("ActionTypeID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.MaintenanceId)
                    .HasColumnName("MaintenanceID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(200);

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.MaintenanceHistory)
                    .HasForeignKey(d => d.ActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenancehistory_actiontype_id_fk");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MaintenanceHistoryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("maintenancehistory_user_createdby_fk");

                entity.HasOne(d => d.Maintenance)
                    .WithMany(p => p.MaintenanceHistory)
                    .HasForeignKey(d => d.MaintenanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenancehistory_maintenance_id_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MaintenanceHistoryModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("maintenancehistory_user_modifiedby_fk");
            });

            modelBuilder.Entity<PictureGroup>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PictureGroupCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("picturegroup_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.PictureGroupModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("picturegroup_user_modifiedby_fk");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StatusCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("status_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.StatusModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("status_user_modifiedby_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Profilepicture).HasColumnName("profilepicture");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.InverseModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_user_modifiedby_fk");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.PlateNo)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.VehicleTypeId)
                    .HasColumnName("VehicleTypeID")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("vehicle_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.VehicleModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("vehicle_user_modifiedby_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VehicleUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("vehicle_user_id_fk");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicle_vehicletype_id_fk");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.ModifyDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleTypeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("vehicletype_user_createdby_fk");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.VehicleTypeModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("vehicletype_user_modifiedby_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
