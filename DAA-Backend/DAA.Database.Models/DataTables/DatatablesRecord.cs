namespace DAA.Database.Models.DataTables
{
    public class DatatablesRecord : BaseAuditable
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
        public DatatablesTable DatatablesTable { get; set; }
    }
}
