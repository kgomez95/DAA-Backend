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

            // TODO: Realizar un COUNT de los datos que se van a recuperar para poder devolver el total de registros encontrados.

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
            // TODO: Optimizar esta función.

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

            // Añadimos los filtros.
            if (dataFilter.HasFiltersWithData())
            {
                query += "WHERE ";

                // Filtros básicos.
                string basicFilter = string.Empty;
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

                if (!string.IsNullOrEmpty(basicFilter))
                {
                    // NOTE: Quitamos el último "OR".
                    basicFilter = basicFilter.Substring(0, basicFilter.Length - 4);

                    query += $"({basicFilter}) AND ";
                }

                // Filtros avanzados.
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

                // NOTE: Quitamos el último "AND".
                query = query.Substring(0, query.Length - 4);
            }

            // Añadimos la ordenación.
            if (dataSort.HasSortField() && records.FirstOrDefault(x => x.Code.Equals(dataSort.Field, StringComparison.InvariantCultureIgnoreCase)) != null)
            {
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
                query += $"ORDER BY 1 DESC ";
            }

            // Añadimos el offset y el limit para paginar.
            if (offset.HasValue && limit.HasValue)
            {
                query += "OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY ";
                parameters.Add(this.CreateParameter("Offset", offset.Value, command));
                parameters.Add(this.CreateParameter("Limit", limit.Value, command));
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
