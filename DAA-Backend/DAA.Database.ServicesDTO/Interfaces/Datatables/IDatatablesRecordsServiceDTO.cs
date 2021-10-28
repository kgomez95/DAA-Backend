using DAA.API.Models.ApiRest;
using DAA.API.Models.Datatables;
using DAA.Database.ModelsDTO.DataTables;

namespace DAA.Database.ServicesDTO.Interfaces.Datatables
{
    public interface IDatatablesRecordsServiceDTO
    {
        /// <summary>
        /// Coge las cabeceras de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger las cabeceras.</param>
        /// <returns>Retorna un listado con las cabeceras.</returns>
        DataHeader[] GetDataHeaders(string datatable);

        /// <summary>
        /// Coge los filtros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los filtros.</param>
        /// <returns>Retorna un objeto DataFilter con los filtros básicos y los filtros avanzados.</returns>
        DataFilter GetDataFilters(string datatable);

        /// <summary>
        /// Coge los datos de la vista del DataTable especificado.
        /// </summary>
        /// <param name="datatable">Código del DataTable de donde coger los datos.</param>
        /// <param name="dataFilter">Filtros a aplicar en la búsqueda de los datos.</param>
        /// <param name="offset">Posición desde donde se buscarán los próximos datos.</param>
        /// <param name="limit">Cantidad de datos a buscar.</param>
        /// <param name="dataSort">Ordenación de los datos.</param>
        /// <returns>Retorna un objeto ApiResponse con los datos de la vista.</returns>
        ApiResponse<DataRecord> GetDataView(string datatable, DataFilter dataFilter, int offset, int limit, DataSort dataSort);

        /// <summary>
        /// Coge todos los registros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los registros.</param>
        /// <returns>Retorna un listado de registros DataTables.</returns>
        DatatablesRecordDTO[] GetRecords(string datatable);
    }
}
