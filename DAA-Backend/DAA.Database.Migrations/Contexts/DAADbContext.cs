using DAA.Constants.Databases;
using DAA.Database.Models;
using DAA.Database.Models.DataTables;
using DAA.Database.Models.Platforms;
using DAA.Database.Models.VideoGames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAA.Database.Migrations.Contexts
{
    public class DAADbContext : DbContext
    {
        #region Atributos públicos.
        public DbSet<DatatablesRecord> DatatablesRecords { get; set; }
        public DbSet<DatatablesTable> DatatablesTables { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<VideoGame> VideoGames { get; set; }
        #endregion

        #region Atributos privados.
        private string dbConnection { get; set; }
        #endregion

        #region Constructores.
        public DAADbContext()
        {
            // Nombre del fichero de configuración.
            string databaseConfiguration = string.Format(@"{0}\Settings\databaseSettings.json", System.IO.Directory.GetCurrentDirectory());

            // Generamos la configuración para leer el fichero.
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile(databaseConfiguration);
            IConfigurationRoot config = builder.Build();

            // Leemos la cadena de conexión.
            this.dbConnection = config.GetSection("DAADbConnection").Value;
        }

        public DAADbContext(DbContextOptions<DAADbContext> options) : base(options)
        { }
        #endregion

        #region Métodos y funciones sobreescritas.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(this.dbConnection) == false)
            {
                // Si al crear la instancia del DbContext no se ha proporcionado el parámetro "DbContextOptions" lo proporcionamos ahora.
                optionsBuilder.UseSqlServer(this.dbConnection);
            }
        }

        public override int SaveChanges()
        {
            // Buscamos los registros de tipo "BaseAuditable" que han sido creados o actualizados.
            IEnumerable<EntityEntry> entries = base.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            // Actualizamos los campos auditables de los registros obtenidos.
            foreach (var entityEntry in entries)
            {
                ((BaseAuditable)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseAuditable)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            // Guardamos los registros en base de datos.
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Tablas.
            this.OnTableCreating_DatatablesRecords(modelBuilder);
            this.OnTableCreating_DatatablesTables(modelBuilder);
            this.OnTableCreating_Platforms(modelBuilder);
            this.OnTableCreating_VideoGames(modelBuilder);
            #endregion
        }
        #endregion

        #region Configuración de las tablas.
        /// <summary>
        /// Inicializa la tabla "DATATABLES_RECORDS".
        /// </summary>
        /// <param name="modelBuilder">Parámetro necesario para configurar la tabla.</param>
        private void OnTableCreating_DatatablesRecords(ModelBuilder modelBuilder)
        {
            // TODO: ...
        }

        /// <summary>
        /// Inicializa la tabla "DATATABLES_TABLES".
        /// </summary>
        /// <param name="modelBuilder">Parámetro necesario para configurar la tabla.</param>
        private void OnTableCreating_DatatablesTables(ModelBuilder modelBuilder)
        {
            // Nombre de la tabla.
            modelBuilder.Entity<DatatablesTable>().ToTable(DbTablesValues.DatatablesTables.TABLE_NAME);

            // Clave primaria.
            modelBuilder.Entity<DatatablesTable>().HasKey(x => x.Id);

            // Claves foraneas.
            modelBuilder.Entity<DatatablesTable>()
                .HasMany(x => x.DatatablesRecords)
                .WithOne(x => x.DatatablesTable)
                .HasForeignKey(x => x.DataTablesTableId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices.
            modelBuilder.Entity<DatatablesTable>().HasIndex(x => x.Id);
            modelBuilder.Entity<DatatablesTable>().HasIndex(x => x.Name);
            modelBuilder.Entity<DatatablesTable>().HasIndex(x => x.Code).IsUnique();
            modelBuilder.Entity<DatatablesTable>().HasIndex(x => x.Reference);

            // Mapeo de campos.
            modelBuilder.Entity<DatatablesTable>()
                .Property<int>(x => x.Id)
                .HasColumnName(DbTablesValues.DatatablesTables.ID);
            modelBuilder.Entity<DatatablesTable>()
                .Property<string>(x => x.Code)
                .HasColumnName(DbTablesValues.DatatablesTables.CODE)
                .HasMaxLength(DbTablesValues.DatatablesTables.CODE_LENGTH)
                .IsRequired(DbTablesValues.DatatablesTables.CODE_REQUIRED);
            modelBuilder.Entity<DatatablesTable>()
                .Property<string>(x => x.Name)
                .HasColumnName(DbTablesValues.DatatablesTables.NAME)
                .HasMaxLength(DbTablesValues.DatatablesTables.NAME_LENGTH)
                .IsRequired(DbTablesValues.DatatablesTables.NAME_REQUIRED);
            modelBuilder.Entity<DatatablesTable>()
                .Property<string>(x => x.Description)
                .HasColumnName(DbTablesValues.DatatablesTables.DESCRIPTION)
                .IsRequired(DbTablesValues.DatatablesTables.DESCRIPTION_REQUIRED)
                .HasColumnType(DbTablesValues.DatatablesTables.DESCRIPTION_TYPE);
            modelBuilder.Entity<DatatablesTable>()
                .Property<string>(x => x.Reference)
                .HasColumnName(DbTablesValues.DatatablesTables.REFERENCE)
                .HasMaxLength(DbTablesValues.DatatablesTables.REFERENCE_LENGTH)
                .IsRequired(DbTablesValues.DatatablesTables.REFERENCE_REQUIRED);
            modelBuilder.Entity<DatatablesTable>()
                .Property<DateTime>(x => x.CreatedAt)
                .HasColumnName(DbFieldsValues.AuditableFields.CREATED_AT)
                .HasDefaultValueSql(DbFieldsValues.AuditableFields.CREATED_AT_DEFAULT_VALUE);
            modelBuilder.Entity<DatatablesTable>()
                .Property<DateTime>(x => x.UpdatedAt)
                .HasColumnName(DbFieldsValues.AuditableFields.UPDATED_AT)
                .HasDefaultValueSql(DbFieldsValues.AuditableFields.UPDATED_AT_DEFAULT_VALUE);
        }

        /// <summary>
        /// Inicializa la tabla "PLATFORMS".
        /// </summary>
        /// <param name="modelBuilder">Parámetro necesario para configurar la tabla.</param>
        private void OnTableCreating_Platforms(ModelBuilder modelBuilder)
        {
            // TODO: ...
        }

        /// <summary>
        /// Inicializa la tabla "VIDEO_GAMES".
        /// </summary>
        /// <param name="modelBuilder">Parámetro necesario para configurar la tabla.</param>
        private void OnTableCreating_VideoGames(ModelBuilder modelBuilder)
        {
            // TODO: ...
        }
        #endregion
    }
}
