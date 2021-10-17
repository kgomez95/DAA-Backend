using DAA.API.Models.Datatables;
using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.DAO.Models;
using DAA.Database.Migrations.Contexts;
using DAA.Database.Models.DataTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DAA.Database.DAO.Definitions.Datatables
{
    public class DatatablesViewsDAO : BaseDAO, IDatatablesViewsDAO
    {
        #region Constructores.
        public DatatablesViewsDAO(DAADbContext context) : base(context)
        { }
        #endregion

        #region Métodos y funciones públicas.
        /// <summary>
        /// Recupera los datos de la vista proporcionada.
        /// </summary>
        /// <param name="view">Nombre de la vista.</param>
        /// <param name="dataFilter">Filtros a realizar en la búsqueda de los datos.</param>
        /// <param name="offset">Posición desde donde se buscarán los próximos datos.</param>
        /// <param name="limit">Cantidad de datos a buscar.</param>
        /// <param name="dataSort">Ordenación de los datos.</param>
        /// <param name="records">Información de los datos a buscar.</param>
        /// <returns>Retorna los datos de la vista.</returns>
        public DataView RecoverDataView(string view, DataFilter dataFilter, int offset, int limit, DataSort dataSort, DatatablesRecord[] records)
        {
            DataView dataView = new DataView();

            // Cogemos el total de registros.
            using (DbCommand command = this.CreateCommand(view, dataFilter, offset, limit, dataSort, records, true))
            {
                base._context.Database.OpenConnection();
                using (DbDataReader result = command.ExecuteReader())
                {
                    result.Read();
                    dataView.TotalRecords = result.GetInt32(0);
                }
                base._context.Database.CloseConnection();
                command.Dispose();
            }

            // Cogemos los registros.
            using (DbCommand command = this.CreateCommand(view, dataFilter, offset, limit, dataSort, records, false))
            {
                base._context.Database.OpenConnection();
                using (DbDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Dictionary<string, object> dataRecords = new Dictionary<string, object>();

                        for (int i = 0; i < records.Length; i++)
                        {
                            dataRecords.Add(records[i].Code, result[records[i].Code]);
                        }

                        dataView.Data.Add(dataRecords);
                    }
                }
                base._context.Database.CloseConnection();
                command.Dispose();
            }

            return dataView;
        }
        #endregion

        #region Métodos y funciones privadas.
        /// <summary>
        /// Crea el comando de base de datos a ejecutar para recuperar los valores de la vista proporcionada.
        /// </summary>
        /// <param name="view">Nombre de la vista.</param>
        /// <param name="dataFilter">Filtros a realizar en la búsqueda de los datos.</param>
        /// <param name="offset">Posición desde donde se buscarán los próximos datos.</param>
        /// <param name="limit">Cantidad de datos a buscar.</param>
        /// <param name="dataSort">Ordenación de los datos.</param>
        /// <param name="records">Información de los datos a buscar.</param>
        /// <param name="isCount">Poner el valor a "true" para recoger la cantidad total de registros.</param>
        /// <returns>Retorna el comando de base de datos inicializado y listo para ser ejecutado.</returns>
        private DbCommand CreateCommand(string view, DataFilter dataFilter, int? offset, int? limit, DataSort dataSort, DatatablesRecord[] records, bool isCount)
        {
            DbCommand command = base._context.Database.GetDbConnection().CreateCommand();
            IList<DbParameter> parameters = new List<DbParameter>();
            string query = "SELECT ";

            if (isCount)
            {
                query += $"COUNT(*) ";
            }
            else
            {
                query += $"* ";
            }

            // Añadimos la vista.
            query += $"FROM {view} ";

            #region Filtros.
            // Añadimos los filtros solamente si tienen datos para filtrar.
            if (dataFilter.HasFiltersWithData())
            {
                query += "WHERE ";

                // Filtros básicos.
                this.AddBasicFilters(command, dataFilter, ref parameters, ref query);

                // Filtros avanzados.
                this.AddAdvancedFilters(command, dataFilter, ref parameters, ref query);

                // NOTE: Quitamos el último "AND".
                query = query.Substring(0, query.Length - 4);
            }
            #endregion

            // NOTE: La ordenación y el offset y el limit solamente los añadimos si la consulta no es de tipo "count".
            if (!isCount)
            {
                #region Ordenación.
                // Añadimos la ordenación.
                if (dataSort.HasSortField() && records.FirstOrDefault(x => x.Code.Equals(dataSort.Field, StringComparison.InvariantCultureIgnoreCase)) != null)
                {
                    // NOTE: Como no se puede añadir el campo como si fuese un parametro (es decir, que obliga a concatenarlo), realizamos una búsqueda del "dataSort.Field"
                    //       para comprobar que dicho campo realmente existe en la tabla, es por eso que en la condición de arriba buscamos el valor de esta variable en el listado
                    //       "records" (para evitar que nos hagan injección de código).
                    query += $"ORDER BY {dataSort.Field} ";

                    if (dataSort.Asc)
                    {
                        query += "ASC ";
                    }
                    else
                    {
                        query += "DESC ";
                    }
                }
                else
                {
                    // NOTE: En caso de que no haya un campo de ordenación le tenemos que poner una ordenación por defecto.
                    query += $"ORDER BY 1 DESC ";
                }
                #endregion

                // Añadimos el offset y el limit para paginar solamente cuando tengamos sus valores.
                if (offset.HasValue && limit.HasValue)
                {
                    // NOTE: Para usar el Offset y el Fetch es obligatorio poner un "Order By" para que esto funcione.
                    query += "OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY ";
                    parameters.Add(this.CreateParameter("Offset", offset.Value, command));
                    parameters.Add(this.CreateParameter("Limit", limit.Value, command));
                }
            }

            // Añadimos la query al comando.
            command.CommandText = query;

            // Añadimos los parámetros al comando.
            for (int i = 0; i < parameters.Count; i++)
            {
                command.Parameters.Add(parameters[i]);
            }

            return command;
        }

        /// <summary>
        /// Añade los filtros básicos a la query proporcionada.
        /// </summary>
        /// <param name="command">Comando de base de datos a construir.</param>
        /// <param name="dataFilter">Filtros a realizar en la búsqueda de los datos.</param>
        /// <param name="parameters">Parámetros a añadir en la query.</param>
        /// <param name="query">Consulta sql donde añadir los filtros básicos.</param>
        private void AddBasicFilters(DbCommand command, DataFilter dataFilter, ref IList<DbParameter> parameters, ref string query)
        {
            string basicFilter = string.Empty;

            if (dataFilter.HasFilterBasicWithData())
            {
                for (int i = 0; i < dataFilter.Basic.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dataFilter.Basic[i].Value))
                    {
                        //query += $"{dataFilter.Basic[i].Code} like @{dataFilter.Basic[i].Code} AND ";
                        //parameters.Add(this.CreateParameter(dataFilter.Basic[i].Code, dataFilter.Basic[i].Value, command));

                        basicFilter += $"{dataFilter.Basic[i].Code} like @{dataFilter.Basic[i].Code} OR ";
                        parameters.Add(this.CreateParameter(dataFilter.Basic[i].Code, dataFilter.Basic[i].Value, command));
                    }
                }
            }

            if (!string.IsNullOrEmpty(basicFilter))
            {
                // NOTE: Quitamos el último "OR".
                basicFilter = basicFilter.Substring(0, basicFilter.Length - 4);

                query += $"({basicFilter}) AND ";
            }
        }

        /// <summary>
        /// Añade los filtros avanzados a la query proporcionada.
        /// </summary>
        /// <param name="command">Comando de base de datos a construir.</param>
        /// <param name="dataFilter">Filtros a realizar en la búsqueda de los datos.</param>
        /// <param name="parameters">Parámetros a añadir en la query.</param>
        /// <param name="query">Consulta sql donde añadir los filtros avanzados.</param>
        private void AddAdvancedFilters(DbCommand command, DataFilter dataFilter, ref IList<DbParameter> parameters, ref string query)
        {
            if (dataFilter.HasFilterAdvancedWithData())
            {
                for (int i = 0; i < dataFilter.Advanced.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dataFilter.Advanced[i].Value)
                        || !string.IsNullOrEmpty(dataFilter.Advanced[i].From)
                        || !string.IsNullOrEmpty(dataFilter.Advanced[i].To))
                    {
                        switch (dataFilter.Advanced[i].Type.ToLower())
                        {
                            case "decimal":
                            case "currency":
                            case "percentatge":
                            case "datetime":
                                if (dataFilter.Advanced[i].IsRange)
                                {
                                    query += $"{dataFilter.Advanced[i].Code} >= @{dataFilter.Advanced[i].Code}_FROM AND ";
                                    query += $"{dataFilter.Advanced[i].Code} <= @{dataFilter.Advanced[i].Code}_TO AND ";
                                    parameters.Add(this.CreateParameter(string.Format("{0}_FROM", dataFilter.Advanced[i].Code), dataFilter.Advanced[i].From, command));
                                    parameters.Add(this.CreateParameter(string.Format("{0}_TO", dataFilter.Advanced[i].Code), dataFilter.Advanced[i].To, command));
                                }
                                else
                                {
                                    query += $"{dataFilter.Advanced[i].Code} = @{dataFilter.Advanced[i].Code} AND ";
                                    parameters.Add(this.CreateParameter(dataFilter.Advanced[i].Code, dataFilter.Advanced[i].Value, command));
                                }
                                break;
                            default:
                                query += $"{dataFilter.Advanced[i].Code} like @{dataFilter.Advanced[i].Code} AND ";
                                parameters.Add(this.CreateParameter(dataFilter.Advanced[i].Code, dataFilter.Advanced[i].Value, command));
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Crea el parámetro con los valores especificados.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="value">Valor del parámetro.</param>
        /// <param name="dbCommand">DbCommand donde se crea el parámetro.</param>
        /// <returns>Devuelve el parámetro DbParameter creado.</returns>
        private DbParameter CreateParameter(string name, object value, DbCommand dbCommand)
        {
            DbParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = (name.StartsWith("@", StringComparison.CurrentCultureIgnoreCase)) ? name : $"@{name}";
            parameter.Value = value;

            return parameter;
        }
        #endregion
    }
}
