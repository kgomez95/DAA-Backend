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
        string[] GetDataHeaders(string datatable);

        /// <summary>
        /// Coge todos los registros de la tabla proporcionada.
        /// </summary>
        /// <param name="datatable">Tabla de donde coger los registros.</param>
        /// <returns>Retorna un listado de registros DataTables.</returns>
        DatatablesRecordDTO[] GetRecords(string datatable);
    }
}
