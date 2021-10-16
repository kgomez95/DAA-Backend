namespace DAA.API.Models.Datatables
{
    public class DataSort
    {
        public string Field { get; set; }
        public bool Asc { get; set; }
        public bool Desc { get; set; }

        /// <summary>
        /// Comprueba si hay un campo de ordenación especificado.
        /// </summary>
        /// <returns>Retorna "true" si hay un campo de ordenación o "false" en caso de que no lo haya.</returns>
        public bool HasSortField()
        {
            return !string.IsNullOrEmpty(this.Field) && (this.Asc || this.Desc);
        }
    }
}
