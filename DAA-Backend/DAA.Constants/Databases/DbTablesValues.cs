namespace DAA.Constants.Databases
{
    public class DbTablesValues
    {
        public class DatatablesTables
        {
            public const string TABLE_NAME = "DATATABLES_TABLES";
            public const string PREFIX = "DTS_";

            public const string ID = DatatablesTables.PREFIX + "Id";
            public const string CODE = DatatablesTables.PREFIX + "Code";
            public const string NAME = DatatablesTables.PREFIX + "Name";
            public const string DESCRIPTION = DatatablesTables.PREFIX + "Description";
            public const string REFERENCE = DatatablesTables.PREFIX + "Reference";

            public const int CODE_LENGTH = 50;
            public const int NAME_LENGTH = 250;
            public const int REFERENCE_LENGTH = 128;

            public const bool CODE_REQUIRED = true;
            public const bool NAME_REQUIRED = true;
            public const bool DESCRIPTION_REQUIRED = true;
            public const bool REFERENCE_REQUIRED = true;

            public const string DESCRIPTION_TYPE = "TEXT";
        }
    }
}
