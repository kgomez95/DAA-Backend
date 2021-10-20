using DAA.API.Models.Datatables;

namespace DAA.API.Models.ApiRest
{
    public class ApiRequest<T>
    {
        public string DataKey { get; set; }
        public T Data { get; set; }
        public DataSort Sort { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }

        public ApiRequest()
        {
            this.Offset = -1;
            this.Limit = -1;
        }
    }
}
