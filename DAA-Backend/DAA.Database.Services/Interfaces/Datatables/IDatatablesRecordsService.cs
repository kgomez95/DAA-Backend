using DAA.Database.Models.DataTables;

namespace DAA.Database.Services.Interfaces.Datatables
{
    public interface IDatatablesRecordsService
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
        DatatablesRecord[] GetRecords(string datatable);
    }
}
