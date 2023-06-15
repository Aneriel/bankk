using Microsoft.EntityFrameworkCore;

namespace bankk;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LoginAttempt> LoginAttempts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bank;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A619A20C9D");

            entity.Property(e => e.AccountId).IsUnicode(false);
            entity.Property(e => e.AccountNumber).IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Accounts__Custom__29572725");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B809193E4D");

            entity.Property(e => e.CustomerId)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FirstName).IsUnicode(false);
            entity.Property(e => e.LastName).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
        });

        modelBuilder.Entity<LoginAttempt>(entity =>
        {
            entity.HasKey(e => e.AttemptId).HasName("PK__LoginAtt__891A68E60D19B50F");

            entity.Property(e => e.AttemptId).ValueGeneratedNever();
            entity.Property(e => e.AttemptDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.LoginAttempts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__LoginAtte__Custo__2C3393D0");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BCC3B6D9C");

            entity.Property(e => e.TransactionId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType).IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Transacti__Accou__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
