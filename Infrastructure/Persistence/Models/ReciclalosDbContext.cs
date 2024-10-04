using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Infrastructure.Persistence.Models;

public partial class ReciclalosDbContext : DbContext
{
    public ReciclalosDbContext()
    {
    }

    public ReciclalosDbContext(DbContextOptions<ReciclalosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aliado> Aliados { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDeviceLogin> UserDeviceLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Aliado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("aliados")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasColumnType("tinyint(4)")
                .HasColumnName("position");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("answers");

            entity.HasIndex(e => e.QuestionId, "question_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionId)
                .HasColumnType("int(11)")
                .HasColumnName("question_id");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("answers_ibfk_1");
        });

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("benefits")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint(9)")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasColumnType("tinytext")
                .HasColumnName("address");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Finish).HasColumnName("finish");
            entity.Property(e => e.IdPartner)
                .HasColumnType("smallint(6)")
                .HasColumnName("id_partner");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'P'")
                .IsFixedLength()
                .HasComment("Pendiente,Aprobado")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("categories")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("tinyint(4)")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(16)
                .HasColumnName("color");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("collections")
                .UseCollation("utf8_unicode_ci");

            entity.HasIndex(e => e.IdUser, "user_coleccion_fk");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint(9)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("date_updated");
            entity.Property(e => e.IdLocation)
                .HasColumnType("smallint(6)")
                .HasColumnName("id_location");
            entity.Property(e => e.IdReciclador)
                .HasColumnType("int(11)")
                .HasColumnName("id_reciclador");
            entity.Property(e => e.IdUser)
                .HasColumnType("smallint(6)")
                .HasColumnName("id_user");
            entity.Property(e => e.Latas)
                .HasPrecision(10, 3)
                .HasColumnName("latas");
            entity.Property(e => e.Papel)
                .HasPrecision(10, 3)
                .HasColumnName("papel");
            entity.Property(e => e.PlasticoOtros)
                .HasPrecision(10, 3)
                .HasColumnName("plastico_otros");
            entity.Property(e => e.PlasticoPet)
                .HasPrecision(10, 3)
                .HasColumnName("plastico_pet");
            entity.Property(e => e.Tetrapak)
                .HasPrecision(10, 3)
                .HasColumnName("tetrapak");
            entity.Property(e => e.Vidrio)
                .HasPrecision(10, 3)
                .HasColumnName("vidrio");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_coleccion_fk");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departamentos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("downloads")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .HasColumnName("file");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasColumnType("smallint(6)")
                .HasColumnName("position");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("events")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("smallint(6)")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasColumnType("tinytext")
                .HasColumnName("address");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Finish)
                .HasColumnType("datetime")
                .HasColumnName("finish");
            entity.Property(e => e.IdCategories)
                .HasMaxLength(48)
                .HasColumnName("id_categories");
            entity.Property(e => e.IdPartners)
                .HasMaxLength(100)
                .HasColumnName("id_partners");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Materials)
                .HasMaxLength(255)
                .HasColumnName("materials");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(32)
                .HasColumnName("phone");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .HasDefaultValueSql("'E'")
                .IsFixedLength()
                .HasComment("[E]vent, [R]ecycle")
                .HasColumnName("type");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
            entity.Property(e => e.Waze)
                .HasMaxLength(255)
                .HasComment("waze url")
                .HasColumnName("waze");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(32)
                .HasColumnName("whatsapp");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("goals")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("float(12,2)")
                .HasColumnName("amount");
            entity.Property(e => e.Created)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Finish).HasColumnName("finish");
            entity.Property(e => e.IdPartner)
                .HasColumnType("int(11)")
                .HasColumnName("id_partner");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.Visible)
                .HasDefaultValueSql("'1'")
                .HasColumnName("visible");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("locations")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("smallint(6)")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasColumnType("tinytext")
                .HasColumnName("address");
            entity.Property(e => e.Aprobado)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("aprobado");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Departamento)
                .HasMaxLength(64)
                .HasColumnName("departamento");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Estacion)
                .HasMaxLength(10)
                .HasColumnName("estacion");
            entity.Property(e => e.IdPartners)
                .HasMaxLength(255)
                .HasColumnName("id_partners");
            entity.Property(e => e.IdUser)
                .HasColumnType("int(11)")
                .HasColumnName("id_user");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Latlng)
                .HasMaxLength(100)
                .HasColumnName("latlng");
            entity.Property(e => e.Materials)
                .HasMaxLength(255)
                .HasColumnName("materials");
            entity.Property(e => e.Municipio)
                .HasMaxLength(64)
                .HasColumnName("municipio");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .HasColumnName("phone2");
            entity.Property(e => e.Schedule)
                .HasColumnType("tinytext")
                .HasColumnName("schedule");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(24)
                .HasColumnName("whatsapp");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("material");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasColumnName("color");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("municipios");

            entity.HasIndex(e => e.DepartamentoId, "departamento_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DepartamentoId)
                .HasColumnType("int(11)")
                .HasColumnName("departamento_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("municipios_ibfk_1");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("partners")
                .UseCollation("utf8_unicode_ci");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("smallint(11)")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Created)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.IdLocations)
                .HasMaxLength(255)
                .HasColumnName("id_locations");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("questions");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Question1)
                .HasColumnType("text")
                .HasColumnName("question");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sliders")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("smallint(11)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Image)
                .HasMaxLength(128)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(96)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasColumnType("tinyint(4)")
                .HasColumnName("position");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("smallint(6)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.IdLocations)
                .HasMaxLength(255)
                .HasColumnName("id_locations");
            entity.Property(e => e.IdUsers)
                .HasComment("usuarios acopiadores")
                .HasColumnType("text")
                .HasColumnName("id_users");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'ACTIVE'")
                .HasColumnType("enum('ACTIVE','INACTIVE','PENDING')")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .HasDefaultValueSql("'U'")
                .IsFixedLength()
                .HasComment("Admin-User")
                .HasColumnName("type");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserDeviceLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_device_login");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AppVersion)
                .HasMaxLength(10)
                .HasColumnName("app_version");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Device)
                .HasMaxLength(100)
                .HasColumnName("device");
            entity.Property(e => e.Os)
                .HasMaxLength(50)
                .HasColumnName("os");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
