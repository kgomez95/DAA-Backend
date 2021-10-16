using DAA.API.Models.Datatables;
using DAA.Database.DAO.Models;
using DAA.Database.Models.DataTables;

namespace DAA.Database.DAO.Interfaces.Datatables
{
    public interface IDatatablesViewsDAO
    {
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
        DataView RecoverDataView(string view, DataFilter dataFilter, int offset, int limit, DataSort dataSort, DatatablesRecord[] records);
    }
}
