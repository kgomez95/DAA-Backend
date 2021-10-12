using System;
using System.Collections.Generic;

namespace DAA.Database.ModelsDTO.DataTables
{
    [Serializable]
    public class DatatablesTableDTO : BaseAuditableDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }

        public IList<DatatablesRecordDTO> DatatablesRecords { get; set; }
    }
}
