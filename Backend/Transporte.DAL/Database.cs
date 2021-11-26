using Microsoft.EntityFrameworkCore;
using Transporte.DAL.Models;
using ActionModel = Transporte.DAL.Models.Action;

namespace Transporte.DAL
{
    public partial class Database : DbContext
    {
        private string connectionString { get; }

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionModel> Action { get; set; }
        public virtual DbSet<AppKey> AppKey { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<BrandModel> BrandModel { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseType> ExpenseType { get; set; }
        public virtual DbSet<Pilot> Pilot { get; set; }
        public virtual DbSet<RegistrationType> RegistrationType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleAction> RoleAction { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionDailyResume> TransactionDailyResume { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetail { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionModel>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqAction")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Icon)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("fkActionParent");
            });

            modelBuilder.Entity<AppKey>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqBrand")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Brand)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBrandVehicletype");
            });

            modelBuilder.Entity<BrandModel>(entity =>
            {
                entity.HasIndex(e => new { e.Brand, e.Name })
                    .HasName("uqBrandModel")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.BrandModel)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBrandmodelBrand");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasIndex(e => new { e.Type, e.Name })
                    .HasName("uqExpense")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkExpenseExpensetype");
            });

            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqExpenseType")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.BornDate).HasColumnType("date");

                entity.Property(e => e.CompleteName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.HiringDate).HasColumnType("date");

                entity.Property(e => e.IsMale)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RegistrationType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqRegistrationType")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqRole")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleAction>(entity =>
            {
                entity.HasKey(e => new { e.Action, e.Role })
                    .HasName("pkRoleAction");

                entity.HasOne(d => d.ActionNavigation)
                    .WithMany(p => p.RoleAction)
                    .HasForeignKey(d => d.Action)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkRoleactionAction");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.RoleAction)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkRoleactionRole");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Role })
                    .HasName("pkRoleUser");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.RoleUser)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkRoleuserRole");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.RoleUser)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkRoleuserUser");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => new { e.Type, e.Name })
                    .HasName("uqService")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkServiceServicetype");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqServiceType")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.HasOne(d => d.ExpenseNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.Expense)
                    .HasConstraintName("fkTransactionExpense");

                entity.HasOne(d => d.ServiceNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.Service)
                    .HasConstraintName("fkTransactionService");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTransactionUser");

                entity.HasOne(d => d.VehicleNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.Vehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTransactionVehicle");
            });

            modelBuilder.Entity<TransactionDailyResume>(entity =>
            {
                entity.HasKey(e => new { e.Day, e.Month, e.Year, e.Vehicle })
                    .HasName("pkTransactionDailyResume");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.VehicleNavigation)
                    .WithMany(p => p.TransactionDailyResume)
                    .HasForeignKey(d => d.Vehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTransactiondailyresumeVehicle");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.TransactionNavigation)
                    .WithMany(p => p.TransactionDetail)
                    .HasForeignKey(d => d.Transaction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTransactiondetailTransaction");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("uqUser")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CompleteName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => new { e.RegistrationType, e.Registration })
                    .HasName("uqVehicle")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Registration)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationType)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVehicleBrand");

                entity.HasOne(d => d.ModelNavigation)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.Model)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVehicleModel");

                entity.HasOne(d => d.RegistrationTypeNavigation)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.RegistrationType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVehicleRegistrsationtype");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVehicleType");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uqVehicleType")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CanExpense)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CanService)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
