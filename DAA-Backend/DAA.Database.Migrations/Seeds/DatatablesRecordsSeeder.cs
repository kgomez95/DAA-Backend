using DAA.Constants.Databases;
using DAA.Database.Models.DataTables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAA.Database.Migrations.Seeds
{
    public class DatatablesRecordsSeeder : BaseSeeder
    {
        public DatatablesRecordsSeeder(IConfigurationRoot seedsConfig) : base(seedsConfig)
        { }

        /// <summary>
        /// Ejecuta las semillas de la tabla "DATATABLES_RECORDS".
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
                        // Comprobamos que exista el DataTablesTable.
                        DatatablesTable datatablesTable = base._databaseContext.DatatablesTables.FirstOrDefault(x => x.Code == values["DatatablesTableCode"]);
                        if (datatablesTable == null)
                        {
                            throw new Exception(string.Format("¡El DataTablesTable '{0}' no existe!", values["DatatablesTableCode"]));
                        }

                        // Comprobamos si el registro ya existe en base de datos.
                        DatatablesRecord datatablesRecord = base._databaseContext.DatatablesRecords.FirstOrDefault(x => x.Code == values["Code"] && x.DataTablesTableId == datatablesTable.Id);

                        if (datatablesRecord == null)
                        {
                            // Si el registro no existe entonces lo añadimos.
                            datatablesRecord = new DatatablesRecord()
                            {
                                Code = values["Code"],
                                Name = values["Name"],
                                Type = Convert.ToInt32(values["Type"]),
                                Order = Convert.ToInt32(values["Order"]),
                                HasFilter = Convert.ToBoolean(values["HasFilter"]),
                                IsBasic = Convert.ToBoolean(values["IsBasic"]),
                                IsRange = Convert.ToBoolean(values["IsRange"]),
                                DefaultValue = values["DefaultValue"],
                                DefaultFrom = values["DefaultFrom"],
                                DefaultTo = values["DefaultTo"],
                                DataTablesTableId = datatablesTable.Id
                            };
                            base._databaseContext.DatatablesRecords.Add(datatablesRecord);
                        }
                        else
                        {
                            // Si el registro existe entonces lo actualizamos.
                            datatablesRecord.Name = values["Name"];
                            datatablesRecord.Type = Convert.ToInt32(values["Type"]);
                            datatablesRecord.Order = Convert.ToInt32(values["Order"]);
                            datatablesRecord.HasFilter = Convert.ToBoolean(values["HasFilter"]);
                            datatablesRecord.IsBasic = Convert.ToBoolean(values["IsBasic"]);
                            datatablesRecord.IsRange = Convert.ToBoolean(values["IsRange"]);
                            datatablesRecord.DefaultValue = values["DefaultValue"];
                            datatablesRecord.DefaultFrom = values["DefaultFrom"];
                            datatablesRecord.DefaultTo = values["DefaultTo"];
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
                base.ExecuteSeeds(this._seedsConfig.GetSection("DatatablesRecordsFile").Value, anonym);
                #endregion
            }
            else
            {
                // Eliminamos todos los registros de la tabla.
                base.DeleteContentTable(DbTablesValues.DatatablesRecords.TABLE_NAME);
            }

            // Aplicamos los cambios.
            base.SaveChanges();
        }
    }
}
