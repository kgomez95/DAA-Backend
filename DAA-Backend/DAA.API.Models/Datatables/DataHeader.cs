namespace DAA.API.Models.Datatables
{
    public class DataHeader
    {
        public string Code { get; set; }
        public string Value { get; set; }

        public DataHeader(string code, string value)
        {
            this.Code = code;
            this.Value = value;
        }
    }
}
