namespace DAA.Database.DAO.Interfaces.Datatables
{
    public interface IDatatablesTableDAO
    {
        /// <summary>
        /// Busca el identificador del DataTable que coincida con el código especificado por parámetros.
        /// </summary>
        /// <param name="code">Código del DataTable a buscar.</param>
        /// <returns>Retorna el identificador del DataTable.</returns>
        int GetIdFromCode(string code);
    }
}
