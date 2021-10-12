using DAA.Database.Models.DataTables;

namespace DAA.Database.DAO.Interfaces.Datatables
{
    public interface IDatatablesRecordDAO
    {
        /// <summary>
        /// Busca los registros del DataTable proporcionado.
        /// </summary>
        /// <param name="tableId">Identificador del DataTable.</param>
        /// <returns>Retorna un listado de registros del DataTable proporcionado.</returns>
        DatatablesRecord[] GetRecordsByTable(int tableId);

        /// <summary>
        /// Busca los registros del DataTable proporcionado.
        /// </summary>
        /// <param name="datatable">Código del DataTable.</param>
        /// <returns>Retorna un listado de registros del DataTable proporcionado.</returns>
        DatatablesRecord[] GetRecordsByTable(string datatable);
    }
}
