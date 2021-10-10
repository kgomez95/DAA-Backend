using DAA.Constants.Databases;
using DAA.Database.Models.DataTables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAA.Database.Migrations.Seeds
{
    public class DatatablesTablesSeeder : BaseSeeder
    {
        public DatatablesTablesSeeder(IConfigurationRoot seedsConfig) : base(seedsConfig)
        { }

        /// <summary>
        /// Ejecuta las semillas de la tabla "DATATABLES_TABLES".
        /// </summary>
        /// <param name="action">0: Ejecuta las semillas. 1: Borra todo el contenido de la tabla.</param>
        public override void Execute(byte action)
        {
            if (action == 0)
            {
                #region Ejecución de las semillas.
                // NOTE: Creamos una función anónima para ejecutarla en el método padre llamado "ExecuteSeeds".
                Func<Dictionary<string, string>, bool> anonym = delegate (Dictionary<string, string> values)
                {
                    bool success = false;

                    try
                    {
                        // Comprobamos si el registro ya existe en base de datos.
                        DatatablesTable datatablesTable = base._databaseContext.DatatablesTables.FirstOrDefault(x => x.Code == values["Code"]);

                        if (datatablesTable == null)
                        {
                            // Si el registro no existe entonces lo añadimos.
                            datatablesTable = new DatatablesTable()
                            {
                                Code = values["Code"].ToUpper(),
                                Name = values["Name"],
                                Description = values["Description"],
                                Reference = values["Reference"]
                            };
                            base._databaseContext.DatatablesTables.Add(datatablesTable);
                        }
                        else
                        {
                            // Si el registro existe entonces lo actualizamos.
                            datatablesTable.Name = values["Name"];
                            datatablesTable.Description = values["Description"];
                            datatablesTable.Reference = values["Reference"];
                        }

                        // Indicamos que todo ha ido correcto.
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    }

                    return success;
                };

                // Ejecutamos las semillas.
                base.ExecuteSeeds(this._seedsConfig.GetSection("DatatablesTablesFile").Value, anonym);
                #endregion
            }
            else
            {
                // Eliminamos todos los registros de la tabla.
                base.DeleteContentTable(DbTablesValues.DatatablesTables.TABLE_NAME);
            }

            // Aplicamos los cambios.
            base.SaveChanges();
        }
    }
}
