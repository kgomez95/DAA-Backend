using DAA.Database.DAO.Interfaces.Datatables;
using DAA.Database.Migrations.Contexts;
using DAA.Database.Models.DataTables;
using System;
using System.Linq;

namespace DAA.Database.DAO.Definitions.Datatables
{
    public class DatatablesTableDAO : BaseDAO, IDatatablesTableDAO
    {
        public DatatablesTableDAO(DAADbContext context) : base(context)
        { }

        /// <summary>
        /// Busca el identificador del DataTable que coincida con el código especificado por parámetros.
        /// </summary>
        /// <param name="code">Código del DataTable a buscar.</param>
        /// <returns>Retorna el identificador del DataTable.</returns>
        public int GetIdFromCode(string code)
        {
            DatatablesTable datatablesTable = base._context.DatatablesTables.FirstOrDefault(x => x.Code == code);

            if (datatablesTable == null)
            {
                throw new Exception(string.Format("El DataTable con código '{0}' no existe.", code));
            }

            return datatablesTable.Id;
        }
    }
}
