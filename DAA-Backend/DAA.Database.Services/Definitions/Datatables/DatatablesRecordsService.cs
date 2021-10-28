using DAA.API.Models.ApiRest;
using DAA.API.Models.Datatables;
using DAA.API.Models.Datatables.Filters;
using DAA.API.Models.Datatables.Records;
using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.DAO.Models;
using DAA.Database.Models.DataTables;
using DAA.Database.Services.Interfaces.Datatables;
using DAA.Utils.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAA.Database.Services.Definitions.Datatables
{
    public class DatatablesRecordsService : IDatatablesRecordsService
    {
        #region Atributos privados.
        private readonly IDatatablesRecordDAO _datatablesRecordDAO;
        private readonly IDatatablesTableDAO _datatablesTableDAO;
        private readonly IDatatablesViewsDAO _datatablesViewsDAO;
        #endregion

        #region Constructores.
        public DatatablesRecordsService(
            IDatatablesRecordDAO datatablesRecordDAO, 
            IDatatablesTableDAO datatablesTableDAO, 
            IDatatablesViewsDAO datatablesViewsDAO)
        {
            this._datatablesRecordDAO = datatablesRecordDAO;
            this._datatablesTableDAO = datatablesTableDAO;
            this._datatablesViewsDAO = datatablesViewsDAO;
        }
        #endregion

        #region Metodos y funciones públicas.
        /// <summary>
        /// Coge las cabeceras de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger las cabeceras.</param>
        /// <returns>Retorna un listado con las cabeceras.</returns>
        public DataHeader[] GetDataHeaders(string datatable)
        {
            try
            {
                // Cogemos los registros del DataTable.
                DatatablesRecord[] datatablesRecords = this._datatablesRecordDAO.GetRecordsByTable(datatable);

                // Cogemos el listado con los nombres de la cabecera y los retornamos.
                return datatablesRecords.Select(x => new DataHeader(x.Code, x.Name)).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Coge los filtros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los filtros.</param>
        /// <returns>Retorna un objeto DataFilter con los filtros básicos y los filtros avanzados.</returns>
        public DataFilter GetDataFilters(string datatable)
        {
            try
            {
                // Cogemos los registros del DataTable.
                DatatablesRecord[] datatablesRecords = this._datatablesRecordDAO.GetRecordsByTable(datatable);

                // Comprobamos que realmente haya registros.
                if (datatablesRecords == null || datatablesRecords.Length == 0)
                {
                    throw new Exception(string.Format("No se han encontrado registros para el DataTable '{0}'.", datatable));
                }

                // Creamos el objeto que almacena los filtros.
                DataFilter dataFilter = new DataFilter();

                // Inicializamos el DataFilter con los datos recogidos de base de datos..
                foreach (DatatablesRecord record in datatablesRecords)
                {
                    if (record.HasFilter)
                    {
                        if (record.IsBasic)
                        {
                            // Se añade el filtro básico.
                            dataFilter.Basic.Add(new BasicFilter()
                            {
                                Code = record.Code,
                                Name = record.Name
                            });
                        }
                        else
                        {
                            // Se añade el filtro avanzado.
                            dataFilter.Advanced.Add(new AdvancedFilter()
                            {
                                Code = record.Code,
                                Name = record.Name,
                                IsRange = record.IsRange,
                                Type = ConstantsConversion.ConvertType(record.Type),
                                DefaultValue = record.DefaultValue,
                                DefaultFrom = record.DefaultFrom,
                                DefaultTo = record.DefaultTo
                            });
                        }
                    }
                }

                // Retornamos los filtros.
                return dataFilter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Coge los datos de la vista del DataTable especificado.
        /// </summary>
        /// <param name="datatable">Código del DataTable de donde coger los datos.</param>
        /// <param name="dataFilter">Filtros a aplicar en la búsqueda de los datos.</param>
        /// <param name="offset">Posición desde donde se buscarán los próximos datos.</param>
        /// <param name="limit">Cantidad de datos a buscar.</param>
        /// <param name="dataSort">Ordenación de los datos.</param>
        /// <returns>Retorna un objeto ApiResponse con los datos de la vista.</returns>
        public ApiResponse<DataRecord> GetDataView(string datatable, DataFilter dataFilter, int offset, int limit, DataSort dataSort)
        {
            try
            {
                // Creamos los datos de la respuesta.
                ApiResponse<DataRecord> response = new ApiResponse<DataRecord>()
                {
                    Data = new DataRecord()
                };

                // Recogemos los datos de la vista.
                DatatablesTable datatablesTable = this._datatablesTableDAO.GetFromCode(datatable);
                DatatablesRecord[] datatablesRecords = this._datatablesRecordDAO.GetRecordsByTable(datatablesTable.Id);

                // Recogemos los valores de la vista.
                DataView dataView = this._datatablesViewsDAO.RecoverDataView(datatablesTable.Reference, dataFilter, offset, limit, dataSort, datatablesRecords);

                // Construimos la respuesta.
                foreach (Dictionary<string, object> data in dataView.Data)
                {
                    IList<Record> records = new List<Record>();

                    foreach (KeyValuePair<string, object> record in data)
                    {
                        records.Add(new Record()
                        {
                            Code = record.Key,
                            Value = record.Value,
                            Type = ConstantsConversion.ConvertType(datatablesRecords.First(x => x.Code.Equals(record.Key, StringComparison.InvariantCultureIgnoreCase)).Type)
                        });
                    }

                    response.Data.Records.Add(records);
                }

                // Asignamos el total de registros y calculamos el total de páginas.
                response.TotalRecords = dataView.TotalRecords;
                response.TotalPages = Convert.ToInt32(Math.Ceiling((decimal)dataView.TotalRecords / (decimal)limit));

                // TODO: Recoger de forma correcta las acciones de la vista. 
                response.Data.Actions = new 
                {
                    Create = true,
                    Delete = true,
                    Update = true,
                    View = true
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Coge todos los registros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los registros.</param>
        /// <returns>Retorna un listado de registros DataTables.</returns>
        public DatatablesRecord[] GetRecords(string datatable)
        {
            try
            {
                // Cogemos los registros del DataTable y los retornamos.
                return this._datatablesRecordDAO.GetRecordsByTable(datatable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
