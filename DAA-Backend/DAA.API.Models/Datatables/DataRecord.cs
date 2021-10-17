using DAA.API.Models.Datatables.Records;
using System.Collections.Generic;

namespace DAA.API.Models.Datatables
{
    public class DataRecord
    {
        public IList<IList<Record>> Records { get; set; }
        public object Actions { get; set; }

        public DataRecord()
        {
            this.Records = new List<IList<Record>>();
        }
    }
}
