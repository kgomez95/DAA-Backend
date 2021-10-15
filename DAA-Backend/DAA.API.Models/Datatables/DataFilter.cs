using DAA.API.Models.Datatables.Filters;
using System.Collections.Generic;

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
    }
}
