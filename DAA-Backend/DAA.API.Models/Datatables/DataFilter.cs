using DAA.API.Models.Datatables.Filters;
using System.Collections.Generic;
using System.Linq;

namespace DAA.API.Models.Datatables
{
    public class DataFilter
    {
        public IList<BasicFilter> Basic { get; set; }
        public IList<AdvancedFilter> Advanced { get; set; }

        public DataFilter()
        {
            this.Basic = new List<BasicFilter>();
            this.Advanced = new List<AdvancedFilter>();
        }

        /// <summary>
        /// Comprueba si el DataFilter tiene filtros.
        /// </summary>
        /// <returns>Retorna "true" si el DataFilter tiene filtros o "false" en caso de que no tenga.</returns>
        public bool HasFilters()
        {
            return this.Basic.Count > 0 || this.Advanced.Count > 0;
        }

        /// <summary>
        /// Comprueba si el DataFilter tiene filtros con valores a filtrar.
        /// </summary>
        /// <returns>Retorna "true" si el DataFilter tiene filtros con valores a filtrar o "false" en caso de que no los tenga.</returns>
        public bool HasFiltersWithData()
        {
            return this.Basic.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value)) != null 
                || this.Advanced.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value) || !string.IsNullOrEmpty(x.From) || !string.IsNullOrEmpty(x.To)) != null;
        }

        /// <summary>
        /// Comprueba si el DataFilter tiene filtros básicos con valores a filtrar.
        /// </summary>
        /// <returns>Retorna "true" si el DataFilter tiene filtros básicos con valores a filtrar o "false" en caso e que no los tenga.</returns>
        public bool HasFilterBasicWithData()
        {
            return this.Basic.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value)) != null;
        }

        /// <summary>
        /// Comprueba si el DataFilter tiene filtros avanzados con valores a filtrar.
        /// </summary>
        /// <returns>Retorna "true" si el DataFilter tiene filtros avanzados con valores a filtrar o "false" en caso de que no los tenga.</returns>
        public bool HasFilterAdvancedWithData()
        {
            return this.Advanced.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value) || !string.IsNullOrEmpty(x.From) || !string.IsNullOrEmpty(x.To)) != null;
        }
    }
}
