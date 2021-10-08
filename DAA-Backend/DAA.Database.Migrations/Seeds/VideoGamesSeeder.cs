using DAA.Constants.Databases;
using DAA.Database.Models.Platforms;
using DAA.Database.Models.VideoGames;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DAA.Database.Migrations.Seeds
{
    public class VideoGamesSeeder : BaseSeeder
    {
        public VideoGamesSeeder(IConfigurationRoot seedsConfig) : base(seedsConfig)
        { }

        /// <summary>
        /// Ejecuta las semillas de la tabla "VIDEO_GAMES".
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
                            Platform platform = base._databaseContext.Platforms.FirstOrDefault(x => x.Name == values["PlatformName"] && x.Company == values["PlatformCompany"]);

                            if (platform != null)
                            {
                                // Comprobamos si el registro ya existe en base de datos.
                                VideoGame videoGame = base._databaseContext.VideoGames.FirstOrDefault(x => x.Name == values["Name"] && x.PlatformId == platform.Id);

                                if (videoGame == null)
                                {
                                    // Si el registro no existe entonces lo añadimos.
                                    videoGame = new VideoGame()
                                    {
                                        Name = values["Name"],
                                        Description = values["Description"],
                                        Price = Convert.ToDecimal(values["Price"]),
                                        Score = Convert.ToDecimal(values["Score"]),
                                        ReleaseDate = releaseDate,
                                        PlatformId = platform.Id
                                    };
                                    base._databaseContext.VideoGames.Add(videoGame);
                                }
                                else
                                {
                                    // Si el registro existe entonces lo actualizamos.
                                    videoGame.Name = values["Name"];
                                    videoGame.Description = values["Description"];
                                    videoGame.Price = Convert.ToDecimal(values["Price"]);
                                    videoGame.Score = Convert.ToDecimal(values["Score"]);
                                    videoGame.ReleaseDate = releaseDate;
                                }

                                // Indicamos que todo ha ido correcto.
                                success = true;
                            }
                            else
                            {
                                // Si no se ha podido encontrar la plataforma retornamos un error.
                                throw new Exception(string.Format("No se ha encontrado la plataforma con nombre '{0}' y con compañía '{1}'.", values["PlatformName"], values["PlatformCompany"]));
                            }
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
                base.ExecuteSeeds(this._seedsConfig.GetSection("VideoGamesFile").Value, anonym);
                #endregion
            }
            else
            {
                // Eliminamos todos los registros de la tabla.
                base.DeleteContentTable(DbTablesValues.VideoGames.TABLE_NAME);
            }

            // Aplicamos los cambios.
            base.SaveChanges();
        }
    }
}
