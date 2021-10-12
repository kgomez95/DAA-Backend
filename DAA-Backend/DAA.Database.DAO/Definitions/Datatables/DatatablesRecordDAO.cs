using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.Migrations.Contexts;
using DAA.Database.Models.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAA.Database.DAO.Definitions.Datatables
{
    public class DatatablesRecordDAO : BaseDAO, IDatatablesRecordDAO
    {
        public DatatablesRecordDAO(DAADbContext context) : base(context)
        { }

        /// <summary>
        /// Busca los registros del DataTable proporcionado.
        /// </summary>
        /// <param name="tableId">Identificador del DataTable.</param>
        /// <returns>Retorna un listado de registros del DataTable proporcionado.</returns>
        public DatatablesRecord[] GetRecordsByTable(int tableId)
        {
            return base._context.DatatablesRecords.Where(x => x.DataTablesTableId == tableId).OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Busca los registros del DataTable proporcionado.
        /// </summary>
        /// <param name="datatable">Código del DataTable.</param>
        /// <returns>Retorna un listado de registros del DataTable proporcionado.</returns>
        public DatatablesRecord[] GetRecordsByTable(string datatable)
        {
            return base._context.DatatablesRecords.Where(x => x.DatatablesTable.Code == datatable).Include(x => x.DatatablesTable).OrderBy(x => x.Order).ToArray();
        }
    }
}
