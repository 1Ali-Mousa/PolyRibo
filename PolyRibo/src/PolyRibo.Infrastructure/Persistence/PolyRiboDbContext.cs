using PolyRibo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PolyRibo.Infrastructure.Persistence;

public partial class PolyRiboDbContext : DbContext
{
    public PolyRiboDbContext(DbContextOptions<PolyRiboDbContext> options)
        : base(options)
    {
    }

    public DbSet<ArchitectureType> ArchitectureTypes { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<DataAccessTechnology> DataAccessTechnologies { get; set; }

    public DbSet<DataAccessesOfProject> DataAccessesOfProjects { get; set; }

    public DbSet<Database> Databases { get; set; }

    public DbSet<DatabaseColumn> DatabaseColumns { get; set; }

    public DbSet<DatabaseTable> DatabaseTables { get; set; }

    public DbSet<DatabaseType> DatabaseTypes { get; set; }

    public DbSet<DockerConfiguration> DockerConfigurations { get; set; }

    public DbSet<FrameworkType> FrameworkTypes { get; set; }

    public DbSet<GitConfiguration> GitConfigurations { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<Package> Packages { get; set; }

    public DbSet<PackagesOfProject> PackagesOfProjects { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchitectureType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Architec__3214EC270E82BBDA");

            entity.HasIndex(e => e.Name, "UQ__Architec__737584F6E670ECD8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC27E6F39027");

            entity.HasIndex(e => e.Code, "UQ__Countrie__A25C5AA70E77C0A7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DataAccessTechnology>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataAcce__3214EC278C0658CA");

            entity.HasIndex(e => e.Name, "UQ__DataAcce__737584F68CAD58EC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DataAccessesOfProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DataAcce__3214EC27F8CE4CDE");

            entity.HasIndex(e => new { e.ProjectId, e.DataAccessTechnologyId }, "UQ_DataAccessesOfProjects").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataAccessTechnologyId).HasColumnName("DataAccessTechnologyID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.DataAccessTechnology).WithMany(p => p.DataAccessesOfProjects)
                .HasForeignKey(d => d.DataAccessTechnologyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DataAccessesOfProjects_DataAccessTechnologies");

            entity.HasOne(d => d.Project).WithMany(p => p.DataAccessesOfProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DataAccessesOfProjects_Projects");
        });

        modelBuilder.Entity<Database>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Database__3214EC271AD8E672");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BackUpFileName).HasMaxLength(255);
            entity.Property(e => e.DatabaseTypeId).HasColumnName("DatabaseTypeID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.DatabaseType).WithMany(p => p.Databases)
                .HasForeignKey(d => d.DatabaseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Databases_DatabaseTypes");
        });

        modelBuilder.Entity<DatabaseColumn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Database__3214EC273EBF0A12");

            entity.HasIndex(e => new { e.DatabaseTableId, e.Name }, "UQ_DatabaseColumns_Table_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataType).HasMaxLength(100);
            entity.Property(e => e.DatabaseTableId).HasColumnName("DatabaseTableID");
            entity.Property(e => e.FkcolumnId).HasColumnName("FKColumnID");
            entity.Property(e => e.IsNullable).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.DatabaseTable).WithMany(p => p.DatabaseColumns)
                .HasForeignKey(d => d.DatabaseTableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DatabaseColumns_DatabaseTables");

            entity.HasOne(d => d.Fkcolumn).WithMany(p => p.InverseFkcolumn)
                .HasForeignKey(d => d.FkcolumnId)
                .HasConstraintName("FK_DatabaseColumns_FKColumn");
        });

        modelBuilder.Entity<DatabaseTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Database__3214EC27E93F1944");

            entity.HasIndex(e => new { e.DatabaseId, e.SchemaName, e.Name }, "UQ_DatabaseTables_Database_Schema_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DatabaseId).HasColumnName("DatabaseID");
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.SchemaName)
                .HasMaxLength(128)
                .HasDefaultValue("dbo");

            entity.HasOne(d => d.Database).WithMany(p => p.DatabaseTables)
                .HasForeignKey(d => d.DatabaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DatabaseTables_Databases");
        });

        modelBuilder.Entity<DatabaseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Database__3214EC2791BB097B");

            entity.HasIndex(e => e.Name, "UQ__Database__737584F6D6D56DB2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DockerConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DockerCo__3214EC27A3C4F877");

            entity.HasIndex(e => e.ProjectId, "UQ__DockerCo__761ABED179FF7831").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithOne(p => p.DockerConfiguration)
                .HasForeignKey<DockerConfiguration>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DockerConfigurations_Projects");
        });

        modelBuilder.Entity<FrameworkType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Framewor__3214EC27F6E271AF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<GitConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GitConfi__3214EC275866C4A3");

            entity.HasIndex(e => e.ProjectId, "UQ__GitConfi__761ABED1C587F056").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DefaultBranch).HasMaxLength(100);
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.RemoteUrl).HasMaxLength(500);

            entity.HasOne(d => d.Project).WithOne(p => p.GitConfiguration)
                .HasForeignKey<GitConfiguration>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GitConfigurations_Projects");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC27592FF133");

            entity.HasIndex(e => e.Name, "UQ__Language__737584F6D4D42FCF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC2758BB113B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<PackagesOfProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC27DE9BFBDF");

            entity.HasIndex(e => new { e.ProjectId, e.PackageId }, "UQ_PackagesOfProjects").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Package).WithMany(p => p.PackagesOfProjects)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackagesOfProjects_Packages");

            entity.HasOne(d => d.Project).WithMany(p => p.PackagesOfProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackagesOfProjects_Projects");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC272912577E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ArchitectureTypeId).HasColumnName("ArchitectureTypeID");
            entity.Property(e => e.CreateAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DatabaseId).HasColumnName("DatabaseID");
            entity.Property(e => e.FrameworkTypeId).HasColumnName("FrameworkTypeID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ArchitectureType).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ArchitectureTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_ArchitectureTypes");

            entity.HasOne(d => d.Database).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DatabaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Databases");

            entity.HasOne(d => d.FrameworkType).WithMany(p => p.Projects)
                .HasForeignKey(d => d.FrameworkTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_FrameworkTypes");

            entity.HasOne(d => d.Language).WithMany(p => p.Projects)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Languages");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC273FD9216D");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534891E6E79").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456FC756CA2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHashed).HasMaxLength(500);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Countries");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
