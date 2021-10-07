using DAA.Database.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAA.Database.Migrations
{
    public abstract class BaseSeeder
    {
        protected readonly DAADbContext _databaseContext;
        protected readonly IConfigurationRoot _seedsConfig;
        protected DateTime dateTime { get; set; }

        public BaseSeeder(IConfigurationRoot seedsConfig)
        {
            this._databaseContext = new DAADbContext();
            this._seedsConfig = seedsConfig;
            this.dateTime = DateTime.Now;
        }

        /// <summary>
        /// Inserta y/o modifica las semillas de base de datos.
        /// </summary>
        /// <param name="fileName">Nombre del fichero de semillas a leer.</param>
        /// <param name="add">Función anónima a ejecutar para insertar y/o actualizar las semillas.</param>
        protected void ExecuteSeeds(string fileName, Func<Dictionary<string, string>, bool> add)
        {
            int count = 0;

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Resources\{1}", Directory.GetCurrentDirectory(), fileName)))
            {
                bool isWorking = true;
                string currentLine = string.Empty;

                // Cogemos los cabeceras del fichero.
                currentLine = sr.ReadLine();
                string[] headers = currentLine.Split(';');

                // Recorremos los valores del fichero.
                while ((currentLine = sr.ReadLine()) != null && isWorking)
                {
                    string[] values = currentLine.Split(';');
                    Dictionary<string, string> headerValues = headers.Zip(values, (x, y) => new { x, y }).ToDictionary(item => item.x, item => item.y);
                    isWorking = add(headerValues);

                    if (isWorking)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine($"Se ha procesado {count} línea/s del fichero '{fileName}' (sin contar la cabecera).");
        }

        /// <summary>
        /// Elimina todo el contenido de la tabla proporcionada.
        /// </summary>
        /// <param name="table">Nombre de la tabla a vaciar.</param>
        protected void DeleteContentTable(string table)
        {
            // Borra todo el contenido de la tabla.
            this._databaseContext.Database.ExecuteSqlRaw(string.Format("DELETE FROM {0}", table));

            // Reinicia el contador de la tabla.
            this._databaseContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ('{0}', RESEED, 0)", table));
        }

        /// <summary>
        /// Aplica los cambios realizados en base de datos.
        /// </summary>
        protected void SaveChanges()
        {
            this._databaseContext.SaveChanges();
        }

        /// <summary>
        /// Ejecuta las semillas.
        /// </summary>
        public abstract void Execute(byte action);
    }
}
