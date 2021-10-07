using DAA.Database.Migrations.Contexts;
using DAA.Database.Migrations.Seeds;
using DAA.Database.Views.VideoGames;
using DAA.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace DAA.Database.Migrations
{
    class Program
    {
        private readonly IConfigurationRoot _seedsConfig;

        public Program()
        {
            // Nombre del fichero de configuración.
            string seedsConfigRoute = string.Format(@"{0}\Settings\seedsSettings.json", System.IO.Directory.GetCurrentDirectory());

            // Generamos la configuración para leer el fichero.
            this._seedsConfig = new ConfigurationBuilder()
                .AddJsonFile(seedsConfigRoute)
                .Build();
        }

        /// <summary>
        /// Ejecuta el asistente de semillas.
        /// </summary>
        public void Run()
        {
            ConsoleKeyInfo pressedKey;
            bool exit = false;

            do
            {
                Console.WriteLine("[Presione la tecla 'Esc' para salir]\n");
                Console.WriteLine("(0) Ejecutar una o varias semillas.");
                Console.WriteLine("(1) Borrar todo el contenido de una o varias.");
                Console.Write(": ");
                pressedKey = Console.ReadKey();

                switch (pressedKey.KeyChar)
                {
                    case '\u001b':
                        exit = true;
                        break;
                    case '0':
                        this.ExecuteCommand(0);
                        break;
                    case '1':
                        this.ExecuteCommand(1);
                        break;
                }

                Console.Clear();
            }
            while (!exit);
        }

        #region Funciones y métodos privados.
        /// <summary>
        /// Ejecuta la instrucción del usuario.
        /// </summary>
        /// <param name="action">0: Ejecuta las semillas. 1: Borra todo el contenido de la tabla.</param>
        private void ExecuteCommand(byte action)
        {
            string[] tables = null;
            string command = string.Empty;
            bool executed = false;
            int executions = 0;

            Console.Clear();
            Console.WriteLine("[Presione la tecla 'Entrar' o 'Intro' sin introducir ningún texto para volver al menú]\n");

            if (action == 0)
            {
                Console.WriteLine("Escriba el nombre de la o las tablas donde quiera ejecutar las semillas (ejemplo: platforms, video_games)");
            }
            else
            {
                Console.WriteLine("Escriba el nombre de la o las tablas que quiera vaciar (ejemplo: platforms, video_games)");
            }

            Console.Write(": ");
            command = Console.ReadLine();

            if (string.IsNullOrEmpty(command) == false)
            {
                tables = command.ToLower().Replace(" ", "").Split(',');

                for (int i = 0; i < tables.Length; i++)
                {
                    executed = this.ExecuteSeeder(tables[i], action);

                    if (executed == true)
                    {
                        executions++;
                    }
                }

                Console.WriteLine("\nSe han ejecutado {0} elemento/s de {1}.", executions, tables.Length);
                Console.WriteLine("\n[Pulse cualquier tecla para volver al menú]");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Ejecuta el seeder correspondiente a la tabla proporcionada.
        /// </summary>
        /// <param name="table">Nombre de la tabla que identifica la clase seeder.</param>
        /// <param name="action">0: Ejecuta las semillas. 1: Borra todo el contenido de la tabla.</param>
        /// <returns>Retorna "true" si se ejecuta el seeder o "false" en caso de que no se ejecute.</returns>
        private bool ExecuteSeeder(string table, byte action)
        {
            bool executed = true;

            #region Seeders.
            switch (table)
            {
                case "platforms":
                    Console.WriteLine("Ejecutando '{0}'...", table);
                    new PlatformsSeeder(this._seedsConfig).Execute(action);
                    break;

                default:
                    executed = false;
                    Console.WriteLine("No se ha encontrado la tabla '{0}'.", table);
                    break;
            }
            #endregion

            return executed;
        }
        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("¡Bienvenido al asistente de semillas!\n");
            p.Run();

            //DAADbContext dbContext = new DAADbContext();
            //VideoGameScoreView[] test = dbContext.VideoGamesScoreView.ToArray();

            //for (int i = 0; i < test.Length; i++)
            //{
            //    Console.WriteLine(test[i].ToJson());
            //}
        }
    }
}
