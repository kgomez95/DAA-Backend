namespace DAA.API.Models.Datatables.Filters
{
    public class AdvancedFilter
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsRange { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultFrom { get; set; }
        public string DefaultTo { get; set; }
    }
}
