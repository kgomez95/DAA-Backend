namespace DAA.API.Models.ApiRest
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
