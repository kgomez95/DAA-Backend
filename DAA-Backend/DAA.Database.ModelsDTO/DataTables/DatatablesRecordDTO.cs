namespace DAA.Database.ModelsDTO.DataTables
{
    public class DatatablesRecordDTO : BaseAuditableDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public bool HasFilter { get; set; }
        public bool IsBasic { get; set; }
        public bool IsRange { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultFrom { get; set; }
        public string DefaultTo { get; set; }

        public int DataTablesTableId { get; set; }
        public DatatablesTableDTO DatatablesTable { get; set; }
    }
}
