using DAA.API.Models.Datatables;
using DAA.API.Models.Datatables.Filters;
using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.Models.DataTables;
using DAA.Database.Services.Interfaces.Datatables;
using DAA.Utils.Conversions;
using System;
using System.Linq;

namespace DAA.Database.Services.Definitions.Datatables
{
    public class DatatablesRecordsService : IDatatablesRecordsService
    {
        #region Atributos privados.
        private readonly IDatatablesRecordDAO _datatablesRecordDAO;
        #endregion

        #region Constructores.
        public DatatablesRecordsService(IDatatablesRecordDAO datatablesRecordDAO)
        {
            this._datatablesRecordDAO = datatablesRecordDAO;
        }
        #endregion

        #region Metodos y funciones públicas.
        /// <summary>
        /// Coge las cabeceras de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger las cabeceras.</param>
        /// <returns>Retorna un listado con las cabeceras.</returns>
        public string[] GetDataHeaders(string datatable)
        {
            try
            {
                // Cogemos los registros del DataTable.
                DatatablesRecord[] datatablesRecords = this._datatablesRecordDAO.GetRecordsByTable(datatable);

                // Cogemos el listado con los nombres de la cabecera y los retornamos.
                return datatablesRecords.Select(x => x.Name).ToArray();
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
