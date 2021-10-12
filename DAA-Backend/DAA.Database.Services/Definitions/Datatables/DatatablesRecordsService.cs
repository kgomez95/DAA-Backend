using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.Models.DataTables;
using DAA.Database.Services.Interfaces.Datatables;
using System;
using System.Linq;

namespace DAA.Database.Services.Definitions.Datatables
{
    public class DatatablesRecordsService : IDatatablesRecordsService
    {
        #region Atributos privados.
        private readonly IDatatablesTableDAO _datatablesTableDAO;
        private readonly IDatatablesRecordDAO _datatablesRecordDAO;
        #endregion

        #region Constructores.
        public DatatablesRecordsService(IDatatablesTableDAO datatablesTableDAO, IDatatablesRecordDAO datatablesRecordDAO)
        {
            this._datatablesTableDAO = datatablesTableDAO;
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
                // Cogemos el identificador del datatable.
                int datatableId = this._datatablesTableDAO.GetIdFromCode(datatable);

                // Cogemos los registros del DataTable.
                DatatablesRecord[] datatablesRecords = this._datatablesRecordDAO.GetRecordsByTable(datatableId);

                // Cogemos el listado con los nombres de la cabecera y los retornamos.
                return datatablesRecords.Select(x => x.Name).ToArray();
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
                // Cogemos el identificador del datatable.
                int datatableId = this._datatablesTableDAO.GetIdFromCode(datatable);

                // Cogemos los registros del DataTable y los retornamos.
                return this._datatablesRecordDAO.GetRecordsByTable(datatableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
