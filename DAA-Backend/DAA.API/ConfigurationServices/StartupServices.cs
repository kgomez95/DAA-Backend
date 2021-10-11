using DAA.Database.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAA.API.ConfigurationServices
{
    public class StartupServices
    {
        #region Atributos privados.
        private readonly IServiceCollection _services;
        private IConfigurationRoot databaseSettings { get; set; }
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
            this.databaseSettings = builder.Build();
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
                    .UseSqlServer(this.databaseSettings.GetSection("DAADbConnection").Value)
                );
            return this;
        }
        #endregion
    }
}
