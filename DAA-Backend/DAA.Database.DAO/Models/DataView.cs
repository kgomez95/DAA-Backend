using System.Collections.Generic;

namespace DAA.Database.DAO.Models
{
    public class DataView
    {
        public IList<Dictionary<string, object>> Data { get; set; }
        public int TotalRecords { get; set; }

        public DataView()
        {
            this.Data = new List<Dictionary<string, object>>();
        }
    }
}
