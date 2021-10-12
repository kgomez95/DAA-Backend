#region Usings.
using AutoMapper;
using DAA.API.Mappings;
using DAA.Database.DAO.Definitions.Datatables;
using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.Migrations.Contexts;
using DAA.Database.Services.Definitions.Datatables;
using DAA.Database.Services.Interfaces.Datatables;
using DAA.Database.ServicesDTO.Definitions.Datatables;
using DAA.Database.ServicesDTO.Interfaces.Datatables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace DAA.API.ConfigurationServices
{
    public class StartupServices
    {
        #region Atributos privados.
        private readonly IServiceCollection _services;
        private IConfigurationRoot DatabaseSettings { get; set; }
        #endregion

        #region Constructores.
        public StartupServices(IServiceCollection services)
        {
            this._services = services;
            this.LoadDatabaseSettings();
        }
        #endregion

        #region Métodos y funciones privadas.
        /// <summary>
        /// Carga la configuración de la base de datos.
        /// </summary>
        private void LoadDatabaseSettings()
        {
            // Ruta del fichero de configuración.
            string databaseConfiguration = string.Format(@"{0}\Settings\databaseSettings.json", System.IO.Directory.GetCurrentDirectory());

            // Generamos la configuración para leer el fichero.
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile(databaseConfiguration);
            this.DatabaseSettings = builder.Build();
        }

        /// <summary>
        /// Añade los servicios para los controladores.
        /// </summary>
        /// <returns></returns>
        public StartupServices AddControllers()
        {
            this._services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Loop Include Fixed
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });
            return this;
        }

        /// <summary>
        /// Inicializa el DbContext de la base de datos.
        /// </summary>
        /// <returns></returns>
        public StartupServices Database_InitializeContext()
        {
            this._services.AddDbContext<DAADbContext>(options =>
                options
                    .UseSqlServer(this.DatabaseSettings.GetSection("DAADbConnection").Value)
                );
            return this;
        }

        /// <summary>
        /// Inicializa los DAO de la aplicación.
        /// </summary>
        /// <returns></returns>
        public StartupServices DAO_Initialize()
        {
            this._services.AddScoped<IDatatablesTableDAO, DatatablesTableDAO>();
            this._services.AddScoped<IDatatablesRecordDAO, DatatablesRecordDAO>();
            // NOTE: Ir añadiendo aquí los nuevos DAO.

            return this;
        }

        /// <summary>
        /// Inicializa los servicios de base de datos de la aplicación.
        /// </summary>
        /// <returns></returns>
        public StartupServices DatabaseServices_Initialize()
        {
            this._services.AddScoped<IDatatablesRecordsService, DatatablesRecordsService>();
            // NOTE: Ir añadiendo aquí los nuevos servicios.

            return this;
        }

        /// <summary>
        /// Inicializa los servicios DTO de base de datos de la aplicación.
        /// </summary>
        /// <returns></returns>
        public StartupServices DatabaseServicesDTO_Initialize()
        {
            this._services.AddScoped<IDatatablesRecordsServiceDTO, DatatablesRecordsServiceDTO>();
            // NOTE: Ir añadiendo aquí los nuevos servicios DTO.

            return this;
        }

        /// <summary>
        /// Añade el servicio de Automapper.
        /// </summary>
        /// <returns></returns>
        public StartupServices Automapper()
        {
            // Configuración del AutoMapper.
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            this._services.AddSingleton(mapper);

            return this;
        }
        #endregion
    }
}
