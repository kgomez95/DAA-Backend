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

        public class DatatablesRecords
        {
            public const string TABLE_NAME = "DATATABLES_RECORDS";
            public const string PREFIX = "DRS_";

            public const string ID = DatatablesRecords.PREFIX + "Id";
            public const string CODE = DatatablesRecords.PREFIX + "Code";
            public const string NAME = DatatablesRecords.PREFIX + "Name";
            public const string TYPE = DatatablesRecords.PREFIX + "Type";
            public const string ORDER = DatatablesRecords.PREFIX + "Order";
            public const string HAS_FILTER = DatatablesRecords.PREFIX + "HasFilter";
            public const string IS_BASIC = DatatablesRecords.PREFIX + "IsBasic";
            public const string IS_RANGE = DatatablesRecords.PREFIX + "IsRange";
            public const string DEFAULT_VALUE = DatatablesRecords.PREFIX + "DefaultValue";
            public const string DEFAULT_FROM = DatatablesRecords.PREFIX + "DefaultFrom";
            public const string DEFAULT_TO = DatatablesRecords.PREFIX + "DefaultTo";
            public const string DATATABLES_TABLES = DatatablesTables.ID;

            public const int CODE_LENGTH = 128;
            public const int NAME_LENGTH = 250;
            public const int DEFAULT_VALUE_LENGTH = 2000;
            public const int DEFAULT_FROM_LENGTH = 2000;
            public const int DEFAULT_TO_LENGTH = 2000;

            public const bool CODE_REQUIRED = true;
            public const bool NAME_REQUIRED = true;
            public const bool TYPE_REQUIRED = true;
            public const bool ORDER_REQUIRED = true;
            public const bool HAS_FILTER_REQUIRED = true;
            public const bool IS_BASIC_REQUIRED = true;
            public const bool IS_RANGE_REQUIRED = true;
            public const bool DEFAULT_VALUE_REQUIRED = true;
            public const bool DEFAULT_FROM_REQUIRED = true;
            public const bool DEFAULT_TO_REQUIRED = true;
            public const bool DATATABLES_TABLES_REQUIRED = true;
        }

        public class Platforms
        {
            public const string TABLE_NAME = "PLATFORMS";
            public const string PREFIX = "PTS_";

            public const string ID = Platforms.PREFIX + "Id";
            public const string NAME = Platforms.PREFIX + "Name";
            public const string COMPANY = Platforms.PREFIX + "Company";
            public const string PRICE = Platforms.PREFIX + "Price";
            public const string RELEASE_DATE = Platforms.PREFIX + "ReleaseDate";

            public const int NAME_LENGTH = 250;
            public const int COMPANY_LENGTH = 250;

            public const bool NAME_REQUIRED = true;
            public const bool COMPANY_REQUIRED = true;
            public const bool PRICE_REQUIRED = true;
            public const bool RELEASE_DATE_REQUIRED = true;

            public const string PRICE_TYPE = "MONEY";
        }

        public class VideoGames
        {
            public const string TABLE_NAME = "VIDEO_GAMES";
            public const string PREFIX = "VGS_";

            public const string ID = VideoGames.PREFIX + "Id";
            public const string NAME = VideoGames.PREFIX + "Name";
            public const string DESCRIPTION = VideoGames.PREFIX + "Description";
            public const string PRICE = VideoGames.PREFIX + "Price";
            public const string SCORE = VideoGames.PREFIX + "Score";
            public const string RELEASE_DATE = VideoGames.PREFIX + "ReleaseDate";
            public const string PLATFORM = Platforms.ID;

            public const int NAME_LENGTH = 250;

            public const bool NAME_REQUIRED = true;
            public const bool DESCRIPTION_REQUIRED = true;
            public const bool PRICE_REQUIRED = true;
            public const bool SCORE_REQUIRED = true;
            public const bool RELEASE_DATE_REQUIRED = true;
            public const bool PLATFORM_REQUIRED = true;

            public const string DESCRIPTION_TYPE = "TEXT";
            public const string PRICE_TYPE = "MONEY";
            public const string SCORE_TYPE = "NUMERIC(3,2)";
        }
    }
}
