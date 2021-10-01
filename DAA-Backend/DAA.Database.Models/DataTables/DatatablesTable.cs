using System.Collections.Generic;

namespace DAA.Database.Models.DataTables
{
    public class DatatablesTable : BaseAuditable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }

        public IList<DatatablesRecord> DatatablesRecords { get; set; }
    }
}
