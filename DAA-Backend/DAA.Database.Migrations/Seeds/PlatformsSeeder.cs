using DAA.Constants.Databases;
using DAA.Database.Models.Platforms;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DAA.Database.Migrations.Seeds
{
    public class PlatformsSeeder : BaseSeeder
    {
        public PlatformsSeeder(IConfigurationRoot seedsConfig) : base(seedsConfig)
        { }

        /// <summary>
        /// Ejecuta las semillas de la tabla "PLATFORMS".
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
                        // Parseamos la fecha para convertirla al tipo correspondiente.
                        DateTime releaseDate;
                        CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
                        DateTimeStyles styles = DateTimeStyles.None;

                        if (DateTime.TryParse(values["ReleaseDate"], culture, styles, out releaseDate))
                        {
                            // Comprobamos si el registro ya existe en base de datos.
                            Platform platform = base._databaseContext.Platforms.FirstOrDefault(x => x.Name == values["Name"] && x.Company == values["Company"]);

                            if (platform == null)
                            {
                                // Si el registro no existe entonces lo añadimos.
                                platform = new Platform()
                                {
                                    Name = values["Name"],
                                    Company = values["Company"],
                                    Price = Convert.ToDecimal(values["Price"]),
                                    ReleaseDate = releaseDate
                                };
                                base._databaseContext.Platforms.Add(platform);
                            }
                            else
                            {
                                // Si el registro existe entonces lo actualizamos.
                                platform.Name = values["Name"];
                                platform.Company = values["Company"];
                                platform.Price = Convert.ToDecimal(values["Price"]);
                                platform.ReleaseDate = releaseDate;
                            }

                            // Indicamos que todo ha ido correcto.
                            success = true;
                        }
                        else
                        {
                            // Si no se ha podido parsear la fecha entonces retornamos un error.
                            throw new Exception(string.Format("'{0}' no es una fecha válida.", values["ReleaseDate"]));
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(string.Format("Error: '{0}' no es un decimal. Error: {1}", values["Price"], ex.Message));
                    }
                    catch (OverflowException ex)
                    {
                        Console.WriteLine(string.Format("Error: '{0}' no es un decimal. Error: {1}", values["Price"], ex.Message));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    }
                    
                    return success;
                };

                // Ejecutamos las semillas.
                base.ExecuteSeeds(this._seedsConfig.GetSection("PlatformsFile").Value, anonym);
                #endregion
            }
            else
            {
                // Eliminamos todos los registros de la tabla.
                base.DeleteContentTable(DbTablesValues.Platforms.TABLE_NAME);
            }

            // Aplicamos los cambios.
            base.SaveChanges();
        }
    }
}
